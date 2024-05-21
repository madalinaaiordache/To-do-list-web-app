using Todolist.Data;
using Todolist.Interfaces;
// using Todolist.Models;

namespace Todolist.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApiDbContext _context;
        public TaskRepository(ApiDbContext context)
        {
            _context = context;
        }

        public ICollection<Task> GetTasks()
        {
            return _context.Task.OrderBy(t => t.TaskID).ToList();
        }


        public Task GetTaskById(string id)
        {
            return _context.Task.FirstOrDefault(t => t.TaskID == id)!;
        }

        public void AddTask(Task task)
        {
            _context.Task.Add(task);
            _context.SaveChanges();
        }

        public void UpdateTask(Task task)
        {
            var existingTask = _context.Task.Find(task.TaskID);
            if (existingTask == null)
            {
                throw new ArgumentException("Task not found");
            }

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.DueDate = task.DueDate;
            existingTask.AppUserId = task.AppUserId;
            existingTask.PriorityID = task.PriorityID;

            _context.SaveChanges();
        }

        public void DeleteTask(string id)
        {
            var taskToDelete = _context.Task.Find(id);
            if (taskToDelete == null)
            {
                throw new ArgumentException("Task not found");
            }

            _context.Task.Remove(taskToDelete);
            _context.SaveChanges();
        }
    }

    
}