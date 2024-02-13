using ArmWrestling.View.StartWindow;
using ArmWrestling.ViewModel.StartWindow;
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
                .RegisterModule<ViewModel.RegisterModule>();

            _container = containerBuilder.Build();
        }

        public Window Run()
        {
            var mainWindow = _container.Resolve<IMainWindow>();

            if (mainWindow is not Window window)
            {
                throw new NotImplementedException();
            }

            window.Show();

            return window;


        }
        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
