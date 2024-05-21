namespace Todolist.Interfaces
{
    public interface IPriorityRepository
    {
        ICollection<Priority> GetPriorities();
        Priority GetPriorityById(string id);
        void AddPriority(Priority priority);
        void UpdatePriority(Priority priority);
        void DeletePriority(string id);
    }
}