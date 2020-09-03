using DevExpress.Xpf.Core.Native;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.WPF.Helper
{
    public class ListsOfAttributes
    {
       public List<string> SiteAttributes { get; set; }
        public List<string> ZoneAttributes { get; set; }
        public List<string> PipeAttributes { get; set; }
        public List<string> BranchAttributes { get; set; }
        public List<string> PipePartAttributes { get; set; }

        public List<string> GetListsOfAttributes(string type)
        {
            switch (type)
            {
                case "Site":
                    return SiteAttributes;
                case "Zone":
                    return ZoneAttributes;
                case "Pipe":
                    return PipeAttributes;
                case "Branch":
                    return BranchAttributes;
                case "PipePart":
                    return PipePartAttributes;
                default:
                    return null;
            }
        }

    }
}
