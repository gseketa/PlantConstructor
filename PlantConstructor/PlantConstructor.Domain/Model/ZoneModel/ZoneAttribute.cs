using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.Domain.Model.ZoneModel
{
    public class ZoneAttribute : DomainObject
    {
        public string ZoneAttributeName { get; set; }
        public Project ProjectFK { get; set; }
    }
}
