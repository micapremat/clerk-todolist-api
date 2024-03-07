using Clerk_todolist_backend.Models;

namespace Clerk_todolist_backend.DTO
{
    public class TaskEditDTO
    {
        public string? Title { get; set; }
        public string? Subtitle { get; set; }
        public string? Description { get; set; }
        public bool Done { get; set; }
        public TaskType Priority { get; set; }
    }
}
