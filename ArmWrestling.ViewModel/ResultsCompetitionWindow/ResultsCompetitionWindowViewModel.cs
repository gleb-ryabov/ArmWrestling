using ArmWrestling.Applications.Services.CategoryService;
using ArmWrestling.Applications.Services.ResultPersonService;
using ArmWrestling.Applications.Services.ResultTeamService;
using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryInCompetitionRepository;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryRepository;
using ArmWrestling.Infrastructure.Database.Repositories.CompetitionReposirory;
using ArmWrestling.ViewModel.Commands;
using ArmWrestling.ViewModel.SelectTableCategoriesWindow;
using ArmWrestling.ViewModel.Windows;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ArmWrestling.ViewModel.ResultsCompetitionWindow
{
    public class ResultsCompetitionWindowViewModel : IResultsCompetitionWindowViewModel, INotifyPropertyChanged
    {
        private readonly IWindowManager _windowManager;

        private readonly IResultPersonService _resultPersonService;
        private readonly IResultTeamService _resultTeamService;
        private readonly ICategoryService _categoryService;
        private readonly ICategoryInCompetitionRepository _categoryInCompetitionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICompetitionRepository _competitionRepository;

        private readonly Command _getResultCommand;
        public ICommand GetResultCommand => _getResultCommand;
        private readonly Command _closeWindowCommand;
        public ICommand CloseWindowCommand => _closeWindowCommand;

        private Competition currentCompetition;

        public ResultsCompetitionWindowViewModel(IWindowManager windowManager,
            IResultPersonService resultPersonService,
            IResultTeamService resultTeamService,
            ICategoryService categoryService,
            ICategoryInCompetitionRepository categoryInCompetitionRepository,
            ICategoryRepository categoryRepository,
            ICompetitionRepository competitionRepository)
        {
            _windowManager = windowManager;

            _resultPersonService = resultPersonService;
            _resultTeamService = resultTeamService;
            _categoryService = categoryService;

            _categoryInCompetitionRepository = categoryInCompetitionRepository;
            _categoryRepository = categoryRepository;
            _competitionRepository = competitionRepository;

            _getResultCommand = new Command(GetResult);
            _closeWindowCommand = new Command(CloseWindow);
        }

        //Function for initialize
        public void Initialize(Competition competition)
        {
            Categories = _categoryInCompetitionRepository.GetComplitedCategory(competition).ToList();
            SetNameCategories();

            currentCompetition = _competitionRepository.GetLast();
        }

        //Properties for binding values
        private List<ResultPerson> _results = new List<ResultPerson>();
        public List<ResultPerson> Results
        {
            get { return _results; }
            set
            {
                _results = value;
                OnPropertyChanged(nameof(Results));
            }
        }

        private List<ResultTeam> _resultTeams = new List<ResultTeam>();
        public List<ResultTeam> ResultsTeams
        {
            get { return _resultTeams; }
            set
            {
                _resultTeams = value;
                OnPropertyChanged(nameof(ResultsTeams));
            }
        }

        private List<CategoryInCompetition> _categories = new List<CategoryInCompetition>();
        public List<CategoryInCompetition> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        private CategoryInCompetition _selectedCategory;
        public CategoryInCompetition SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        //Function for get result for selected category
        private void GetResult()
        {
            if(currentCompetition.TypeCompetition == "Личные" || currentCompetition.TypeCompetition == "Лично-командные")
            {
                if (SelectedCategory != null)
                {
                    Results = _resultPersonService.GetResult(SelectedCategory);
                }
            }
            if (currentCompetition.TypeCompetition == "Командные" || currentCompetition.TypeCompetition == "Лично-командные")
                ResultsTeams = _resultTeamService.GetResult(currentCompetition);
        }

        //Function for set the categories name for the combobox
        private void SetNameCategories()
        {
            foreach (var category in Categories)
            {
                category.Category =  _categoryRepository.Get(category.CategoryId);
                //save name category
                category.Name = _categoryService.CreateCategoryName(category.Category);
            }
        }

        //Function for close window
        private void CloseWindow()
        {
            _windowManager.Close<IResultsCompetitionWindowViewModel>(this);
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
