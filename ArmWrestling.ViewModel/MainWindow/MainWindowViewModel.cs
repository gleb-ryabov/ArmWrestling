using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database;
using ArmWrestling.Infrastructure.Database.Repositories.TeamRepository;
using ArmWrestling.ViewModel.Commands;
using ArmWrestling.ViewModel.CreateCompetitionWindow;
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
        private readonly Command _openCreateCompetitionWindowCommand;

        public MainWindowViewModel(IWindowManager windowManager, 
            ICreateCompetitionWindowViewModel createCompetitionWindowViewModel)
        {
            _windowManager = windowManager;
            _createCompetitionWindowViewModel = createCompetitionWindowViewModel;

            _openCreateCompetitionWindowCommand = new Command(OpenCreateCompetitionWindow);
        }

        public ICommand OpenCreateCompetitionWindowCommand => _openCreateCompetitionWindowCommand;

        private void OpenCreateCompetitionWindow()
        {
            _windowManager.Show(_createCompetitionWindowViewModel);
            _windowManager.Close<IMainWindowViewModel>(this);
        }
    }
}
