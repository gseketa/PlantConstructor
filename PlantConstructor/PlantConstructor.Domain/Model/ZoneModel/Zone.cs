﻿using PlantConstructor.Domain.Model.SiteModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.Domain.Model.ZoneModel
{
    public class Zone : DomainObject
    {
        public string ZoneName { get; set; }
        public Site SiteFK { get; set; }

    }
}