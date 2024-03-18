﻿using ArmWrestling.Applications.Services.CategoryService;
using ArmWrestling.Applications.Services.PersonService;
using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryInCompetitionRepository;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryRepository;
using ArmWrestling.Infrastructure.Database.Repositories.CompetitionReposirory;
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

namespace ArmWrestling.ViewModel.RegistrationOfPersonsWindow
{
    public class RegistrationOfPersonsWindowViewModel : IRegistrationOfPersonsWindowViewModel, INotifyPropertyChanged
    {
        private readonly IWindowManager _windowManager;
        private readonly IPersonService _personService;
        private readonly ICategoryService _categoryService;
        private readonly ICompetitionRepository _competitionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryInCompetitionRepository _categoryInCompetitionRepository;

        private readonly IManagerCompetitionWindowViewModel _managerCompetitionWindowViewModel;

        private readonly Command _registerUserCommand;
        private readonly Command _checkAviableCategoriesCommand;
        private readonly Command _completeRegistrationCommand;

        public ICommand RegisterUserCommand => _registerUserCommand;
        public ICommand CheckAviableCategoriesCommand => _checkAviableCategoriesCommand;
        public ICommand CompleteRegistrationCommand => _completeRegistrationCommand;

        public RegistrationOfPersonsWindowViewModel(IWindowManager windowManager,
            IPersonService personService,
            ICategoryService categoryService,
            ICompetitionRepository competitionRepository,
            ICategoryRepository categoryRepository,
            ICategoryInCompetitionRepository categoryInCompetitionRepository,
            IManagerCompetitionWindowViewModel managerCompetitionWindowViewModel)
        {
            _windowManager = windowManager;
            _personService = personService;
            _categoryService = categoryService;
            _competitionRepository = competitionRepository;
            _categoryRepository = categoryRepository;
            _categoryInCompetitionRepository = categoryInCompetitionRepository;

            _managerCompetitionWindowViewModel = managerCompetitionWindowViewModel;

            _registerUserCommand = new Command(RegisterUser);
            _checkAviableCategoriesCommand = new Command(CheckAviableCategories);
            _completeRegistrationCommand = new Command(CompleteRegistration);
        }

        //Properties for binding values
            //for creating person
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public byte Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public DateOnly FormattedBirthDate { get; set; }
        public float Weight { get; set; }
        //public CategoryInCompetition CategoryInCompetition { get; set; }
        //public Team? Team { get; set; }
            //for choice of categories
        private List<CategoryInCompetition> _aviableCategories {  get; set; }
        public List<CategoryInCompetition> AviableCategories
        {
            get { return _aviableCategories; }
            set
            {
                if (_aviableCategories != value)
                {
                    _aviableCategories = value;
                    OnPropertyChanged(nameof(AviableCategories));
                }
            }
        }

        //Function for registration user
        public void RegisterUser()
        {
            //create person for each category
            foreach (var category in AviableCategories)
            {
                if (category.IsSelected == true)
                {
                    _personService.Create(Surname, Name, MiddleName, Gender, FormattedBirthDate, Weight, category, null);
                }
            }

            //zeroing out the variables
            Surname = string.Empty;
            Name = string.Empty;
            MiddleName = string.Empty;
            Gender = 1;
            BirthDate = DateTime.Today;
            Weight = 0;
            AviableCategories = new List<CategoryInCompetition> { };

            //notify about changes in variables
            OnPropertyChanged(nameof(Surname));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(MiddleName));
            OnPropertyChanged(nameof(Gender));
            OnPropertyChanged(nameof(BirthDate));
            OnPropertyChanged(nameof(Weight));
            OnPropertyChanged(nameof(AviableCategories));
        }

        //Function for check aviable categories for person
        public void CheckAviableCategories()
        {
            Competition competition = _competitionRepository.GetLast();

            //changing the date type
            FormattedBirthDate = new DateOnly(BirthDate.Year, BirthDate.Month, BirthDate.Day);

            //get aviable categories for person
            AviableCategories = new List<CategoryInCompetition>
            (_categoryInCompetitionRepository.GetAviableCategories(Gender, Weight, FormattedBirthDate, competition));

            SetNameCategories();
        }

        //Function for set the categories name for the text in checkboxes
        private void SetNameCategories()
        {
            foreach (var category in AviableCategories)
            {
                //save name category
                category.Name = _categoryService.CreateCategoryName(category.Category);
            }

        }

        //Function for complete the registration and go to the window conducting competitions
        public void CompleteRegistration()
        {
            _managerCompetitionWindowViewModel.Initialize();
            _windowManager.Show(_managerCompetitionWindowViewModel);
            _windowManager.Close<IRegistrationOfPersonsWindowViewModel>(this);
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void INotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
