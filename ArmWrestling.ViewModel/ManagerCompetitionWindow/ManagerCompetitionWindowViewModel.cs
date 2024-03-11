using ArmWrestling.Applications.Services.PersonService;
using ArmWrestling.ViewModel.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.ViewModel.ManagerCompetitionWindow
{
    public class ManagerCompetitionWindowViewModel : IManagerCompetitionWindowViewModel
    {
        private readonly IWindowManager _windowManager;
        private readonly IPersonService _personService;

        public ManagerCompetitionWindowViewModel(IWindowManager windowManager,
            IPersonService personService)
        {
            _windowManager = windowManager;

            _personService = personService;
        }
    }
}
