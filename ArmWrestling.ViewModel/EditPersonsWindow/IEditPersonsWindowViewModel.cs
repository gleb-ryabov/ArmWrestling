using ArmWrestling.Domain.Database;
using ArmWrestling.ViewModel.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.ViewModel.EditPersonsWindow
{
    public interface IEditPersonsWindowViewModel : IWindowViewModel
    {
        void Initialize(Person person);
    }
}
