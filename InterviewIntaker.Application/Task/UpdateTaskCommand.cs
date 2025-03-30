using MediatR;

namespace InterviewIntaker.Application.Task
{
    /// <summary>
    /// Update task command
    /// </summary>
    /// <param name="id">Task id</param>
    /// <param name="status">Task status</param>
    public class UpdateTaskCommand(Guid id, TaskStatus status) : IRequest<Domain.DomainModels.Task>
    {
        /// <summary>
        /// Gets task id
        /// </summary>
        public Guid Id { get; } = id;

        /// <summary>
        /// Gets task status
        /// </summary>
        public TaskStatus Status { get; } = status;
    }
}
