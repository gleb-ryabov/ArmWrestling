using ArmWrestling.Applications.Services.TeamService;
using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database.Repositories.CompetitionReposirory;
using ArmWrestling.Infrastructure.Database.Repositories.TeamRepository;
using ArmWrestling.ViewModel.Commands;
using ArmWrestling.ViewModel.EditPersonsWindow;
using ArmWrestling.ViewModel.MainWindow;
using ArmWrestling.ViewModel.RegistrationOfPersonsWindow;
using ArmWrestling.ViewModel.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ArmWrestling.ViewModel.ManagerTeamsWindow
{
    public class ManagerTeamsWindowViewModel : IManagerTeamsWindowViewModel, INotifyPropertyChanged
    {
        private List<Team> _teamsList = new List<Team>();
        public List<Team> TeamsList
        {
            get { return _teamsList; }
            set 
            { 
                _teamsList = value;
                OnPropertyChanged(nameof(TeamsList));
            }
        }
        private string _teamName = "";
        public string TeamName
        {
            get { return _teamName; }
            set
            {
                _teamName = value;
                OnPropertyChanged(nameof(TeamName));
            }
        }

        Competition competition;

        private readonly IWindowManager _windowManager;

        private readonly IRegistrationOfPersonsWindowViewModel _registrationOfPersonsWindowViewModel;

        private readonly ICompetitionRepository _competitionRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamService _teamService;

        private readonly Command _createTeamCommand;
        private readonly Command _registrationPersonCommand;
        private readonly Command _closeWindowCommand;

        public ICommand CreateTeamCommand => _createTeamCommand;
        public ICommand RegistrationPersonCommand => _registrationPersonCommand;
        public ICommand CloseWindowCommand => _closeWindowCommand;

        public ManagerTeamsWindowViewModel(IWindowManager windowManager,
                IRegistrationOfPersonsWindowViewModel registrationOfPersonsWindowViewModel,
                ICompetitionRepository competitionRepository,
                ITeamRepository teamRepository,
                ITeamService teamService) 
        {
            _windowManager = windowManager;

            _registrationOfPersonsWindowViewModel = registrationOfPersonsWindowViewModel;

            _competitionRepository = competitionRepository;
            _teamRepository = teamRepository;
            _teamService = teamService;

            _createTeamCommand = new Command(CreateTeam);
            _registrationPersonCommand = new Command(RegistrationPerson);
            _closeWindowCommand = new Command(CloseWindow);
        }

        public void Initialize()
        {
            competition = _competitionRepository.GetLast();

            TeamsList = _teamRepository.GetByCompetition(competition.Id).ToList();
        }

        //Function for create teams
        private void CreateTeam()
        {
            Team newTeam = _teamService.Create(TeamName, competition);
            TeamName = "";
            TeamsList = _teamRepository.GetByCompetition(competition.Id).ToList();
        }

        //Function for go to registration person
        private void RegistrationPerson()
        {
            _registrationOfPersonsWindowViewModel.Initialize();
            _windowManager.Show(_registrationOfPersonsWindowViewModel);
            _windowManager.Close<IManagerTeamsWindowViewModel>(this);
        }

        //Funciton for close window
        private void CloseWindow()
        {
            _windowManager.Close<IManagerTeamsWindowViewModel>(this);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
