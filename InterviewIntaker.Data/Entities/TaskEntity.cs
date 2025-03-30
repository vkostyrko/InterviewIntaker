using Microsoft.EntityFrameworkCore;

namespace InterviewIntaker.Data.Entities
{
    /// <summary>
    /// Task Entity
    /// </summary>
    [PrimaryKey("Id")]
    internal class TaskEntity
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
