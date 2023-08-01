using Library2._2.Entities;
using Library2._2.Infrastructure;
using NodaTime;

namespace Library2._2.Commands.TestTaskCommands
{
    public class AddTaskCommand
    {
        //public DateTime DateTime { get; set; }
        public int Number { get; set; }

        private readonly ApplicationContext _context;
        public AddTaskCommand(ApplicationContext context)
        {
            _context = context;
        }

        public int Add(int number)  
        {
            var task = new TestTableHangfire();
            task.TimeAdd = DateTime.UtcNow; 
            task.Number = number;
            _context.Add(task);
            _context.SaveChanges();
            return task.Id;
        }
    }
}
