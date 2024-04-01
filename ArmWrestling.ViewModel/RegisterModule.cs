using ArmWrestling.ViewModel.CreateCompetitionWindow;
using ArmWrestling.ViewModel.MainWindow;
using ArmWrestling.ViewModel.ManagerCompetitionWindow;
using ArmWrestling.ViewModel.ParticipantListWindow;
using ArmWrestling.ViewModel.RegistrationOfPersonsWindow;
using ArmWrestling.ViewModel.ResultsCompetitionWindow;
using ArmWrestling.ViewModel.SelectTableCategoriesWindow;
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
            builder.RegisterType<CreateCompetitionWindowViewModel>().As<ICreateCompetitionWindowViewModel>().InstancePerDependency();
            builder.RegisterType<RegistrationOfPersonsWindowViewModel>().As<IRegistrationOfPersonsWindowViewModel>().InstancePerDependency();
            builder.RegisterType<ManagerCompetitionWindowViewModel>().As<IManagerCompetitionWindowViewModel>().SingleInstance();
            builder.RegisterType<SelectTableCategoriesWindowViewModel>().As<ISelectTableCategoriesWindowViewModel>().InstancePerDependency();
            builder.RegisterType<ParticipantListWindowViewModel>().As<IParticipantListWindowViewModel>().InstancePerDependency();
            builder.RegisterType<ResultsCompetitionWindowViewModel>().As<IResultsCompetitionWindowViewModel>().InstancePerDependency();
        }
    }
}
