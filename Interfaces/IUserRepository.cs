namespace Todolist.Interfaces
{
    public interface IUserRepository
    {
        ICollection<AppUser> GetUsers();

        AppUser GetUserById(string userId);
        void AddUser(User user);
        void UpdateUser(AppUser user);
        void DeleteUser(string userId);
    }
}