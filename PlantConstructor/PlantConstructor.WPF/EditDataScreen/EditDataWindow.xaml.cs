using DevExpress.Spreadsheet;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Diagram.Native;
using PlantConstructor.Domain.Model;
using PlantConstructor.Domain.Services;
using PlantConstructor.EntityFramework;
using PlantConstructor.WPF.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PlantConstructor.WPF.EditDataScreen
{
    /// <summary>
    /// Interaction logic for EditDataWindow.xaml
    /// </summary>
    public partial class EditDataWindow : ThemedWindow
    {

        IDataService<ProjectAttribute> projectAttributeService;
        IDataService<AttributeG> attributeGService;
        IDataService<Element> elementService;
        IDataService<AttributeValue> attributeValueService;


        IEnumerable<ProjectAttribute> allProjectAttributes;
        IEnumerable<AttributeG> allAttributesG;
        IEnumerable<Element> allElements;
        IEnumerable<AttributeValue> allAttributeValues;

        private IWorkbook workbook;
        
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

        public EditDataWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            projectAttributeService = new GenericDataService<ProjectAttribute>(new PlantConstructorDbContextFactory());
            attributeGService = new GenericDataService<AttributeG>(new PlantConstructorDbContextFactory());
            elementService = new GenericDataService<Element>(new PlantConstructorDbContextFactory());
            attributeValueService = new GenericDataService<AttributeValue>(new PlantConstructorDbContextFactory());

            CreateWorkbook();
            LoadDataFromDatabase();
            LoadedWindowCommand = new RelayCommand(DisplayDefaultCursor);
        }

        public void DisplayDefaultCursor(object parameter)
        {
            Mouse.OverrideCursor = null;
        }

        private void CreateWorkbook()
        {
            workbook = this.spreadsheet.Document;

            //protect workbook
            if (!workbook.IsProtected)
                workbook.Protect("4321", true, false);

            workbook.Worksheets[0].Name = "Site";
            workbook.Worksheets.Add().Name = "Zone";
            workbook.Worksheets.Add().Name = "Pipe";
            workbook.Worksheets.Add().Name = "Branch";
            workbook.Worksheets.Add().Name = "Part";

            //set Site as active worksheet
            workbook.Worksheets.ActiveWorksheet = workbook.Worksheets[0];
        }

        private async void LoadDataFromDatabase ()
        {
            allProjectAttributes = await projectAttributeService.GetAll();
            allAttributesG = await attributeGService.GetAll();
            allElements = await elementService.GetAll();

            SetHeader(LoadAttributeHeaders("Site"), workbook.Worksheets[0]);
            SetHeader(LoadAttributeHeaders("Zone"), workbook.Worksheets[1]);
            SetHeader(LoadAttributeHeaders("Pipe"), workbook.Worksheets[2]);
            SetHeader(LoadAttributeHeaders("Branch"), workbook.Worksheets[3]);

            LoadAttributeValues("Site", workbook.Worksheets[0]);
            LoadAttributeValues("Zone", workbook.Worksheets[0]);
            LoadAttributeValues("Pipe", workbook.Worksheets[0]);
            LoadAttributeValues("Branch", workbook.Worksheets[0]);
        }

        private IEnumerable<string> LoadAttributeHeaders(string _type)
        {
            IEnumerable<string> headerAttributes =
                from allA in allAttributesG
                join allP in allProjectAttributes on allA.Id equals allP.AttributeGId
                where allP.ProjectId == SelectedProject.Id
                where allA.Type == _type
                select allA.Name;

            return headerAttributes;

        }

        private void SetHeader (IEnumerable<string> attributes, Worksheet sheet)
        {
            int columnCounter = 0;
            foreach (string attName in attributes)
            {
                sheet.Rows[0][columnCounter].Value = attName;
                columnCounter++;
            }

            sheet.Rows[0].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            sheet.Rows[0].Font.FontStyle = SpreadsheetFontStyle.Bold;

            //Protect the header row from editing
            if (!sheet.IsProtected)
            {
                sheet.Range["$A:$XFD"].Protection.Locked = false; // Unlock the entire document  
                sheet.Rows[0].Protection.Locked = true; // Lock the specified range  
                sheet.Protect("4321", WorksheetProtectionPermissions.Default
                    | WorksheetProtectionPermissions.DeleteRows
                    | WorksheetProtectionPermissions.Sort);
            }
        }

        private void LoadAttributeValues(string _type, Worksheet sheet)
        {
            
            int rowCount = 1;
            var allTypeElements = allElements.Where(x => x.Type ==_type && x.ProjectId==SelectedProject.Id).Select(x=>x.Id);
            if (allAttributesG != null && allAttributeValues != null)
            {
                var allTypeAttributeValues =
                        from allA in allAttributesG
                        join allV in allAttributeValues on allA.Id equals allV.AttributeGId
                        where allA.Type == _type
                        select new { allA.Name, allV.ElementId, allV.Value };
                
                foreach (int elementID in allTypeElements)
                {
                    for (int columnCount = 0; sheet.Cells[0, columnCount].Value.ToString() != ""; columnCount++)
                    {
                        var attributeValue = allTypeAttributeValues.
                            Where(x => x.Name == sheet.Cells[0, columnCount].Value.ToString() && x.ElementId == elementID).
                            Select(x => x.Value).FirstOrDefault();
                        if (attributeValue != default)
                        {
                            sheet.Rows[rowCount][columnCount].Value = attributeValue;
                        }
                        else sheet.Rows[rowCount][columnCount].Value = "";
                    }
                    rowCount++;
                }

            }

        }
    }
}
