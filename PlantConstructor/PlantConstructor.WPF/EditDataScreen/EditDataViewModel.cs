using PlantConstructor.Domain.Model;
using PlantConstructor.WPF.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PlantConstructor.WPF.EditDataScreen
{
    public class EditDataViewModel
    {

        private Project selectedProject;

        public Project SelectedProject
        {
            get { return selectedProject; }
            set { selectedProject = value; }
        }


        private ICommand loadedWindowCommand;
        public ICommand LoadedWindowCommand
        {
            get
            {
                return loadedWindowCommand;
            }
            set
            {
                loadedWindowCommand = value;
            }
        }
        public EditDataViewModel()
        {
            LoadedWindowCommand = new RelayCommand(DisplayDefaultCursor);
        }

        public void DisplayDefaultCursor (object parameter)
        {
            Mouse.OverrideCursor = null;
        }
    }
}
