using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.Domain.Model.ZoneModel
{
    public class ZoneAttributeValue : DomainObject
    {
        public Zone ZoneFK { get; set; }
        public ZoneAttribute ZoneAttributeFK { get; set; }
        public string AttributeValue { get; set; }
    }
}
