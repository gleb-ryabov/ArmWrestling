using ArmWrestling.View.CreateCompetitionWindow;
using ArmWrestling.View.MainWindow;
using ArmWrestling.View.ManagerCompetitionWindow;
using ArmWrestling.View.RegistrationOfPersonsWindow;
using ArmWrestling.View.Windows;
using ArmWrestling.ViewModel.MainWindow;
using ArmWrestling.ViewModel.Windows;
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

            builder.RegisterType<MainWindow.MainWindow>().As<IMainWindow>().InstancePerDependency();
            builder.RegisterType<CreateCompetitionWindow.CreateCompetitionWindow>().As<ICreateCompetitionWindow>().InstancePerDependency();
            builder.RegisterType<RegistrationOfPersonsWindow.RegistrationOfPersonsWindow>().As<IRegistrationOfPersonsWindow>().InstancePerDependency();
            builder.RegisterType<ManagerCompetitionWindow.ManagerCompetitionWindow>().As<IManagerCompetitionWindow>().InstancePerDependency();
            builder.RegisterType<WindowManager>().As<IWindowManager>().SingleInstance();

        }
    }
}
