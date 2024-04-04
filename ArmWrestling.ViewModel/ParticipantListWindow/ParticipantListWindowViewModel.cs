using ArmWrestling.Applications.Services.CategoryService;
using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryInCompetitionRepository;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryRepository;
using ArmWrestling.Infrastructure.Database.Repositories.CompetitionReposirory;
using ArmWrestling.Infrastructure.Database.Repositories.PersonRepository;
using ArmWrestling.ViewModel.Commands;
using ArmWrestling.ViewModel.EditPersonsWindow;
using ArmWrestling.ViewModel.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.ViewModel.ParticipantListWindow
{
    public class ParticipantListWindowViewModel : IParticipantListWindowViewModel, INotifyPropertyChanged
    {
        private readonly ICategoryInCompetitionRepository _categoryInCompetitionRepository;
        private readonly ICompetitionRepository _competitionRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryService _categoryService;

        private readonly ParameterizedCommand<object> _editPersonCommand;
        public ParameterizedCommand<object> EditPersonCommand => _editPersonCommand;

        private readonly IWindowManager _windowManager;
        private readonly IEditPersonsWindowViewModel _editPersonsWindowViewModel;

        private Dictionary<CategoryInCompetition, List<Person>> _participantList = 
            new Dictionary<CategoryInCompetition, List<Person>>();
        public Dictionary<CategoryInCompetition, List<Person>> ParticipantList
        {
            get { return _participantList; }
            set
            {
                _participantList = value;
                OnPropertyChanged(nameof(ParticipantList));
            }
        }
        private int _countParticipants = 0;
        public int CountParticipants
        {
            get => _countParticipants;
            set
            {
                _countParticipants = value;
                OnPropertyChanged(nameof(CountParticipants));
            }
        }


        public ParticipantListWindowViewModel(
            ICategoryInCompetitionRepository categoryInCompetitionRepository,
            ICompetitionRepository competitionRepository,
            IPersonRepository personRepository,
            ICategoryRepository categoryRepository,
            ICategoryService categoryService,
            IWindowManager windowManager,
            IEditPersonsWindowViewModel editPersonsWindowViewModel) 
        {
            _categoryInCompetitionRepository = categoryInCompetitionRepository;
            _competitionRepository = competitionRepository;
            _personRepository = personRepository;
            _categoryRepository = categoryRepository;
            _categoryService = categoryService;

            _editPersonCommand = new ParameterizedCommand<object>(EditPerson);

            _windowManager = windowManager;
            _editPersonsWindowViewModel = editPersonsWindowViewModel;

            GetParticipantList();
        }

        public void GetParticipantList()
        {
            Competition competition = _competitionRepository.GetLast();
            List<CategoryInCompetition> categories =
                _categoryInCompetitionRepository.GetByCompetition(competition).ToList();

            foreach (CategoryInCompetition category in categories)
            {
                List<Person> persons = _personRepository.GetPersonsByCategory(category).ToList();
                ParticipantList.Add(category, persons);

                CountParticipants += persons.Count;
            }

            SetNameCategories();
        }

        //Function for set the categories name for the combobox
        private void SetNameCategories()
        {
            foreach (var category in ParticipantList.Keys)
            {
                if (category.Category is null)
                {
                    category.Category = _categoryRepository.Get(category.CategoryId);
                }

                //save name category
                category.Name = _categoryService.CreateCategoryName(category.Category);
            }
        }

        //Functions for open window for edit person
        private void EditPerson(object parameter)
        {
            if (parameter is int personId)
            {
                Person person = _personRepository.Get(personId);
                _editPersonsWindowViewModel.Initialize(person);
                _windowManager.Show(_editPersonsWindowViewModel);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
