using Todolist.Data;
using Todolist.Interfaces;
// using Todolist.Models;

namespace Todolist.Repository
{
    public class PriorityRepository : IPriorityRepository
    {
        private readonly ApiDbContext _context;
        public PriorityRepository(ApiDbContext context)
        {
            _context = context;
        }

        public ICollection<Priority> GetPriorities()
        {
            return _context.Priority.OrderBy(u => u.PriorityID).ToList();
        }


        public Priority GetPriorityById(string id)
        {
            return _context.Priority.FirstOrDefault(p => p.PriorityID == id)!;
        }

        public void AddPriority(Priority priority)
        {
            _context.Priority.Add(priority);
            _context.SaveChanges();
        }

        public void UpdatePriority(Priority priority)
        {
            _context.Priority.Update(priority);
            _context.SaveChanges();
        }

        public void DeletePriority(string id)
        {
            var priority = _context.Priority.FirstOrDefault(p => p.PriorityID == id);
            if (priority != null)
            {
                _context.Priority.Remove(priority);
                _context.SaveChanges();
            }
        }
    }
}