﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpProject.Model.JsonModel
{
   public class JsonRootObject
    {
        public string BrandName { get; set; }
        public Features Features { get; set; }
        public int Id { get; set; }
        public string LaptopName { get; set; }
    }
}