using Domain.Entities;
using Domain.Infrastructure;
using Library2._2.Interfaces.TaskInterfaces;

namespace Library2._2.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationContext _context;

        public TaskService(ApplicationContext context)
        {
            _context = context;
        }

        public int Add(DateTime dateTime, int number)
        {
            var task = new TestTableHangfire();
            task.TimeAdd = DateTime.Now;
            task.Number = number;
            _context.Add(task);
            _context.SaveChanges();
            return task.Id; 
        }

        public void Delete(int taskId)
        {
            var task = _context.TestTableHangfires.SingleOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                _context.Remove(task);
            }
        }
    }
}
