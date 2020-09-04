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

        public List<ListBoxAttributes> GetAttForListbox(string type)
        {
            List <ListBoxAttributes> returnList = new List<ListBoxAttributes>();
            switch (type)
            {
                case "Site":
                    foreach (string tempString in SiteAttributes)
                    {
                        returnList.Add(new ListBoxAttributes { Item = tempString });
                    }
                    return returnList;
                case "Zone":
                    foreach (string tempString in ZoneAttributes)
                    {
                        returnList.Add(new ListBoxAttributes { Item = tempString });
                    }
                    return returnList;
                case "Pipe":
                    foreach (string tempString in PipeAttributes)
                    {
                        returnList.Add(new ListBoxAttributes { Item = tempString });
                    }
                    return returnList;
                case "Branch":
                    foreach (string tempString in BranchAttributes)
                    {
                        returnList.Add(new ListBoxAttributes { Item = tempString });
                    }
                    return returnList;
                case "PipePart":
                    foreach (string tempString in PipePartAttributes)
                    {
                        returnList.Add(new ListBoxAttributes { Item = tempString });
                    }
                    return returnList;
                default:
                    return null;
            }
        }

    }
}
