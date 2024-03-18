using ArmWrestling.Domain.Database;

namespace ArmWrestling.Applications.Services.CategoryInCompetitionService
{
    public interface ICategoryInCompetitionService
    {
        CategoryInCompetition Create(Category category, Competition competition);
        List<CategoryInCompetition> GetNotUsedCategories(Competition competition);
    }
}