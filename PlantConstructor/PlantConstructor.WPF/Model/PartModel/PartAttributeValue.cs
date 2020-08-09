using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.WPF.Model.PartModel
{
    public class PartAttributeValue
    {
        public int PartAttributeValueID { get; set; }
        public Part PartFK { get; set; }
        public PartAttribute PartAttributeFK { get; set; }
        public string AttributeValue { get; set; }
    }
}
