using AutoMapper;
using InterviewIntaker.Contracts.Tasks;

namespace InterviewIntaker.Application.MappingProfiles
{
    /// <summary>
    /// Task mapping profile
    /// </summary>
    public class TaskProfile : Profile
    {
        /// <summary>
        /// Creates new instance of <see cref="TaskProfile"/>
        /// </summary>
        public TaskProfile()
        {
            CreateMap<Domain.DomainModels.Task, TaskDto>();
            CreateMap<CreateTaskRequest, Domain.DomainModels.Task>();
        }
    }
}
