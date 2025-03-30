using AutoMapper;
using InterviewIntaker.Data.Repositories;
using MediatR;

namespace InterviewIntaker.Application.Task
{
    /// <summary>
    /// Creates new instance of <see cref="CreateTaskCommandHandler"/>
    /// </summary>
    /// <param name="taskRepository">task repository</param>
    /// <param name="mapper"></param>
    public class CreateTaskCommandHandler(ITaskRepository taskRepository, IMapper mapper) : IRequestHandler<CreateTaskCommand, Domain.DomainModels.Task>
    {
        public async Task<Domain.DomainModels.Task> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
        {
            var taskToCreate = mapper.Map<Domain.DomainModels.Task>(command.Task);
            return await taskRepository.Create(taskToCreate);
        }
    }
}
