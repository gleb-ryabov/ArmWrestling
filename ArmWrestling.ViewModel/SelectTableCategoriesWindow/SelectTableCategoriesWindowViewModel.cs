using ArmWrestling.Applications.Services.CategoryInCompetitionService;
using ArmWrestling.Applications.Services.CategoryService;
using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryRepository;
using ArmWrestling.Infrastructure.Database.Repositories.CompetitionReposirory;
using ArmWrestling.Infrastructure.Database.Repositories.TableRepository;
using ArmWrestling.ViewModel.Commands;
using ArmWrestling.ViewModel.ManagerCompetitionWindow;
using ArmWrestling.ViewModel.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ArmWrestling.ViewModel.SelectTableCategoriesWindow
{
    public class SelectTableCategoriesWindowViewModel : ISelectTableCategoriesWindowViewModel, INotifyPropertyChanged
    {
        private readonly ITableRepository _tableRepository;
        private readonly ICompetitionRepository _competitionRepository;
        private readonly ICategoryRepository _categoryRepository;

        private readonly ICategoryInCompetitionService _categoryInCompetitionService;
        private readonly ICategoryService _categoryService;

        private readonly IWindowManager _windowManager;

        private readonly Command _setCategoryinTableCommand;
        private readonly Command _closeWindowCommand;

        public ICommand SetCategoryinTableCommand => _setCategoryinTableCommand;
        public ICommand CloseWindowCommand=> _closeWindowCommand;

        private ManagerCompetitionWindowViewModel _managerCompetitionWindowViewModel;

        public SelectTableCategoriesWindowViewModel(
            ITableRepository tableRepository,
            ICompetitionRepository competitionRepository,
            ICategoryRepository categoryRepository,
            ICategoryService categoryService,
            ICategoryInCompetitionService categoryInCompetitionService,
            IWindowManager windowManager)
        {
            _tableRepository = tableRepository;
            _competitionRepository = competitionRepository;
            _categoryRepository = categoryRepository;

            _categoryInCompetitionService = categoryInCompetitionService;
            _categoryService = categoryService;

            _windowManager = windowManager;

            _setCategoryinTableCommand = new Command(SetCategoryInTable);
            _closeWindowCommand = new Command(CloseWindow);
        }


        //properties for binding values
        public Table Table { get; set; }
        public CategoryInCompetition CategoryInCompetition { get; set; }

        public List<CategoryInCompetition> _categoryInCompetitions = new List<CategoryInCompetition>();
        public List<CategoryInCompetition> CategoryInCompetitions
        {
            get { return _categoryInCompetitions; }
            set
            {
                _categoryInCompetitions = value;
                OnPropertyChanged(nameof(CategoryInCompetitions));
            }
        }


        //Function for initializing components in window
        public void Initialize(ManagerCompetitionWindowViewModel viewModel, int tableId)
        {
            Competition competition = _competitionRepository.GetLast();

            Table = _tableRepository.Get(tableId);

            CategoryInCompetitions = _categoryInCompetitionService.GetNotUsedCategories(competition);
            SetNameCategories();

            _managerCompetitionWindowViewModel = viewModel;
        }


        //Function for set category in table and update tables in manager competition window
        private void SetCategoryInTable()
        {
            _tableRepository.SetCategory(Table, CategoryInCompetition);

            _managerCompetitionWindowViewModel.UpdateTable(Table.Id);

            _windowManager.Close<ISelectTableCategoriesWindowViewModel>(this);
        }

        //Function for set the categories name for the combobox
        private void SetNameCategories()
        {
            foreach (var category in CategoryInCompetitions)
            {
                if (category.Category is null)
                {
                    category.Category = _categoryRepository.Get(category.CategoryId);
                }

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
