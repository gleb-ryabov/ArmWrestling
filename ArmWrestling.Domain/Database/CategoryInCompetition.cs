using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped]
        public bool IsSelected { get; set; } = false;
        [NotMapped]
        public string Name { get; set; }
    }

}
