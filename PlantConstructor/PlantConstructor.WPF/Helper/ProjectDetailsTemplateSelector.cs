using PlantConstructor.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace PlantConstructor.WPF.Helper
{
    public class ProjectDetailsTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ProjectDetailsTemplate { get; set; }
        public DataTemplate EmptyDetailsTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is Project)
                return ProjectDetailsTemplate;
            return EmptyDetailsTemplate;
        }
    }
}
