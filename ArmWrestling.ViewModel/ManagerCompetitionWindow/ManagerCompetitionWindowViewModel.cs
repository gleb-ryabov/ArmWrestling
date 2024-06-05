using ArmWrestling.Applications.Services.CategoryService;
using ArmWrestling.Applications.Services.DuelService;
using ArmWrestling.Applications.Services.PersonService;
using ArmWrestling.Applications.Services.TableService;
using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryInCompetitionRepository;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryRepository;
using ArmWrestling.Infrastructure.Database.Repositories.CompetitionReposirory;
using ArmWrestling.Infrastructure.Database.Repositories.DuelRepository;
using ArmWrestling.Infrastructure.Database.Repositories.PersonRepository;
using ArmWrestling.Infrastructure.Database.Repositories.TableRepository;
using ArmWrestling.ViewModel.Commands;
using ArmWrestling.ViewModel.EditPersonsWindow;
using ArmWrestling.ViewModel.ParticipantListWindow;
using ArmWrestling.ViewModel.ResultsCompetitionWindow;
using ArmWrestling.ViewModel.SelectTableCategoriesWindow;
using ArmWrestling.ViewModel.Windows;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ArmWrestling.ViewModel.ManagerCompetitionWindow
{
    public class ManagerCompetitionWindowViewModel : IManagerCompetitionWindowViewModel, INotifyPropertyChanged
    {
        private readonly IWindowManager _windowManager;
        private readonly ISelectTableCategoriesWindowViewModel _selectTableCategoriesWindowViewModel;
        private readonly IResultsCompetitionWindowViewModel _resultsCompetitionWindowViewModel;
        private readonly IParticipantListWindowViewModel _participantListWindowViewModel;

        private readonly ICategoryService _categoryService;
        private readonly IDuelService _duelService;
        private readonly IPersonService _personService;
        private readonly ITableService _tableService;

        private readonly ICategoryRepository _categoryRepository;
        private readonly ICompetitionRepository _competitionRepository;
        private readonly ICategoryInCompetitionRepository _categoryInCompetitionRepository;
        private readonly IDuelRepository _duelRepository;
        private readonly ITableRepository _tableRepository;
        private readonly IPersonRepository _personRepository;
        
        private readonly ParameterizedCommand<object> _selectTableCategoriesCommand;
        private readonly ParameterizedCommand<object> _setFirstAsWinnerCommand;
        private readonly ParameterizedCommand<object> _setSecondAsWinnerCommand;
        public ParameterizedCommand<object> SelectTableCategoriesCommand => _selectTableCategoriesCommand;
        public ParameterizedCommand<object> SetFirstAsWinnerCommand => _setFirstAsWinnerCommand;
        public ParameterizedCommand<object> SetSecondAsWinnerCommand => _setSecondAsWinnerCommand;

        private readonly Command _completeCompetitionCommand;
        private readonly Command _browseParticipantsCommand;
        private readonly Command _closeWindowCommand;
        private readonly Command _closeInfo1Command;
        private readonly Command _closeInfo2Command;

        public ICommand CompleteCompetitionCommand => _completeCompetitionCommand;
        public ICommand BrowseParticipantsCommand => _browseParticipantsCommand;
        public ICommand CloseWindowCommand => _closeWindowCommand;
        public ICommand CloseInfo1Command => _closeInfo1Command;
        public ICommand CloseInfo2Command => _closeInfo2Command;

        public ManagerCompetitionWindowViewModel(IWindowManager windowManager,
            ISelectTableCategoriesWindowViewModel selectTableCategoriesWindowViewModel,
            IResultsCompetitionWindowViewModel resultsCompetitionWindowViewModel,
            IParticipantListWindowViewModel participantListWindowViewModel,
            ICategoryService categoryService,
            IDuelService duelService,
            IPersonService personService,
            ITableService tableService,
            ICategoryRepository categoryRepository,
            ICompetitionRepository competitionRepository,
            ICategoryInCompetitionRepository categoryInCompetitionRepository,
            IDuelRepository duelRepository,
            ITableRepository tableRepository,
            IPersonRepository personRepository)
        {
            _windowManager = windowManager;
            _selectTableCategoriesWindowViewModel = selectTableCategoriesWindowViewModel;
            _resultsCompetitionWindowViewModel = resultsCompetitionWindowViewModel;
            _participantListWindowViewModel = participantListWindowViewModel;

            _categoryService = categoryService;
            _duelService = duelService;
            _personService = personService;
            _tableService = tableService;

            _categoryRepository = categoryRepository;
            _competitionRepository = competitionRepository;
            _categoryInCompetitionRepository = categoryInCompetitionRepository;
            _duelRepository = duelRepository;
            _tableRepository = tableRepository;
            _personRepository = personRepository;

            _selectTableCategoriesCommand = new ParameterizedCommand<object>(SelectTableCategories);
            _setFirstAsWinnerCommand = new ParameterizedCommand<object>(SetFirstAsWinner);
            _setSecondAsWinnerCommand = new ParameterizedCommand<object>(SetSecondAsWinner);

            _completeCompetitionCommand = new Command(CompleteCompetition);
            _browseParticipantsCommand = new Command(BrowseParticipants);
            _closeWindowCommand = new Command(CloseWindow);
            _closeInfo1Command = new Command(CloseInfo1);
            _closeInfo2Command = new Command(CloseInfo2);
        }

        //Function for initialize components in window
        public void Initialize()
        {
            CreateTables();
        }

        //properties for binding values
        public List<Table> _tables = new List<Table>();
        public List<Table> Tables
        {
            get { return _tables; }
            set
            {
                _tables = value;
                OnPropertyChanged(nameof(Tables));
            }
        }
        //notify window 1
        public string _stringInfo1;
        public string StringInfo1
        {
            get { return _stringInfo1; }
            set
            {
                _stringInfo1 = value;
                OnPropertyChanged(nameof(StringInfo1));
            }
        }
        public List<string> _infoPersons1 = new List<string>();
        public List<string> InfoPersons1
        {
            get { return _infoPersons1; }
            set
            {
                _infoPersons1 = value;
                OnPropertyChanged(nameof(InfoPersons1));
            }
        }
        //notify window 2
        public string _stringInfo2;
        public string StringInfo2
        {
            get { return _stringInfo2; }
            set
            {
                _stringInfo2 = value;
                OnPropertyChanged(nameof(StringInfo2));
            }
        }
        public List<string> _infoPersons2 = new List<string>();
        public List<string> InfoPersons2
        {
            get { return _infoPersons2; }
            set
            {
                _infoPersons2 = value;
                OnPropertyChanged(nameof(InfoPersons2));
            }
        }
        //for queue notify
        int lastNotify = 2;


        //Function for create tables
        private void CreateTables()
        {
            
            //getting competition
            Competition competition = _competitionRepository.GetLast();
            int countTable = competition.CountTable;


            for (int i = 0; i < countTable; i++)
            {
                Table newTable = _tableService.Create(i + 1, competition);
                Tables.Add(newTable);
            }

            //Tables = _tableRepository.GetByCompetition(competition).ToList();

        }

        //Function for choice categories for tables
        private void SelectTableCategories(object parameter)
        {
            if(parameter is int tableId)
            {
                _selectTableCategoriesWindowViewModel.Initialize(this, tableId);
                _windowManager.Show(_selectTableCategoriesWindowViewModel);
            }

        }

        //Function for update the table (called when the table category is changed)
        public void UpdateTable(Table newTable)
        {
            //finding the modified table
            foreach (var table in Tables)
            {
                if (table.Id == newTable.Id)
                {

                    //setting modified parameters for table
                    table.IsBusy = 1; //newTable.IsBusy;
                    table.CategoryInCompetitionId = newTable.CategoryInCompetitionId;
                    table.CategoryInCompetition = _categoryInCompetitionRepository.Get((int)table.CategoryInCompetitionId);
                    table.Competition = _competitionRepository.Get(table.CompetitionId);

                    SetNameCategory(table);

                    //setting queue for table
                    GetQueue(table);


                    //Notify for invite participants
                    List<Person> personsInCategory = _personRepository.GetPersonsByCategory(table.CategoryInCompetition).ToList();
                    List<string> fioPersons = new List<string>();
                    foreach (var person in personsInCategory)
                    {
                        fioPersons.Add(person.Surname + " " + person.Name + " " + person.MiddleName);
                    }
                    string infoTable = "За столом номер " + table.Number + " начинается категория «" + 
                        table.CategoryInCompetition.Name + "». Участники: ";

                    if (lastNotify == 2)
                    {
                        StringInfo1 = infoTable;
                        InfoPersons1 = fioPersons;
                        lastNotify = 1;
                    }
                    else 
                    {
                        StringInfo2 = infoTable;
                        InfoPersons2 = fioPersons;
                        lastNotify = 2;
                    }


                    int a;
                    if(table.IsBusy == 1)
                    {
                        a = 1 + 1;
                    }
                    if(table.CategoryInCompetitionId == 11)
                    {
                        a = 10 + 1;
                    }
                }
            }
        }

        //Function for set the queue and opponents for the table
        private void GetQueue(Table table)
        {
            //getting competition parameters
            char firstArm = table.Competition.FirstArm;
            CategoryInCompetition category = table.CategoryInCompetition;
            char arm = _duelService.GetLastArmInCategory(table.CategoryInCompetition);

            table.NumberTour = _duelRepository.GetLastNumberTour(arm, table.CategoryInCompetition) + 1;

            //calculating the opposite arm (second arm)
            char secondArm = ' ';
            switch (firstArm)
            {
                case 'л':
                    secondArm = 'п';
                    break;
                case 'п':
                    secondArm = 'л';
                    break;
            }

            //checking the presence of duels in the category
            bool duelsInFirstArm = _duelRepository.CheckExstenceDuels(category, firstArm);
            bool duelsInSecondArm = _duelRepository.CheckExstenceDuels(category, firstArm);
            bool queueIsCreated = false;
            if (duelsInFirstArm || duelsInSecondArm)
            {

                List<List<Person>> queue = _personService.CreateDraw(category, arm);

                //Сhecking the need to conduct more duels in this arm
                if (queue.Count != 0)
                {
                    table.Queue = queue;

                    queueIsCreated = true;
                    SetTableOpponents(table, arm);
                }
            }
            
            if (!queueIsCreated)
            {

                //setting arm and checking the need to continue
                if (!_duelRepository.CheckExstenceDuels(category, firstArm))
                    arm = firstArm;
                else if (!_duelRepository.CheckExstenceDuels(category, secondArm))
                {
                    arm = secondArm;
                    table.NumberTour = 1;
                }
                else
                {
                    //table = _tableService.ClearTable(table);
                    _tableService.ClearTable(table);
                    table.IsBusy = 0;
                    table.CategoryInCompetition.Complited = 1;
                    table.CategoryInCompetition = null;
                }

                //setting the queue and calling function for setting opponents if the queue is set
                if (table.IsBusy == 1)
                {
                    table.Queue = _personService.CreateDraw(category, arm);
                    SetTableOpponents(table, arm);
                }
            }
        }

        //Function for set opponents for the table
        private void SetTableOpponents(Table table, char arm)
        {
            //checking count pairs in the queue
            if((table.Queue).Count != 0)
            {
                //getting opponents and removing they from queue
                List<Person> opponents = table.Queue[0];
                table.Queue.Remove(opponents);

                //setting first and second opponents in the table
                table.FirstOpponent = opponents[0];
                table.SecondOpponent = null;
                if(opponents.Count > 1)
                {
                    table.SecondOpponent = opponents[1];
                }

                //creating the duel
                char groupDuel = _duelService.CalculateGroup(arm, table.FirstOpponent, table.SecondOpponent);
                byte typeDuel = _personService.GetTypeDuel(table.CategoryInCompetition, arm);
                if (table.SecondOpponent == null)
                    typeDuel = 4;

                Duel duel = _duelService.Create
                    (table.CategoryInCompetition, arm, table.NumberTour, typeDuel, groupDuel);
                table.Duel = duel;
            }
            else
            {
                //call method for setting the new queue or resetting the table category
                GetQueue(table);
            }
        }

        //Function for set the categories name for the combobox
        private void SetNameCategory(Table table)
        {
            table.CategoryInCompetition.Category = _categoryRepository.Get(table.CategoryInCompetition.CategoryId);

            //save name category
            table.CategoryInCompetition.Name = _categoryService.CreateCategoryName(table.CategoryInCompetition.Category);
        }

        //Functions for set the winner and set the looser
        private void SetFirstAsWinner(object parameter)
        {
            if(parameter is Table table)
                RecordDuelResult(table, table.FirstOpponent, table.SecondOpponent);
        }
        private void SetSecondAsWinner(object parameter)
        {
            if (parameter is Table table)
                RecordDuelResult(table, table.SecondOpponent, table.FirstOpponent);
        }

        //Function for record the result
        private void RecordDuelResult(Table table, Person winner, Person looser)
        {
            _duelRepository.SetWinnerAndLooser(table.Duel, winner, looser);

            SetTableOpponents(table, table.Duel.Arm);
        }

        //Function for complete the competition and get result
        private void CompleteCompetition()
        {
            Competition competition = _competitionRepository.GetLast();
            _resultsCompetitionWindowViewModel.Initialize(competition);

            _windowManager.Show<IResultsCompetitionWindowViewModel>(_resultsCompetitionWindowViewModel);
        }

        //Function for open the participant list
        private void BrowseParticipants()
        {
            _participantListWindowViewModel.Initialize();
            _windowManager.Show<IParticipantListWindowViewModel>(_participantListWindowViewModel);
        }

        //Funciton for close window
        private void CloseWindow()
        {
            _windowManager.Close<IManagerCompetitionWindowViewModel>(this);
        }

        //Funcitons for close notify Windows
        private void CloseInfo1()
        {
            StringInfo1 = "";
            InfoPersons1 = new List<string>();
        }
        private void CloseInfo2()
        {
            StringInfo2 = "";
            InfoPersons2 = new List<string>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
