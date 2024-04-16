using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Domain.Database
{
    public class Duel : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public int CategoryInCompetitionId { get; set; }
        public CategoryInCompetition CategoryInCompetition { get; set; }

        [ForeignKey(nameof(Person))]
        public int? WinnerId { get; set; }
        public Person? Winner { get; set; }

        [ForeignKey(nameof(Person))]
        public int? LooserId { get; set; }
        public Person? Looser { get; set; }

        [NotMapped]
        private char _arm;
        public char Arm
        {
            get { return _arm; }
            set 
            { 
                _arm = value; 
                OnPropertyChanged(nameof(Arm));
            }
        }

        public int TourNumber { get; set; }

        public char? Group { get; set; }

        /*
         * The field for identify type duel
         * 
         * 0 - The ordinary duel
         * 1 - The Superfinal
         * 2 - The Final
         * 3 - The Semi–final
         * 4 - The Free Circle
         * 5 - The fight to reach the semifinals
         * 6 - The fight to reach the final
         * 7 - The fight for the 5th place
        */
        public byte? TypeDuel { get; set; }

        [NotMapped]
        private string _descriptionType;
        [NotMapped]
        public string DescriptionType
        {
            get { return _descriptionType; }
            set
            {
                _descriptionType = value;
                OnPropertyChanged(nameof(DescriptionType));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
