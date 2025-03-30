using InterviewIntaker.Contracts.Tasks;
using MediatR;

namespace InterviewIntaker.Application.Task
{
    /// <summary>
    /// Create task command
    /// </summary>
    /// <param name="task">Task to create</param>
    public class CreateTaskCommand(CreateTaskRequest task) : IRequest<Domain.DomainModels.Task>
    {
        /// <summary>
        /// Gets task
        /// </summary>
        public CreateTaskRequest Task { get; } = task;
    }
}
