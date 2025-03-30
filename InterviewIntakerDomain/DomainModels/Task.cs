namespace InterviewIntaker.Domain.DomainModels
{
    /// <summary>
    /// Domain model for Task
    /// </summary>
    public class Task
    {
        /// <summary>
        /// Gets or sets task id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets status
        /// </summary>
        public TaskStatus Status { get; set; }

        /// <summary>
        /// Gets or sets assigned to
        /// </summary>
        public string AssignedTo { get; set; }
    }
}