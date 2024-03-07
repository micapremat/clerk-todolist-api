using System.ComponentModel.DataAnnotations.Schema;

namespace Clerk_todolist_backend.Models
{
    public class Subtask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Subtitle { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; } = false;
        public int TaskId { get; set; }
        public virtual Task Task { get; set; }
    }
}
