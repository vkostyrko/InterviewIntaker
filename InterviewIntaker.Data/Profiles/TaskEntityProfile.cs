using AutoMapper;
using InterviewIntaker.Data.Entities;

namespace InterviewIntaker.Data.Profiles
{
    /// <summary>
    /// Task entity mapping profile
    /// </summary>
    public class TaskEntityProfile : Profile
    {
        /// <summary>
        /// creates new instance of TaskEntityProfile
        /// </summary>
        public TaskEntityProfile()
        {
            CreateMap<Domain.DomainModels.Task, TaskEntity>();
            CreateMap<TaskEntity, Domain.DomainModels.Task>();
        }
    }
}
