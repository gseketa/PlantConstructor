﻿using DevExpress.Xpf.Core;
using DevExpress.Xpo.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PlantConstructor.Domain.Services;
using PlantConstructor.Domain.Model;
using PlantConstructor.EntityFramework;

namespace PlantConstructor.WPF.MainScreen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
        public MainWindow()
        {            
            InitializeComponent();
        }

    }

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
