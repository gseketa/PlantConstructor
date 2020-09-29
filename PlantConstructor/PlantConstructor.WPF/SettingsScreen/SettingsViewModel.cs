using PlantConstructor.Domain.Model;
using PlantConstructor.Domain.Services;
using PlantConstructor.EntityFramework;
using PlantConstructor.WPF.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PlantConstructor.WPF.SettingsScreen
{
    public class SettingsViewModel :INotifyPropertyChanged
    {
        IDataService<AttributeG> attributeGService;

        private List<string> settingsProjectAttributeGroupesComboBox;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<string> SettingsProjectAttributeGroupesComboBox
        {
            get { return settingsProjectAttributeGroupesComboBox; }
            set
            {
                settingsProjectAttributeGroupesComboBox = value;
                OnPropertyRaised("SettingsProjectAttributeGroupesComboBox");
            }
        }

        private string settingsSelectedAttributeGroup;

        public string SettingsSelectedAttributeGroup
        {
            get { return settingsSelectedAttributeGroup; }
            set
            {
                settingsSelectedAttributeGroup = value;
                OnPropertyRaised("SettingsSelectedAttributeGroup");
                SettingsDisplayProjectAttributes();
            }
        }

        private List<string> settingsAllAttributes;

        public List<string> SettingsAllAttributes
        {
            get { return settingsAllAttributes; }
            set { 
                settingsAllAttributes = value;
                OnPropertyRaised("SettingsAllAttributes");
            }
        }

        private string settingsSelectedAttribute;

        public string SettingsSelectedAttribute
        {
            get { return settingsSelectedAttribute; }
            set { settingsSelectedAttribute = value;
                OnPropertyRaised("SettingsSelectedAttribute");
            }
        }

        private string newAttributeName;

        public string NewAttributeName
        {
            get { return newAttributeName; }
            set
            {
                newAttributeName = value;
                OnPropertyRaised("NewAttributeName");
            }
        }

        private ICommand addNewAttributeCommand;

        public ICommand AddNewAttributeCommand
        {
            get { return addNewAttributeCommand; }
            set { addNewAttributeCommand = value; }
        }

        private ICommand deleteSelectedAttributeCommand;

        public ICommand DeleteSelectedAttributeCommand
        {
            get { return deleteSelectedAttributeCommand; }
            set { deleteSelectedAttributeCommand = value; }
        }




        private IEnumerable<AttributeG> allAttributesFromDB;


        public SettingsViewModel()
        {
            attributeGService = new GenericDataService<AttributeG>(new PlantConstructorDbContextFactory());
            SettingsProjectAttributeGroupesComboBox = new List<string> { "Site", "Zone", "Pipe", "Branch", "PipePart", "Structure", "SubStructure", "StructurePart" };
            AddNewAttributeCommand = new RelayCommand(AddNewAttributeButtonClick);
            DeleteSelectedAttributeCommand = new RelayCommand(DeleteSelectedAttributeButtonClick);

            LoadAttributeDataFromDatabase();
        }

        private async void LoadAttributeDataFromDatabase()
        {
            allAttributesFromDB = await attributeGService.GetAll();
            SettingsSelectedAttributeGroup = "Site";
        }

        private void SettingsDisplayProjectAttributes()
        {
            SettingsAllAttributes = allAttributesFromDB.Where(x => x.Type == SettingsSelectedAttributeGroup).Select(x => x.Name).ToList();
        }

        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        public async void AddNewAttributeButtonClick(object parameter)
        {
            if (!SettingsAllAttributes.Contains(NewAttributeName))
            {
                if (!string.IsNullOrWhiteSpace(NewAttributeName))
                {
                    await attributeGService.Create(new AttributeG { Name = NewAttributeName, Type = SettingsSelectedAttributeGroup });
                    allAttributesFromDB = await attributeGService.GetAll();
                    SettingsDisplayProjectAttributes();
                    NewAttributeName = "";
                }
                else
                {
                    MessageBox.Show("Please enter a valid attribute name. Attribute not added!");
                }

            }
            else
            {
                MessageBox.Show("An attribute with that name already exists in the database. Attribute not added!");
            }
        }

        public async void DeleteSelectedAttributeButtonClick(object parameter)
        {
            if (SettingsSelectedAttribute != null)
            {
                if (MessageBox.Show("Are you sure you want to delete this attribute? This will impact all projects that contain values for this attribute.", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    await attributeGService.Delete(allAttributesFromDB.
                    Where(x => x.Type == SettingsSelectedAttributeGroup && x.Name == SettingsSelectedAttribute).Select(x => x.Id).FirstOrDefault());
                    allAttributesFromDB = await attributeGService.GetAll();
                    SettingsDisplayProjectAttributes();
                }
            }
            else
            {
                MessageBox.Show("No atribute selected. Please select an atribute first");
            }
        }
    }
}
