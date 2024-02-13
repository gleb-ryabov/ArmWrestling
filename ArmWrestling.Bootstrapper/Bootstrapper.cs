using ArmWrestling.View.StartWindow;
using ArmWrestling.ViewModel.StartWindow;
using ArmWrestling.ViewModel.Windows;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ArmWrestling.Bootstrapper
{
    public class Bootstrapper : IDisposable
    {
        private IContainer _container;

        public Bootstrapper()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterModule<View.RegisterModule>()
                .RegisterModule<ViewModel.RegisterModule>()
                .RegisterModule<RegisterModule>();

            _container = containerBuilder.Build();
        }

        public Window Run()
        {
            var mainWindowViewModel = _container.Resolve<IMainWindowViewModel>();
            var windowManager = _container.Resolve<IWindowManager>();

            var mainWindow = windowManager.Show(mainWindowViewModel);

            if (mainWindow is not Window window)
            {
                throw new NotImplementedException();
            }

            return window;
        }
        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
