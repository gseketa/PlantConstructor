﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.WPF.Model.PipeModel
{
    public class PipeAttribute : DomainObject
    {
        public string PipeAttributeName { get; set; }
        public Project ProjectFK { get; set; }
    }
}
