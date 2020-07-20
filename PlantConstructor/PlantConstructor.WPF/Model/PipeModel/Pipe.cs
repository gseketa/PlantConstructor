using PlantConstructor.WPF.Model.BranchModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.WPF.Model.PipeModel
{
    public class Pipe
    {

        public int PipeID { get; set; }
        public string PipeName { get; set; }
        public Branch BranchFK { get; set; }
    }
}
