﻿using PlantConstructor.WPF.Model.ZoneModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.WPF.Model.BranchModel
{
    public class Branch : DomainObject
    {
        public string BranchName { get; set; }
        public Zone ZoneFK { get; set; }
    }
}
