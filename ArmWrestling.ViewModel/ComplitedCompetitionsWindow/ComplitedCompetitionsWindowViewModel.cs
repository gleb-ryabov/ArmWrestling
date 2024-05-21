using ArmWrestling.ViewModel.Commands;
using ArmWrestling.ViewModel.EditPersonsWindow;
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

        private readonly Command _closeWindowCommand;
        public ICommand CloseWindowCommand => _closeWindowCommand;

        public ComplitedCompetitionsWindowViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;

            _closeWindowCommand = new Command(CloseWindow);
        }


        //Function for close window
        private void CloseWindow()
        {
            _windowManager.Close<ISelectTableCategoriesWindowViewModel>(this);
        }
    }
}
