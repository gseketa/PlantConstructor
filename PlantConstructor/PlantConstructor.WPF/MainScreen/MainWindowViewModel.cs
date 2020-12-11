using DevExpress.Xpf.Editors.Helpers;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraReports.Wizards.Templates;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using PlantConstructor.Domain.Model;
using PlantConstructor.Domain.Services;
using PlantConstructor.EntityFramework;
using PlantConstructor.WPF.EditDataScreen;
using PlantConstructor.WPF.Helper;
using PlantConstructor.WPF.SettingsScreen;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace PlantConstructor.WPF.MainScreen
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ProjectDepartment> ProjectDepartments { get; set; }
        
        //IDataService<Project> projectService;
        //IDataService<ProjectAttribute> projectAttributeService;
        //IDataService<AttributeG> attributeGService;

        IEnumerable<AttributeG> allAttributesFromDB;
        IEnumerable<ProjectAttribute> allProjectAttributesFromDB;

        private PlantConstructorDbContextFactory _contextFactory;

        private List<string> projectAttributeGroupesComboBox;

        public List<string> ProjectAttributeGroupesComboBox
        {
            get { return projectAttributeGroupesComboBox; }
            set { 
                projectAttributeGroupesComboBox = value;
                OnPropertyRaised("ProjectAttributeGroupesComboBox");
            }
        }

        private List<AttributeG> allAvailableAttributes;

        public List<AttributeG> AllAvailableAttributes
        {
            get { return allAvailableAttributes; }
            set { 
                allAvailableAttributes = value;
                //OnPropertyRaised("AllAvailableAttributes");
            }
        }


        private List<ProjectAttribute> allProjectAttributes;

        public List<ProjectAttribute> AllProjectAttributes
        {
            get { return allProjectAttributes; }
            set { 
                allProjectAttributes = value;
                //OnPropertyRaised("AllProjectAttributes");
            }
        }

        private List<ProjectAttribute> allProjectAttributesToAdd;

        public List<ProjectAttribute> AllProjectAttributesToAdd
        {
            get { return allProjectAttributesToAdd; }
            set
            {
                allProjectAttributesToAdd = value;
                //OnPropertyRaised("AllProjectAttributes");
            }
        }

        private List<ProjectAttribute> allProjectAttributesToRemove;

        public List<ProjectAttribute> AllProjectAttributesToRemove
        {
            get { return allProjectAttributesToRemove; }
            set
            {
                allProjectAttributesToRemove = value;
                //OnPropertyRaised("AllProjectAttributes");
            }
        }


        private List<ListBoxAttributes> allAvailableAttributesForDisplay;

        public List<ListBoxAttributes> AllAvailableAttributesForDisplay
        {
            get { return allAvailableAttributesForDisplay; }
            set
            {
                allAvailableAttributesForDisplay = value;
                OnPropertyRaised("AllAvailableAttributesForDisplay");
            }
        }

        private List<ListBoxAttributes> allProjectAttributesForDisplay;

        public List<ListBoxAttributes> AllProjectAttributesForDisplay
        {
            get { return allProjectAttributesForDisplay; }
            set
            {
                allProjectAttributesForDisplay = value;
                OnPropertyRaised("AllProjectAttributesForDisplay");
            }
        }
      

        private string selectedAttributeGroup;

        public string SelectedAttributeGroup
        {
            get { return selectedAttributeGroup; }
            set { 
                selectedAttributeGroup = value;
                OnPropertyRaised("SelectedAttributeGroup");
                DisplayProjectAttributes(selectedAttributeGroup);
            }
        }

        private List<ListBoxAttributes> selectedAttributeFromAvailable;

        public ListBoxAttributes SelectedAttributeFromAvailable
        {
            get { return null; }
            set {
                //var selectedItems = AllAvailableAttributesForDisplay.Where(x => x.IsSelected).Count();
                selectedAttributeFromAvailable = AllAvailableAttributesForDisplay.Where(x => x.IsSelected).ToList();
                OnPropertyRaised("SelectedAttributeFromAvailable");
            }
        }

        private List<ListBoxAttributes> selectedAttributeFromProject;

        public ListBoxAttributes SelectedAttributeFromProject
        {
            get { return null; }
            set {
                //var selectedItems = AllProjectAttributesForDisplay.Where(x => x.IsSelected).Count();
                selectedAttributeFromProject = AllProjectAttributesForDisplay.Where(x => x.IsSelected).ToList();
                OnPropertyRaised("SelectedAttributeFromProject");
            }
        }

        private Project selectedItem;
        public Project SelectedItem {
            get
            { 
                return selectedItem; 
            }
            set
            { 
              selectedItem = value;
              OnPropertyRaised("SelectedItem");
              if (selectedItem != null)
                {
                    ResetLocalAttributeStorage();
                    LoadAttributesForTheProjectFromDB();
                }
            }
        }

        private ICommand openSettingsWindowCommand;

        public ICommand OpenSettingsWindowCommand
        {
            get { return openSettingsWindowCommand; }
            set { openSettingsWindowCommand = value; }
        }


        private ICommand saveProjectButtonCommand;
        public ICommand SaveProjectButtonCommand
        {
            get
            {
                return saveProjectButtonCommand;
            }
            set
            {
                saveProjectButtonCommand = value;
            }
        }
        

        private ICommand addProjectButtonCommand;

        public ICommand AddProjectButtonCommand
        {
            get { return addProjectButtonCommand; }
            set { addProjectButtonCommand = value; }
        }

        private ICommand deleteProjectButtonCommand;

        public ICommand DeleteProjectButtonCommand
        {
            get { return deleteProjectButtonCommand; }
            set { deleteProjectButtonCommand = value; }
        }

        private ICommand editDataButtonCommand;

        public ICommand EditDataButtonCommand
        {
            get { return editDataButtonCommand; }
            set { editDataButtonCommand = value; }
        }

        private ICommand allProjectAttributesButtonCommand;

        public ICommand AllProjectAttributesButtonCommand
        {
            get { return allProjectAttributesButtonCommand; }
            set { allProjectAttributesButtonCommand = value; }
        }

        private ICommand selectAllProjectAttributesButtonCommand;

        public ICommand SelectAllProjectAttributesButtonCommand
        {
            get { return selectAllProjectAttributesButtonCommand; }
            set { selectAllProjectAttributesButtonCommand = value; }
        }

        private ICommand allAvailableAttributesButtonCommand;

        public ICommand AllAvailableAttributesButtonCommand
        {
            get { return allAvailableAttributesButtonCommand; }
            set { allAvailableAttributesButtonCommand = value; }
        }

        private ICommand selectAllAvailableAttributesButtonCommand;

        public ICommand SelectAllAvailableAttributesButtonCommand
        {
            get { return selectAllAvailableAttributesButtonCommand; }
            set { selectAllAvailableAttributesButtonCommand = value; }
        }




        public MainWindowViewModel()
        {
            
            //projectService = new GenericDataService<Project>(new PlantConstructorDbContextFactory());
            //projectAttributeService = new GenericDataService<ProjectAttribute>(new PlantConstructorDbContextFactory());
            //attributeGService = new GenericDataService<AttributeG>(new PlantConstructorDbContextFactory());

            _contextFactory = new PlantConstructorDbContextFactory();

            LoadProjectsFromDatabase();
                                   
            SaveProjectButtonCommand = new RelayCommand(SaveProjectToDBAsync);
            AddProjectButtonCommand = new RelayCommand(AddNewProjectToDB);
            DeleteProjectButtonCommand = new RelayCommand(DeleteProjectFromDBAsync);
            EditDataButtonCommand = new RelayCommand(OpenEditDataWindow);
            OpenSettingsWindowCommand = new RelayCommand(OpenSettingsWindow);

            AllAvailableAttributesButtonCommand = new RelayCommand(MoveAttributeFromLeftToRight);
            AllProjectAttributesButtonCommand = new RelayCommand(MoveAttributeFromRightToLeft);

            SelectAllAvailableAttributesButtonCommand = new RelayCommand(SelectAllAvailableAttributes);
            SelectAllProjectAttributesButtonCommand = new RelayCommand(SelectAllProjectAttributes);

            ProjectAttributeGroupesComboBox = new List<string> {"Site", "Zone", "Pipe", "Branch",
                "PipePart", "Structure", "SubStructure", "StructurePart", "Equipment",
                "SubEquipment", "EquipmentPart"};

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        private void LoadProjectsFromDatabase()
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                LoadProjectsFromDatabaseWorker();
                Mouse.OverrideCursor = null;
            }
            catch
            {
                MessageBox.Show("Could not establish connection to the database", "Connection Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                System.Windows.Application.Current.Shutdown();
            }
        }

        private void LoadProjectsFromDatabaseWorker ()
        {

            using (PlantConstructorDbContext context = _contextFactory.CreateDbContext())
            {
                var allProjects = context.Set<Project>().
                    FromSqlRaw($"SELECT * FROM Projects").
                    ToList();
                var projectDepartments = allProjects.GroupBy(x => x.ProjectGroup).Select(x => new ProjectDepartment(x.Key, x.ToArray()));
                ProjectDepartments = new ObservableCollection<ProjectDepartment>(projectDepartments.ToArray());
                OnPropertyRaised("ProjectDepartments");
            }
     
        }

        public void SaveProjectToDBAsync (object parameter)
        {
            var values = (object[])parameter;
            string newProjectName=(string)values[0];
            string newProjectGroup = (string)values[1];

            Mouse.OverrideCursor = Cursors.Wait;

            Project updatedProject = new Project { Id = SelectedItem.Id, Name = newProjectName, ProjectGroup = newProjectGroup };
            using (PlantConstructorDbContext context = _contextFactory.CreateDbContext())
            {              
                context.Update(updatedProject);
                context.SaveChanges();
            }

            LoadProjectsFromDatabaseWorker();

            SelectedItem = updatedProject;

            //var updatedProject = await projectService.Update(SelectedItem.Id, new Project { Name = newProjectName, ProjectGroup = newProjectGroup });

            Mouse.OverrideCursor = null;
        }

        public async void MoveAttributeFromLeftToRight (object parameter)
        {
            var listAtt = selectedAttributeFromAvailable;
            foreach (ListBoxAttributes item in AllAvailableAttributesForDisplay)
            {
                item.IsSelected = false;
            }
            SelectedAttributeFromAvailable = null;
            if (listAtt != null)
            {
                    foreach (ListBoxAttributes attribute in listAtt)
                    {
                        var attG = allAttributesFromDB.
                            Where(x => x.Name == attribute.Item && x.Type==SelectedAttributeGroup).
                            Select(x => x).FirstOrDefault();
                        AllAvailableAttributes.Remove(AllAvailableAttributes.Single(x => x.Name == attG.Name && x.Type==attG.Type));
                        AllProjectAttributes.Add(new ProjectAttribute {ProjectId=SelectedItem.Id, AttributeG=attG});
                        AllAvailableAttributesForDisplay.Remove(AllAvailableAttributesForDisplay.Single(x => x.Item == attribute.Item));
                        AllProjectAttributesForDisplay.Add(new ListBoxAttributes { Item=attribute.Item });
                        AllProjectAttributesToAdd.Add(new ProjectAttribute { ProjectId = SelectedItem.Id, AttributeGId = attG.Id });
                }

                

                //SelectedAttributeGroup = SelectedAttributeGroup;
                var temp1 = AllAvailableAttributesForDisplay;
                var temp2 = AllProjectAttributesForDisplay;

                AllAvailableAttributesForDisplay = new List<ListBoxAttributes>();
                AllProjectAttributesForDisplay = new List<ListBoxAttributes>();

                AllAvailableAttributesForDisplay = temp1;
                AllProjectAttributesForDisplay = temp2;
                
                Mouse.OverrideCursor = Cursors.Wait;
                await SaveChangesToAttributes();
                Mouse.OverrideCursor = null;
            }
        }

        public async void MoveAttributeFromRightToLeft(object parameter)
        {
            var listAtt = selectedAttributeFromProject;
            foreach (ListBoxAttributes item in AllProjectAttributesForDisplay)
            {
                item.IsSelected = false;
            }
            SelectedAttributeFromProject = null;
            if (listAtt != null)
            {
                foreach (ListBoxAttributes attribute in listAtt)
                {
                    var attG = allAttributesFromDB.
                        Where(x => x.Name == attribute.Item && x.Type == SelectedAttributeGroup).
                        Select(x => x).FirstOrDefault();
                    AllAvailableAttributes.Add(new AttributeG {Name=attG.Name, Type=attG.Type });
                    AllProjectAttributesToRemove.Add(AllProjectAttributes.Where(x => x.AttributeG.Id == attG.Id && x.ProjectId==SelectedItem.Id).Select(x=>x).FirstOrDefault());
                    AllProjectAttributes.Remove(AllProjectAttributes.Single(x=>x.AttributeG.Id==attG.Id));
                    AllAvailableAttributesForDisplay.Add(new ListBoxAttributes {Item=attribute.Item });
                    AllProjectAttributesForDisplay.Remove(AllProjectAttributesForDisplay.Single(x => x.Item == attribute.Item));
                    
                }

                

                var temp1 = AllAvailableAttributesForDisplay;
                var temp2 = AllProjectAttributesForDisplay;

                AllAvailableAttributesForDisplay = new List<ListBoxAttributes>();
                AllProjectAttributesForDisplay = new List<ListBoxAttributes>();

                AllAvailableAttributesForDisplay = temp1;
                AllProjectAttributesForDisplay = temp2;
                //SelectedAttributeGroup = SelectedAttributeGroup;
                Mouse.OverrideCursor = Cursors.Wait;
                await SaveChangesToAttributes();
                Mouse.OverrideCursor = null;

            }
        }

        public void SelectAllAvailableAttributes(object parameter)
        {
            if (AllAvailableAttributesForDisplay != null)
            {
                foreach(ListBoxAttributes attrib in AllAvailableAttributesForDisplay)
                {
                    attrib.IsSelected = true;
                }
                selectedAttributeFromAvailable = AllAvailableAttributesForDisplay.Where(x => x.IsSelected).ToList();
                OnPropertyRaised("SelectedAttributeFromAvailable");
            }
        }

        public void SelectAllProjectAttributes(object parameter)
        {
            if (AllProjectAttributesForDisplay != null)
            {
                foreach (ListBoxAttributes attrib in AllProjectAttributesForDisplay)
                {
                    attrib.IsSelected = true;
                }
                selectedAttributeFromProject = AllProjectAttributesForDisplay.Where(x => x.IsSelected).ToList();
                OnPropertyRaised("SelectedAttributeFromProject");
            }

        }

        public async Task SaveChangesToAttributes()
        {
            //Mouse.OverrideCursor = Cursors.Wait;

            using (PlantConstructorDbContext context = _contextFactory.CreateDbContext())
            {
                await context.BulkDeleteAsync<ProjectAttribute>(AllProjectAttributesToRemove);
                await context.BulkInsertAsync<ProjectAttribute>(AllProjectAttributesToAdd);
            }

            AllProjectAttributesToAdd = new List<ProjectAttribute>();
            AllProjectAttributesToRemove = new List<ProjectAttribute>();

            //Mouse.OverrideCursor = null;
        }

        public void AddNewProjectToDB (object parameter)
        {
            var createdProject = new Project { Name = "NewProject", ProjectGroup = "New Project Group" };
            using (PlantConstructorDbContext context = _contextFactory.CreateDbContext())
            {
                var created = context.Add<Project>(createdProject);
                context.SaveChanges();

               LoadProjectsFromDatabaseWorker();
                SelectedItem = created.Entity;
            }
            


        }
        
        public void DeleteProjectFromDBAsync (object parameter)
        {
            if (MessageBox.Show("Do you want to Delete the selected Project?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                using (PlantConstructorDbContext context = _contextFactory.CreateDbContext())
                {
                    context.Remove<Project>(SelectedItem);
                    context.SaveChanges();

                }
                LoadProjectsFromDatabaseWorker();
            }
        }

        private void DisplayProjectAttributes(string attributeGroup)
        {
            AllAvailableAttributesForDisplay = new List<ListBoxAttributes>();
            AllProjectAttributesForDisplay = new List<ListBoxAttributes>();
            
            if (AllAvailableAttributes ==null)
            {
                AllProjectAttributes = allProjectAttributesFromDB.ToList();
                AllAvailableAttributes = allAttributesFromDB.ToList().Except(AllProjectAttributes.Select(x => x.AttributeG)).ToList();   
            }

            foreach (string attName in AllAvailableAttributes.Where(x => x.Type == attributeGroup).Select(x => x.Name).ToList())
            {
                AllAvailableAttributesForDisplay.Add(new ListBoxAttributes { Item = attName });
            }
            foreach (string attName in AllProjectAttributes.Where(x => x.AttributeG.Type == attributeGroup).Select(x => x.AttributeG.Name).ToList())
            {
                AllProjectAttributesForDisplay.Add(new ListBoxAttributes { Item = attName });
            }

            var temp1 = AllAvailableAttributesForDisplay;
            var temp2 = AllProjectAttributesForDisplay;

            AllAvailableAttributesForDisplay = new List<ListBoxAttributes>();
            AllProjectAttributesForDisplay = new List<ListBoxAttributes>();

            AllAvailableAttributesForDisplay = temp1;
            AllProjectAttributesForDisplay = temp2;

        }

        private void ResetLocalAttributeStorage()
        {
            AllProjectAttributes = null;
            AllAvailableAttributes = null;
            AllProjectAttributesToAdd = new List<ProjectAttribute>();
            AllProjectAttributesToRemove= new List<ProjectAttribute>();
        }

        private void ResetAttributeGroupComboBoxSelection ()
        {
            if (SelectedAttributeGroup == null)
            {
                SelectedAttributeGroup = "Site";
            }
            else
            {
                DisplayProjectAttributes(SelectedAttributeGroup);
            }
        }

        private void LoadAttributesForTheProjectFromDB()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            using (PlantConstructorDbContext context = _contextFactory.CreateDbContext())
            {
                allAttributesFromDB = context.Set<AttributeG>().
                    FromSqlRaw($"SELECT * FROM AttributesG").
                    ToList();
                allProjectAttributesFromDB= context.Set<ProjectAttribute>().
                    FromSqlRaw($"SELECT * FROM ProjectAttributes WHERE ProjectId={SelectedItem.Id}").
                    Include(x=>x.AttributeG).
                    ToList();
            }

            ResetAttributeGroupComboBoxSelection();
            
            Mouse.OverrideCursor = null;
        }

        public void OpenSettingsWindow(object parameter)
        {
            SelectedItem = null;
            SettingsWindow objSettingsWindow = new SettingsWindow();
            objSettingsWindow.DataContext = new SettingsViewModel();
            objSettingsWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            objSettingsWindow.ShowDialog();
        }

        public void OpenEditDataWindow(object parameter)
        {
            EditDataWindow objEditDataWindow = new EditDataWindow();
            objEditDataWindow.SelectedProject = SelectedItem;
            Mouse.OverrideCursor = Cursors.Wait; 
            objEditDataWindow.ShowDialog();    
        }

    }

}

