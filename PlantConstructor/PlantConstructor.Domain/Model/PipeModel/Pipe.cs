using PlantConstructor.Domain.Model.BranchModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.Domain.Model.PipeModel
{
    public class Pipe : DomainObject
    {

        public string PipeName { get; set; }
        public Branch BranchFK { get; set; }
    }
}
