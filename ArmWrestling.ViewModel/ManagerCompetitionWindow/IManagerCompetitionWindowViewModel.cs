using ArmWrestling.Domain.Database;
using ArmWrestling.ViewModel.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.ViewModel.ManagerCompetitionWindow
{
    public interface IManagerCompetitionWindowViewModel: IWindowViewModel
    {
        void Initialize();
        void UpdateTable(Table newTable);
    }
}
