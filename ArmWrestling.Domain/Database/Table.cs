using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Domain.Database
{
    public class Table : INotifyPropertyChanged
    {
        public int Id { get; set; }
        [Required]
        public int Number { get; set; }

        public int CompetitionId { get; set; }
        public Competition Competition { get; set; }

        public int? CategoryInCompetitionId { get; set; }
        public CategoryInCompetition? CategoryInCompetition { get; set; }

        [NotMapped]
        private int _isBusy;
        public int IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged(nameof(IsBusy));
                }
            }
        }

        [NotMapped]
        public List<List<Person>>? Queue {  get; set; }

        [NotMapped]
        private Person? _firstOpponent;
        [NotMapped]
        public Person? FirstOpponent
        {
            get { return _firstOpponent; }
            set
            {
                _firstOpponent = value;
                OnPropertyChanged(nameof(FirstOpponent));
            }
        }

        [NotMapped] 
        private Person? _secondOpponent;
        [NotMapped]
        public Person? SecondOpponent
        {
            get { return _secondOpponent; }
            set
            {
                _secondOpponent = value;
                OnPropertyChanged(nameof(SecondOpponent));
            }
        }

        [NotMapped]
        public int? DuelId { get; set; }
        [NotMapped]
        public Duel Duel { get; set; }

        [NotMapped]
        public int NumberTour { get; set; }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 

    }
}
