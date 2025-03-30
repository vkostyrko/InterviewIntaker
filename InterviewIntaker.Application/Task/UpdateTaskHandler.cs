using AutoMapper;
using InterviewIntaker.Data.Repositories;
using MediatR;

namespace InterviewIntaker.Application.Task
{
    /// <summary>
    /// Update task handler
    /// </summary>
    /// <param name="taskRepository"> task repository</param>
    /// <param name="mapper">automapper</param>
    public class UpdateTaskHandler(ITaskRepository taskRepository, IMapper mapper) : IRequestHandler<UpdateTaskCommand, Domain.DomainModels.Task>
    {
        public async Task<Domain.DomainModels.Task> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            return await taskRepository.UpdateStatus(request.Id, request.Status);
        }
    }
}
