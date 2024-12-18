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
using PlantConstructor.WPF.Generate3DCodeScreen;


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
        List<string> structureHeaderAttributes;
        List<string> subStructureHeaderAttributes;
        List<string> structurePartHeaderAttributes;
        List<string> equipmentHeaderAttributes;
        List<string> subEquipmentHeaderAttributes;
        List<string> equipmentPartHeaderAttributes;

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
                workbook.Worksheets.Add().Name = "Structure";
                workbook.Worksheets.Add().Name = "SubStructure";
                workbook.Worksheets.Add().Name = "StructurePart";
                workbook.Worksheets.Add().Name = "Equipment";
                workbook.Worksheets.Add().Name = "SubEquipment";
                workbook.Worksheets.Add().Name = "EquipmentPart";
                workbook.Worksheets.Add().Name = "HangerElements";

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
            structureHeaderAttributes = new List<string>();
            subStructureHeaderAttributes = new List<string>();
            structurePartHeaderAttributes = new List<string>();
            equipmentHeaderAttributes = new List<string>();
            subEquipmentHeaderAttributes = new List<string>();
            equipmentPartHeaderAttributes = new List<string>();

            LoadTypeValues("Site", workbook.Worksheets[0]);
            LoadTypeValues("Zone", workbook.Worksheets[1]);
            LoadTypeValues("Pipe", workbook.Worksheets[2]);
            LoadTypeValues("Branch", workbook.Worksheets[3]);
            LoadTypeValues("PipePart", workbook.Worksheets[4]);
            LoadTypeValues("Structure", workbook.Worksheets[5]);
            LoadTypeValues("SubStructure", workbook.Worksheets[6]);
            LoadTypeValues("StructurePart", workbook.Worksheets[7]);
            LoadTypeValues("Equipment", workbook.Worksheets[8]);
            LoadTypeValues("SubEquipment", workbook.Worksheets[9]);
            LoadTypeValues("EquipmentPart", workbook.Worksheets[10]);

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

                    //List<AttributeValue> attValueList = null;
                    //using (PlantConstructorDbContext context = _contextFactory.CreateDbContext())
                    //{
                    //    attValueList = context.Set<AttributeValue>().FromSqlRaw($"SELECT * FROM AttributeValues WHERE ProjectAttributeId={att.Id}").ToList();
                    //}

                    //if (attValueList!=null)
                    //{
                    //    foreach (AttributeValue attValue in attValueList)
                    //    {
                    //        sheet.Rows[attValue.Rowindex][columnIndex].Value = attValue.Value;
                    //    }
                    //}

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
                case "Structure":
                    structureHeaderAttributes.Add(value);
                    break;
                case "SubStructure":
                    subStructureHeaderAttributes.Add(value);
                    break;
                case "StructurePart":
                    structurePartHeaderAttributes.Add(value);
                    break;
                case "Equipment":
                    equipmentHeaderAttributes.Add(value);
                    break;
                case "SubEquipment":
                    subEquipmentHeaderAttributes.Add(value);
                    break;
                case "EquipmentPart":
                    equipmentPartHeaderAttributes.Add(value);
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


        //private async void SaveToDB_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        //{
        //    Mouse.OverrideCursor = Cursors.Wait;

        //    await SaveTypeValues("Site", workbook.Worksheets[0]);
        //    await SaveTypeValues("Zone", workbook.Worksheets[1]);
        //    await SaveTypeValues("Pipe", workbook.Worksheets[2]);
        //    await SaveTypeValues("Branch", workbook.Worksheets[3]);
        //    await SaveTypeValues("PipePart", workbook.Worksheets[4]);
        //    await SaveTypeValues("Structure", workbook.Worksheets[5]);
        //    await SaveTypeValues("SubStructure", workbook.Worksheets[6]);
        //    await SaveTypeValues("StructurePart", workbook.Worksheets[7]);
        //    await SaveTypeValues("Equipment", workbook.Worksheets[8]);
        //    await SaveTypeValues("SubEquipment", workbook.Worksheets[9]);
        //    await SaveTypeValues("EquipmentPart", workbook.Worksheets[10]);

        //    LogText.Text = "Data saved to DB";
        //    Mouse.OverrideCursor = null;
        //}


        //private async Task SaveTypeValues(string _type, Worksheet sheet)
        //{
        //    CellRange usedRange = sheet.GetUsedRange();

        //    List<ProjectAttribute> typeProjAtt = allProjectAttributes.Where(x => x.AttributeG.Type == _type).Select(x => x).ToList();
        //    int columnIndex = 0;

        //    while (sheet.Cells[0, columnIndex].Value.ToString() != "")
        //    {
        //        int temp_attValueId= typeProjAtt.Where(x => x.AttributeG.Name == sheet.Cells[0, columnIndex].Value.ToString()).Select(x => x.Id).FirstOrDefault();
        //        using (PlantConstructorDbContext context = _contextFactory.CreateDbContext())
        //        {
        //            var toDelete = context.Set<AttributeValue>().
        //                FromSqlRaw($"SELECT * FROM AttributeValues WHERE ProjectAttributeId={temp_attValueId}").ToList();
        //            await context.BulkDeleteAsync<AttributeValue>(toDelete);
        //        }
                
        //        int rowIndex = 0;
        //        List<AttributeValue> toInsert = new List<AttributeValue>();
        //        while (rowIndex<usedRange.RowCount)
        //        {
        //            toInsert.Add(new AttributeValue {
        //                ProjectAttributeID=temp_attValueId, 
        //                Rowindex=rowIndex, 
        //                Value= sheet.Cells[rowIndex, columnIndex].Value.ToString() });
        //            rowIndex++;
        //        }
        //        using (PlantConstructorDbContext context = _contextFactory.CreateDbContext())
        //        {
        //            await context.BulkInsertAsync<AttributeValue>(toInsert);
        //        }

        //        columnIndex++;
        //    }
        //}


        private void CreateCode_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            List<string> attributesToPass = new List<string>();
            var activeSheetName = workbook.Worksheets.ActiveWorksheet.Name;
            switch (activeSheetName)
            {
                case "Site":
                    attributesToPass = siteHeaderAttributes;
                    break;
                case "Zone":
                    attributesToPass = zoneHeaderAttributes;
                    break;
                case "Pipe":
                    attributesToPass = pipeHeaderAttributes;
                    break;
                case "Branch":
                    attributesToPass = branchHeaderAttributes;
                    break;
                case "PipePart":
                    attributesToPass = pipePartHeaderAttributes;
                    break;
                case "Structure":
                    attributesToPass = structureHeaderAttributes;
                    break;
                case "SubStructure":
                    attributesToPass = subStructureHeaderAttributes;
                    break;
                case "StructurePart":
                    attributesToPass = structurePartHeaderAttributes;
                    break;
                case "Equipment":
                    attributesToPass = equipmentHeaderAttributes;
                    break;
                case "SubEquipment":
                    attributesToPass = subEquipmentHeaderAttributes;
                    break;
                case "EquipmentPart":
                    attributesToPass = equipmentPartHeaderAttributes;
                    break;
            }

            Generate3DCodeWindow objGenerate3DCodeWindow = new Generate3DCodeWindow();
            objGenerate3DCodeWindow.DataContext = new Generate3DCodeViewModel(workbook.Worksheets.ActiveWorksheet, attributesToPass);
            objGenerate3DCodeWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            objGenerate3DCodeWindow.ShowDialog();

        }


        private async void ImportData_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "Supported files (*.att, *.datal)|*.att; *.datal";
            theDialog.InitialDirectory = @"C:\";
            if (theDialog.ShowDialog() == true)
            {
                string filename = theDialog.FileName;
                string filetype = System.IO.Path.GetExtension(theDialog.FileName);
                Mouse.OverrideCursor = Cursors.Wait;
                LogText.Text = "Import started... ";
                string[] allFileLines = await ReadDataFromFile(filename);
                switch (filetype)
                {
                    case ".att":
                        WriteDataInSpreadsheet(await InterpretDataAtt(
                            new Progress<string>(status => LogText.Text = status), allFileLines));
                        break;
                    case ".datal":
                        WriteDataInSpreadsheet(await InterpretDataDatal(
                            new Progress<string>(status => LogText.Text = status), allFileLines));
                        break;
                }
               
                Mouse.OverrideCursor = null;
                LogText.Text = "Import finished... ";
            }
        }

        private void ExportHangerData_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            //first check if all attributes are present
            int attaNameItemIndex = pipePartHeaderAttributes.FindIndex(s => s == "NAME");
            int attaOwnerItemIndex = pipePartHeaderAttributes.FindIndex(s => s == "OWNER");
            int attaTypeItemIndex = pipePartHeaderAttributes.FindIndex(s => s == "TYPE");
            int attaStexItemIndex = pipePartHeaderAttributes.FindIndex(s => s == "STEX");
            int attaDtxrItemIndex = pipePartHeaderAttributes.FindIndex(s => s == "DTXR");
            int attaIspeItemIndex = pipePartHeaderAttributes.FindIndex(s => s == "ISPE");

            int branchNameItemIndex = branchHeaderAttributes.FindIndex(s => s == "NAME");
            int branchOwnerItemIndex = branchHeaderAttributes.FindIndex(s => s == "OWNER");
            int branchTempItemIndex = branchHeaderAttributes.FindIndex(s => s == "TEMP");
            //int branchHborItemIndex = branchHeaderAttributes.FindIndex(s => s == "HBOR");

            int pipeNameItemIndex = pipeHeaderAttributes.FindIndex(s => s == "NAME");
            int pipeGaspecItemIndex = pipeHeaderAttributes.FindIndex(s => s == ":GASPEC");

            if (attaNameItemIndex<0 || attaOwnerItemIndex<0 || attaStexItemIndex<0 || branchNameItemIndex<0 || branchOwnerItemIndex<0 || branchTempItemIndex<0 || attaIspeItemIndex<0  || pipeGaspecItemIndex<0 || pipeNameItemIndex<0 || attaDtxrItemIndex<0)
            {
                MessageBox.Show("An attribute is missing. Check if NAME, OWNER, TYPE, ISPE, DTXR and STEX are available for PipePart. NAME and :GASPEC are available for Pipe. NAME, OWNER, TEMP and ISPE are available for Branch.");
            }
            else
            {
                Mouse.OverrideCursor = Cursors.Wait;
                List<HangerElements> listHangerElements = new List<HangerElements> { };
                
                int pipeRowCount;
                for (pipeRowCount = 1; workbook.Worksheets[2].Cells[pipeRowCount, 0].Value.ToString() != ""; pipeRowCount++) { }
                pipeRowCount--;

                int branchRowCount;
                for (branchRowCount = 1; workbook.Worksheets[3].Cells[branchRowCount, 0].Value.ToString() != ""; branchRowCount++) { }
                branchRowCount--;

                int pipePartRowCount;
                for (pipePartRowCount = 1; workbook.Worksheets[4].Cells[pipePartRowCount, 0].Value.ToString() != ""; pipePartRowCount++) { }
                pipePartRowCount--;

                int partCounter;
                int branchCounter;
                int pipeCounter;

                int hangerCounter = 0; 
                workbook.Worksheets[11].Cells[hangerCounter, 0].Value = "attaName / Object Code";
                workbook.Worksheets[11].Cells[hangerCounter, 1].Value = "attaStex / Support Name (HANG)";             
                workbook.Worksheets[11].Cells[hangerCounter, 2].Value = "branchName";
                workbook.Worksheets[11].Cells[hangerCounter, 3].Value = "branchTemp / Temp (3D)";
                workbook.Worksheets[11].Cells[hangerCounter, 4].Value = "attaIspe / Insulation (HANG)";
                workbook.Worksheets[11].Cells[hangerCounter, 5].Value = "attaDtxrDN / DN (HANG)";
                workbook.Worksheets[11].Cells[hangerCounter, 6].Value = "pipeName / Owner (3D)";
                workbook.Worksheets[11].Cells[hangerCounter, 7].Value = "pipeGaspec / Gaspec (3D)";
                workbook.Worksheets[11].Cells[hangerCounter, 8].Value = "attaDtxr / HangType (HANG)";
                hangerCounter++;


                string attaName = "";
                string attaOwner = "";
                string attaStex = "";
                string attaDtxr = "";
                string branchName = "";
                string branchOwner = "";
                string branchTemp = "";
                string attaIspe = "";
                //string branchHbor = "";
                string pipeName = "";
                string pipeGaspec = "";
                string attaDtxrDN = "";

                for (partCounter = 1; partCounter <= pipePartRowCount; partCounter++)
                {
                    if (workbook.Worksheets[4].Cells[partCounter, attaTypeItemIndex].Value.ToString() == "ATTA" && workbook.Worksheets[4].Cells[partCounter, attaNameItemIndex].Value.ToString().Contains("BQ"))
                    {
                        attaName = workbook.Worksheets[4].Cells[partCounter, attaNameItemIndex].Value.ToString();
                        if (attaName != "") attaName=attaName.Remove(0, 1);
                        attaOwner = workbook.Worksheets[4].Cells[partCounter, attaOwnerItemIndex].Value.ToString();
                        attaStex = workbook.Worksheets[4].Cells[partCounter, attaStexItemIndex].Value.ToString();
                        attaDtxr = workbook.Worksheets[4].Cells[partCounter, attaDtxrItemIndex].Value.ToString();
                        attaIspe= workbook.Worksheets[4].Cells[partCounter, attaIspeItemIndex].Value.ToString();
                        if (attaIspe != "") attaIspe = attaIspe.Remove(0, 1);
                        if (attaIspe == "0/0") attaIspe = "";
                        int dnIndex = attaDtxr.IndexOf("DN");
                        if (dnIndex >= 0)
                        {
                            attaDtxrDN = new string (attaDtxr.Substring(dnIndex+2, (attaDtxr.Length)-(dnIndex + 2)).Trim().Where(c => char.IsDigit(c)).ToArray());

                            //string[] array = attaDtxr.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                            //attaDtxr = array[array.Length - 1];
                        }
                        else
                            attaDtxrDN = "";



                        branchName = "";
                        branchOwner = "";
                        branchTemp = "";
                        //branchIspe = "";
                        //branchHbor = "";
                        for (branchCounter = 1; branchCounter <= branchRowCount; branchCounter++)
                        {
                            if (workbook.Worksheets[3].Cells[branchCounter, branchNameItemIndex].Value.ToString() == attaOwner)
                            {
                                branchName = workbook.Worksheets[3].Cells[branchCounter, branchNameItemIndex].Value.ToString();
                                branchOwner = workbook.Worksheets[3].Cells[branchCounter, branchOwnerItemIndex].Value.ToString();
                                branchTemp = workbook.Worksheets[3].Cells[branchCounter, branchTempItemIndex].Value.ToString().Replace("degC","");
                                if (branchTemp == "-100000") branchTemp = "";
                                //branchIspe = workbook.Worksheets[3].Cells[branchCounter, branchIspeItemIndex].Value.ToString();
                                //if (branchIspe != "") branchIspe=branchIspe.Remove(0, 1);
                                //if (branchIspe == "0/0") branchIspe = "";
                                //branchHbor = workbook.Worksheets[3].Cells[branchCounter, branchHborItemIndex].Value.ToString().Replace("mm","");
                            }
                        }
                        pipeName = "";
                        pipeGaspec = "";
                        if (branchOwner != "")
                        {
                            for (pipeCounter = 1; pipeCounter <= pipeRowCount; pipeCounter++)
                            {
                                if (workbook.Worksheets[2].Cells[pipeCounter, pipeNameItemIndex].Value.ToString() == branchOwner)
                                {
                                    pipeName = workbook.Worksheets[2].Cells[pipeCounter, pipeNameItemIndex].Value.ToString();
                                    if (pipeName != "") pipeName=pipeName.Remove(0, 1);
                                    pipeGaspec = workbook.Worksheets[2].Cells[pipeCounter, pipeGaspecItemIndex].Value.ToString();
                                }
                            }
                        }
                        listHangerElements.Add(new HangerElements{ AttaMyName = attaName, AttaMyStex=attaStex, BranchMyName=branchName, BranchMyTemp=branchTemp, BranchMyIspe=attaIspe, BranchMyHbor=attaDtxrDN, PipeMyName=pipeName, PipeMyGaspec=pipeGaspec });
                        workbook.Worksheets[11].Cells[hangerCounter, 0].Value=attaName;
                        workbook.Worksheets[11].Cells[hangerCounter, 1].Value = attaStex;
                        workbook.Worksheets[11].Cells[hangerCounter, 2].Value = branchName;
                        workbook.Worksheets[11].Cells[hangerCounter, 3].Value = branchTemp;
                        workbook.Worksheets[11].Cells[hangerCounter, 4].Value = attaIspe;
                        workbook.Worksheets[11].Cells[hangerCounter, 5].Value = attaDtxrDN;
                        workbook.Worksheets[11].Cells[hangerCounter, 6].Value = pipeName;
                        workbook.Worksheets[11].Cells[hangerCounter, 7].Value = pipeGaspec;
                        workbook.Worksheets[11].Cells[hangerCounter, 8].Value = attaDtxr;
                        hangerCounter++;
                    }
                }
                
            }
            Mouse.OverrideCursor = null;
        }


            private async Task<string[]> ReadDataFromFile(string filename)
        {
            string[] filelines = await File.ReadAllLinesAsync(filename);
            return filelines;
        }

        private async Task<ListOfSpreadsheetElements> InterpretDataAtt(IProgress<string> progress, string[] filelines)
        {
            string currentType = "";
            string currentCategory = "";

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

            int structureRowCount;
            for (structureRowCount = 1; workbook.Worksheets[5].Cells[structureRowCount, 0].Value.ToString() != ""; structureRowCount++) { }
            structureRowCount--;

            int subStructureRowCount;
            for (subStructureRowCount = 1; workbook.Worksheets[6].Cells[subStructureRowCount, 0].Value.ToString() != ""; subStructureRowCount++) { }
            subStructureRowCount--;

            int structurePartRowCount;
            for (structurePartRowCount = 1; workbook.Worksheets[7].Cells[structurePartRowCount, 0].Value.ToString() != ""; structurePartRowCount++) { }
            structurePartRowCount--;

            int equipmentRowCount;
            for (equipmentRowCount = 1; workbook.Worksheets[8].Cells[equipmentRowCount, 0].Value.ToString() != ""; equipmentRowCount++) { }
            equipmentRowCount--;

            int subEquipmentRowCount;
            for (subEquipmentRowCount = 1; workbook.Worksheets[9].Cells[subEquipmentRowCount, 0].Value.ToString() != ""; subEquipmentRowCount++) { }
            subEquipmentRowCount--;

            int equipmentPartRowCount;
            for (equipmentPartRowCount = 1; workbook.Worksheets[10].Cells[equipmentPartRowCount, 0].Value.ToString() != ""; equipmentPartRowCount++) { }
            equipmentPartRowCount--;

            ListOfSpreadsheetElements dataStorage = new ListOfSpreadsheetElements { 
                SiteElements = new List<SpreadsheetElement> { }, 
                BranchElements = new List<SpreadsheetElement> { }, 
                PipeElements = new List<SpreadsheetElement> { }, 
                ZoneElements = new List<SpreadsheetElement> { }, 
                PipePartElements = new List<SpreadsheetElement> { }, 
                StructureElements=new List<SpreadsheetElement> { },
                SubStructureElements = new List<SpreadsheetElement> { },
                StructurePartElements = new List<SpreadsheetElement> { },
                EquipmentElements = new List<SpreadsheetElement> { },
                SubEquipmentElements = new List<SpreadsheetElement> { },
                EquipmentPartElements = new List<SpreadsheetElement> { },
            };

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
                    if (importCodeLine != null && importCodeLine.Length >= 2 && importCodeLine[0] != "\t" && importCodeLine[0] != "END" && importCodeLine[0] != "AVEVA_Attributes_File" && importCodeLine[1] != "Header")
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
                                else if (currentType == "PIPE") { pipeRowCount++; currentCategory = "PIPE"; }
                                else if (currentType == "BRAN") { branchRowCount++; currentCategory = "PIPE"; }
                                else if (currentCategory == "PIPE" && (currentType == "VALV" || currentType == "GASK" || currentType == "PCOM"
                                || currentType == "FLAN" || currentType == "ELBO" || currentType == "ATTA"
                                || currentType == "OLET" || currentType == "FBLI" || currentType == "REDU"
                                || currentType == "TEE" || currentType == "CAP" || currentType == "INST" 
                                || currentType == "FILT" || currentType == "BEND" || currentType == "TUBE" || currentType == "FTUB")) pipePartRowCount++;
                                else if (currentType == "STRU") { structureRowCount++; currentCategory = "STRU"; }
                                else if (currentType == "FRMW" || currentType == "SUBS") subStructureRowCount++;
                                else if (currentCategory == "STRU" && (currentType == "SCTN" || currentType == "SNOD" || currentType == "SJOI"
                                || currentType == "SBFR" || currentType == "PANE" || currentType == "PLOO"
                                || currentType == "PAVE" || currentType == "BOX" || currentType == "CYLI"
                                || currentType == "RTOR" || currentType == "FLOOR" || currentType == "STWALL"
                                || currentType == "NBOX" || currentType == "CMPF" || currentType == "FITT"
                                || currentType == "PNOD" || currentType == "PJOI" || currentType == "NXTR"
                                || currentType == "LOOP" || currentType == "VERT" || currentType == "PFIT"
                                || currentType == "NPYR" || currentType == "CTOR" || currentType == "POHE"
                                || currentType == "POGO" || currentType == "POIN" || currentType == "SLCY"
                                || currentType == "PYRA" || currentType == "TMPL" || currentType == "NCYL"
                                || currentType == "NRTO" || currentType == "GENSEC" || currentType == "SPINE"
                                || currentType == "POINSP" || currentType == "CURVE" || currentType == "CONE")) structurePartRowCount++;
                                else if (currentType == "EQUI") { equipmentRowCount++; currentCategory = "EQUI"; }
                                else if (currentType == "SUBE") subEquipmentRowCount++;
                                else if (currentCategory == "EQUI" && (currentType == "DISH" || currentType == "CYLI" || currentType == "NOZZ"
                                || currentType == "BOX" || currentType == "CONE" || currentType == "CTOR"
                                || currentType == "NCYL" || currentType == "RTOR" || currentType == "PYRA"
                                || currentType == "NBOX" || currentType == "SNOU" || currentType == "NRTO"
                                || currentType == "DDSE" || currentType == "DDAT" || currentType == "TMPL"
                                || currentType == "DPSE" || currentType == "DPSP" || currentType == "VVALUE"
                                || currentType == "DPCA" || currentType == "GENPRI" || currentType == "ARCHIV"
                                || currentType == "VERT" || currentType == "LOOP" || currentType == "ATTRRL"
                                || currentType == "SCTN" || currentType == "EXTR" || currentType == "NPYR")) equipmentPartRowCount++;
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
                                    attributeValue = attributeValue.TrimStart();

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
                                    else if (currentCategory=="PIPE" && (currentType == "VALV" || currentType == "GASK" || currentType == "PCOM"
                                            || currentType == "FLAN" || currentType == "ELBO" || currentType == "ATTA"
                                            || currentType == "OLET" || currentType == "FBLI" || currentType == "REDU"
                                             || currentType == "TEE" || currentType == "CAP" || currentType == "INST" || currentType == "FILT"
                                             || currentType == "BEND" || currentType == "TUBE" || currentType == "FTUB"))
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
                                    else if (currentType == "STRU")
                                    {
                                        //find out whether the value exists in the dictionary 
                                        int listItemIndex = structureHeaderAttributes.FindIndex(s => s == attributeName);
                                        if (listItemIndex >= 0)
                                        {
                                            //write the attribute value to the appropriate row and column
                                            //this.Dispatcher.Invoke(() =>
                                            //{
                                            //    workbook.Worksheets[3].Rows[branchRowCount][listItemIndex].Value = attributeValue;
                                            //});
                                            dataStorage.StructureElements.Add(new SpreadsheetElement { Row = structureRowCount, Column = listItemIndex, Value = attributeValue });
                                        }

                                    }
                                    else if (currentType == "FRMW" || currentType == "SUBS")
                                    {
                                        //find out whether the value exists in the dictionary 
                                        int listItemIndex = subStructureHeaderAttributes.FindIndex(s => s == attributeName);
                                        if (listItemIndex >= 0)
                                        {
                                            //write the attribute value to the appropriate row and column
                                            //this.Dispatcher.Invoke(() =>
                                            //{
                                            //    workbook.Worksheets[3].Rows[branchRowCount][listItemIndex].Value = attributeValue;
                                            //});
                                            dataStorage.SubStructureElements.Add(new SpreadsheetElement { Row = subStructureRowCount, Column = listItemIndex, Value = attributeValue });
                                        }

                                    }
                                    else if (currentCategory == "STRU" && (currentType == "SCTN" || currentType == "SNOD" || currentType == "SJOI"
                                            || currentType == "SBFR" || currentType == "PANE" || currentType == "PLOO"
                                            || currentType == "PAVE" || currentType == "BOX" || currentType == "CYLI"
                                            || currentType == "RTOR" || currentType == "FLOOR" || currentType == "STWALL"
                                            || currentType == "NBOX" || currentType == "CMPF" || currentType == "FITT"
                                            || currentType == "PNOD" || currentType == "PJOI" || currentType == "NXTR"
                                            || currentType == "LOOP" || currentType == "VERT" || currentType == "PFIT"
                                            || currentType == "NPYR" || currentType == "CTOR" || currentType == "POHE"
                                            || currentType == "POGO" || currentType == "POIN" || currentType == "SLCY"
                                            || currentType == "PYRA" || currentType == "TMPL" || currentType == "NCYL"
                                            || currentType == "NRTO" || currentType == "GENSEC" || currentType == "SPINE"
                                            || currentType == "POINSP" || currentType == "CURVE" || currentType == "CONE"))
                                    {
                                        //find out whether the value exists in the dictionary 
                                        int listItemIndex = structurePartHeaderAttributes.FindIndex(s => s == attributeName);
                                        if (listItemIndex >= 0)
                                        {
                                            //write the attribute value to the appropriate row and column
                                            //this.Dispatcher.Invoke(() =>
                                            //{
                                            //    workbook.Worksheets[3].Rows[branchRowCount][listItemIndex].Value = attributeValue;
                                            //});
                                            dataStorage.StructurePartElements.Add(new SpreadsheetElement { Row = structurePartRowCount, Column = listItemIndex, Value = attributeValue });
                                        }

                                    }
                                    else if (currentType == "EQUI")
                                    {
                                        //find out whether the value exists in the dictionary 
                                        int listItemIndex = equipmentHeaderAttributes.FindIndex(s => s == attributeName);
                                        if (listItemIndex >= 0)
                                        {
                                            //write the attribute value to the appropriate row and column
                                            //this.Dispatcher.Invoke(() =>
                                            //{
                                            //    workbook.Worksheets[3].Rows[branchRowCount][listItemIndex].Value = attributeValue;
                                            //});
                                            dataStorage.EquipmentElements.Add(new SpreadsheetElement { Row = equipmentRowCount, Column = listItemIndex, Value = attributeValue });
                                        }

                                    }
                                    else if (currentType == "SUBE")
                                    {
                                        //find out whether the value exists in the dictionary 
                                        int listItemIndex = subEquipmentHeaderAttributes.FindIndex(s => s == attributeName);
                                        if (listItemIndex >= 0)
                                        {
                                            //write the attribute value to the appropriate row and column
                                            //this.Dispatcher.Invoke(() =>
                                            //{
                                            //    workbook.Worksheets[3].Rows[branchRowCount][listItemIndex].Value = attributeValue;
                                            //});
                                            dataStorage.SubEquipmentElements.Add(new SpreadsheetElement { Row = subEquipmentRowCount, Column = listItemIndex, Value = attributeValue });
                                        }

                                    }
                                    else if (currentCategory == "EQUI" && (currentType == "DISH" || currentType == "CYLI" || currentType == "NOZZ"
                                || currentType == "BOX" || currentType == "CONE" || currentType == "CTOR"
                                || currentType == "NCYL" || currentType == "RTOR" || currentType == "PYRA"
                                || currentType == "NBOX" || currentType == "SNOU" || currentType == "NRTO"
                                || currentType == "DDSE" || currentType == "DDAT" || currentType == "TMPL"
                                || currentType == "DPSE" || currentType == "DPSP" || currentType == "VVALUE"
                                || currentType == "DPCA" || currentType == "GENPRI" || currentType == "ARCHIV"
                                || currentType == "VERT" || currentType == "LOOP" || currentType == "ATTRRL"
                                || currentType == "SCTN" || currentType == "EXTR" || currentType == "NPYR"))
                                    {
                                        //find out whether the value exists in the dictionary 
                                        int listItemIndex = equipmentPartHeaderAttributes.FindIndex(s => s == attributeName);
                                        if (listItemIndex >= 0)
                                        {
                                            //write the attribute value to the appropriate row and column
                                            //this.Dispatcher.Invoke(() =>
                                            //{
                                            //    workbook.Worksheets[3].Rows[branchRowCount][listItemIndex].Value = attributeValue;
                                            //});
                                            dataStorage.EquipmentPartElements.Add(new SpreadsheetElement { Row = equipmentPartRowCount, Column = listItemIndex, Value = attributeValue });
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

        private async Task<ListOfSpreadsheetElements> InterpretDataDatal(IProgress<string> progress, string[] filelines)
        {
            string currentType = "";
            string currentCategory = "";

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

            int structureRowCount;
            for (structureRowCount = 1; workbook.Worksheets[5].Cells[structureRowCount, 0].Value.ToString() != ""; structureRowCount++) { }
            structureRowCount--;

            int subStructureRowCount;
            for (subStructureRowCount = 1; workbook.Worksheets[6].Cells[subStructureRowCount, 0].Value.ToString() != ""; subStructureRowCount++) { }
            subStructureRowCount--;

            int structurePartRowCount;
            for (structurePartRowCount = 1; workbook.Worksheets[7].Cells[structurePartRowCount, 0].Value.ToString() != ""; structurePartRowCount++) { }
            structurePartRowCount--;

            int equipmentRowCount;
            for (equipmentRowCount = 1; workbook.Worksheets[8].Cells[equipmentRowCount, 0].Value.ToString() != ""; equipmentRowCount++) { }
            equipmentRowCount--;

            int subEquipmentRowCount;
            for (subEquipmentRowCount = 1; workbook.Worksheets[9].Cells[subEquipmentRowCount, 0].Value.ToString() != ""; subEquipmentRowCount++) { }
            subEquipmentRowCount--;

            int equipmentPartRowCount;
            for (equipmentPartRowCount = 1; workbook.Worksheets[10].Cells[equipmentPartRowCount, 0].Value.ToString() != ""; equipmentPartRowCount++) { }
            equipmentPartRowCount--;

            ListOfSpreadsheetElements dataStorage = new ListOfSpreadsheetElements
            {
                SiteElements = new List<SpreadsheetElement> { },
                BranchElements = new List<SpreadsheetElement> { },
                PipeElements = new List<SpreadsheetElement> { },
                ZoneElements = new List<SpreadsheetElement> { },
                PipePartElements = new List<SpreadsheetElement> { },
                StructureElements = new List<SpreadsheetElement> { },
                SubStructureElements = new List<SpreadsheetElement> { },
                StructurePartElements = new List<SpreadsheetElement> { },
                EquipmentElements = new List<SpreadsheetElement> { },
                SubEquipmentElements = new List<SpreadsheetElement> { },
                EquipmentPartElements = new List<SpreadsheetElement> { },
            };

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
                        if (importCodeLine != null && importCodeLine.Length >= 2 && importCodeLine[0] != "\t" && importCodeLine[0] != "END" && importCodeLine[0] != "AVEVA_Attributes_File" && importCodeLine[1] != "Header")
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
                                else if (currentType == "PIPE") { pipeRowCount++; currentCategory = "PIPE"; }
                                else if (currentType == "BRAN") branchRowCount++;
                                else if (currentCategory == "PIPE" && (currentType == "VALV" || currentType == "GASK" || currentType == "PCOM"
                                || currentType == "FLAN" || currentType == "ELBO" || currentType == "ATTA"
                                || currentType == "OLET" || currentType == "FBLI" || currentType == "REDU"
                                || currentType == "TEE" || currentType == "CAP" || currentType == "INST" 
                                || currentType == "FILT" || currentType == "BEND" || currentType == "TUBE" || currentType == "FTUB")) pipePartRowCount++;
                                else if (currentType == "STRU") { structureRowCount++; currentCategory = "STRU"; }
                                else if (currentType == "FRMW" || currentType == "SUBS") subStructureRowCount++;
                                else if (currentCategory == "STRU" && (currentType == "SCTN" || currentType == "SNOD" || currentType == "SJOI"
                                || currentType == "SBFR" || currentType == "PANE" || currentType == "PLOO"
                                || currentType == "PAVE" || currentType == "BOX" || currentType == "CYLI"
                                || currentType == "RTOR" || currentType == "FLOOR" || currentType == "STWALL"
                                || currentType == "NBOX" || currentType == "CMPF" || currentType == "FITT"
                                || currentType == "PNOD" || currentType == "PJOI" || currentType == "NXTR"
                                || currentType == "LOOP" || currentType == "VERT" || currentType == "PFIT"
                                || currentType == "NPYR" || currentType == "CTOR" || currentType == "POHE"
                                || currentType == "POGO" || currentType == "POIN" || currentType == "SLCY"
                                || currentType == "PYRA" || currentType == "TMPL" || currentType == "NCYL"
                                || currentType == "NRTO" || currentType == "GENSEC" || currentType == "SPINE"
                                || currentType == "POINSP" || currentType == "CURVE" || currentType == "CONE")) structurePartRowCount++;
                                else if (currentType == "EQUI") { equipmentRowCount++; currentCategory = "EQUI"; }
                                else if (currentType == "SUBE") subEquipmentRowCount++;
                                else if (currentCategory == "EQUI" && (currentType == "DISH" || currentType == "CYLI" || currentType == "NOZZ"
                                || currentType == "BOX" || currentType == "CONE" || currentType == "CTOR"
                                || currentType == "NCYL" || currentType == "RTOR" || currentType == "PYRA"
                                || currentType == "NBOX" || currentType == "SNOU" || currentType == "NRTO"
                                || currentType == "DDSE" || currentType == "DDAT" || currentType == "TMPL"
                                || currentType == "DPSE" || currentType == "DPSP" || currentType == "VVALUE"
                                || currentType == "DPCA" || currentType == "GENPRI" || currentType == "ARCHIV"
                                || currentType == "VERT" || currentType == "LOOP" || currentType == "ATTRRL"
                                || currentType == "SCTN" || currentType == "EXTR" || currentType == "NPYR")) equipmentPartRowCount++;
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
                                    attributeValue = attributeValue.TrimStart();

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
                                    else if (currentCategory == "PIPE" && (currentType == "VALV" || currentType == "GASK" || currentType == "PCOM"
                                            || currentType == "FLAN" || currentType == "ELBO" || currentType == "ATTA"
                                            || currentType == "OLET" || currentType == "FBLI" || currentType == "REDU"
                                             || currentType == "TEE" || currentType == "CAP" || currentType == "INST" 
                                             || currentType == "FILT" || currentType == "BEND" || currentType == "TUBE" || currentType == "FTUB"))
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
                                    else if (currentType == "STRU")
                                    {
                                        //find out whether the value exists in the dictionary 
                                        int listItemIndex = structureHeaderAttributes.FindIndex(s => s == attributeName);
                                        if (listItemIndex >= 0)
                                        {
                                            //write the attribute value to the appropriate row and column
                                            //this.Dispatcher.Invoke(() =>
                                            //{
                                            //    workbook.Worksheets[3].Rows[branchRowCount][listItemIndex].Value = attributeValue;
                                            //});
                                            dataStorage.StructureElements.Add(new SpreadsheetElement { Row = structureRowCount, Column = listItemIndex, Value = attributeValue });
                                        }

                                    }
                                    else if (currentType == "FRMW" || currentType == "SUBS")
                                    {
                                        //find out whether the value exists in the dictionary 
                                        int listItemIndex = subStructureHeaderAttributes.FindIndex(s => s == attributeName);
                                        if (listItemIndex >= 0)
                                        {
                                            //write the attribute value to the appropriate row and column
                                            //this.Dispatcher.Invoke(() =>
                                            //{
                                            //    workbook.Worksheets[3].Rows[branchRowCount][listItemIndex].Value = attributeValue;
                                            //});
                                            dataStorage.SubStructureElements.Add(new SpreadsheetElement { Row = subStructureRowCount, Column = listItemIndex, Value = attributeValue });
                                        }

                                    }
                                    else if (currentCategory == "STRU" && (currentType == "SCTN" || currentType == "SNOD" || currentType == "SJOI"
                                            || currentType == "SBFR" || currentType == "PANE" || currentType == "PLOO"
                                            || currentType == "PAVE" || currentType == "BOX" || currentType == "CYLI"
                                            || currentType == "RTOR" || currentType == "FLOOR" || currentType == "STWALL"
                                            || currentType == "NBOX" || currentType == "CMPF" || currentType == "FITT"
                                            || currentType == "PNOD" || currentType == "PJOI" || currentType == "NXTR"
                                            || currentType == "LOOP" || currentType == "VERT" || currentType == "PFIT"
                                            || currentType == "NPYR" || currentType == "CTOR" || currentType == "POHE"
                                            || currentType == "POGO" || currentType == "POIN" || currentType == "SLCY"
                                            || currentType == "PYRA" || currentType == "TMPL" || currentType == "NCYL"
                                            || currentType == "NRTO" || currentType == "GENSEC" || currentType == "SPINE"
                                            || currentType == "POINSP" || currentType == "CURVE" || currentType == "CONE"))
                                    {
                                        //find out whether the value exists in the dictionary 
                                        int listItemIndex = structurePartHeaderAttributes.FindIndex(s => s == attributeName);
                                        if (listItemIndex >= 0)
                                        {
                                            //write the attribute value to the appropriate row and column
                                            //this.Dispatcher.Invoke(() =>
                                            //{
                                            //    workbook.Worksheets[3].Rows[branchRowCount][listItemIndex].Value = attributeValue;
                                            //});
                                            dataStorage.StructurePartElements.Add(new SpreadsheetElement { Row = structurePartRowCount, Column = listItemIndex, Value = attributeValue });
                                        }

                                    }
                                    else if (currentType == "EQUI")
                                    {
                                        //find out whether the value exists in the dictionary 
                                        int listItemIndex = equipmentHeaderAttributes.FindIndex(s => s == attributeName);
                                        if (listItemIndex >= 0)
                                        {
                                            //write the attribute value to the appropriate row and column
                                            //this.Dispatcher.Invoke(() =>
                                            //{
                                            //    workbook.Worksheets[3].Rows[branchRowCount][listItemIndex].Value = attributeValue;
                                            //});
                                            dataStorage.EquipmentElements.Add(new SpreadsheetElement { Row = equipmentRowCount, Column = listItemIndex, Value = attributeValue });
                                        }

                                    }
                                    else if (currentType == "SUBE")
                                    {
                                        //find out whether the value exists in the dictionary 
                                        int listItemIndex = subEquipmentHeaderAttributes.FindIndex(s => s == attributeName);
                                        if (listItemIndex >= 0)
                                        {
                                            //write the attribute value to the appropriate row and column
                                            //this.Dispatcher.Invoke(() =>
                                            //{
                                            //    workbook.Worksheets[3].Rows[branchRowCount][listItemIndex].Value = attributeValue;
                                            //});
                                            dataStorage.SubEquipmentElements.Add(new SpreadsheetElement { Row = subEquipmentRowCount, Column = listItemIndex, Value = attributeValue });
                                        }

                                    }
                                    else if (currentCategory == "EQUI" && (currentType == "DISH" || currentType == "CYLI" || currentType == "NOZZ"
                                || currentType == "BOX" || currentType == "CONE" || currentType == "CTOR"
                                || currentType == "NCYL" || currentType == "RTOR" || currentType == "PYRA"
                                || currentType == "NBOX" || currentType == "SNOU" || currentType == "NRTO"
                                || currentType == "DDSE" || currentType == "DDAT" || currentType == "TMPL"
                                || currentType == "DPSE" || currentType == "DPSP" || currentType == "VVALUE"
                                || currentType == "DPCA" || currentType == "GENPRI" || currentType == "ARCHIV"
                                || currentType == "VERT" || currentType == "LOOP" || currentType == "ATTRRL"
                                || currentType == "SCTN" || currentType == "EXTR" || currentType == "NPYR"))
                                    {
                                        //find out whether the value exists in the dictionary 
                                        int listItemIndex = equipmentPartHeaderAttributes.FindIndex(s => s == attributeName);
                                        if (listItemIndex >= 0)
                                        {
                                            //write the attribute value to the appropriate row and column
                                            //this.Dispatcher.Invoke(() =>
                                            //{
                                            //    workbook.Worksheets[3].Rows[branchRowCount][listItemIndex].Value = attributeValue;
                                            //});
                                            dataStorage.EquipmentPartElements.Add(new SpreadsheetElement { Row = equipmentPartRowCount, Column = listItemIndex, Value = attributeValue });
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
                foreach (SpreadsheetElement element in dataStorage.StructureElements)
                {
                    //this.Dispatcher.Invoke(new Action(() => workbook.Worksheets[3].Rows[element.Row][element.Column].Value = element.Value));
                    workbook.Worksheets[5].Rows[element.Row][element.Column].Value = element.Value;
                }
                foreach (SpreadsheetElement element in dataStorage.SubStructureElements)
                {
                    //this.Dispatcher.Invoke(new Action(() => workbook.Worksheets[3].Rows[element.Row][element.Column].Value = element.Value));
                    workbook.Worksheets[6].Rows[element.Row][element.Column].Value = element.Value;
                }
                foreach (SpreadsheetElement element in dataStorage.StructurePartElements)
                {
                    //this.Dispatcher.Invoke(new Action(() => workbook.Worksheets[3].Rows[element.Row][element.Column].Value = element.Value));
                    workbook.Worksheets[7].Rows[element.Row][element.Column].Value = element.Value;
                }
                foreach (SpreadsheetElement element in dataStorage.EquipmentElements)
                {
                    //this.Dispatcher.Invoke(new Action(() => workbook.Worksheets[3].Rows[element.Row][element.Column].Value = element.Value));
                    workbook.Worksheets[8].Rows[element.Row][element.Column].Value = element.Value;
                }
                foreach (SpreadsheetElement element in dataStorage.SubEquipmentElements)
                {
                    //this.Dispatcher.Invoke(new Action(() => workbook.Worksheets[3].Rows[element.Row][element.Column].Value = element.Value));
                    workbook.Worksheets[9].Rows[element.Row][element.Column].Value = element.Value;
                }
                foreach (SpreadsheetElement element in dataStorage.EquipmentPartElements)
                {
                    //this.Dispatcher.Invoke(new Action(() => workbook.Worksheets[3].Rows[element.Row][element.Column].Value = element.Value));
                    workbook.Worksheets[10].Rows[element.Row][element.Column].Value = element.Value;
                }

            }
            finally
            {
                spreadsheet.EndUpdate();
            }
            //}
            //));
        }

        private void RemoveHeaderProtection(Worksheet sheet)
        {
            if (sheet.IsProtected)
            {
                sheet.Unprotect("48800609");
            }
        }

        private void RemoveProtectionBeforeSave(object sender, SpreadsheetBeforeExportEventArgs e)
        {
            RemoveHeaderProtection(workbook.Worksheets[0]);
            RemoveHeaderProtection(workbook.Worksheets[1]);
            RemoveHeaderProtection(workbook.Worksheets[2]);
            RemoveHeaderProtection(workbook.Worksheets[3]);
            RemoveHeaderProtection(workbook.Worksheets[4]);
        }

        private void AddProtectionAfterSave(object sender, EventArgs e)
        {
            SetHeaderProtection(workbook.Worksheets[0]);
            SetHeaderProtection(workbook.Worksheets[1]);
            SetHeaderProtection(workbook.Worksheets[2]);
            SetHeaderProtection(workbook.Worksheets[3]);
            SetHeaderProtection(workbook.Worksheets[4]);

        }
    }
}