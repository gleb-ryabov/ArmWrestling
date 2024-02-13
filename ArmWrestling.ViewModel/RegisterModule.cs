using ArmWrestling.ViewModel.StartWindow;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.ViewModel
{
    public class RegisterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>().InstancePerDependency();
        }
    }
}
