using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.WPF.Model.SiteModel
{
    class SiteAttributeValue
    {
        public int SiteAttributeValueID { get; set; }
        public Site SiteFK { get; set; }
        public SiteAttribute SiteAttributeFK { get; set; }
        public string AttributeValue { get; set; }
    }
}
