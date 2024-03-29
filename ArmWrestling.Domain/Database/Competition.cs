﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Domain.Database
{
    public class Competition
    {
        public int Id { get; set; }
        public DateTime Created {  get; set; }
        [Required]
        public string TypeJudging { get; set; }
        [Required]
        public string TypeCompetition { get; set; }
        public byte CountTable { get; set; }
        public char FirstArm { get; set; }

        public int WeightTolerance {  get; set; }
    }
}
