﻿using PlantConstructor.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlantConstructor.Domain.Model.SiteModel
{
    public class Site : DomainObject
    {
        public string SiteName { get; set; }
        public Project ProjectFK { get; set; }


    }
}