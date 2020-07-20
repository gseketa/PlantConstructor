using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.WPF.Model.BranchModel
{
    public class BranchAttribute
    {
        public int BranchAttributeID { get; set; }
        public string BranchAttributeName { get; set; }
        public Project ProjectFK { get; set; }
    }
}
