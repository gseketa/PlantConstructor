using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace PlantConstructor.Domain.Model
{
    public class AttributeValue :DomainObject
    {
        public int AttributeGId { get; set; }
        public AttributeG AttributeG { get; set; }
        public int ElementId { get; set; }
        public Element Element { get; set; }
        public string Value { get; set; }
    }
}
