using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.WPF.Model.PipeModel
{
    public class PipeAttribute
    {
        public int PipeAttributeID { get; set; }
        public string PipeAttributeName { get; set; }
        public Project ProjectFK { get; set; }
    }
}
