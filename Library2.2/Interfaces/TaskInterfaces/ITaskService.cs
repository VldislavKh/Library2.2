namespace Library2._2.Interfaces.TaskInterfaces
{
    public interface ITaskService
    {
        public int Add(DateTime dateTime, int number);

        public void Delete(int id);
    }
}
