using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Models
{
    public class TaskDetail
    {
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public string? Title {  get; set; }
        public string? Description { get; set; }
        public  string? DueDate { get; set; }

        public string? Status { get; set; }

    }

}



