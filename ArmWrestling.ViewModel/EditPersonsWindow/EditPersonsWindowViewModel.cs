﻿using ArmWrestling.Applications.Services.CategoryService;
using ArmWrestling.Applications.Services.PersonService;
using ArmWrestling.Applications.Services.TeamService;
using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryInCompetitionRepository;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryRepository;
using ArmWrestling.Infrastructure.Database.Repositories.CompetitionReposirory;
using ArmWrestling.Infrastructure.Database.Repositories.PersonRepository;
using ArmWrestling.Infrastructure.Database.Repositories.TeamRepository;
using ArmWrestling.ViewModel.Commands;
using ArmWrestling.ViewModel.ManagerCompetitionWindow;
using ArmWrestling.ViewModel.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace ArmWrestling.ViewModel.EditPersonsWindow
{
    public class EditPersonsWindowViewModel : IEditPersonsWindowViewModel, INotifyPropertyChanged
    {
        private readonly IWindowManager _windowManager;
        private readonly IPersonService _personService;
        private readonly ICategoryService _categoryService;
        private readonly ITeamService _teamService;

        private readonly ICompetitionRepository _competitionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryInCompetitionRepository _categoryInCompetitionRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ITeamRepository _teamRepository;

        private readonly IManagerCompetitionWindowViewModel _managerCompetitionWindowViewModel;

        private readonly Command _editUserCommand;
        private readonly Command _checkAviableCategoriesCommand;
        private readonly Command _closeWindowCommand;
        private readonly Command _completeRegistrationCommand;

        public ICommand EditUserCommand => _editUserCommand;
        public ICommand CheckAviableCategoriesCommand => _checkAviableCategoriesCommand;
        public ICommand CloseWindowCommand => _closeWindowCommand;
        public ICommand CompleteRegistrationCommand => _completeRegistrationCommand;
        public EditPersonsWindowViewModel(
            IWindowManager windowManager,
            IPersonService personService,
            ICategoryService categoryService,
            ITeamService teamService,
            ICompetitionRepository competitionRepository,
            ICategoryRepository categoryRepository,
            ICategoryInCompetitionRepository categoryInCompetitionRepository,
            IPersonRepository personRepository,
            ITeamRepository teamRepository
            )
        {
            _windowManager = windowManager;
            _personService = personService;
            _categoryService = categoryService;
            _teamService = teamService;

            _competitionRepository = competitionRepository;
            _categoryRepository = categoryRepository;
            _categoryInCompetitionRepository = categoryInCompetitionRepository;
            _personRepository = personRepository;
            _teamRepository = teamRepository;



            _editUserCommand = new Command(EditUser);
            _checkAviableCategoriesCommand = new Command(CheckAviableCategories);
            _closeWindowCommand = new Command(CloseWindow);
        }


        //properties
        private Person _person;
        public Person Person
        {
            get { return _person; }
            set
            {
                _person = value;
                OnPropertyChanged(nameof(Person));
            }
        }

        private List<Team> _teams { get; set; }
        public List<Team> Teams
        {
            get { return _teams; }
            set
            {
                _teams = value;
                OnPropertyChanged(nameof(Teams));
            }
        }
        private Team? _team = null;
        public Team? Team
        {
            get { return _team; }
            set
            {
                _team = value;
                OnPropertyChanged(nameof(Team));
            }
        }

        public DateTime BirthDate
        {
            get
            {
                TimeOnly convertTime = new TimeOnly(000000);
                DateTime convertDate = Person.BirthDate.ToDateTime(convertTime);
                return convertDate;
            }
            set
            {
                Person.BirthDate = new DateOnly(BirthDate.Year, BirthDate.Month, BirthDate.Day);
                OnPropertyChanged(nameof(BirthDate));
            }
        }
        private List<CategoryInCompetition> _aviableCategories { get; set; }
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

        public DateOnly FormattedBirthDate { get; set; }


        
        //Function for initialization peron before open window
        public void Initialize(Person person)
        {
            GetTeams();

            Person = person;

            CheckAviableCategories();
            for (int i = 0; i < AviableCategories.Count; i++)
            {
                if (AviableCategories[i].Id == person.CategoryInCompetitionId)
                    AviableCategories[i].IsSelected = true;
            }

            if (person.TeamId != null)
            {
                int teamId = (int)person.TeamId;
                Team = _teamRepository.Get(teamId);
            }
            
        }

        //Function for check aviable categories for person
        public void CheckAviableCategories()
        {
            Competition competition = _competitionRepository.GetLast();

            //unset property isSelected for categories
            if (AviableCategories != null)
            {
                for (int i = 0; i < AviableCategories.Count; i++)
                {
                    AviableCategories[i].IsSelected = false;
                }
            }

            //changing the date type
            FormattedBirthDate = new DateOnly(BirthDate.Year, BirthDate.Month, BirthDate.Day);

            //get aviable categories for person
            AviableCategories = new List<CategoryInCompetition>
            (_categoryInCompetitionRepository.GetAviableCategories(Person.Gender, Person.Weight, FormattedBirthDate, competition));

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

        //Function for edit person
        private void EditUser()
        {
            foreach (var category in AviableCategories)
            {
                if (category.IsSelected == true)
                {
                    Person.CategoryInCompetitionId = category.Id;
                    _personRepository.UpdatePerson(Person);
                }
            }
            //_personRepository.UpdatePerson(Person);
            _windowManager.Close<IEditPersonsWindowViewModel>(this);
        }

        //Function for get teams in competiton
        private void GetTeams()
        {
            Teams = _teamService.GetByLastCompetiton();
        }

        //Funciton for close window
        private void CloseWindow()
        {
            _windowManager.Close<IEditPersonsWindowViewModel>(this);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
