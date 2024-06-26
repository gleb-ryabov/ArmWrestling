﻿using ArmWrestling.View.ComplitedCompetitionsWindow;
using ArmWrestling.View.CreateCompetitionWindow;
using ArmWrestling.View.EditPersonsWindow;
using ArmWrestling.View.MainWindow;
using ArmWrestling.View.ManagerCompetitionWindow;
using ArmWrestling.View.ManagerTeamsWindow;
using ArmWrestling.View.ParticipantListWindow;
using ArmWrestling.View.RegistrationOfPersonsWindow;
using ArmWrestling.View.ResultsCompetitionWindow;
using ArmWrestling.View.SelectTableCategoriesWindow;
using ArmWrestling.View.Windows;
using ArmWrestling.ViewModel.ComplitedCompetitionsWindow;
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
            builder.RegisterType<ComplitedCompetitionsWindow.ComplitedCompetitionsWindow>().As<IComplitedCompetitionsWindow>().InstancePerDependency();
            builder.RegisterType<CreateCompetitionWindow.CreateCompetitionWindow>().As<ICreateCompetitionWindow>().InstancePerDependency();
            builder.RegisterType<ManagerTeamsWindow.ManagerTeamsWindow>().As<IManagerTeamsWindow>().InstancePerDependency();
            builder.RegisterType<RegistrationOfPersonsWindow.RegistrationOfPersonsWindow>().As<IRegistrationOfPersonsWindow>().InstancePerDependency();
            builder.RegisterType<ManagerCompetitionWindow.ManagerCompetitionWindow>().As<IManagerCompetitionWindow>().InstancePerDependency();
            builder.RegisterType<SelectTableCategoriesWindow.SelectTableCategoriesWindow>().As<ISelectTableCategoriesWindow>().InstancePerDependency();
            builder.RegisterType<ParticipantListWindow.ParticipantListWindow>().As<IParticipantListWindow>().InstancePerDependency();
            builder.RegisterType<EditPersonsWindow.EditPersonsWindow>().As<IEditPersonsWindow>().InstancePerDependency();
            builder.RegisterType<ResultsCompetitionWindow.ResultsCompetitionWindow>().As<IResultsCompetitionWindow>().InstancePerDependency();
            builder.RegisterType<WindowManager>().As<IWindowManager>().SingleInstance();

        }
    }
}
