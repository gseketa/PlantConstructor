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

    }
}
