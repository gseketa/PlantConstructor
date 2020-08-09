using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.WPF.Model.ZoneModel
{
    public class ZoneAttribute : DomainObject
    {
        public string ZoneAttributeName { get; set; }
        public Project ProjectFK { get; set; }
    }
}
