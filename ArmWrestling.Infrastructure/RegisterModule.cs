using ArmWrestling.Infrastructure.Database;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryGroupRepository;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryInCompetitionRepository;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryRepository;
using ArmWrestling.Infrastructure.Database.Repositories.CompetitionReposirory;
using ArmWrestling.Infrastructure.Database.Repositories.DuelRepository;
using ArmWrestling.Infrastructure.Database.Repositories.PersonRepository;
using ArmWrestling.Infrastructure.Database.Repositories.ResultPersonRepository;
using ArmWrestling.Infrastructure.Database.Repositories.ResultTeamRepository;
using ArmWrestling.Infrastructure.Database.Repositories.TableRepository;
using ArmWrestling.Infrastructure.Database.Repositories.TeamRepository;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Infrastructure
{
    public class RegisterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ApplicationContext>().InstancePerDependency();
            builder.RegisterType<TeamRepository>().As<ITeamRepository>().InstancePerDependency();
            builder.RegisterType<TableRepository>().As<ITableRepository>().InstancePerDependency();
            builder.RegisterType<ResultTeamRepository>().As<IResultTeamRepository>().InstancePerDependency();
            builder.RegisterType<ResultPersonRepository>().As<IResultPersonRepository>().InstancePerDependency();
            builder.RegisterType<PersonRepository>().As<IPersonRepository>().InstancePerDependency();
            builder.RegisterType<DuelRepository>().As<IDuelRepository>().InstancePerDependency();
            builder.RegisterType<CompetitionRepository>().As<ICompetitionRepository>().InstancePerDependency();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>().InstancePerDependency();
            builder.RegisterType<CategoryInCompetitionRepository>().As<ICategoryInCompetitionRepository>().InstancePerDependency();
            builder.RegisterType<CategoryGroupRepository>().As<ICategoryGroupRepository>().InstancePerDependency();
        }
    }
}
