using ArmWrestling.Domain.Database;
using ArmWrestling.ViewModel.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.ViewModel.ResultsCompetitionWindow
{
    public interface IResultsCompetitionWindowViewModel : IWindowViewModel
    {
        void Initialize(Competition competition);
    }
}
