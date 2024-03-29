﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Domain.Database
{
    public class Person
    {
        public int Id { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public byte Gender { get; set; }
        public DateOnly BirthDate { get; set; }
        public float Weight { get; set; }

        public int CategoryInCompetitionId { get; set; }
        public CategoryInCompetition CategoryInCompetition { get; set; }

        public int? TeamId { get; set; }
        public Team? Team { get; set; }
        public int Score { get; set; } = 0;
    }
}
