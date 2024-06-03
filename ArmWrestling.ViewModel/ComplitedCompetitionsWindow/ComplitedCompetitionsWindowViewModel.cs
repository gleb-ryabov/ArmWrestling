using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database.Repositories.CompetitionReposirory;
using ArmWrestling.ViewModel.Commands;
using ArmWrestling.ViewModel.EditPersonsWindow;
using ArmWrestling.ViewModel.ManagerCompetitionWindow;
using ArmWrestling.ViewModel.ResultsCompetitionWindow;
using ArmWrestling.ViewModel.SelectTableCategoriesWindow;
using ArmWrestling.ViewModel.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ArmWrestling.ViewModel.ComplitedCompetitionsWindow
{
    public class ComplitedCompetitionsWindowViewModel : IComplitedCompetitionsWindowViewModel
    {
        private readonly IWindowManager _windowManager;
        private readonly IManagerCompetitionWindowViewModel _managerCompetitionWindowViewModel;
        private readonly IResultsCompetitionWindowViewModel _resultsCompetitionWindowViewModel;

        private readonly ICompetitionRepository _competitionRepository;

        private readonly Command _closeWindowCommand;
        private readonly Command _openManagerCompetitionComand;
        private readonly ParameterizedCommand<object> _viewCompetitionCommand;

        public ICommand CloseWindowCommand => _closeWindowCommand;
        public ICommand OpenManagerCompetitionComand => _openManagerCompetitionComand;
        public ParameterizedCommand<object> ViewCompetitionCommand => _viewCompetitionCommand;

        public ComplitedCompetitionsWindowViewModel(
            IWindowManager windowManager,
            ICompetitionRepository competitionRepository,
            IManagerCompetitionWindowViewModel managerCompetitionWindowViewModel,
            IResultsCompetitionWindowViewModel resultsCompetitionWindowViewModel)
        {
            _windowManager = windowManager;
            _managerCompetitionWindowViewModel = managerCompetitionWindowViewModel;
            _resultsCompetitionWindowViewModel = resultsCompetitionWindowViewModel;

            _competitionRepository = competitionRepository;

            _closeWindowCommand = new Command(CloseWindow);
            _openManagerCompetitionComand = new Command(OpenManagerCompetition);
            _viewCompetitionCommand = new ParameterizedCommand<object>(ViewCompetition);
            GetLastCompetition();
        }

        //properties
        public Competition lastCompetition { get; set; }
        public List<Competition> lastCompetitions { get; set; } = new List<Competition> { };


        //Function for get last competitions
        private void GetLastCompetition()
        {
            //last competition
            lastCompetition = _competitionRepository.GetLast();

            //other last competitions
            List<Competition>  lastCompetitionsTmp = _competitionRepository.GetAll().ToList();
            foreach(Competition lastCompetitionTmp  in lastCompetitionsTmp) 
            {
                if (lastCompetition.Id != lastCompetitionTmp.Id)
                    lastCompetitions.Add(lastCompetitionTmp);
            }
        }

        //Funtion for open window - ManagerCompetitionWindow
        private void OpenManagerCompetition()
        {
            _managerCompetitionWindowViewModel.Initialize();
            _windowManager.Show(_managerCompetitionWindowViewModel);
            _windowManager.Close<ComplitedCompetitionsWindowViewModel>(this);
        }

        //Function for view result competition
        private void ViewCompetition(object parameter)
        {
            if (parameter is int compeitionId)
            {
                Competition competition = _competitionRepository.Get(compeitionId);
                _resultsCompetitionWindowViewModel.Initialize(competition);
                _windowManager.Show(_resultsCompetitionWindowViewModel);
            }
        }


        //Function for close window
        private void CloseWindow()
        {
            _windowManager.Close<ISelectTableCategoriesWindowViewModel>(this);
        }
    }
}
