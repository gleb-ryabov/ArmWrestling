using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Domain.Database
{
    public class CategoryInCompetition
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int CompetitionId { get; set; }
        public Competition Competition { get; set; }

        //public CategoryInCompetition (Category category, Competition competition)
        //{
        //    Category = category;
        //    Competition = competition;
        //}
    }
}
