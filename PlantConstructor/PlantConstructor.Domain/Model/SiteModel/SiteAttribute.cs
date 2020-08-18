using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.Domain.Model.SiteModel
{
    public class SiteAttribute : DomainObject
    {
        public string SiteAttributeName { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
