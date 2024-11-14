using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniTask_API.Dtos;
using MiniTask_API.Models;
using MiniTask_API.Repository;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MiniTask_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetTasks()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var tasks = await _taskRepository.GetTasksByUserAsync(userId);

            var taskDtos = tasks.Select(t => new TaskItemDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                DueDate = t.DueDate,
                Status = t.Status,
                CategoryId = t.CategoryId
            }).ToList();

            return Ok(taskDtos);
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemDto>> GetTask(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var task = await _taskRepository.GetTaskByIdAsync(id, userId);

            if (task == null)
            {
                return NotFound();
            }

            var taskDto = new TaskItemDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Status = task.Status,
                CategoryId = task.CategoryId
            };

            return Ok(taskDto);
        }

        // POST: api/Tasks
        [HttpPost]
        public async Task<ActionResult<TaskItemDto>> CreateTask([FromBody] TaskItemDto taskDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var task = new TaskItem
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate,
                Status = taskDto.Status,
                CategoryId = taskDto.CategoryId,
                UserId = userId
            };

            await _taskRepository.CreateTaskAsync(task);

            taskDto.Id = task.Id;
            return CreatedAtAction(nameof(GetTask), new { id = taskDto.Id }, taskDto);
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskItemDto taskDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id != taskDto.Id)
            {
                return BadRequest("Task ID mismatch");
            }

            var task = await _taskRepository.GetTaskByIdAsync(id, userId);
            if (task == null)
            {
                return NotFound("Task not found");
            }

            task.Title = taskDto.Title;
            task.Description = taskDto.Description;
            task.DueDate = taskDto.DueDate;
            task.Status = taskDto.Status;
            task.CategoryId = taskDto.CategoryId;

            await _taskRepository.UpdateTaskAsync(task);

            return NoContent();
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var task = await _taskRepository.GetTaskByIdAsync(id, userId);

            if (task == null)
            {
                return NotFound("Task not found");
            }

            await _taskRepository.DeleteTaskAsync(task);
            return NoContent();
        }
    }
}