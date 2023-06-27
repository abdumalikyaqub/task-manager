using Microsoft.EntityFrameworkCore;
using Tasks.Microservice.Domain;
using Tasks.Microservice.Infrastructure;

namespace Tasks.Microservice.Services
{
    public class TasksService
    {
        public ApplicationDbContext _dbContext = new ApplicationDbContext();
        /*public TasksService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }*/
        public async Task<List<TaskModel>> GetTasks()
        {
            var tasks = await _dbContext.Tasks.ToListAsync();
            return tasks;
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _dbContext.Users.ToListAsync();
            return users;
        }

        public async Task<bool> AddTask(TaskModel task)
        {
            var item = _dbContext.Tasks.ToList().Find(t => t.Id == task.Id);
            if (item != null)
            {
                Console.WriteLine("We have");
            }
            else
            {
                _dbContext.Tasks.Add(task);
                await _dbContext.SaveChangesAsync();
                return true;
            }
           
            return false;
            
        }

        public async Task<bool> AddUser(User user)
        {
            var item = _dbContext.Tasks.ToList().Find(u => u.Id == user.Id);
            if (item != null)
            {
                Console.WriteLine("We have");
            }
            else
            {
                _dbContext.Users.Add(
                    new User
                    {
                        UserID = user.Id,
                        Fullname = user.Fullname
                    }
                    );
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;

        }
    }
}
