using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.Domain.Model.ZoneModel
{
    public class ZoneAttribute : DomainObject
    {
        public string ZoneAttributeName { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
