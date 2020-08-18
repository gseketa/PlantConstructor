using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.Domain.Model.BranchModel
{
    public class BranchAttributeValue : DomainObject
    { 
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public int BranchAttributeId { get; set; }
        public BranchAttribute BranchAttribute { get; set; }
        public string AttributeValue { get; set; }
    }
}
