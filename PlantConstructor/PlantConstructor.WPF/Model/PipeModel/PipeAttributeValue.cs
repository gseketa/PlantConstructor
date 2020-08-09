using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.WPF.Model.PipeModel
{
    public class PipeAttributeValue
    {
        public int PipeAttributeValueID { get; set; }
        public Pipe PipeFK { get; set; }
        public PipeAttribute SiteAttributeFK { get; set; }
        public string AttributeValue { get; set; }
    }
}
