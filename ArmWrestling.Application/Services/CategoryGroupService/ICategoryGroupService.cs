using ArmWrestling.Domain.Database;

namespace ArmWrestling.Applications.Services.CategoryGroupService
{
    public interface ICategoryGroupService
    {
        CategoryGroup Create(string name);
    }
}