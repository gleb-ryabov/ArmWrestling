using ArmWrestling.Applications.Services.CategoryService;
using ArmWrestling.Applications.Services.ResultPersonService;
using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryInCompetitionRepository;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryRepository;
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
        private readonly ICategoryService _categoryService;
        private readonly ICategoryInCompetitionRepository _categoryInCompetitionRepository;
        private readonly ICategoryRepository _categoryRepository;

        private readonly Command _getResultCommand;
        public ICommand GetResultCommand => _getResultCommand;
        private readonly Command _closeWindowCommand;
        public ICommand CloseWindowCommand => _closeWindowCommand;

        public ResultsCompetitionWindowViewModel(IWindowManager windowManager,
            IResultPersonService resultPersonService,
            ICategoryService categoryService,
            ICategoryInCompetitionRepository categoryInCompetitionRepository,
            ICategoryRepository categoryRepository)
        {
            _windowManager = windowManager;

            _resultPersonService = resultPersonService;
            _categoryService = categoryService;

            _categoryInCompetitionRepository = categoryInCompetitionRepository;
            _categoryRepository = categoryRepository;

            _getResultCommand = new Command(GetResult);
            _closeWindowCommand = new Command(CloseWindow);
        }

        //Function for initialize
        public void Initialize(Competition competition)
        {
            Categories = _categoryInCompetitionRepository.GetComplitedCategory(competition).ToList();
            SetNameCategories();
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
            Results = _resultPersonService.GetResult(SelectedCategory);
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
            _windowManager.Close<ISelectTableCategoriesWindowViewModel>(this);
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
