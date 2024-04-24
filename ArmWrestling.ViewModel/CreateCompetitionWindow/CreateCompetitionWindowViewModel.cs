using ArmWrestling.Applications.Services.CategoryInCompetitionService;
using ArmWrestling.Applications.Services.CategoryService;
using ArmWrestling.Applications.Services.CompetitionService;
using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryRepository;
using ArmWrestling.ViewModel.Commands;
using ArmWrestling.ViewModel.EditPersonsWindow;
using ArmWrestling.ViewModel.MainWindow;
using ArmWrestling.ViewModel.ManagerTeamsWindow;
using ArmWrestling.ViewModel.RegistrationOfPersonsWindow;
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

        private readonly IWindowManager _windowManager;
        private readonly IRegistrationOfPersonsWindowViewModel _registrationOfPersonsWindowViewModel;
        private readonly IManagerTeamsWindowViewModel _managerTeamsWindowViewModel;
        
        private readonly ICompetitionService _competitionService;
        private readonly ICategoryInCompetitionService _categoryInCompetitionService;
        private readonly ICategoryService _categoryService;

        private readonly List<Category> _categories;
        public List<Category> Categories => _categories;

        private readonly Command _closeWindowCommand;
        public ICommand CloseWindowCommand => _closeWindowCommand;

        public CreateCompetitionWindowViewModel(IWindowManager windowManager,
            IRegistrationOfPersonsWindowViewModel registrationOfPersonsWindowViewModel,
            IManagerTeamsWindowViewModel managerTeamsWindowViewModel,
            ICompetitionService competitionService, 
            ICategoryInCompetitionService categoryInCompetitionService, 
            ICategoryService categoryService,
            ICategoryRepository categoryRepository)
        {
            _windowManager = windowManager;
            _registrationOfPersonsWindowViewModel = registrationOfPersonsWindowViewModel;
            _managerTeamsWindowViewModel = managerTeamsWindowViewModel;

            _competitionService = competitionService;
            _categoryInCompetitionService = categoryInCompetitionService;
            _categoryService = categoryService;

            _categories = new List<Category>(categoryRepository.GetAll());

            SetNameCategories();

            _closeWindowCommand = new Command(CloseWindow);
            _createCompetitionCommand = new Command(CreateCompetition);
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

            //open the window for create teams if it is necessary
            if (TypeCompetition == "Командные" || TypeCompetition == "Лично-командные")
            {
                _managerTeamsWindowViewModel.Initialize();
                _windowManager.Show(_managerTeamsWindowViewModel);
                _windowManager.Close<IMainWindowViewModel>(this);
            }
            else
            {
                //open the window for registration of persons
                _windowManager.Show(_registrationOfPersonsWindowViewModel);
                _windowManager.Close<IMainWindowViewModel>(this);
            }
            
        }

        //Function for set the categories name for the text in checkboxes
        private void SetNameCategories()
        {
            foreach (var category in Categories)
            {
                string Name = "";

                //Save name category
                category.Name = _categoryService.CreateCategoryName(category);
            }
        }

        //Funciton for close window
        private void CloseWindow()
        {
            _windowManager.Close<ICreateCompetitionWindowViewModel>(this);
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
