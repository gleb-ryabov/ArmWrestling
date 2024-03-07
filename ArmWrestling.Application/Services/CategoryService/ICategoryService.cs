using ArmWrestling.Domain.Database;

namespace ArmWrestling.Applications.Services.CategoryService
{
    public interface ICategoryService
    {
        Category Create(byte gender, byte minAge, byte maxAge, byte maxWeight, CategoryGroup categoryGroup);
        string CreateCategoryName(Category category);
    }
}