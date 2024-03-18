using ArmWrestling.Applications.Services.CategoryGroupService;
using ArmWrestling.Applications.Services.CategoryInCompetitionService;
using ArmWrestling.Applications.Services.CategoryService;
using ArmWrestling.Applications.Services.CompetitionService;
using ArmWrestling.Applications.Services.DuelService;
using ArmWrestling.Applications.Services.PersonService;
using ArmWrestling.Applications.Services.ResultPersonService;
using ArmWrestling.Applications.Services.ResultTeamService;
using ArmWrestling.Applications.Services.TableService;
using ArmWrestling.Applications.Services.TeamService;
using ArmWrestling.Infrastructure.Database.Repositories.TeamRepository;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Applications
{
    public class RegisterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<CategoryGroupService>().As<ICategoryGroupService>().InstancePerDependency();
            builder.RegisterType<CategoryInCompetitionService>().As<ICategoryInCompetitionService>().InstancePerDependency();
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerDependency();
            builder.RegisterType<CompetitionService>().As<ICompetitionService>().InstancePerDependency();
            builder.RegisterType<DuelService>().As<IDuelService>().InstancePerDependency();
            builder.RegisterType<PersonService>().As<IPersonService>().InstancePerDependency();
            builder.RegisterType<ResultPersonService>().As<IResultPersonService>().InstancePerDependency();
            builder.RegisterType<ResultTeamService>().As<IResultTeamService>().InstancePerDependency();
            builder.RegisterType<TeamService>().As<ITeamService>().InstancePerDependency();
            builder.RegisterType<TableService>().As<ITableService>().InstancePerDependency();
        }
    }
}
