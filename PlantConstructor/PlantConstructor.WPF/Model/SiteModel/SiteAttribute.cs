using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.WPF.Model.SiteModel
{
    public class SiteAttribute
    {
        public int SiteAttributeID { get; set; }
        public string SiteAttributeName { get; set; }
        public Project ProjectFK { get; set; }
    }
}
