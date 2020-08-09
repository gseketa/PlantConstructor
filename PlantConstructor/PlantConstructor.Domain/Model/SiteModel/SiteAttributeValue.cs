using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.Domain.Model.SiteModel
{
    public class SiteAttributeValue : DomainObject
    {
        public Site SiteFK { get; set; }
        public SiteAttribute SiteAttributeFK { get; set; }
        public string AttributeValue { get; set; }
    }
}
