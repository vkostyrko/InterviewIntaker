using AutoMapper;
using InterviewIntaker.Application.Task;
using InterviewIntaker.Contracts.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Task = InterviewIntaker.Domain.DomainModels.Task;

namespace InterviewIntaker.Api.Controllers
{
    /// <summary>
    /// Task controllers
    /// </summary>
    /// <param name="mediator">Mediator</param>
    /// <param name="mapper">Mapper</param>
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        /// <summary>
        /// Creates new task
        /// </summary>
        /// <param name="task">Task</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<TaskDto> CreateTask(CreateTaskRequest task)
        {
            var command = new CreateTaskCommand(task);
            Task result = await mediator.Send(command);
            return mapper.Map<TaskDto>(result);
        }

        /// <summary>
        /// Updates task status
        /// </summary>
        /// <param name="id">Task id</param>
        /// <param name="status">Task status</param>
        /// <returns></returns>
        [HttpPatch("{id}/status")]
        public async Task<TaskDto> UpdateTaskStatus(Guid id, TaskStatus status)
        {
            var command = new UpdateTaskCommand(id, status);
            var result = await mediator.Send(command);
            return mapper.Map<TaskDto>(result);
        }

        /// <summary>
        /// Gets all Tasks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IList<TaskDto>> GetAllTasks()
        {
            var command = new GetTasksCommand();
            var result = await mediator.Send(command);
            return mapper.Map<List<TaskDto>>(result);
        }
    }
}
