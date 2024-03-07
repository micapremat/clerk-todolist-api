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
    }
}
