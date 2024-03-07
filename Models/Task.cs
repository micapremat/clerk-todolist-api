using System.ComponentModel.DataAnnotations.Schema;

namespace Clerk_todolist_backend.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Subtitle { get; set; }
        public string Description { get; set; }
        public TaskType Priority { get; set; }
        public bool Done { get; set; } = false;
        public virtual List<Subtask>? Subtasks { get; set; }

    }
}
