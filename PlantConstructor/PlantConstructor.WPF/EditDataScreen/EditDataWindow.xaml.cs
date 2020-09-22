﻿using DevExpress.CodeParser;
using DevExpress.CodeParser.Diagnostics;
using DevExpress.DataProcessing.InMemoryDataProcessor.GraphGenerator;
using DevExpress.Spreadsheet;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Diagram.Native;
using DevExpress.Xpf.PivotGrid.Printing.TypedStyles;
using DevExpress.Xpf.Spreadsheet;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
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

        private PlantConstructorDbContextFactory _contextFactory;

        //IDataService<ProjectAttribute> projectAttributeService;
        //IDataService<AttributeG> attributeGService;
        //IDataService<AttributeValue> attributeValueService;

        IEnumerable<ProjectAttribute> allProjectAttributes;
        //IEnumerable<AttributeG> allAttributesG;
        //IEnumerable<AttributeValue> allAttributeValues;

        List<string> siteHeaderAttributes;
        List<string> zoneHeaderAttributes;
        List<string> pipeHeaderAttributes;
        List<string> branchHeaderAttributes;
        List<string> pipePartHeaderAttributes;

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

        public void InitializeWorkEnvironment(object parameter)
        {
            //projectAttributeService = new GenericDataService<ProjectAttribute>(new PlantConstructorDbContextFactory());
            //attributeGService = new GenericDataService<AttributeG>(new PlantConstructorDbContextFactory());
            //attributeValueService = new GenericDataService<AttributeValue>(new PlantConstructorDbContextFactory());

            _contextFactory = new PlantConstructorDbContextFactory();

            CreateWorkbook();
            
            LoadDataFromDatabase();
            
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
                    workbook.Protect("48800609", true, false);

                workbook.Worksheets[0].Name = "Site";
                workbook.Worksheets.Add().Name = "Zone";
                workbook.Worksheets.Add().Name = "Pipe";
                workbook.Worksheets.Add().Name = "Branch";
                workbook.Worksheets.Add().Name = "PipePart";

                //set Site as active worksheet
                workbook.Worksheets.ActiveWorksheet = workbook.Worksheets[0];
            }
            finally
            {
                spreadsheet.EndUpdate();
            }
        }

        private void LoadDataFromDatabase()
        {

            using (PlantConstructorDbContext context = _contextFactory.CreateDbContext())
            {
                allProjectAttributes = context.Set<ProjectAttribute>().
                    FromSqlRaw($"SELECT * FROM ProjectAttributes WHERE ProjectId={SelectedProject.Id}").
                    Include(x=>x.AttributeG).
                    ToList();
            }

            siteHeaderAttributes = new List<string>();
            zoneHeaderAttributes = new List<string>();
            pipeHeaderAttributes = new List<string>();
            branchHeaderAttributes = new List<string>();
            pipePartHeaderAttributes = new List<string>();

            LoadTypeValues("Site", workbook.Worksheets[0]);
            LoadTypeValues("Zone", workbook.Worksheets[1]);
            LoadTypeValues("Pipe", workbook.Worksheets[2]);
            LoadTypeValues("Branch", workbook.Worksheets[3]);
            LoadTypeValues("PipePart", workbook.Worksheets[4]);

        }

        private void LoadTypeValues(string _type, Worksheet sheet)
        {

            List<ProjectAttribute> typeProjAtt = allProjectAttributes.Where(x => x.AttributeG.Type == _type).Select(x=>x).ToList();
            int columnIndex = 0;

            spreadsheet.BeginUpdate();
            try
            {
                foreach (ProjectAttribute att in typeProjAtt)
                {

                    sheet.Rows[0][columnIndex].Value = att.AttributeG.Name;
                    sheet.Rows[0][columnIndex].FillColor = System.Drawing.Color.Tomato;
                    
                    AddHeaderValueToList(_type, att.AttributeG.Name);

                    List<AttributeValue> attValueList = null;
                    using (PlantConstructorDbContext context = _contextFactory.CreateDbContext())
                    {
                        attValueList = context.Set<AttributeValue>().FromSqlRaw($"SELECT * FROM AttributeValues WHERE ProjectAttributeId={att.Id}").ToList();
                    }

                    if (attValueList!=null)
                    {
                        foreach (AttributeValue attValue in attValueList)
                        {
                            sheet.Rows[attValue.Rowindex][columnIndex].Value = attValue.Value;
                        }
                    }

                    columnIndex++;
                }
                
                SetHeaderProtection(sheet);

            }
            finally
            {
                spreadsheet.EndUpdate();
            }

        }

        private void AddHeaderValueToList(string _type, string value)
        {
            switch(_type)
            {
                case "Site":
                    siteHeaderAttributes.Add(value);
                    break;
                case "Zone":
                    zoneHeaderAttributes.Add(value);
                    break;
                case "Pipe":
                    pipeHeaderAttributes.Add(value);
                    break;
                case "Branch":
                    branchHeaderAttributes.Add(value);
                    break;
                case "PipePart":
                    pipePartHeaderAttributes.Add(value);
                    break;

            }
        }

        private void SetHeaderProtection(Worksheet sheet)
        {
                sheet.Rows[0].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                sheet.Rows[0].Font.FontStyle = SpreadsheetFontStyle.Bold;

                //Protect the header row from editing
                if (!sheet.IsProtected)
                {
                    sheet.Range["$A:$XFD"].Protection.Locked = false; // Unlock the entire document  
                    sheet.Rows[0].Protection.Locked = true; // Lock the specified range  
                    sheet.Protect("48800609", WorksheetProtectionPermissions.Default
                        | WorksheetProtectionPermissions.DeleteRows
                        | WorksheetProtectionPermissions.FormatColumns
                        | WorksheetProtectionPermissions.FormatRows
                        | WorksheetProtectionPermissions.Sort
                        | WorksheetProtectionPermissions.AutoFilters
                        | WorksheetProtectionPermissions.PivotTables
                        | WorksheetProtectionPermissions.FormatCells
                        | WorksheetProtectionPermissions.InsertRows);
                }
            }


        private async void SaveToDB_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            await SaveTypeValues("Site", workbook.Worksheets[0]);
            await SaveTypeValues("Zone", workbook.Worksheets[1]);
            await SaveTypeValues("Pipe", workbook.Worksheets[2]);
            await SaveTypeValues("Branch", workbook.Worksheets[3]);
            await SaveTypeValues("PipePart", workbook.Worksheets[4]);

            LogText.Text = "Data saved to DB";
            Mouse.OverrideCursor = null;
        }


        private async Task SaveTypeValues(string _type, Worksheet sheet)
        {
            CellRange usedRange = sheet.GetUsedRange();

            List<ProjectAttribute> typeProjAtt = allProjectAttributes.Where(x => x.AttributeG.Type == _type).Select(x => x).ToList();
            int columnIndex = 0;

            while (sheet.Cells[0, columnIndex].Value.ToString() != "")
            {
                int temp_attValueId= typeProjAtt.Where(x => x.AttributeG.Name == sheet.Cells[0, columnIndex].Value.ToString()).Select(x => x.Id).FirstOrDefault();
                using (PlantConstructorDbContext context = _contextFactory.CreateDbContext())
                {
                    var toDelete = context.Set<AttributeValue>().
                        FromSqlRaw($"SELECT * FROM AttributeValues WHERE ProjectAttributeId={temp_attValueId}").ToList();
                    await context.BulkDeleteAsync<AttributeValue>(toDelete);
                }
                
                int rowIndex = 0;
                List<AttributeValue> toInsert = new List<AttributeValue>();
                while (rowIndex<usedRange.RowCount)
                {
                    toInsert.Add(new AttributeValue {
                        ProjectAttributeID=temp_attValueId, 
                        Rowindex=rowIndex, 
                        Value= sheet.Cells[rowIndex, columnIndex].Value.ToString() });
                    rowIndex++;
                }
                using (PlantConstructorDbContext context = _contextFactory.CreateDbContext())
                {
                    await context.BulkInsertAsync<AttributeValue>(toInsert);
                }

                columnIndex++;
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

            int pipePartRowCount;
            for (pipePartRowCount = 1; workbook.Worksheets[4].Cells[pipePartRowCount, 0].Value.ToString() != ""; pipePartRowCount++) { }
            pipePartRowCount--;

            ListOfSpreadsheetElements dataStorage = new ListOfSpreadsheetElements { SiteElements = new List<SpreadsheetElement> { }, BranchElements = new List<SpreadsheetElement> { }, PipeElements = new List<SpreadsheetElement> { }, ZoneElements = new List<SpreadsheetElement> { }, PipePartElements = new List<SpreadsheetElement> { } };

            int a = 0;
            try
            {
                //read line by line
                for (a = 0; a < filelines.Length; a++)
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
                            if (currentType == "SITE") siteRowCount++;
                                else if (currentType == "ZONE") zoneRowCount++;
                                else if (currentType == "PIPE") pipeRowCount++;
                                else if (currentType == "BRAN") branchRowCount++;
                                else if (currentType == "VALV" || currentType == "GASK" || currentType == "PCOM"
                                || currentType == "FLAN" || currentType == "ELBO" || currentType == "ATTA"
                                || currentType == "OLET" || currentType == "FBLI" || currentType == "REDU"
                                || currentType == "TEE" || currentType == "CAP" || currentType == "INST") pipePartRowCount++;



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
                                    else if (currentType == "VALV" || currentType == "GASK" || currentType == "PCOM"
                                            || currentType == "FLAN" || currentType == "ELBO" || currentType == "ATTA"
                                            || currentType == "OLET" || currentType == "FBLI" || currentType == "REDU"
                                             || currentType == "TEE" || currentType == "CAP" || currentType == "INST")
                                    {
                                        int listItemIndex = pipePartHeaderAttributes.FindIndex(s => s == attributeName);
                                        if (listItemIndex >= 0)
                                        {
                                        //write the attribute value to the appropriate row and column
                                        //this.Dispatcher.Invoke(() =>
                                        //{
                                        //    workbook.Worksheets[3].Rows[branchRowCount][listItemIndex].Value = attributeValue;
                                        //});
                                        dataStorage.PipePartElements.Add(new SpreadsheetElement { Row = pipePartRowCount, Column = listItemIndex, Value = attributeValue });
                                        }


                                    }
                                }

                            }

                        }

                    });
                }
            }
            catch
            {
                MessageBox.Show("Error in input file, line: " + a.ToString(), "Input File Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
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
                foreach (SpreadsheetElement element in dataStorage.PipePartElements)
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