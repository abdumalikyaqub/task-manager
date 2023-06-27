using System.ComponentModel.DataAnnotations;

namespace Tasks.Microservice.Domain
{
    public class TaskModel
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Title { get; set; }
        public string? Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
