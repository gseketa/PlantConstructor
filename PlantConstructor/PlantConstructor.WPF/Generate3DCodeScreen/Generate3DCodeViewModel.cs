﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DevExpress.Spreadsheet;
using Microsoft.Win32;
using PlantConstructor.WPF.Helper;

namespace PlantConstructor.WPF.Generate3DCodeScreen
{
    class Generate3DCodeViewModel : INotifyPropertyChanged
    {
        private List<string> exportTypeComboBox;

        private string logText;

        public string LogText
        {
            get { return logText; }
            set { 
                logText = value;
                OnPropertyRaised("LogText");
            }
        }

        public List<string> ExportTypeComboBox
        {
            get { return exportTypeComboBox; }
            set
            {
                exportTypeComboBox = value;
                OnPropertyRaised("ExportTypeComboBox");
            }
        }

        private string selectedExportType;

        public string SelectedExportType
        {
            get { return selectedExportType; }
            set
            {
                selectedExportType = value;
                OnPropertyRaised("SelectedExportType");
            }
        }

        private List<ListBoxAttributes> allLeftAttributesForDisplay;

        public List<ListBoxAttributes> AllLeftAttributesForDisplay
        {
            get { return allLeftAttributesForDisplay; }
            set
            {
                allLeftAttributesForDisplay = value;
                OnPropertyRaised("AllLeftAttributesForDisplay");
            }
        }

        private ICommand allLeftAttributesButtonCommand;

        public ICommand AllLeftAttributesButtonCommand
        {
            get { return allLeftAttributesButtonCommand; }
            set { allLeftAttributesButtonCommand = value; }
        }

        private ICommand selectAllLeftAttributesButtonCommand;

        public ICommand SelectAllLeftAttributesButtonCommand
        {
            get { return selectAllLeftAttributesButtonCommand; }
            set { selectAllLeftAttributesButtonCommand = value; }
        }

        private List<ListBoxAttributes> selectedAttributeFromLeft;

        public ListBoxAttributes SelectedAttributeFromLeft
        {
            get { return null; }
            set
            {
                //var selectedItems = AllAvailableAttributesForDisplay.Where(x => x.IsSelected).Count();
                selectedAttributeFromLeft = AllLeftAttributesForDisplay.Where(x => x.IsSelected).ToList();
                OnPropertyRaised("SelectedAttributeFromLeft");
            }
        }

        private List<ListBoxAttributes> allCenterAttributesForDisplay;

        public List<ListBoxAttributes> AllCenterAttributesForDisplay
        {
            get { return allCenterAttributesForDisplay; }
            set
            {
                allCenterAttributesForDisplay = value;
                OnPropertyRaised("AllCenterAttributesForDisplay");
            }
        }

        private List<ListBoxAttributes> selectedAttributeFromCenter;

        public ListBoxAttributes SelectedAttributeFromCenter
        {
            get { return null; }
            set
            {
                //var selectedItems = AllProjectAttributesForDisplay.Where(x => x.IsSelected).Count();
                selectedAttributeFromCenter = AllCenterAttributesForDisplay.Where(x => x.IsSelected).ToList();
                OnPropertyRaised("SelectedAttributeFromCenter");
            }
        }

        private ICommand allCenterAttributesButtonCommand;

        public ICommand AllCenterAttributesButtonCommand
        {
            get { return allCenterAttributesButtonCommand; }
            set { allCenterAttributesButtonCommand = value; }
        }

        private ICommand allCenterToRightAttributesButtonCommand;

        public ICommand AllCenterToRightAttributesButtonCommand
        {
            get { return allCenterToRightAttributesButtonCommand; }
            set { allCenterToRightAttributesButtonCommand = value; }
        }

        private ICommand selectAllCenterAttributesButtonCommand;

        public ICommand SelectAllCenterAttributesButtonCommand
        {
            get { return selectAllCenterAttributesButtonCommand; }
            set { selectAllCenterAttributesButtonCommand = value; }
        }

