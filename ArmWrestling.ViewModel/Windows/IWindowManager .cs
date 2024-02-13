using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.ViewModel.Windows
{
    public interface IWindowManager
    {
        IWindow Show<TWindowViewModel>(TWindowViewModel windowViewModel)
            where TWindowViewModel : IWindowViewModel;
        void Close<TWindowViewModel>(IWindowViewModel windowViewModel) 
            where TWindowViewModel : IWindowViewModel;
    }
}
