using MediatR;

namespace InterviewIntaker.Application.Task
{
    /// <summary>
    /// Get task command
    /// </summary>
    public class GetTasksCommand : IRequest<IList<Domain.DomainModels.Task>>
    {
    }
}