        private List<ListBoxAttributes> allRightAttributesForDisplay;

        public List<ListBoxAttributes> AllRightAttributesForDisplay
        {
            get { return allRightAttributesForDisplay; }
            set
            {
                allRightAttributesForDisplay = value;
                OnPropertyRaised("AllRightAttributesForDisplay");
            }
        }

        private ICommand allRightAttributesButtonCommand;

        public ICommand AllRightAttributesButtonCommand
        {
            get { return allRightAttributesButtonCommand; }
            set { allRightAttributesButtonCommand = value; }
        }

        private ICommand selectAllRightAttributesButtonCommand;

        public ICommand SelectAllRightAttributesButtonCommand
        {
            get { return selectAllRightAttributesButtonCommand; }
            set { selectAllRightAttributesButtonCommand = value; }
        }

        private List<ListBoxAttributes> selectedAttributeFromRight;

        public ListBoxAttributes SelectedAttributeFromRight
        {
            get { return null; }
            set
            {
                //var selectedItems = AllAvailableAttributesForDisplay.Where(x => x.IsSelected).Count();
                selectedAttributeFromRight = AllRightAttributesForDisplay.Where(x => x.IsSelected).ToList();
                OnPropertyRaised("SelectedAttributeFromRight");
            }
        }

        private ICommand startExportButtonCommand;

        public ICommand StartExportButtonCommand
        {
            get { return startExportButtonCommand; }
            set { startExportButtonCommand = value; }
        }

        private Worksheet localSheet;

