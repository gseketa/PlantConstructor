using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.Domain.Model
{
    public class Element : DomainObject
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string Type { get; set; }
        public int RowIndex { get; set; }
    }
}
