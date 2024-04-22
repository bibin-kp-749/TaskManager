using Microsoft.AspNetCore.Mvc;
using TaskManager.Model;

namespace TaskManager.Services
{
    //Interface for Manage Tasks

    public interface ITaskService
    {
        public IEnumerable<Tasks> ViewTask();
        public void CreateTask(Tasks task);
        public void UpdateTask(Tasks task);
        public void DeleteTask(int id);
        public Tasks ViewTaskById(int id);
    }
}
