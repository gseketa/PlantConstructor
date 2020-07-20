using PlantConstructor.WPF.Model.SiteModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.WPF.Model.ZoneModel
{
    public class Zone
    {
        public int ZoneID { get; set; }
        public string ZoneName { get; set; }
        public Site SiteFK { get; set; }

    }
}
