using ArmWrestling.Domain.Database;
using ArmWrestling.ViewModel.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.ViewModel.RegistrationOfPersonsWindow
{
    public interface IRegistrationOfPersonsWindowViewModel : IWindowViewModel
    {
        public void Initialize();
        void RegisterUser();
        void CompleteRegistration();
    }
}
