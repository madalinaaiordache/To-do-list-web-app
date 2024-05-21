namespace Todolist.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategoryById(string id);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(string id);
    }
}