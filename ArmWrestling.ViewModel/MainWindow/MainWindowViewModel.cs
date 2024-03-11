using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database;
using ArmWrestling.Infrastructure.Database.Repositories.TeamRepository;
using ArmWrestling.ViewModel.Commands;
using ArmWrestling.ViewModel.CreateCompetitionWindow;
using ArmWrestling.ViewModel.ManagerCompetitionWindow;
using ArmWrestling.ViewModel.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ArmWrestling.ViewModel.MainWindow
{
    public class MainWindowViewModel : IMainWindowViewModel
    {
        private readonly IWindowManager _windowManager;
        private readonly ICreateCompetitionWindowViewModel _createCompetitionWindowViewModel;
        private readonly IManagerCompetitionWindowViewModel _managerCompetitionWindowViewModel;
        private readonly Command _openCreateCompetitionWindowCommand;
        private readonly Command _openPastCompetitionsCommand;

        public ICommand OpenCreateCompetitionWindowCommand => _openCreateCompetitionWindowCommand;
        public ICommand OpenPastCompetitionsCommand => _openPastCompetitionsCommand;


        public MainWindowViewModel(IWindowManager windowManager, 
            ICreateCompetitionWindowViewModel createCompetitionWindowViewModel,
            IManagerCompetitionWindowViewModel managerCompetitionWindowViewModel)
        {
            _windowManager = windowManager;

            _createCompetitionWindowViewModel = createCompetitionWindowViewModel;
            _managerCompetitionWindowViewModel = managerCompetitionWindowViewModel;

            _openCreateCompetitionWindowCommand = new Command(OpenCreateCompetitionWindow);
            _openPastCompetitionsCommand = new Command(OpenPastCompetitions);
        }

        private void OpenCreateCompetitionWindow()
        {
            _windowManager.Show(_createCompetitionWindowViewModel);
            _windowManager.Close<IMainWindowViewModel>(this);
        }
        private void OpenPastCompetitions()
        {
            _windowManager.Show(_managerCompetitionWindowViewModel);
            _windowManager.Close<IMainWindowViewModel>(this);
        }
    }
}
