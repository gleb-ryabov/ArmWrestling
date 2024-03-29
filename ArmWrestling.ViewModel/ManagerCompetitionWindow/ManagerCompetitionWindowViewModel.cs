﻿using ArmWrestling.Applications.Services.CategoryService;
using ArmWrestling.Applications.Services.DuelService;
using ArmWrestling.Applications.Services.PersonService;
using ArmWrestling.Applications.Services.TableService;
using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryInCompetitionRepository;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryRepository;
using ArmWrestling.Infrastructure.Database.Repositories.CompetitionReposirory;
using ArmWrestling.Infrastructure.Database.Repositories.DuelRepository;
using ArmWrestling.Infrastructure.Database.Repositories.TableRepository;
using ArmWrestling.ViewModel.Commands;
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

namespace ArmWrestling.ViewModel.ManagerCompetitionWindow
{
    public class ManagerCompetitionWindowViewModel : IManagerCompetitionWindowViewModel, INotifyPropertyChanged
    {
        private readonly IWindowManager _windowManager;
        private readonly ISelectTableCategoriesWindowViewModel _selectTableCategoriesWindowViewModel;

        private readonly ICategoryService _categoryService;
        private readonly IDuelService _duelService;
        private readonly IPersonService _personService;
        private readonly ITableService _tableService;

        private readonly ICategoryRepository _categoryRepository;
        private readonly ICompetitionRepository _competitionRepository;
        private readonly ICategoryInCompetitionRepository _categoryInCompetitionRepository;
        private readonly IDuelRepository _duelRepository;
        private readonly ITableRepository _tableRepository;
        
        private readonly ParameterizedCommand<object> _selectTableCategoriesCommand;
        public ParameterizedCommand<object> SelectTableCategoriesCommand => _selectTableCategoriesCommand;

        private readonly ParameterizedCommand<object> _setFirstAsWinnerCommand;
        public ParameterizedCommand<object> SetFirstAsWinnerCommand => _setFirstAsWinnerCommand;

        private readonly ParameterizedCommand<object> _setSecondAsWinnerCommand;
        public ParameterizedCommand<object> SetSecondAsWinnerCommand => _setSecondAsWinnerCommand;

        public ManagerCompetitionWindowViewModel(IWindowManager windowManager,
            ISelectTableCategoriesWindowViewModel selectTableCategoriesWindowViewModel,
            ICategoryService categoryService,
            IDuelService duelService,
            IPersonService personService,
            ITableService tableService,
            ICategoryRepository categoryRepository,
            ICompetitionRepository competitionRepository,
            ICategoryInCompetitionRepository categoryInCompetitionRepository,
            IDuelRepository duelRepository,
            ITableRepository tableRepository)
        {
            _windowManager = windowManager;
            _selectTableCategoriesWindowViewModel = selectTableCategoriesWindowViewModel;

            _categoryService = categoryService;
            _duelService = duelService;
            _personService = personService;
            _tableService = tableService;

            _categoryRepository = categoryRepository;
            _competitionRepository = competitionRepository;
            _categoryInCompetitionRepository = categoryInCompetitionRepository;
            _duelRepository = duelRepository;
            _tableRepository = tableRepository;
            
            _selectTableCategoriesCommand = new ParameterizedCommand<object>(SelectTableCategories);
            _setFirstAsWinnerCommand = new ParameterizedCommand<object>(SetFirstAsWinner);
            _setSecondAsWinnerCommand = new ParameterizedCommand<object>(SetSecondAsWinner);
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
        //char Arm { get; set; }

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
        public void UpdateTable(int tableId)
        {
            //finding the modified table
            foreach (var table in Tables)
            {
                if (table.Id == tableId)
                {
                    Table newTable = _tableRepository.Get(tableId);

                    //setting modified parameters for table
                    table.IsBusy = newTable.IsBusy;
                    table.CategoryInCompetitionId = newTable.CategoryInCompetitionId;
                    table.CategoryInCompetition = _categoryInCompetitionRepository.Get((int)table.CategoryInCompetitionId);
                    SetNameCategory(table);

                    //setting queue for table
                    GetQueue(table);
                }
            }
        }

        //Function for set the queue and opponents for the table
        private void GetQueue(Table table)
        {
            //set competition to table
            if (table.Competition is null)
                table.Competition = _competitionRepository.Get(table.CompetitionId);

            //getting competition parameters
            char firstArm = table.Competition.FirstArm;
            CategoryInCompetition category = table.CategoryInCompetition;
            char arm = _duelService.GetLastArmInCategory(table.CategoryInCompetition);
            if (arm == ' ')
            {

            }
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
                //здесь испольуем прошлое значение
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
                    table.IsBusy = 0;

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

                Duel duel = _duelService.Create
                    (table.CategoryInCompetition, arm, table.NumberTour, 1, groupDuel);
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


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
