using DevExpress.DataProcessing.InMemoryDataProcessor;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.WPF.Model.SiteModel
{
    public class Site
    {
        public int SiteID { get; set; }
        public string SiteName { get; set; }
        public Project ProjectFK { get; set; }


    }
}
