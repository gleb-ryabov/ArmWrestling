﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Domain.Database
{
    public class CategoryGroup
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public CategoryGroup() { }
        public void Create(string name)
        {
            Name = name;
        }
    }
}
