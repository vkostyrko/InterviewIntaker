using Task = InterviewIntaker.Domain.DomainModels.Task;

namespace InterviewIntaker.Data.Repositories
{
    /// <summary>
    /// Interface for TaskRepository
    /// </summary>
    public interface ITaskRepository
    {
        /// <summary>
        /// Creates new Task
        /// </summary>
        /// <param name="task">Task</param>
        /// <returns><see cref="Task"/></returns>
        Task<Task> Create(Task task);

        /// <summary>
        /// Updates Task status
        /// </summary>
        /// <param name="id">Task id</param>
        /// <param name="status">Task status</param>
        /// <returns><see cref="Task"/></returns>
        Task<Task> UpdateStatus(Guid id, TaskStatus status);

        /// <summary>
        /// Gets all Tasks
        /// </summary>
        /// <returns><see cref="IList&lt;Task&gt;"/></returns>
        Task<IList<Task>> GetAllTasks();
    }
}
