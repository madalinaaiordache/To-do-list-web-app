namespace Todolist.Interfaces
{
    public interface IRoleRepository
    {
        ICollection<Role> GetRoles();
        Role GetRoleById(string id);
        void AddRole(Role role);
        void UpdateRole(Role role);
        void DeleteRole(string id);
    }
}