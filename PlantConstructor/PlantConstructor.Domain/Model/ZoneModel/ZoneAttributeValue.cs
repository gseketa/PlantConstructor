using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.Domain.Model.ZoneModel
{
    public class ZoneAttributeValue : DomainObject
    {
        public int ZoneId { get; set; }
        public Zone Zone { get; set; }
        public int ZoneAttributeId { get; set; }
        public ZoneAttribute ZoneAttribute { get; set; }
        public string AttributeValue { get; set; }
    }
}
