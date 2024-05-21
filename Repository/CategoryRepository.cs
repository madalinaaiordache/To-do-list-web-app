using Todolist.Data;
using Todolist.Interfaces;
// using Todolist.Models;

namespace Todolist.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApiDbContext _context;
        public CategoryRepository(ApiDbContext context)
        {
            _context = context;
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Category.OrderBy(u => u.CategoryID).ToList();
        }


        public Category GetCategoryById(string id)
        {
            return _context.Category.FirstOrDefault(c => c.CategoryID == id)!;
        }

        public void AddCategory(Category category)
        {
            _context.Category.Add(category);
            _context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            _context.Category.Update(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(string id)
        {
            var category = _context.Category.FirstOrDefault(c => c.CategoryID == id);
            if (category != null)
            {
                _context.Category.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}