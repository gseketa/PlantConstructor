using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.Domain.Model.PipeModel
{
    public class PipeAttributeValue : DomainObject
    {
        public int PipeId { get; set; }
        public Pipe Pipe { get; set; }
        public int PiteAttributeId { get; set; }
        public PipeAttribute PiteAttribute { get; set; }
        public string AttributeValue { get; set; }
    }
}
