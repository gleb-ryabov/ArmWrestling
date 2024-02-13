using ArmWrestling.View.Factories;
using ArmWrestling.View.StartWindow;
using ArmWrestling.ViewModel.StartWindow;
using ArmWrestling.ViewModel.Windows;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Bootstrapper.Factories
{
    public class WindowFactory : IWindowFactory
    {
        private readonly IComponentContext _componentContext;

        //compliance card
        private readonly Dictionary<Type, Type> _map = new()
        {
        {typeof(IMainWindowViewModel), typeof(IMainWindow) }
        };

        public WindowFactory(IComponentContext componentContext) 
        {
            _componentContext = componentContext;
        }

        public IWindow Create<TWindowViewModel>(TWindowViewModel viewModel) where TWindowViewModel : IWindowViewModel
        {

            if (!_map.TryGetValue(typeof(TWindowViewModel), out var windowType))
            {
                throw new InvalidOperationException(
                    $"There is no window registered for {typeof(TWindowViewModel)}");
            }

            var instance = _componentContext.Resolve(windowType, TypedParameter.From(viewModel));

            return (IWindow)instance;

        }
    }
}
