using System.Data.Entity;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Todolist.Data;
using Todolist.Interfaces;
// using Todolist.Models;

namespace Todolist.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApiDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public UserRepository(ApiDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public ICollection<AppUser> GetUsers()
        {
            return _userManager.Users.ToList();
        
        } 


        public AppUser GetUserById(string userId)
        {
            return _userManager.FindByIdAsync(userId).Result!;     
        }

        public void AddUser(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(AppUser user)
        {
            _userManager.UpdateAsync(user);
            _context.SaveChanges();
        }

       public async void DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
                await _context.SaveChangesAsync();
            }
        }

        // public User GetUserById(string userId)
        // {
        //     throw new NotImplementedException();
        // }

        // public void DeleteUser(string userId)
        // {
        //     throw new NotImplementedException();
        // }
    }
}