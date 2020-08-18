using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.Domain.Model.PartModel
{
    public class PartAttributeValue : DomainObject
    {
        public int PartId { get; set; }
        public Part Part { get; set; }
        public int PartAttributeId { get; set; }
        public PartAttribute PartAttribute { get; set; }
        public string AttributeValue { get; set; }
    }
}
