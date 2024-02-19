﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Domain.Database
{
    public class ResultTeam
    {
        public int Id { get; set; }

        public int CompetitionId { get; set; }
        public Competition Competition { get; set; }

        public int PersonId { get; set; }
        public Team Team { get; set; }

        public int Place { get; set; }

        //public ResultTeam(Competition competition, Person person, int place)
        //{
        //    Competition = competition;
        //    Person = person;
        //    Place = place;
        //}
    }
}