using ArmWrestling.Applications.Services.PersonService;
using ArmWrestling.Applications.Services.ResultPersonService;
using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryInCompetitionRepository;
using ArmWrestling.Infrastructure.Database.Repositories.PersonRepository;
using ArmWrestling.Infrastructure.Database.Repositories.ResultTeamRepository;
using ArmWrestling.Infrastructure.Database.Repositories.TeamRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Applications.Services.ResultTeamService
{
    public class ResultTeamService : IResultTeamService
    {
        private readonly IResultTeamRepository _resultTeamRepository;
        private readonly ICategoryInCompetitionRepository _categoryInCompetitionRepository;
        private readonly IResultPersonService _resultPersonService;
        private readonly ITeamRepository _teamRepository;
        private readonly IPersonRepository _personRepository;

        public ResultTeamService(IResultTeamRepository resultTeamRepository,
            ICategoryInCompetitionRepository categoryInCompetitionRepository,
            IResultPersonService resultPersonService,
            ITeamRepository teamRepository,
            IPersonRepository personRepository)
        {
            _resultTeamRepository = resultTeamRepository;
            _categoryInCompetitionRepository = categoryInCompetitionRepository;
            _resultPersonService = resultPersonService;
            _teamRepository = teamRepository;
            _personRepository = personRepository;
        }
        public ResultTeam Create(Competition competition, Team team, int place)
        {
            ResultTeam resultTeam = new ResultTeam()
            {
                CompetitionId = competition.Id,
                TeamId = team.Id,
                Place = place
            };
            if (_resultTeamRepository.Create(resultTeam))
                return resultTeam;
            else
                return null;
        }

        public List<ResultTeam> GetResult(Competition competition)
        {
            //check exist result teams
            if(!_resultTeamRepository.ExistResult(competition.Id))
            {
                //if result is not exist - create it
                SetResult(competition);
            }
            List <ResultTeam> resultTeams = 
                _resultTeamRepository.GetResulByCompetition(competition.Id).ToList();
            
            //set team on result
            foreach (ResultTeam result in resultTeams)
            {
                if (result.Team == null)
                    result.Team = _teamRepository.Get(result.TeamId);
            }
            
            return resultTeams;
        }

        private void SetResult(Competition competition)
        {
            //get list categories in competition
            List<CategoryInCompetition> categories = 
                _categoryInCompetitionRepository.GetByCompetition(competition).ToList();

            List<Team> teams = _teamRepository.GetByCompetition(competition.Id).ToList();

            foreach (CategoryInCompetition category in categories)
            {
                //get results persons and teams in this category
                List<ResultPerson> resultsPerson = _resultPersonService.GetResult(category).ToList();
                
                //add score person to teams
                foreach (ResultPerson result in resultsPerson)
                {
                    Person person = _personRepository.Get(result.PersonId);
                    foreach (Team team in teams)
                    {
                        if (team.Id == person.TeamId)
                        {
                            //team.Score += person.Score;
                            //save score in database
                            _teamRepository.AddScore(team, person.Score);
                        }
                    }
                }
            }

            //create results Teams 
            List<Team> sortedTeams = _teamRepository.GetOrderByScore(competition.Id).ToList();
            int place = 0;
            int score = 100000;
            foreach (Team team in sortedTeams)
            {
                if (team.Score < score)
                {
                    place++;
                    this.Create(competition, team, place);
                }
                else
                    this.Create(competition, team, place);
                
                score = team.Score;
            }
        }



    }
}
