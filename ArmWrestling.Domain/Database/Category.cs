﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Domain.Database
{
    public class Category
    {
        public int Id {  get; set; }
        public byte Gender { get; set; }
        public byte MinAge { get; set; }
        public byte MaxAge { get; set; }
        public byte MaxWeight { get; set; }

        public int CategoryGroupId { get; set; }
        public CategoryGroup CategoryGroup { get; set; }
    }
}
