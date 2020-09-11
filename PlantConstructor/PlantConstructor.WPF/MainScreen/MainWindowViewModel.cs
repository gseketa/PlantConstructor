using DevExpress.Xpf.Editors.Helpers;
using DevExpress.XtraPrinting.Native;
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

        private ICommand allAvailableAttributesButtonCommand;

        public ICommand AllAvailableAttributesButtonCommand
        {
            get { return allAvailableAttributesButtonCommand; }
            set { allAvailableAttributesButtonCommand = value; }
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

        public async void SaveProjectToDBAsync (object parameter)
        {
            var values = (object[])parameter;
            string newProjectName=(string)values[0];
            string newProjectGroup = (string)values[1];

            Mouse.OverrideCursor = Cursors.Wait;

            List<ProjectAttribute> listForProjectAttSave = new List<ProjectAttribute>();
            
            foreach (string att in AllProjectAttributes.SiteAttributes)
            {
                int attId = allAttributesFromDB.Where(x => x.Name == att && x.Type == "Site").Select(x => x.Id).FirstOrDefault();
                listForProjectAttSave.Add(new ProjectAttribute {ProjectId=SelectedItem.Id, AttributeGId=attId });
            }
            foreach (string att in AllProjectAttributes.ZoneAttributes)
            {
                int attId = allAttributesFromDB.Where(x => x.Name == att && x.Type == "Zone").Select(x => x.Id).FirstOrDefault();
                listForProjectAttSave.Add(new ProjectAttribute { ProjectId = SelectedItem.Id, AttributeGId = attId });
            }
            foreach (string att in AllProjectAttributes.PipeAttributes)
            {
                int attId = allAttributesFromDB.Where(x => x.Name == att && x.Type == "Pipe").Select(x => x.Id).FirstOrDefault();
                listForProjectAttSave.Add(new ProjectAttribute { ProjectId = SelectedItem.Id, AttributeGId = attId });
            }
            foreach (string att in AllProjectAttributes.BranchAttributes)
            {
                int attId = allAttributesFromDB.Where(x => x.Name == att && x.Type == "Branch").Select(x => x.Id).FirstOrDefault();
                listForProjectAttSave.Add(new ProjectAttribute { ProjectId = SelectedItem.Id, AttributeGId = attId });
            }
            foreach (string att in AllProjectAttributes.PipePartAttributes)
            {
                int attId = allAttributesFromDB.Where(x => x.Name == att && x.Type == "PipePart").Select(x => x.Id).FirstOrDefault();
                listForProjectAttSave.Add(new ProjectAttribute { ProjectId = SelectedItem.Id, AttributeGId = attId });
            }


            //foreach (ProjectAttribute projAtt in allProjectAttributesFromDB.Where(x=>x.ProjectId==SelectedItem.Id))
            //{
            //    await projectAttributeService.Delete(projAtt.Id);
            //}
            //foreach (string att in AllProjectAttributes.SiteAttributes)
            //{
            //    //int attId = allAttributesFromDB.Where(x => x.Name == att && x.Type=="Site").Select(x => x.Id).FirstOrDefault();
            //    var attG = allAttributesFromDB.Where(x => x.Name == att && x.Type == "Site").Select(x => x).FirstOrDefault();
            //    await projectAttributeService.Create(new ProjectAttribute { ProjectId = SelectedItem.Id, AttributeGId = attG.Id});
            //}
            //foreach (string att in AllProjectAttributes.ZoneAttributes)
            //{
            //    var attG = allAttributesFromDB.Where(x => x.Name == att && x.Type == "Zone").Select(x => x).FirstOrDefault();
            //    await projectAttributeService.Create(new ProjectAttribute { ProjectId = SelectedItem.Id, AttributeGId = attG.Id});
            //}
            //foreach (string att in AllProjectAttributes.PipeAttributes)
            //{
            //    var attG = allAttributesFromDB.Where(x => x.Name == att && x.Type == "Pipe").Select(x => x).FirstOrDefault();
            //    await projectAttributeService.Create(new ProjectAttribute { ProjectId = SelectedItem.Id, AttributeGId = attG.Id});
            //}
            //foreach (string att in AllProjectAttributes.BranchAttributes)
            //{
            //    var attG = allAttributesFromDB.Where(x => x.Name == att && x.Type == "Branch").Select(x => x).FirstOrDefault();
            //    await projectAttributeService.Create(new ProjectAttribute { ProjectId = SelectedItem.Id, AttributeGId = attG.Id});
            //}
            //foreach (string att in AllProjectAttributes.PipePartAttributes)
            //{
            //    var attG = allAttributesFromDB.Where(x => x.Name == att && x.Type == "PipePart").Select(x => x).FirstOrDefault();
            //    await projectAttributeService.Create(new ProjectAttribute { ProjectId = SelectedItem.Id, AttributeGId = attG.Id});
            //}

            Project updatedProject = new Project { Id = SelectedItem.Id, Name = newProjectName, ProjectGroup = newProjectGroup };
            using (PlantConstructorDbContext context = _contextFactory.CreateDbContext())
            {
                await context.BulkInsertOrUpdateOrDeleteAsync(listForProjectAttSave);
                context.Set<Project>().Update(updatedProject);
            }

            LoadProjectsFromDatabaseWorker();

            SelectedItem = updatedProject;

            //var updatedProject = await projectService.Update(SelectedItem.Id, new Project { Name = newProjectName, ProjectGroup = newProjectGroup });

            Mouse.OverrideCursor = null;
        }

        public void MoveAttributeFromLeftToRight (object parameter)
        {
            var listAtt = selectedAttributeFromAvailable;
            if (listAtt != null)
            {
                if (SelectedAttributeGroup == "Site")
                {
                    foreach (ListBoxAttributes attribute in listAtt)
                    {
                        AllAvailableAttributes.SiteAttributes.RemoveAt(AllAvailableAttributes.SiteAttributes.IndexOf(attribute.Item));
                        AllProjectAttributes.SiteAttributes.Add(attribute.Item);                       
                    }
                    SelectedAttributeFromAvailable = null;
                    AllAvailableAttributesForDisplay = null;
                    AllProjectAttributesForDisplay = null;
                    AllAvailableAttributesForDisplay = AllAvailableAttributes.GetAttForListbox("Site");
                    AllProjectAttributesForDisplay = AllProjectAttributes.GetAttForListbox("Site");
                    SelectedAttributeGroup = "Site";
                }
                else if (SelectedAttributeGroup == "Zone")
                {
                    foreach (ListBoxAttributes attribute in listAtt)
                    {
                        AllAvailableAttributes.ZoneAttributes.RemoveAt(AllAvailableAttributes.ZoneAttributes.IndexOf(attribute.Item));
                        AllProjectAttributes.ZoneAttributes.Add(attribute.Item);
                    }
                    SelectedAttributeFromAvailable = null;
                    AllAvailableAttributesForDisplay = null;
                    AllProjectAttributesForDisplay = null;
                    AllAvailableAttributesForDisplay = AllAvailableAttributes.GetAttForListbox("Zone");
                    AllProjectAttributesForDisplay = AllProjectAttributes.GetAttForListbox("Zone");
                }
                else if (SelectedAttributeGroup == "Pipe")
                {
                    foreach (ListBoxAttributes attribute in listAtt)
                    {
                        AllAvailableAttributes.PipeAttributes.RemoveAt(AllAvailableAttributes.PipeAttributes.IndexOf(attribute.Item));
                        AllProjectAttributes.PipeAttributes.Add(attribute.Item);
                    }
                    SelectedAttributeFromAvailable = null;
                    AllAvailableAttributesForDisplay = null;
                    AllProjectAttributesForDisplay = null;
                    AllAvailableAttributesForDisplay = AllAvailableAttributes.GetAttForListbox("Pipe");
                    AllProjectAttributesForDisplay = AllProjectAttributes.GetAttForListbox("Pipe");
                }
                else if (SelectedAttributeGroup == "Branch")
                {
                    foreach (ListBoxAttributes attribute in listAtt)
                    {
                        AllAvailableAttributes.BranchAttributes.RemoveAt(AllAvailableAttributes.BranchAttributes.IndexOf(attribute.Item));
                        AllProjectAttributes.BranchAttributes.Add(attribute.Item);
                        
                    }
                    SelectedAttributeFromAvailable = null;
                    AllAvailableAttributesForDisplay = null;
                    AllProjectAttributesForDisplay = null;
                    AllAvailableAttributesForDisplay = AllAvailableAttributes.GetAttForListbox("Branch");
                    AllProjectAttributesForDisplay = AllProjectAttributes.GetAttForListbox("Branch");
                }
                else if (SelectedAttributeGroup == "PipePart")
                {
                    foreach (ListBoxAttributes attribute in listAtt)
                    {
                        AllAvailableAttributes.PipePartAttributes.RemoveAt(AllAvailableAttributes.PipePartAttributes.IndexOf(attribute.Item));
                        AllProjectAttributes.PipePartAttributes.Add(attribute.Item);
                    }
                    SelectedAttributeFromAvailable = null;
                    AllAvailableAttributesForDisplay = null;
                    AllProjectAttributesForDisplay = null;
                    AllAvailableAttributesForDisplay = AllAvailableAttributes.GetAttForListbox("PipePart");
                    AllProjectAttributesForDisplay = AllProjectAttributes.GetAttForListbox("PipePart");
                }
            }
        }

        public void MoveAttributeFromRightToLeft(object parameter)
        {
            var listAtt = selectedAttributeFromProject;
            if (listAtt != null)
            {
                if (SelectedAttributeGroup == "Site")
                {
                    foreach (ListBoxAttributes attribute in listAtt)
                    {
                        AllProjectAttributes.SiteAttributes.RemoveAt(AllProjectAttributes.SiteAttributes.IndexOf(attribute.Item));
                        AllAvailableAttributes.SiteAttributes.Add(attribute.Item);
                    }
                    SelectedAttributeFromProject = null;
                    AllAvailableAttributesForDisplay = null;
                    AllProjectAttributesForDisplay = null;
                    AllAvailableAttributesForDisplay = AllAvailableAttributes.GetAttForListbox("Site");
                    AllProjectAttributesForDisplay = AllProjectAttributes.GetAttForListbox("Site");
                }
                else if (SelectedAttributeGroup == "Zone")
                {
                    foreach (ListBoxAttributes attribute in listAtt)
                    {
                        AllProjectAttributes.ZoneAttributes.RemoveAt(AllProjectAttributes.ZoneAttributes.IndexOf(attribute.Item));
                        AllAvailableAttributes.ZoneAttributes.Add(attribute.Item);
                        
                    }
                    SelectedAttributeFromProject = null;
                    AllAvailableAttributesForDisplay = null;
                    AllProjectAttributesForDisplay = null;
                    AllAvailableAttributesForDisplay = AllAvailableAttributes.GetAttForListbox("Zone");
                    AllProjectAttributesForDisplay = AllProjectAttributes.GetAttForListbox("Zone");
                }
                else if (SelectedAttributeGroup == "Pipe")
                {
                    foreach (ListBoxAttributes attribute in listAtt)
                    {
                        AllProjectAttributes.PipeAttributes.RemoveAt(AllProjectAttributes.PipeAttributes.IndexOf(attribute.Item));
                        AllAvailableAttributes.PipeAttributes.Add(attribute.Item);
                       
                    }
                    SelectedAttributeFromProject = null;
                    AllAvailableAttributesForDisplay = null;
                    AllProjectAttributesForDisplay = null;
                    AllAvailableAttributesForDisplay = AllAvailableAttributes.GetAttForListbox("Pipe");
                    AllProjectAttributesForDisplay = AllProjectAttributes.GetAttForListbox("Pipe");
                }
                else if (SelectedAttributeGroup == "Branch")
                {
                    foreach (ListBoxAttributes attribute in listAtt)
                    {
                        AllProjectAttributes.BranchAttributes.RemoveAt(AllProjectAttributes.BranchAttributes.IndexOf(attribute.Item));
                        AllAvailableAttributes.BranchAttributes.Add(attribute.Item);
                        
                    }
                    SelectedAttributeFromProject = null;
                    AllAvailableAttributesForDisplay = null;
                    AllProjectAttributesForDisplay = null;
                    AllAvailableAttributesForDisplay = AllAvailableAttributes.GetAttForListbox("Branch");
                    AllProjectAttributesForDisplay = AllProjectAttributes.GetAttForListbox("Branch");
                }
                else if (SelectedAttributeGroup == "PipePart")
                {
                    foreach (ListBoxAttributes attribute in listAtt)
                    {
                        AllAvailableAttributes.PipePartAttributes.RemoveAt(AllProjectAttributes.PipePartAttributes.IndexOf(attribute.Item));
                        AllProjectAttributes.PipePartAttributes.Add(attribute.Item);
                        
                    }
                    SelectedAttributeFromProject = null;
                    AllAvailableAttributesForDisplay = null;
                    AllProjectAttributesForDisplay = null;
                    AllAvailableAttributesForDisplay = AllAvailableAttributes.GetAttForListbox("PipePart");
                    AllProjectAttributesForDisplay = AllProjectAttributes.GetAttForListbox("PipePart");
                }

            }
        }

        public void AddNewProjectToDB (object parameter)
        {
            var createdProject = new Project { Name = "NewProject", ProjectGroup = "New Project Group" };
            using (PlantConstructorDbContext context = _contextFactory.CreateDbContext())
            {
                var created = context.Set<Project>().Add(createdProject);
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
                    var created = context.Set<Project>().Remove(SelectedItem);

                }
                LoadProjectsFromDatabaseWorker();
            }
        }

        private void DisplayProjectAttributes(string attributeGroup)
        {
           if (AllAvailableAttributes.GetListsOfAttributes(attributeGroup) != null)
            {
                AllAvailableAttributesForDisplay = AllAvailableAttributes.GetAttForListbox(attributeGroup);
                AllProjectAttributesForDisplay = AllProjectAttributes.GetAttForListbox(attributeGroup);
            }
           else
            {
                List<string> temp_allProjAtt = new List<string>();


                AllProjectAttributes.SiteAttributes = (from allA in allAttributesFromDB
                                                               join allP in allProjectAttributesFromDB on allA.Id equals allP.AttributeGId
                                                              where allP.ProjectId == SelectedItem.Id
                                                               where allA.Type == "Site"
                                                               select allA.Name)?.ToList();
                AllAvailableAttributes.SiteAttributes = allAttributesFromDB.Where(x => x.Type == "Site").Select(x => x.Name).ToList().Except(AllProjectAttributes.SiteAttributes).ToList();

                AllProjectAttributes.ZoneAttributes = (from allA in allAttributesFromDB
                                                       join allP in allProjectAttributesFromDB on allA.Id equals allP.AttributeGId
                                                       where allP.ProjectId == SelectedItem.Id
                                                       where allA.Type == "Zone"
                                                       select allA.Name)?.ToList();
                AllAvailableAttributes.ZoneAttributes = allAttributesFromDB.Where(x => x.Type == "Zone").Select(x => x.Name).ToList().Except(AllProjectAttributes.ZoneAttributes).ToList();

                AllProjectAttributes.PipeAttributes = (from allA in allAttributesFromDB
                                                       join allP in allProjectAttributesFromDB on allA.Id equals allP.AttributeGId
                                                       where allP.ProjectId == SelectedItem.Id
                                                       where allA.Type == "Pipe"
                                                       select allA.Name)?.ToList();
                AllAvailableAttributes.PipeAttributes = allAttributesFromDB.Where(x => x.Type == "Pipe").Select(x => x.Name).ToList().Except(AllProjectAttributes.PipeAttributes).ToList();

                AllProjectAttributes.BranchAttributes = (from allA in allAttributesFromDB
                                                       join allP in allProjectAttributesFromDB on allA.Id equals allP.AttributeGId
                                                       where allP.ProjectId == SelectedItem.Id
                                                       where allA.Type == "Branch"
                                                       select allA.Name)?.ToList();
                AllAvailableAttributes.BranchAttributes = allAttributesFromDB.Where(x => x.Type == "Branch").Select(x => x.Name).ToList().Except(AllProjectAttributes.BranchAttributes).ToList();

                AllProjectAttributes.PipePartAttributes = (from allA in allAttributesFromDB
                                                       join allP in allProjectAttributesFromDB on allA.Id equals allP.AttributeGId
                                                       where allP.ProjectId == SelectedItem.Id
                                                       where allA.Type == "PipePart"
                                                       select allA.Name)?.ToList();
                AllAvailableAttributes.PipePartAttributes = allAttributesFromDB.Where(x => x.Type == "PipePart").Select(x => x.Name).ToList().Except(AllProjectAttributes.PipePartAttributes).ToList();


                AllAvailableAttributesForDisplay = AllAvailableAttributes.GetAttForListbox(attributeGroup);
                AllProjectAttributesForDisplay = AllProjectAttributes.GetAttForListbox(attributeGroup);
                
            }
        }

        private void ResetLocalAttributeStorage()
        {
            AllProjectAttributes = new ListsOfAttributes();
            AllAvailableAttributes = new ListsOfAttributes();
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
                    FromSqlRaw($"SELECT * FROM ProjectAttributes").
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

