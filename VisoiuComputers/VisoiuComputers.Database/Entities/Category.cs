﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisoiuComputers.Database.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public List<Component> Components { get; set; }
    }
}
