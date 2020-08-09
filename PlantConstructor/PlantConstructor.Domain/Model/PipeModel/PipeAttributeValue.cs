using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.Domain.Model.PipeModel
{
    public class PipeAttributeValue : DomainObject
    {
        public Pipe PipeFK { get; set; }
        public PipeAttribute SiteAttributeFK { get; set; }
        public string AttributeValue { get; set; }
    }
}