        public Generate3DCodeViewModel(Worksheet activeWorksheet, List<string> allAttributes)
        {
            LogText = "Log started";

            localSheet = activeWorksheet;
            
            AllLeftAttributesButtonCommand = new RelayCommand(MoveAttributeFromLeftToCenter);
            AllCenterAttributesButtonCommand = new RelayCommand(MoveAttributeFromCenterToLeft);
            AllCenterToRightAttributesButtonCommand = new RelayCommand(MoveAttributeFromCenterToRight);
            AllRightAttributesButtonCommand = new RelayCommand(MoveAttributeFromRightToCenter);

            SelectAllLeftAttributesButtonCommand = new RelayCommand(SelectAllLeftAttributes);
            SelectAllCenterAttributesButtonCommand = new RelayCommand(SelectAllCenterAttributes);
            SelectAllRightAttributesButtonCommand = new RelayCommand(SelectAllRightAttributes);

            StartExportButtonCommand = new RelayCommand(StartExport);

            ExportTypeComboBox = new List<string> { "New Elements", "Edit Elements" };
            SelectedExportType = "New Elements";

            AllCenterAttributesForDisplay = new List<ListBoxAttributes>();
            AllLeftAttributesForDisplay = new List<ListBoxAttributes>();
            AllRightAttributesForDisplay = new List<ListBoxAttributes>();
            foreach (string attribute in allAttributes)
            {
                AllLeftAttributesForDisplay.Add(new ListBoxAttributes { Item = attribute });
            }

            var temp1 = AllLeftAttributesForDisplay;
            var temp2 = AllCenterAttributesForDisplay;
            var temp3 = AllRightAttributesForDisplay;

            AllLeftAttributesForDisplay = new List<ListBoxAttributes>();
            AllCenterAttributesForDisplay = new List<ListBoxAttributes>();
            AllRightAttributesForDisplay = new List<ListBoxAttributes>();

            AllLeftAttributesForDisplay = temp1;
            AllCenterAttributesForDisplay = temp2;
            AllRightAttributesForDisplay = temp3;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        public void MoveAttributeFromLeftToCenter(object parameter)
        {
            var listAtt = selectedAttributeFromLeft;
            foreach (ListBoxAttributes item in AllLeftAttributesForDisplay)
            {
                item.IsSelected = false;
            }
            SelectedAttributeFromLeft = null;
            if (listAtt != null)
            {
                foreach (ListBoxAttributes attribute in listAtt)
                {
                    AllLeftAttributesForDisplay.Remove(AllLeftAttributesForDisplay.Single(x => x.Item == attribute.Item));
                    AllCenterAttributesForDisplay.Add(new ListBoxAttributes { Item = attribute.Item });
                }

                //SelectedAttributeGroup = SelectedAttributeGroup;
                var temp1 = AllLeftAttributesForDisplay;
                var temp2 = AllCenterAttributesForDisplay;

                AllLeftAttributesForDisplay = new List<ListBoxAttributes>();
                AllCenterAttributesForDisplay = new List<ListBoxAttributes>();


                AllLeftAttributesForDisplay = temp1;
                AllCenterAttributesForDisplay = temp2;
            }
        }

        public void MoveAttributeFromCenterToLeft(object parameter)
        {
            var listAtt = selectedAttributeFromCenter;
            foreach (ListBoxAttributes item in AllCenterAttributesForDisplay)
            {
                item.IsSelected = false;
            }
            SelectedAttributeFromCenter = null;
            if (listAtt != null)
            {
                foreach (ListBoxAttributes attribute in listAtt)
                {
                    AllLeftAttributesForDisplay.Add(new ListBoxAttributes { Item = attribute.Item });
                    AllCenterAttributesForDisplay.Remove(AllCenterAttributesForDisplay.Single(x => x.Item == attribute.Item));
                }

                var temp1 = AllLeftAttributesForDisplay;
                var temp2 = AllCenterAttributesForDisplay;

                AllLeftAttributesForDisplay = new List<ListBoxAttributes>();
                AllCenterAttributesForDisplay = new List<ListBoxAttributes>();

                AllLeftAttributesForDisplay = temp1;
                AllCenterAttributesForDisplay = temp2;
            }
        }

        public void MoveAttributeFromCenterToRight(object parameter)
        {
            var listAtt = selectedAttributeFromCenter;
            foreach (ListBoxAttributes item in AllCenterAttributesForDisplay)
            {
                item.IsSelected = false;
            }
            SelectedAttributeFromCenter = null;
            if (listAtt != null)
            {
                foreach (ListBoxAttributes attribute in listAtt)
                {
                    AllRightAttributesForDisplay.Add(new ListBoxAttributes { Item = attribute.Item });
                    AllCenterAttributesForDisplay.Remove(AllCenterAttributesForDisplay.Single(x => x.Item == attribute.Item));
                }

                var temp1 = AllRightAttributesForDisplay;
                var temp2 = AllCenterAttributesForDisplay;

                AllRightAttributesForDisplay = new List<ListBoxAttributes>();
                AllCenterAttributesForDisplay = new List<ListBoxAttributes>();

                AllRightAttributesForDisplay = temp1;
                AllCenterAttributesForDisplay = temp2;
            }
        }

        public void MoveAttributeFromRightToCenter(object parameter)
        {
            var listAtt = selectedAttributeFromRight;
            foreach (ListBoxAttributes item in AllRightAttributesForDisplay)
            {
                item.IsSelected = false;
            }
            SelectedAttributeFromRight = null;
            if (listAtt != null)
            {
                foreach (ListBoxAttributes attribute in listAtt)
                {
                    AllCenterAttributesForDisplay.Add(new ListBoxAttributes { Item = attribute.Item });
                    AllRightAttributesForDisplay.Remove(AllRightAttributesForDisplay.Single(x => x.Item == attribute.Item));
                }

                var temp1 = AllRightAttributesForDisplay;
                var temp2 = AllCenterAttributesForDisplay;

                AllRightAttributesForDisplay = new List<ListBoxAttributes>();
                AllCenterAttributesForDisplay = new List<ListBoxAttributes>();

                AllRightAttributesForDisplay = temp1;
                AllCenterAttributesForDisplay = temp2;
            }
        }

        public void SelectAllLeftAttributes(object parameter)
        {
            if (AllLeftAttributesForDisplay != null)
            {
                foreach (ListBoxAttributes attrib in AllLeftAttributesForDisplay)
                {
                    attrib.IsSelected = true;
                }
                selectedAttributeFromLeft = AllLeftAttributesForDisplay.Where(x => x.IsSelected).ToList();
                OnPropertyRaised("SelectedAttributeFromLeft");
            }
        }

        public void SelectAllCenterAttributes(object parameter)
        {
            if (AllCenterAttributesForDisplay != null)
            {
                foreach (ListBoxAttributes attrib in AllCenterAttributesForDisplay)
                {
                    attrib.IsSelected = true;
                }
                selectedAttributeFromCenter = AllCenterAttributesForDisplay.Where(x => x.IsSelected).ToList();
                OnPropertyRaised("SelectedAttributeFromCenter");
            }

        }

        public void SelectAllRightAttributes(object parameter)
        {
            if (AllRightAttributesForDisplay != null)
            {
                foreach (ListBoxAttributes attrib in AllRightAttributesForDisplay)
                {
                    attrib.IsSelected = true;
                }
                selectedAttributeFromRight = AllRightAttributesForDisplay.Where(x => x.IsSelected).ToList();
                OnPropertyRaised("SelectedAttributeFromRight");
            }

        }

        public void StartExport(object parameter)
        {
            if (AllCenterAttributesForDisplay.Any(x => x.Item == "NAME") ||
                AllRightAttributesForDisplay.Any(x => x.Item == "NAME"))
            {
                if (SelectedExportType == "New Elements" && (AllCenterAttributesForDisplay.Any(x => x.Item == "TYPE")  ||
                AllRightAttributesForDisplay.Any(x => x.Item == "TYPE")) && (AllCenterAttributesForDisplay.Any(x => x.Item == "OWNER") ||
                AllRightAttributesForDisplay.Any(x => x.Item == "OWNER")) || SelectedExportType == "Edit Elements")
                {

                    Mouse.OverrideCursor = Cursors.Wait;

                    string exportCode = "";

                    int columnIndex = 0;
                    int indexName = -1;
                    int indexType = -1;
                    int indexOwner = -1;
                    List<int> indexAttribute = new List<int>();
                    List<int> indexApostropheAttribute = new List<int>();
                    while (localSheet.Cells[0, columnIndex].Value.ToString() != "")
                    {
                        if (localSheet.Cells[0, columnIndex].Value.ToString() == "NAME")
                        {
                            indexName = columnIndex;
                        }
                        else if (localSheet.Cells[0, columnIndex].Value.ToString() == "TYPE")
                        {
                            indexType = columnIndex;
                        }
                        else if (localSheet.Cells[0, columnIndex].Value.ToString() == "OWNER")
                        {
                            indexOwner = columnIndex;
                        }
                        else if (AllCenterAttributesForDisplay.Any(
                            x => x.Item == localSheet.Cells[0, columnIndex].Value.ToString()))
                        {
                            indexAttribute.Add(columnIndex);
                        }
                        else if (AllRightAttributesForDisplay.Any(
                            x => x.Item == localSheet.Cells[0, columnIndex].Value.ToString()))
                        {
                            indexApostropheAttribute.Add(columnIndex);
                        }
                        columnIndex++;
                    }

                    //if (SelectedExportType == "New Elements" && indexType == -1)
                    //{
                    //    LogText = LogText + Environment.NewLine + "Error: " +
                    //        "TYPE Attribute missing for mode New Element. Export Aborted! ";
                    //}
                    //else
                    //{
                        LogText = LogText + Environment.NewLine + "Export Started.";

                        CellRange usedRange = localSheet.GetUsedRange();
                        int rowIndex = 1;
                        while (rowIndex < usedRange.RowCount)
                        {
                            if (localSheet.Cells[rowIndex, indexName].Value.ToString() == "")
                            {
                                LogText = LogText + Environment.NewLine +
                                    "Warning: NAME attribute missing in line: " + rowIndex.ToString() +
                                    ". Line skipped.";
                            }
                            else if (SelectedExportType == "New Elements" &&
                                localSheet.Cells[rowIndex, indexType].Value.ToString() == "")
                            {
                                LogText = LogText + Environment.NewLine +
                                    "Warning: TYPE attribute missing in line: " + rowIndex.ToString() +
                                    ". Line skipped.";
                            }
                            else if (SelectedExportType == "New Elements" &&
                                localSheet.Cells[rowIndex, indexOwner].Value.ToString() == "")
                            {
                                LogText = LogText + Environment.NewLine +
                                    "Warning: OWNER attribute missing in line: " + rowIndex.ToString() +
                                    ". Line skipped.";
                            }
                            else
                            {

                                if (SelectedExportType == "New Elements")
                                {

                                    exportCode = exportCode + Environment.NewLine + 
                                        localSheet.Cells[rowIndex, indexOwner].Value.ToString() + " " +
                                        "NEW" + " " +
                                        localSheet.Cells[rowIndex, indexType].Value.ToString() + " " +
                                        localSheet.Cells[rowIndex, indexName].Value.ToString();
                                }
                                else
                                {
                                    exportCode = exportCode + Environment.NewLine +
                                        localSheet.Cells[rowIndex, indexName].Value.ToString();
                                }
                                foreach (int currentAttIndex in indexAttribute)
                                {
                                    if (localSheet.Cells[rowIndex, currentAttIndex].Value.ToString() == "")
                                    {
                                        LogText = LogText + Environment.NewLine +
                                    "Warning: Attribute value for attribute: " +
                                    localSheet.Cells[0, currentAttIndex].Value.ToString() + " missing in line: "
                                    + rowIndex.ToString() +
                                    ". Attribute skipped.";
                                    }
                                    else
                                    {
                                        exportCode = exportCode + " " +
                                        localSheet.Cells[0, currentAttIndex].Value.ToString() + " " +
                                        localSheet.Cells[rowIndex, currentAttIndex].Value.ToString();
                                    }
                                }
                                foreach (int currentAttIndex in indexApostropheAttribute)
                                {
                                    if (localSheet.Cells[rowIndex, currentAttIndex].Value.ToString() == "")
                                    {
                                        LogText = LogText + Environment.NewLine +
                                    "Warning: Attribute value for attribute: " +
                                    localSheet.Cells[0, currentAttIndex].Value.ToString() + " missing in line: "
                                    + rowIndex.ToString() +
                                    ". Attribute skipped.";
                                    }
                                    else
                                    {
                                        exportCode = exportCode + " " +
                                        localSheet.Cells[0, currentAttIndex].Value.ToString() + " '" +
                                        localSheet.Cells[rowIndex, currentAttIndex].Value.ToString() + "'";
                                    }
                                }

                            }
                            rowIndex++;
                        }



                        LogText = LogText + Environment.NewLine + "Export Finished. Saving to file...";
                        SaveFileDialog dialog = new SaveFileDialog()
                        {
                            Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
                        };

                        if (dialog.ShowDialog() == true)
                        {
                            File.WriteAllText(dialog.FileName, exportCode);
                        }

                        LogText = LogText + Environment.NewLine + "File Saved";
                    //}

                    Mouse.OverrideCursor = null;

                }
                else
                {
                    LogText = LogText + Environment.NewLine + "Error: TYPE or OWNER attribute " +
                    "was not found. Export aborted, task NOT FINISHED!";
                }
            }
            else
            {
                LogText = LogText + Environment.NewLine + "Error: NAME attribute " +
                    "was not found. Export aborted, task NOT FINISHED!";
            }
        }
    }
}