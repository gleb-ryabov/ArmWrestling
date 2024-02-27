using ArmWrestling.Domain.Database;

namespace ArmWrestling.Applications.Services.CompetitionService
{
    public interface ICompetitionService
    {
        Competition Create(string typeJudging, string typeCompetition, byte countTable, char firstArm);
    }
}