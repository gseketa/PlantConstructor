using DevExpress.CodeParser.Diagnostics;
using DevExpress.DataProcessing.InMemoryDataProcessor.GraphGenerator;
using DevExpress.Spreadsheet;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Diagram.Native;
using DevExpress.Xpf.PivotGrid.Printing.TypedStyles;
using DevExpress.Xpf.Spreadsheet;
using Microsoft.Win32;
using PlantConstructor.Domain.Model;
using PlantConstructor.Domain.Services;
using PlantConstructor.EntityFramework;
using PlantConstructor.WPF.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

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

        List<string> siteHeaderAttributes;
        List<string> zoneHeaderAttributes;
        List<string> pipeHeaderAttributes;
        List<string> branchHeaderAttributes;
        List<string> partHeaderAttributes;

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
            LoadedWindowCommand = new RelayCommand(InitializeWorkEnvironment);
        }

        public async void InitializeWorkEnvironment(object parameter)
        {
            projectAttributeService = new GenericDataService<ProjectAttribute>(new PlantConstructorDbContextFactory());
            attributeGService = new GenericDataService<AttributeG>(new PlantConstructorDbContextFactory());
            elementService = new GenericDataService<Element>(new PlantConstructorDbContextFactory());
            attributeValueService = new GenericDataService<AttributeValue>(new PlantConstructorDbContextFactory());

            CreateWorkbook();
            await LoadDataFromDatabase();
            
            LogText.Text = "Data loaded from DB";
            Mouse.OverrideCursor = null;
        }

        private void CreateWorkbook()
        {
            spreadsheet.BeginUpdate();
            try
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
            finally
            {
                spreadsheet.EndUpdate();
            }
        }

        private async Task LoadDataFromDatabase()
        {
            
            allProjectAttributes = await projectAttributeService.GetAll();
            allAttributesG = await attributeGService.GetAll();
            allElements = await elementService.GetAll();
            allAttributeValues = await attributeValueService.GetAll();

            siteHeaderAttributes = LoadAttributeHeaders("Site").ToList();
            zoneHeaderAttributes = LoadAttributeHeaders("Zone").ToList();
            pipeHeaderAttributes = LoadAttributeHeaders("Pipe").ToList();
            branchHeaderAttributes = LoadAttributeHeaders("Branch").ToList();
            partHeaderAttributes = LoadAttributeHeaders("Part").ToList();

            SetHeader(siteHeaderAttributes, workbook.Worksheets[0]);
            SetHeader(zoneHeaderAttributes, workbook.Worksheets[1]);
            SetHeader(pipeHeaderAttributes, workbook.Worksheets[2]);
            SetHeader(branchHeaderAttributes, workbook.Worksheets[3]);
            SetHeader(partHeaderAttributes, workbook.Worksheets[4]);

            LoadAttributeValues("Site", workbook.Worksheets[0]);
            LoadAttributeValues("Zone", workbook.Worksheets[1]);
            LoadAttributeValues("Pipe", workbook.Worksheets[2]);
            LoadAttributeValues("Branch", workbook.Worksheets[3]);
            LoadAttributeValues("Part", workbook.Worksheets[4]);
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

        private void SetHeader(IEnumerable<string> attributes, Worksheet sheet)
        {
            int columnCounter = 0;
            spreadsheet.BeginUpdate();
            try
            {
                foreach (string attName in attributes)
                {
                    sheet.Rows[0][columnCounter].Value = attName;
                    sheet.Rows[0][columnCounter].FillColor = System.Drawing.Color.Tomato;
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
                        | WorksheetProtectionPermissions.FormatColumns
                        | WorksheetProtectionPermissions.FormatRows
                        | WorksheetProtectionPermissions.Sort);
                }
            }
            finally
            {
                spreadsheet.EndUpdate();
            }
        }

        private void LoadAttributeValues(string _type, Worksheet sheet)
        {

            int rowCount = 1;
            var allTypeElements = allElements.Where(x => x.Type == _type && x.ProjectId == SelectedProject.Id).Select(x => x.Id);
            if (allAttributesG != null && allAttributeValues != null)
            {
                var allTypeAttributeValues =
                        from allA in allAttributesG
                        join allV in allAttributeValues on allA.Id equals allV.AttributeGId
                        where allA.Type == _type
                        select new { allA.Name, allV.ElementId, allV.Value };
                
                spreadsheet.BeginUpdate();
                try
                {
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
                finally
                {
                    spreadsheet.EndUpdate();
                }

            }

        }

        private async void SaveToDB_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            await DeleteAllElementsForTheProject();

            await SaveNewDataToDB("Site", workbook.Worksheets[0]);
            await SaveNewDataToDB("Zone", workbook.Worksheets[1]);
            await SaveNewDataToDB("Pipe", workbook.Worksheets[2]);
            await SaveNewDataToDB("Branch", workbook.Worksheets[3]);

            LogText.Text = "Data saved to DB";
            Mouse.OverrideCursor = null;
        }

        private async Task DeleteAllElementsForTheProject()
        {
            allElements = await elementService.GetAll();
            var allProjectElements = allElements.Where(x => x.ProjectId == SelectedProject.Id).Select(x => x.Id);
            foreach (int ElementId in allProjectElements)
            {
                await elementService.Delete(ElementId);
            }
        }

        private async Task SaveNewDataToDB(string _type, Worksheet sheet)
        {
            for (int rowCount = 1; sheet.Cells[rowCount, 0].Value.ToString() != ""; rowCount++)
            {
                Element newElement = await elementService.Create(new Element { ProjectId = SelectedProject.Id, Type = _type });
                for (int columnCount = 0; sheet.Cells[0, columnCount].Value.ToString() != ""; columnCount++)
                {
                    int newAttributeId = allAttributesG.
                            Where(x => x.Name == sheet.Cells[0, columnCount].Value.ToString() && x.Type == _type).
                            Select(x => x.Id).FirstOrDefault();
                    await attributeValueService.Create(new AttributeValue
                    { AttributeGId = newAttributeId, ElementId = newElement.Id, Value = sheet.Cells[rowCount, columnCount].Value.ToString() });
                }
            }
        }

        private void CreateCode_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
        }


        private async void ImportData_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "ATT files|*.att";
            theDialog.InitialDirectory = @"C:\";
            if (theDialog.ShowDialog() == true)
            {
                string filename = theDialog.FileName;
                Mouse.OverrideCursor = Cursors.Wait;
                LogText.Text = "Import started... ";
                string[] allFileLines = await ReadDataFromFile(filename);
                WriteDataInSpreadsheet(await InterpretData(new Progress<string>(status => LogText.Text = status), allFileLines));
                Mouse.OverrideCursor = null;
                LogText.Text = "Import finished... ";
            }
        }

        private async Task<string[]> ReadDataFromFile(string filename)
        {
            string[] filelines = await File.ReadAllLinesAsync(filename);
            return filelines;
        }

        private async Task<ListOfSpreadsheetElements> InterpretData(IProgress<string> progress, string[] filelines)
        {
            string currentType = "";
            string previousPartOwner = "";
            string currentPartOwner = "";

            //find out the last empty rows index in each sheet; pointing at the last filled row
            int siteRowCount;
            for (siteRowCount = 1; workbook.Worksheets[0].Cells[siteRowCount, 0].Value.ToString() != ""; siteRowCount++) { }
            siteRowCount--;

            int zoneRowCount;
            for (zoneRowCount = 1; workbook.Worksheets[1].Cells[zoneRowCount, 0].Value.ToString() != ""; zoneRowCount++) { }
            zoneRowCount--;

            int pipeRowCount;
            for (pipeRowCount = 1; workbook.Worksheets[2].Cells[pipeRowCount, 0].Value.ToString() != ""; pipeRowCount++) { }
            pipeRowCount--;

            int branchRowCount;
            for (branchRowCount = 1; workbook.Worksheets[3].Cells[branchRowCount, 0].Value.ToString() != ""; branchRowCount++) { }
            branchRowCount--;

            int partRowCount;
            for (partRowCount = 1; workbook.Worksheets[4].Cells[partRowCount, 0].Value.ToString() != ""; partRowCount++) { }
            partRowCount--;

            ListOfSpreadsheetElements dataStorage = new ListOfSpreadsheetElements { SiteElements = new List<SpreadsheetElement> { }, BranchElements = new List<SpreadsheetElement> { }, PipeElements = new List<SpreadsheetElement> { }, ZoneElements = new List<SpreadsheetElement> { }, PartElements = new List<SpreadsheetElement> { } };

            //read line by line
            for (int a = 0; a < filelines.Length; a++)
            {
                if (progress != null)
                {
                    progress.Report("Import progress: " + a.ToString() + " / " + filelines.Length.ToString());
                }

                await Task.Run(() =>
                {

                    //split the codeline in parts with empty string delimiter; remove empty spaces
                    string[] importCodeLine = filelines[a].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                    //if the line is not empty or one of the headers, evaluate it
                    if (importCodeLine != null && importCodeLine.Length != 0 && importCodeLine[0] != "\t" && importCodeLine[0] != "END" && importCodeLine[0] != "AVEVA_Attributes_File" && importCodeLine[1] != "Header")
                    {
                        //if the line contains the NEW keyword
                        if (importCodeLine[0] == "NEW")
                        {
                            //split the TYPE codeline and assign the value (if KPCNAME is added to the doc, then the type is on 3rd row not 2nd)
                            string checkTypeLine = filelines[a + 2].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0];
                            if (checkTypeLine.Contains("KPCNAME"))
                            {
                                currentType = filelines[a + 3].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1];
                            }
                            else
                            {
                                currentType = filelines[a + 2].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1];
                            }
                            //LogText.Text += Environment.NewLine + currentType;

                            //move the pointer to the next free line in appropriate sheet
                            switch (currentType)
                            {
                                case "SITE":
                                    siteRowCount++;
                                    break;
                                case "ZONE":
                                    zoneRowCount++;
                                    break;
                                case "PIPE":
                                    pipeRowCount++;
                                    break;
                                case "BRAN":
                                    branchRowCount++;
                                    break;
                                default:
                                    partRowCount++;
                                    break;
                            }

                            //for Part, add new owner if needed
                            //if (currentType != "PIPE" && currentType != "SITE" && currentType != "ZONE" && currentType != "BRAN")
                            //{
                            //    currentPartOwner = filelines[a + 4].Split(new string[] { ":=" }, StringSplitOptions.RemoveEmptyEntries)[1];

                                //if (previousPartOwner != currentPartOwner)
                                //{

                                //    //this.Dispatcher.Invoke(() =>
                                //    //{
                                //    //    //remove protection
                                //    //    pipeConstructor.ChangeProtectionAndColor(pipeConstructorRowCount, new List<string>() { "D" });
                                //    //    //add data
                                //    //    workbook.Worksheets[4].Rows[pipeConstructorRowCount]["A"].Value = "NAME";
                                //    //    workbook.Worksheets[4].Rows[pipeConstructorRowCount]["D"].Value = currentPlantConstructorOwner;
                                //    //});
                                //    dataStorage.PartElements.Add(new SpreadsheetElement { Row = partRowCount, Column = 0, Value = "NAME" });
                                //    dataStorage.PartElements.Add(new SpreadsheetElement { Row = partRowCount, Column = , Value = "NAME" });
                                //    previousPartOwner = currentPartOwner;
                                //    partRowCount++;
                                //}
                                ////remove protection for the next row where the new element will be added
                                //this.Dispatcher.Invoke(() =>
                                //{
                                //    pipeConstructor.ChangeProtectionAndColor(pipeConstructorRowCount, new List<string>() { "C", "D", "E", "F", "G", "H", "I" });
                                //});
                                ////add type
                                ////TODO...
                            //}

                        }
                        else
                        {
                            if (currentType == "")
                            {
                                //LogText.Text += Environment.NewLine + "Error - file not in appropriate format; NEW keyword not found at beginning";
                            }
                            else
                            {
                                //read the attribute - 0 is the attribute name; 1 is the value
                                string attributeName = importCodeLine[0].Split(new string[] { ":=" }, StringSplitOptions.RemoveEmptyEntries)[0];
                                string attributeValue = "";
                                for (int i = 1; i < importCodeLine.Length; i++)
                                {
                                    attributeValue = attributeValue + " " + importCodeLine[i];
                                }

                                if (currentType == "SITE")
                                {
                                    //find out whether the value exists in the dictionary 
                                    int listItemIndex = siteHeaderAttributes.FindIndex(s => s == attributeName);
                                    if (listItemIndex >= 0)
                                    {
                                        //write the attribute value to the appropriate row and column
                                        //this.Dispatcher.Invoke(() =>
                                        //{
                                        //workbook.Worksheets[0].Rows[siteRowCount][listItemIndex].Value = attributeValue;
                                        //});
                                        dataStorage.SiteElements.Add(new SpreadsheetElement { Row = siteRowCount, Column = listItemIndex, Value = attributeValue });
                                    }

                                }

                                else if (currentType == "ZONE")
                                {
                                    //find out whether the value exists in the dictionary 
                                    int listItemIndex = zoneHeaderAttributes.FindIndex(s => s == attributeName);
                                    if (listItemIndex >= 0)
                                    {
                                        //write the attribute value to the appropriate row and column
                                        //this.Dispatcher.Invoke(() =>
                                        //{
                                        //    workbook.Worksheets[1].Rows[zoneRowCount][listItemIndex].Value = attributeValue;
                                        //});
                                        dataStorage.ZoneElements.Add(new SpreadsheetElement { Row = zoneRowCount, Column = listItemIndex, Value = attributeValue });
                                    }

                                }

                                //write the attribute value to appropriate place
                                else if (currentType == "PIPE")
                                {
                                    int listItemIndex = pipeHeaderAttributes.FindIndex(s => s == attributeName);
                                    if (listItemIndex >= 0)
                                    {
                                        //write the attribute value to the appropriate row and column
                                        //this.Dispatcher.Invoke(() =>
                                        //{
                                        //    workbook.Worksheets[2].Rows[pipeRowCount][listItemIndex].Value = attributeValue;
                                        //});
                                        dataStorage.PipeElements.Add(new SpreadsheetElement { Row = pipeRowCount, Column = listItemIndex, Value = attributeValue });
                                    }

                                }
                                else if (currentType == "BRAN")
                                {
                                    //find out whether the value exists in the dictionary 
                                    int listItemIndex = branchHeaderAttributes.FindIndex(s => s == attributeName);
                                    if (listItemIndex >= 0)
                                    {
                                        //write the attribute value to the appropriate row and column
                                        //this.Dispatcher.Invoke(() =>
                                        //{
                                        //    workbook.Worksheets[3].Rows[branchRowCount][listItemIndex].Value = attributeValue;
                                        //});
                                        dataStorage.BranchElements.Add(new SpreadsheetElement { Row = branchRowCount, Column = listItemIndex, Value = attributeValue });
                                    }

                                }
                                else
                                {
                                    int listItemIndex = partHeaderAttributes.FindIndex(s => s == attributeName);
                                    if (listItemIndex >= 0)
                                    {
                                        //write the attribute value to the appropriate row and column
                                        //this.Dispatcher.Invoke(() =>
                                        //{
                                        //    workbook.Worksheets[3].Rows[branchRowCount][listItemIndex].Value = attributeValue;
                                        //});
                                        dataStorage.PartElements.Add(new SpreadsheetElement { Row = partRowCount, Column = listItemIndex, Value = attributeValue });
                                    }
                                    //if (attributeName == "SPRE") this.Dispatcher.Invoke(() =>
                                    //{
                                    //    workbook.Worksheets[4].Rows[pipeConstructorRowCount]["C"].Value = attributeValue;
                                    //});
                                    //else if (attributeName == "TYPE") this.Dispatcher.Invoke(() =>
                                    //{
                                    //    workbook.Worksheets[4].Rows[pipeConstructorRowCount]["A"].Value = attributeValue;
                                    //});
                                    //else if (attributeName == "NAME") this.Dispatcher.Invoke(() =>
                                    //{
                                    //    workbook.Worksheets[4].Rows[pipeConstructorRowCount]["D"].Value = attributeValue;
                                    //});
                                    //else if (attributeName == "POS") this.Dispatcher.Invoke(() =>
                                    //{
                                    //    workbook.Worksheets[4].Rows[pipeConstructorRowCount]["E"].Value = attributeValue;
                                    //});
                                    //else if (attributeName == "ORI") this.Dispatcher.Invoke(() =>
                                    //{
                                    //    workbook.Worksheets[4].Rows[pipeConstructorRowCount]["F"].Value = attributeValue;
                                    //});
                                    //else if (attributeName == "LSTU") this.Dispatcher.Invoke(() =>
                                    //{
                                    //    workbook.Worksheets[4].Rows[pipeConstructorRowCount]["G"].Value = attributeValue;
                                    //});
                                    //else if (attributeName == "ARRI") this.Dispatcher.Invoke(() =>
                                    //{
                                    //    workbook.Worksheets[4].Rows[pipeConstructorRowCount]["H"].Value = attributeValue;
                                    //});
                                    //else if (attributeName == "LEAV") this.Dispatcher.Invoke(() =>
                                    //{
                                    //    workbook.Worksheets[4].Rows[pipeConstructorRowCount]["I"].Value = attributeValue;
                                    //});

                                }
                            }

                        }

                    }

                });
            }
            return dataStorage;
        }
        public void WriteDataInSpreadsheet(ListOfSpreadsheetElements dataStorage)
        {

            //this.Dispatcher.BeginInvoke(new Action(() =>
            //await Task.Run(()=>
            //{
            spreadsheet.BeginUpdate();
            try
            {
                foreach (SpreadsheetElement element in dataStorage.SiteElements)
                {
                    //this.Dispatcher.BeginInvoke(new Action(()=>workbook.Worksheets[0].Rows[element.Row][element.Column].Value = element.Value));
                    workbook.Worksheets[0].Rows[element.Row][element.Column].Value = element.Value;
                }
                foreach (SpreadsheetElement element in dataStorage.ZoneElements)
                {
                    //this.Dispatcher.BeginInvoke(new Action(() => workbook.Worksheets[1].Rows[element.Row][element.Column].Value = element.Value));
                    workbook.Worksheets[1].Rows[element.Row][element.Column].Value = element.Value;
                }
                foreach (SpreadsheetElement element in dataStorage.PipeElements)
                {
                    //this.Dispatcher.BeginInvoke(new Action(() => workbook.Worksheets[2].Rows[element.Row][element.Column].Value = element.Value));
                    workbook.Worksheets[2].Rows[element.Row][element.Column].Value = element.Value;
                }
                foreach (SpreadsheetElement element in dataStorage.BranchElements)
                {
                    //this.Dispatcher.Invoke(new Action(() => workbook.Worksheets[3].Rows[element.Row][element.Column].Value = element.Value));
                    workbook.Worksheets[3].Rows[element.Row][element.Column].Value = element.Value;
                }
                foreach (SpreadsheetElement element in dataStorage.PartElements)
                {
                    //this.Dispatcher.Invoke(new Action(() => workbook.Worksheets[3].Rows[element.Row][element.Column].Value = element.Value));
                    workbook.Worksheets[4].Rows[element.Row][element.Column].Value = element.Value;
                }
            }
            finally
            {
                spreadsheet.EndUpdate();
            }
            //}
            //));
        }
    }
}