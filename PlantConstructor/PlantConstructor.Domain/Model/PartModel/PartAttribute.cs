﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.Domain.Model.PartModel
{
    public class PartAttribute : DomainObject
    {
        public string PartAttributeName { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
