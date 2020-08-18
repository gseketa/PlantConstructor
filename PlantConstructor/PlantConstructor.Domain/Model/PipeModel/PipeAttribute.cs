﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.Domain.Model.PipeModel
{
    public class PipeAttribute : DomainObject
    {
        public string PipeAttributeName { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
