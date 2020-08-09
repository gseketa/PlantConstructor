using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.Domain.Model.BranchModel
{
    public class BranchAttribute : DomainObject
    {
        public string BranchAttributeName { get; set; }
        public Project ProjectFK { get; set; }
    }
}
