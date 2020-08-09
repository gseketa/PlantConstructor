using PlantConstructor.Domain.Model.ZoneModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.Domain.Model.BranchModel
{
    public class Branch : DomainObject
    {
        public string BranchName { get; set; }
        public Zone ZoneFK { get; set; }
    }
}
