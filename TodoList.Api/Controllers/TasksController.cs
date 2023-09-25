using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Repositories;
using TodoList.Models;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        //api/tasks?name=
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TaskListSearch taskListSearch)
        {
            var tasks = await _taskRepository.GetTaskList(taskListSearch);
            tasks.Select(x => new TaskDto()
            {
                Id = x.Id,
                Name = x.Name,
                AssigneeId = x.AssigneeId,
                AssigneeName = x.Assignee!=null ? x.Assignee.FirstName + " " + x.Assignee.LastName : "N/A",
                CreatedDate = x.CreatedDate,
                Priority = x.Priority,
                Status = x.Status
            });
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var task = await _taskRepository.Create(new Entities.Task()
            {
                Id = request.Id,
                Name = request.Name,
                Priority = request.Priority,
                Status = Models.Enums.Status.Open,
                CreatedDate = DateTime.Now,
            });
            return CreatedAtAction(nameof(GetById), new { id = request.Id }, task);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, TaskUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var taskFromDb = await _taskRepository.GetById(id);

            if (taskFromDb == null)
            {
                return NotFound($"{id} is not found");
            }

            taskFromDb.Name = request.Name;
            taskFromDb.Priority = request.Priority;

            try
            {
                await _taskRepository.Update(taskFromDb);
            }
            catch (System.Data.SqlTypes.SqlNullValueException e)
            {
                return BadRequest(e.Message);
            }

            var taskDto = new TaskDto()
            {
                AssigneeId = taskFromDb.AssigneeId,
                CreatedDate = taskFromDb.CreatedDate,
                Id = taskFromDb.Id,
                Name = taskFromDb.Name,
                Priority = taskFromDb.Priority,
                Status = taskFromDb.Status,
            };

            return Ok(taskDto);
        }


        //api/task/xxxx
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var task = await _taskRepository.GetById(id);
            if (task == null) return NotFound($"{id} is not found");
            return Ok(task);
        }

    }
}
