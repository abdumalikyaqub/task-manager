using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks.Microservice.Domain;
using Tasks.Microservice.Services;

namespace Tasks.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private TasksService tasksService = new TasksService();

        [HttpGet]
        public async Task<ActionResult<List<TaskModel>>> GetAll()
        {
            return await tasksService.GetTasks();
        }

        [HttpGet("Users")]
        public async Task<ActionResult<List<User>>> Users()
        {
            return await tasksService.GetUsers();
        }

        [HttpPost]
        public async Task<bool> AddTask(TaskModel task)
        {
            return await tasksService.AddTask(task);
        }
    }
}
