﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kist_api.Model.dashboard
{
    public class Widget
    {
        public int id { get; set; }
        public string textBig { get; set; }
        public string textSmall { get; set; }
        public string bgColor { get; set; }
        public string urlRoute { get; set; }

        public string faIcon { get; set; }

        public int notifications { get; set; }
        public bool disabled { get; set; }
    }
}
