using ArmWrestling.Domain.Database;

namespace ArmWrestling.Applications.Services.ResultTeamService
{
    public interface IResultTeamService
    {
        ResultTeam Create(Competition competition, Team team, int place);
        List<ResultTeam> GetResult(Competition competition);
    }
}