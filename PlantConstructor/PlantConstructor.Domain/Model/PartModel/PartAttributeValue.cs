using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.Domain.Model.PartModel
{
    public class PartAttributeValue : DomainObject
    {
        public Part PartFK { get; set; }
        public PartAttribute PartAttributeFK { get; set; }
        public string AttributeValue { get; set; }
    }
}
