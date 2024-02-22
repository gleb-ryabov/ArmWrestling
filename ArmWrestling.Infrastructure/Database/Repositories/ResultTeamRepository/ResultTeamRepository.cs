﻿using ArmWrestling.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Infrastructure.Database.Repositories.ResultTeamRepository
{
    public class ResultTeamRepository : IResultTeamRepository
    {
        private readonly ApplicationContext _applicationContext;
        public ResultTeamRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public bool Create(ResultTeam entity)
        {
            _applicationContext.ResultTeams.Add(entity);

            return _applicationContext.SaveChanges() > 0;
        }

        public bool Delete(ResultTeam entity)
        {
            _applicationContext.ResultTeams.Remove(entity);

            return _applicationContext.SaveChanges() > 0;
        }

        public ResultTeam Get(int id)
        {
            return _applicationContext.ResultTeams.Find(id);
        }

        public IEnumerable<ResultTeam> GetAll()
        {
            return _applicationContext.ResultTeams.ToList();
        }
    }
}
