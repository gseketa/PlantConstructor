using PlantConstructor.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PlantConstructor.WPF.Helper
{
    public class ProjectDepartment
    {
        public string Name { get; set; }
        public ObservableCollection<Project> Projects { get; set; }

        public ProjectDepartment(string name, IEnumerable<Project> projects)
        {
            Name = name;
            Projects = new ObservableCollection<Project>(projects);
        }
        public override string ToString()
        {
            return Name;

        }
    }
}
