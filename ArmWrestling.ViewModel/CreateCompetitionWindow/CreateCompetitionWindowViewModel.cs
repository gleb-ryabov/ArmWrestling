using ArmWrestling.Applications.Services.CategoryInCompetitionService;
using ArmWrestling.Applications.Services.CompetitionService;
using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryRepository;
using ArmWrestling.ViewModel.Commands;
using ArmWrestling.ViewModel.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ArmWrestling.ViewModel.CreateCompetitionWindow
{
    public class CreateCompetitionWindowViewModel : ICreateCompetitionWindowViewModel, INotifyPropertyChanged
    {
        private Command _createCompetitionCommand;
        public ICommand CreateCompetitionCommand => _createCompetitionCommand;

        private readonly ICompetitionService _competitionService;
        private readonly ICategoryInCompetitionService _categoryInCompetitionService;

        private readonly List<Category> _categories;
        public List<Category> Categories => _categories;

        public CreateCompetitionWindowViewModel(ICompetitionService competitionService, 
            ICategoryInCompetitionService categoryInCompetitionService, ICategoryRepository categoryRepository)
        {
            _createCompetitionCommand = new Command(CreateCompetition);
            _competitionService = competitionService;
            _categoryInCompetitionService = categoryInCompetitionService;
            _categories = new List<Category>(categoryRepository.GetAll());
            SetNameCategories();
        }
        
        //properties for binding values
            //for creating a Competition
        public string TypeJudging { get; set; }
        public string TypeCompetition { get; set; }
        public byte CountTable { get; set; }
        public char FirstArm { get; set; }
        public int WeightTolerance { get; set; }

        //function for creating Competition and CategoryInCompetition by entered parameters
        public void CreateCompetition()
        {
            //for creating Competition
            DateTime Created = DateTime.Now;
            Competition competition = _competitionService.Create(TypeJudging, TypeCompetition, CountTable, FirstArm, WeightTolerance);

            //for creating CategoryInCompetition
            foreach (var category in Categories)
            {
                if (category.IsSelected == true)
                {
                    _categoryInCompetitionService.Create(category, competition);
                }
            }
        }
        private void SetNameCategories()
        {
            foreach (var category in Categories)
            {
                string Name = "";

                //Set man or woman
                if (category.Gender == 1 && category.MaxAge == 255)
                    Name = "Мужчины";
                else if (category.Gender == 1)
                    Name = "Юниоры";
                else if (category.Gender == 0 && category.MaxAge == 255)
                    Name = "Женщины";
                else if (category.Gender == 0)
                    Name = "Юниорки";

                //Set age and weight
                if (category.MinAge == 0 && category.MaxAge == 255 && category.MaxWeight == 255)
                    Name = String.Concat(Name, " Абсолютная");
                else if (category.MaxAge == 255)
                    Name = String.Concat(Name, " от ", category.MinAge, " лет до ", category.MaxWeight, " кг");
                else
                    Name = String.Concat(Name, " ", category.MinAge, "-", category.MaxAge, 
                        " до ", category.MaxWeight, " кг");

                //Save name category
                category.Name = Name;
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void INotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
