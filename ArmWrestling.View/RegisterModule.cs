using ArmWrestling.View.StartWindow;
using ArmWrestling.ViewModel.StartWindow;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.View
{
    public class RegisterModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<MainWindow>().As<IMainWindow>().InstancePerDependency();

        }
    }
}
