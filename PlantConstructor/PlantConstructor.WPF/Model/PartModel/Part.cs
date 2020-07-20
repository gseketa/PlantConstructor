using PlantConstructor.WPF.Model.PipeModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.WPF.Model.PartModel
{
    public class Part
    {
        public int PartID { get; set; }
        public string PartName { get; set; }
        public Pipe PipeFK { get; set; }

    }
}
