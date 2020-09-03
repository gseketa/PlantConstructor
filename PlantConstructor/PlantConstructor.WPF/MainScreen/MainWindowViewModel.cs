﻿using DevExpress.Xpf.Editors.Helpers;
using DevExpress.XtraPrinting.Native;
using PlantConstructor.Domain.Model;
using PlantConstructor.Domain.Services;
using PlantConstructor.EntityFramework;
using PlantConstructor.WPF.EditDataScreen;
using PlantConstructor.WPF.Helper;
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
        IDataService<Project> projectService;
        IDataService<ProjectAttribute> projectAttributeService;
        IDataService<AttributeG> attributeGService;

        IEnumerable<AttributeG> allAttributesFromDB;
        IEnumerable<ProjectAttribute> allProjectAttributesFromDB;

        private List<string> projectAttributeGroupesComboBox;

        public List<string> ProjectAttributeGroupesComboBox
        {
            get { return projectAttributeGroupesComboBox; }
            set { 
                projectAttributeGroupesComboBox = value;
                OnPropertyRaised("ProjectAttributeGroupesComboBox");
            }
        }

        private ListsOfAttributes allAvailableAttributes;

        public ListsOfAttributes AllAvailableAttributes
        {
            get { return allAvailableAttributes; }
            set { 
                allAvailableAttributes = value;
                OnPropertyRaised("AllAvailableAttributes");
            }
        }

        private ListsOfAttributes allProjectAttributes;

        public ListsOfAttributes AllProjectAttributes
        {
            get { return allProjectAttributes; }
            set { 
                allProjectAttributes = value;
                OnPropertyRaised("AllProjectAttributes");
            }
        }

        private List<string> allAvailableAttributesForDisplay;

        public List<string> AllAvailableAttributesForDisplay
        {
            get { return allAvailableAttributesForDisplay; }
            set
            {
                allAvailableAttributesForDisplay = value;
                OnPropertyRaised("AllAvailableAttributesForDisplay");
            }
        }

        private List<string> allProjectAttributesForDisplay;

        public List<string> AllProjectAttributesForDisplay
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

        private string selectedAttributeFromAvailable;

        public string SelectedAttributeFromAvailable
        {
            get { return selectedAttributeFromAvailable; }
            set { 
                selectedAttributeFromAvailable = value;
                OnPropertyRaised("SelectedAttributeFromAvailable");
            }
        }

        private string selectedAttributeFromProject;

        public string SelectedAttributeFromProject
        {
            get { return selectedAttributeFromProject; }
            set {
                selectedAttributeFromProject = value;
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
              ResetLocalAttributeStorage();
              LoadAttributesForTheProjectFromDB();
            }
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

        private ICommand allAvailableAttributesButtonCommand;

        public ICommand AllAvailableAttributesButtonCommand
        {
            get { return allAvailableAttributesButtonCommand; }
            set { allAvailableAttributesButtonCommand = value; }
        }




        public MainWindowViewModel()
        {
            projectService = new GenericDataService<Project>(new PlantConstructorDbContextFactory());
            projectAttributeService = new GenericDataService<ProjectAttribute>(new PlantConstructorDbContextFactory());
            attributeGService = new GenericDataService<AttributeG>(new PlantConstructorDbContextFactory());

            LoadProjectsFromDatabaseAsync();
            SaveProjectButtonCommand = new RelayCommand(SaveProjectToDBAsync);
            AddProjectButtonCommand = new RelayCommand(AddNewProjectToDBAsync);
            DeleteProjectButtonCommand = new RelayCommand(DeleteProjectFromDBAsync);
            EditDataButtonCommand = new RelayCommand(OpenEditDataWindow);

            AllAvailableAttributesButtonCommand = new RelayCommand(MoveAttributeFromLeftToRight);
            AllProjectAttributesButtonCommand = new RelayCommand(MoveAttributeFromRightToLeft);

            ProjectAttributeGroupesComboBox = new List<string> {"Site", "Zone", "Pipe", "Branch", "PipePart"};
            

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        private async void LoadProjectsFromDatabaseAsync()
        {
            await LoadProjectsFromDatabaseWorkerAsync();
        }

        private async Task LoadProjectsFromDatabaseWorkerAsync ()
        {
            var allProjects = await projectService.GetAll();
            var projectDepartments = allProjects.GroupBy(x => x.ProjectGroup).Select(x => new ProjectDepartment(x.Key, x.ToArray()));

            ProjectDepartments = new ObservableCollection<ProjectDepartment>(projectDepartments.ToArray());
            OnPropertyRaised("ProjectDepartments");
        }

        public async void SaveProjectToDBAsync (object parameter)
        {
            var values = (object[])parameter;
            string newProjectName=(string)values[0];
            string newProjectGroup = (string)values[1];

            var updatedProject = await projectService.Update(SelectedItem.Id, new Project {Name=newProjectName, ProjectGroup=newProjectGroup });

            await LoadProjectsFromDatabaseWorkerAsync();

            SelectedItem = updatedProject;
        }

        public void MoveAttributeFromLeftToRight (object parameter)
        {
            string attribute = SelectedAttributeFromAvailable;
            if (attribute != null)
            {
                if (SelectedAttributeGroup == "Site")
                {
                    AllAvailableAttributes.SiteAttributes.RemoveAt(AllAvailableAttributes.SiteAttributes.IndexOf(attribute));
                    AllProjectAttributes.SiteAttributes.Add(attribute);
                    SelectedAttributeFromAvailable = null;
                    AllAvailableAttributesForDisplay = null;
                    AllProjectAttributesForDisplay = null;
                    AllAvailableAttributesForDisplay = AllAvailableAttributes.SiteAttributes;
                    AllProjectAttributesForDisplay = AllProjectAttributes.SiteAttributes;
                    SelectedAttributeGroup = "Site";
                }
                else if (SelectedAttributeGroup == "Zone")
                {
                    AllAvailableAttributes.ZoneAttributes.RemoveAt(AllAvailableAttributes.ZoneAttributes.IndexOf(attribute));
                    AllProjectAttributes.ZoneAttributes.Add(attribute);
                    SelectedAttributeFromAvailable = null;
                    AllAvailableAttributesForDisplay = null;
                    AllProjectAttributesForDisplay = null;
                    AllAvailableAttributesForDisplay = AllAvailableAttributes.ZoneAttributes;
                    AllProjectAttributesForDisplay = AllProjectAttributes.ZoneAttributes;
                }
                else if (SelectedAttributeGroup == "Pipe")
                {
                    AllAvailableAttributes.PipeAttributes.RemoveAt(AllAvailableAttributes.PipeAttributes.IndexOf(attribute));
                    AllProjectAttributes.PipeAttributes.Add(attribute);
                    SelectedAttributeFromAvailable = null;
                    AllAvailableAttributesForDisplay = null;
                    AllProjectAttributesForDisplay = null;
                    AllAvailableAttributesForDisplay = AllAvailableAttributes.PipeAttributes;
                    AllProjectAttributesForDisplay = AllProjectAttributes.PipeAttributes;
                }
                else if (SelectedAttributeGroup == "Branch")
                {
                    AllAvailableAttributes.BranchAttributes.RemoveAt(AllAvailableAttributes.BranchAttributes.IndexOf(attribute));
                    AllProjectAttributes.BranchAttributes.Add(attribute);
                    SelectedAttributeFromAvailable = null;
                    AllAvailableAttributesForDisplay = null;
                    AllProjectAttributesForDisplay = null;
                    AllAvailableAttributesForDisplay = AllAvailableAttributes.BranchAttributes;
                    AllProjectAttributesForDisplay = AllProjectAttributes.BranchAttributes;
                }
                else if (SelectedAttributeGroup == "PipePart")
                {
                    AllAvailableAttributes.PipePartAttributes.RemoveAt(AllAvailableAttributes.PipePartAttributes.IndexOf(attribute));
                    AllProjectAttributes.PipePartAttributes.Add(attribute);
                    SelectedAttributeFromAvailable = null;
                    AllAvailableAttributesForDisplay = null;
                    AllProjectAttributesForDisplay = null;
                    AllAvailableAttributesForDisplay = AllAvailableAttributes.PipePartAttributes;
                    AllProjectAttributesForDisplay = AllProjectAttributes.PipePartAttributes;
                }
            }
        }

        public void MoveAttributeFromRightToLeft(object parameter)
        {
            string attribute = SelectedAttributeFromProject;
            if (attribute != null)
            {
                if (SelectedAttributeGroup == "Site")
                {
                    AllProjectAttributes.SiteAttributes.RemoveAt(AllProjectAttributes.SiteAttributes.IndexOf(attribute));
                    AllAvailableAttributes.SiteAttributes.Add(attribute);
                    SelectedAttributeFromProject = null;
                    AllAvailableAttributesForDisplay = null;
                    AllProjectAttributesForDisplay = null;
                    AllAvailableAttributesForDisplay = AllAvailableAttributes.SiteAttributes;
                    AllProjectAttributesForDisplay = AllProjectAttributes.SiteAttributes;
                }
                else if (SelectedAttributeGroup == "Zone")
                {
                    AllProjectAttributes.ZoneAttributes.RemoveAt(AllProjectAttributes.ZoneAttributes.IndexOf(attribute));
                    AllAvailableAttributes.ZoneAttributes.Add(attribute);
                    SelectedAttributeFromProject = null;
                    AllAvailableAttributesForDisplay = null;
                    AllProjectAttributesForDisplay = null;
                    AllAvailableAttributesForDisplay = AllAvailableAttributes.ZoneAttributes;
                    AllProjectAttributesForDisplay = AllProjectAttributes.ZoneAttributes;
                }
                else if (SelectedAttributeGroup == "Pipe")
                {
                    AllProjectAttributes.PipeAttributes.RemoveAt(AllProjectAttributes.PipeAttributes.IndexOf(attribute));
                    AllAvailableAttributes.PipeAttributes.Add(attribute);
                    SelectedAttributeFromProject = null;
                    AllAvailableAttributesForDisplay = null;
                    AllProjectAttributesForDisplay = null;
                    AllAvailableAttributesForDisplay = AllAvailableAttributes.PipeAttributes;
                    AllProjectAttributesForDisplay = AllProjectAttributes.PipeAttributes;
                }
                else if (SelectedAttributeGroup == "Branch")
                {
                    AllProjectAttributes.BranchAttributes.RemoveAt(AllProjectAttributes.BranchAttributes.IndexOf(attribute));
                    AllAvailableAttributes.BranchAttributes.Add(attribute);
                    SelectedAttributeFromProject = null;
                    AllAvailableAttributesForDisplay = null;
                    AllProjectAttributesForDisplay = null;
                    AllAvailableAttributesForDisplay = AllAvailableAttributes.BranchAttributes;
                    AllProjectAttributesForDisplay = AllProjectAttributes.BranchAttributes;
                }
                else if (SelectedAttributeGroup == "PipePart")
                {
                    AllAvailableAttributes.PipePartAttributes.RemoveAt(AllProjectAttributes.PipePartAttributes.IndexOf(attribute));
                    AllProjectAttributes.PipePartAttributes.Add(attribute);
                    SelectedAttributeFromProject = null;
                    AllAvailableAttributesForDisplay = null;
                    AllProjectAttributesForDisplay = null;
                    AllAvailableAttributesForDisplay = AllAvailableAttributes.PipePartAttributes;
                    AllProjectAttributesForDisplay = AllProjectAttributes.PipePartAttributes;
                }

            }
        }

        public async void AddNewProjectToDBAsync (object parameter)
        {
            var createdProject = await projectService.Create(new Project { Name = "NewProject", ProjectGroup = "New Project Group" });

            await LoadProjectsFromDatabaseWorkerAsync();

            SelectedItem = createdProject;

        }
        
        public async void DeleteProjectFromDBAsync (object parameter)
        {
            if (MessageBox.Show("Do you want to Delete the selected Project?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                await projectService.Delete(SelectedItem.Id);
                await LoadProjectsFromDatabaseWorkerAsync();
            }
        }

        private void DisplayProjectAttributes(string attributeGroup)
        {
           if (AllAvailableAttributes.GetListsOfAttributes(attributeGroup) != null)
            {
                AllAvailableAttributesForDisplay = AllAvailableAttributes.GetListsOfAttributes(attributeGroup);
                AllProjectAttributesForDisplay = AllProjectAttributes.GetListsOfAttributes(attributeGroup);
            }
           else
            {
                List<string> temp_allProjAtt = new List<string>();

                    temp_allProjAtt =
                    (from allA in allAttributesFromDB
                     join allP in allProjectAttributesFromDB on allA.Id equals allP.AttributeGId
                     where allP.ProjectId == SelectedItem.Id
                     where allA.Type == attributeGroup
                     select allA.Name)?.ToList();
                

                    var temp_allAvailableAtt = allAttributesFromDB.Where(x => x.Type == attributeGroup).Select(x => x.Name).ToList().Except(temp_allProjAtt).ToList();

                    if (attributeGroup == "Site")
                    {
                        AllProjectAttributes.SiteAttributes = temp_allProjAtt;
                        AllAvailableAttributes.SiteAttributes = temp_allAvailableAtt;
                    }
                    else if (attributeGroup == "Zone")
                    {
                        AllProjectAttributes.ZoneAttributes = temp_allProjAtt;
                        AllAvailableAttributes.ZoneAttributes = temp_allAvailableAtt;
                    }
                    else if (attributeGroup == "Pipe")
                    {
                        AllProjectAttributes.PipeAttributes = temp_allProjAtt;
                        AllAvailableAttributes.PipeAttributes = temp_allAvailableAtt;
                    }
                    else if (attributeGroup == "Branch")
                    {
                        AllProjectAttributes.BranchAttributes = temp_allProjAtt;
                        AllAvailableAttributes.BranchAttributes = temp_allAvailableAtt;
                    }
                    else if (attributeGroup == "PipePart")
                    {
                        AllProjectAttributes.PipePartAttributes = temp_allProjAtt;
                        AllAvailableAttributes.PipePartAttributes = temp_allAvailableAtt;
                    }

                    AllAvailableAttributesForDisplay = temp_allAvailableAtt;
                    AllProjectAttributesForDisplay = temp_allProjAtt;
                
            }
        }

        private void ResetLocalAttributeStorage()
        {
            AllProjectAttributes = new ListsOfAttributes();
            AllAvailableAttributes = new ListsOfAttributes();
        }

        private void ResetAttributeGroupComboBoxSelection ()
        {
            SelectedAttributeGroup = "Site";
        }

        private async void LoadAttributesForTheProjectFromDB()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            allAttributesFromDB = await attributeGService.GetAll();
            allProjectAttributesFromDB = await projectAttributeService.GetAll();
            
            ResetAttributeGroupComboBoxSelection();
            
            Mouse.OverrideCursor = null;
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

