using Clerk_todolist_backend.Data;
using Clerk_todolist_backend.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task = Clerk_todolist_backend.Models.Task;

namespace Clerk_todolist_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public TaskController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Task>>> GetAllTasks()
        {
            var tasks = await _dataContext.Task.Include(s => s.Subtasks).ToListAsync();
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<ActionResult<TaskDTO>> CreateTask([FromBody] TaskDTO taskDTO)
        {
            if (taskDTO == null)
            {
                return BadRequest(taskDTO);
            }
            var task = new Task
            {
                Title = taskDTO.Title,
                Subtitle = taskDTO.Subtitle,
                Description = taskDTO.Description,
                Priority = taskDTO.Priority,
            };
            _dataContext.Task.Add(task);
            await _dataContext.SaveChangesAsync();

            return Ok(taskDTO);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskEditDTO taskDTO)
        {
            var task = await _dataContext.Task.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            task.Title = taskDTO.Title != null ? taskDTO.Title : task.Title;
            task.Subtitle = taskDTO.Subtitle != null ? taskDTO.Subtitle : task.Subtitle;
            task.Description = taskDTO.Description != null ? taskDTO.Description : task.Description;
            task.Priority = taskDTO.Priority;
            task.Done = taskDTO.Done;
            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error ocurred.");
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                var task = await _dataContext.Task.FindAsync(id);

                if(task == null)
                {
                    return NotFound();
                }
                _dataContext.Task.Remove(task);
                await _dataContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error ocurred.");
            }
        }
    }
}
