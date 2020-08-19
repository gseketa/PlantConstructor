using DevExpress.DataProcessing.InMemoryDataProcessor;
using PlantConstructor.WPF.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PlantConstructor.WPF.EditDataScreen
{
    public class EditDataViewModel
    {
        private Project project;

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
        public EditDataViewModel(Project selectedProject)
        {
            project = selectedProject;
            LoadedWindowCommand = new RelayCommand(DisplayDefaultCursor);
        }

        public void DisplayDefaultCursor (object parameter)
        {
            Mouse.OverrideCursor = null;
        }
    }
}
