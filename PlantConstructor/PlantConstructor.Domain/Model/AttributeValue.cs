using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace PlantConstructor.Domain.Model
{
    public class AttributeValue :DomainObject
    {
        public int ProjectAttributeID { get; set; }
        public ProjectAttribute ProjectAttribute { get; set; }
        public int Rowindex { get; set; }
        public string Value { get; set; }
    }
}
