using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.WPF.Model.ZoneModel
{
    public class ZoneAttributeValue
    {
        public int ZoneAttributeValueID { get; set; }
        public Zone ZoneFK { get; set; }
        public ZoneAttribute ZoneAttributeFK { get; set; }
        public string AttributeValue { get; set; }
    }
}
