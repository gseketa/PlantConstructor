using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.WPF.Model.PartModel
{
    public class PartAttribute
    {
        public int PartAttributeID { get; set; }
        public string PartAttributeName { get; set; }
        public Project ProjectFK { get; set; }
    }
}
