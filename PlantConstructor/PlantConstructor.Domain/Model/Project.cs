using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.Domain.Model
{
    public class Project :DomainObject
    {
        public string Name { get; set; }
        public string ProjectGroup { get; set; }
        public override string ToString()
        {
            return Name;
        }

    }
}
