using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Users.Microservice.Models;
using Users.Microservice.Rabbitmq;

namespace Users.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        private readonly IRabbitMQService _rabbit;

        public UsersController(IRabbitMQService rabbit)
        {
            _rabbit = rabbit;
        }

        [Route("[action]/{message}")]
        [HttpGet]
        public IActionResult SendMessage(string message)
        {
            _rabbit.SendMessage(message);
            return Ok("Отправлено");
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            return await _db.Users.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            var eventData = JsonSerializer.Serialize
                (
                    new
                    {
                        id = user.Id,
                        Fullname = user.Fullname
                    }
                );

            _rabbit.SendMessage(eventData);

            return Ok("Создано новый юзер");
        }
    }
}
