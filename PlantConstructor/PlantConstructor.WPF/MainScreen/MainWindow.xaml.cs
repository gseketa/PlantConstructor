using DevExpress.Xpf.Core;
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

            IDataService<Project> projectService = new GenericDataService<Project>(new PlantConstructorDbContextFactory());
            projectService.Create(new Project
            {
                Name = "Test2"
            });

        }
    }
}
