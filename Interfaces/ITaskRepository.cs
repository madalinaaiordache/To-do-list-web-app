namespace Todolist.Interfaces
{
    public interface ITaskRepository
    {
        ICollection<Task> GetTasks();
        Task GetTaskById(string id);
        void AddTask(Task task);
        void UpdateTask(Task task);
        void DeleteTask(string id);
    }
}