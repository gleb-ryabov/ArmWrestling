using ArmWrestling.Domain.Database;

namespace ArmWrestling.Applications.Services.TeamService
{
    public interface ITeamService
    {
        Team Create(string name, Competition competition);
        List<Team> GetByLastCompetiton();
    }
}