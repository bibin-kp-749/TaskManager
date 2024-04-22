using TaskManager.Model;

namespace TaskManager.Services
{
    //Inherited Class of ITaskService Interface
    public class TaskService:ITaskService
    {
        public static List<Tasks> tasks = new List<Tasks>
      {
          new Tasks{Id=1,Title="cover react js",Description="complete in 4 days",Status="satrted"},
          new Tasks{Id=2,Title="cover React-redux also",Description="complete in 2 days",Status="pending"}
      };
        public IEnumerable<Tasks> ViewTask()
        {
            return tasks;
        }
        public Tasks ViewTaskById(int id)
        {
            var task=tasks.FirstOrDefault(t => t.Id == id);
            return task;
        }
        public void CreateTask(Tasks task)
        {
            tasks.Add(new Tasks { Id = tasks.Count + 1, Title = task.Title, Description = task.Description, Status = task.Status });
        }
        public void UpdateTask(Tasks task)
        {
            var result=tasks.FirstOrDefault(s => s.Id == task.Id);
            result.Title= task.Title;
            result.Description= task.Description;
            result.Status= task.Status;
        }
        public void DeleteTask(int id)
        {
            tasks.RemoveAll(tasks=>tasks.Id==id);
        }
    }
}
