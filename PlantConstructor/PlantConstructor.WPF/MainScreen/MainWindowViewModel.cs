using DevExpress.Xpf.Editors.Helpers;
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
            }
        }

        private IEnumerable<string> officeNavigationItems;
        public IEnumerable<string> OfficeNavigationItems
        {
            get
            {
                return officeNavigationItems;
            }
            set
            {
                officeNavigationItems = value;
                OnPropertyRaised("OfficeNavigationItems");
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

            OfficeNavigationItems = new string[] { "Project", "Data" };
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
        }

        public async void AddNewProjectToDBAsync (object parameter)
        {
            var createdProject = await projectService.Create(new Project { Name = "NewProject", ProjectGroup = "New Project Group" });

            await LoadProjectsFromDatabaseWorkerAsync();

            SelectedItem = createdProject;
            await FillAttributesTablesAsync();
        }

        public async Task FillAttributesTablesAsync ()
        {
            var allAttributes = await attributeGService.GetAll();
            for (int i = 0; i < ProjectAttributes.SiteAttributeNames.Length; i++)
            {
                await projectAttributeService.Create(new ProjectAttribute { ProjectId=SelectedItem.Id, AttributeGId=allAttributes.FirstOrDefault(x=>x.Name==ProjectAttributes.SiteAttributeNames[i] && x.Type=="Site").Id});
            }
            for (int i = 0; i < ProjectAttributes.ZoneAttributeNames.Length; i++)
            {
                await projectAttributeService.Create(new ProjectAttribute { ProjectId = SelectedItem.Id, AttributeGId = allAttributes.FirstOrDefault(x => x.Name == ProjectAttributes.ZoneAttributeNames[i] && x.Type == "Zone").Id });
            }
            for (int i = 0; i < ProjectAttributes.PipeAttributeNames.Length; i++)
            {
                await projectAttributeService.Create(new ProjectAttribute { ProjectId = SelectedItem.Id, AttributeGId = allAttributes.FirstOrDefault(x => x.Name == ProjectAttributes.PipeAttributeNames[i] && x.Type == "Pipe").Id });
            }
            for (int i = 0; i < ProjectAttributes.BranchAttributeNames.Length; i++)
            {
                await projectAttributeService.Create(new ProjectAttribute { ProjectId = SelectedItem.Id, AttributeGId = allAttributes.FirstOrDefault(x => x.Name == ProjectAttributes.BranchAttributeNames[i] && x.Type == "Branch").Id });
            }
        }
        
        public async void DeleteProjectFromDBAsync (object parameter)
        {
            if (MessageBox.Show("Do you want to Delete the selected Project?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                await projectService.Delete(SelectedItem.Id);
                await LoadProjectsFromDatabaseWorkerAsync();
            }
        }

        public void OpenEditDataWindow(object parameter)
        {
            EditDataWindow objEditDataWindow = new EditDataWindow();
            Mouse.OverrideCursor = Cursors.Wait; 
            objEditDataWindow.ShowDialog();    
        }

    }

}
