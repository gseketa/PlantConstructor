using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.WPF.Helper
{
    public class ListOfSpreadsheetElements
    {
        public List<SpreadsheetElement> SiteElements { get; set; }
        public List<SpreadsheetElement> ZoneElements { get; set; }
        public List<SpreadsheetElement> PipeElements { get; set; }
        public List<SpreadsheetElement> BranchElements { get; set; }
        public List<SpreadsheetElement> PipePartElements { get; set; }
        public List<SpreadsheetElement> StructureElements { get; set; }
        public List<SpreadsheetElement> SubStructureElements { get; set; }
        public List<SpreadsheetElement> StructurePartElements { get; set; }
        public List<SpreadsheetElement> EquipmentElements { get; set; }
        public List<SpreadsheetElement> SubEquipmentElements { get; set; }
        public List<SpreadsheetElement> EquipmentPartElements { get; set; }


    }
}
