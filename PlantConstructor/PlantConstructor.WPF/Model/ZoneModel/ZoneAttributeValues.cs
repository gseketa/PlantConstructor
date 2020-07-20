using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.WPF.Model.ZoneModel
{
    class ZoneAttributeValues
    {
        public int ZoneAttributeValueID { get; set; }
        public Zone ZoneFK { get; set; }
        public ZoneAttribute ZoneAttributeFK { get; set; }
        public string AttributeValue { get; set; }
    }
}
