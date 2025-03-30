using AutoMapper;
using InterviewIntaker.Data.DbContext;
using InterviewIntaker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Task = InterviewIntaker.Domain.DomainModels.Task;

namespace InterviewIntaker.Data.Repositories
{
    /// <inheritdoc />
    public class TaskRepository(AppDbContext dbContext, IMapper mapper) : ITaskRepository
    {
        /// <inheritdoc />
        public async Task<Task> Create(Task task)
        {
            var taskToCreate = mapper.Map<TaskEntity>(task);
            dbContext.Tasks.Add(taskToCreate);
            await dbContext.SaveChangesAsync();

            return mapper.Map<Task>(taskToCreate);
        }

        /// <inheritdoc />
        public async Task<Task> UpdateStatus(Guid id, TaskStatus status)
        {
            var taskEntity = await dbContext.Tasks.FindAsync(id);

            if (taskEntity is null)
                throw new KeyNotFoundException($"Task with ID {id} not found.");

            taskEntity.Status = status;
            await dbContext.SaveChangesAsync();

            var updatedTask = mapper.Map<Task>(taskEntity);
            return updatedTask;
        }

        /// <inheritdoc />
        public async Task<IList<Task>> GetAllTasks()
        {
            List<TaskEntity> tasks = await dbContext.Tasks.ToListAsync();
            return mapper.Map<List<Task>>(tasks);
        }
    }
}
