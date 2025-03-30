namespace InterviewIntaker.Contracts.Tasks
{
    public class CreateTaskRequest
    {
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
