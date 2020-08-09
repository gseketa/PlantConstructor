using PlantConstructor.Domain.Model.PipeModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.Domain.Model.PartModel
{
    public class Part : DomainObject
    {
        public string PartName { get; set; }
        public Pipe PipeFK { get; set; }

    }
}
