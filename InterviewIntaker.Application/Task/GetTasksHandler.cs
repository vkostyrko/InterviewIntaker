using AutoMapper;
using InterviewIntaker.Data.Repositories;
using MediatR;

namespace InterviewIntaker.Application.Task
{
    /// <summary>
    /// Create new instance of <see cref="GetTasksHandler"/>
    /// </summary>
    /// <param name="taskRepository">Task repository</param>
    /// <param name="mapper">Mapper</param>
    public class GetTasksHandler(ITaskRepository taskRepository, IMapper mapper) : IRequestHandler<GetTasksCommand, IList<Domain.DomainModels.Task>>
    {
        public async Task<IList<Domain.DomainModels.Task>> Handle(GetTasksCommand request, CancellationToken cancellationToken)
        {
            var taskEntities = await taskRepository.GetAllTasks();
            return mapper.Map<IList<Domain.DomainModels.Task>>(taskEntities);
        }
    }
}
