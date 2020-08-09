using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.WPF.Model.BranchModel
{
    public class BranchAttributeValue
    {
        public int BranchAttributeValueID { get; set; }
        public Branch BranchFK { get; set; }
        public BranchAttribute BranchAttributeFK { get; set; }
        public string AttributeValue { get; set; }
    }
}
