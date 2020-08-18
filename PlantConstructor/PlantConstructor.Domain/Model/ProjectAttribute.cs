using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.Domain.Model
{
    public class ProjectAttribute :DomainObject
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int AttributeGId { get; set; }
        public AttributeG AttributeG { get; set; }
    }
}
