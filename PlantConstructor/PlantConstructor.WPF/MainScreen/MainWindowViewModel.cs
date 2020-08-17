using DevExpress.Xpf.Editors.Helpers;
using DevExpress.XtraPrinting.Native;
using PlantConstructor.Domain.Model;
using PlantConstructor.Domain.Services;
using PlantConstructor.EntityFramework;
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


        public MainWindowViewModel()
        {
            LoadProjectsFromDatabaseAsync();
            SaveProjectButtonCommand = new RelayCommand(SaveProjectToDBAsync);
            AddProjectButtonCommand = new RelayCommand(AddNewProjectToDBAsync);
            DeleteProjectButtonCommand = new RelayCommand(DeleteProjectFromDBAsync);
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
            IDataService<Project> projectService = new GenericDataService<Project>(new PlantConstructorDbContextFactory());
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

            IDataService<Project> projectService = new GenericDataService<Project>(new PlantConstructorDbContextFactory());
            var updatedProject = await projectService.Update(SelectedItem.Id, new Project {Name=newProjectName, ProjectGroup=newProjectGroup });

            await LoadProjectsFromDatabaseWorkerAsync();
        }

        public async void AddNewProjectToDBAsync (object parameter)
        {
            IDataService<Project> projectService = new GenericDataService<Project>(new PlantConstructorDbContextFactory());
            var createdProject = await projectService.Create(new Project { Name = "NewProject", ProjectGroup = "New Project Group" });

            await LoadProjectsFromDatabaseWorkerAsync();

            SelectedItem = createdProject;
        }
        
        public async void DeleteProjectFromDBAsync (object parameter)
        {
            if (MessageBox.Show("Do you want to Delete the selected Project?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                IDataService<Project> projectService = new GenericDataService<Project>(new PlantConstructorDbContextFactory());
                var createdProject = await projectService.Delete(SelectedItem.Id);
                await LoadProjectsFromDatabaseWorkerAsync();
            }

        }

    }

}
