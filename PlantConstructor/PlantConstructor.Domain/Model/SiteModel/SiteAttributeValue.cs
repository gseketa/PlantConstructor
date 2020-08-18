using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.Domain.Model.SiteModel
{
    public class SiteAttributeValue : DomainObject
    {
        public int SiteId { get; set; }
        public Site Site { get; set; }
        public int SiteAttributeId { get; set; }
        public SiteAttribute SiteAttribute { get; set; }
        public string AttributeValue { get; set; }
    }
}
