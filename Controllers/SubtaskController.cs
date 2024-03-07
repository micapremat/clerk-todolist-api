using Clerk_todolist_backend.Data;
using Clerk_todolist_backend.DTO;
using Clerk_todolist_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Collections;

namespace Clerk_todolist_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubtaskController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public SubtaskController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        public async Task<ActionResult<SubtaskDTO>> CreateSubtask([FromBody] SubtaskDTO subtaskDTO)
        {
            if (subtaskDTO == null)
            {
                return BadRequest(subtaskDTO);
            } else
            {
                var subtask = new Subtask
                {
                    TaskId = subtaskDTO.TaskId,
                    Title = subtaskDTO.Title,
                    Subtitle = subtaskDTO.Subtitle,
                    Description = subtaskDTO.Description,
                };
                var task = await _dataContext.Task.FirstOrDefaultAsync(task => task.Id == subtaskDTO.TaskId);
                if (task != null)
                {
                    if (task.Subtasks == null)
                    {
                        task.Subtasks = new List<Subtask>();
                    }
                        task.Subtasks.Add(subtask);
                   
                    await _dataContext.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest();

            }

        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] SubtaskEditDTO subtaskEditDTO)
        {
            var subtaskEdit = await _dataContext.Subtask.FindAsync(id);
            if (subtaskEdit == null)
            {
                return NotFound();
            } else
            {
                subtaskEdit.Title = subtaskEditDTO.Title != null ? subtaskEditDTO.Title : subtaskEdit.Title;
                subtaskEdit.Subtitle = subtaskEditDTO.Subtitle != null ? subtaskEditDTO.Subtitle : subtaskEdit.Subtitle;
                subtaskEdit.Description = subtaskEditDTO.Description != null ? subtaskEditDTO.Description : subtaskEdit.Description;
                subtaskEdit.Done = subtaskEditDTO.Done;
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
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                var subtask = await _dataContext.Subtask.FindAsync(id);

                if (subtask == null)
                {
                    return NotFound();
                }
                _dataContext.Subtask.Remove(subtask);
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
