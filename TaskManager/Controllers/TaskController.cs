using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Model;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    //controller for Managing Tasks

    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        // Dependency  Injected Usinng Constructor

        public TaskController(ITaskService taskService)
        {
            this._taskService = taskService;
        }

        //Method for wiew all the task present in the List
       
        [Authorize]
        [HttpGet("ViewAllTask")]
        public IActionResult ViewAllTasks()
        {
            try
            {
                return Ok(_taskService.ViewTask());
            }catch (Exception ex) {
                return Problem();
            }
        }

        //Method for view task by its unique Id

        [Authorize]
        [HttpGet("ViewTaskById")]
        public IActionResult ViewTaskById(int id)
        {
            try
            {
                if (id == 0||id>TaskService.tasks.Count)
                    return BadRequest();
                return Ok(_taskService.ViewTaskById(id));
            }catch (Exception ex)
            {
                return Problem();
            }
        }

        //Method for create new task 

        [Authorize]
        [HttpPost("CreateNewTask")]
        public IActionResult CreateTask(Tasks task)
        {
            try
            {
                if (task == null||task.Id==0)
                    return BadRequest();
                _taskService.CreateTask(task);
                return NoContent();
            }catch (Exception ex)
            {
                return Problem();
            }
        }

        //Method for Updating an existing task

        [Authorize]
        [HttpPut("UpdateExistingTask")]
        public IActionResult UpdateTask(Tasks task)
        {
            try
            {
                if (task == null)
                    return BadRequest();
                _taskService.UpdateTask(task);
                return NoContent();
            }catch (Exception ex)
            {
                return Problem();
            }
        }

        //Method for Deleting an particular task  

        [Authorize(Roles =Role.Admin)]
        [HttpDelete("DeleteTask")]
        public IActionResult DeleteTask(int id)
        {
            try
            {
                if (id == 0||id>TaskService.tasks.Count)
                    return BadRequest();
                _taskService.DeleteTask(id);
                return NoContent();
            }catch(Exception ex)
            {
                return Problem();
            }
        }
    }
}
