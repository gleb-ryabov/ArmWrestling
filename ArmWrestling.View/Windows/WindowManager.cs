using ArmWrestling.View.Factories;
using ArmWrestling.ViewModel.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.View.Windows
{
    internal class WindowManager : IWindowManager
    {
        private readonly IWindowFactory _windowFactory;
        private readonly Dictionary<IWindowViewModel, IWindow> _viewModelToWindowMap = new();

        public WindowManager(IWindowFactory windowFactory) 
        {
            _windowFactory = windowFactory;
        }

        public IWindow Show<TWindowViewModel>(TWindowViewModel windowViewModel) 
            where TWindowViewModel : IWindowViewModel
        {
            var window = _windowFactory.Create(windowViewModel);

            _viewModelToWindowMap.Add(windowViewModel, window);

            window.Show();

            return window;
        }

        public void Close<TWindowViewModel>(IWindowViewModel windowViewModel) 
            where TWindowViewModel : IWindowViewModel
        {
            if (_viewModelToWindowMap.TryGetValue(windowViewModel, out var window))
            {
                window.Close();

                _viewModelToWindowMap.Remove(windowViewModel);
            }
        }
    }
}
