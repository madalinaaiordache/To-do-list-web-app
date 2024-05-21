using Todolist.Data;
using Todolist.Interfaces;
// using Todolist.Models;

namespace Todolist.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApiDbContext _context;
        public RoleRepository(ApiDbContext context)
        {
            _context = context;
        }

        public ICollection<Role> GetRoles()
        {
            return _context.Role.OrderBy(u => u.RoleID).ToList();
        }



        public Role GetRoleById(string id)
        {
            return _context.Role.FirstOrDefault(r => r.RoleID == id)!;
        }

        public void AddRole(Role role)
        {
            _context.Role.Add(role);
            _context.SaveChanges();
        }

        public void UpdateRole(Role role)
        {
            _context.Role.Update(role);
            _context.SaveChanges();
        }

        public void DeleteRole(string id)
        {
            var role = _context.Role.FirstOrDefault(r => r.RoleID == id);
            if (role != null)
            {
                _context.Role.Remove(role);
                _context.SaveChanges();
            }
        }
    }
}