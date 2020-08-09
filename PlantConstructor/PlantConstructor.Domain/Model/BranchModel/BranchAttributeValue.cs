using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.Domain.Model.BranchModel
{
    public class BranchAttributeValue : DomainObject
    { 
    
        public Branch BranchFK { get; set; }
        public BranchAttribute BranchAttributeFK { get; set; }
        public string AttributeValue { get; set; }
    }
}
