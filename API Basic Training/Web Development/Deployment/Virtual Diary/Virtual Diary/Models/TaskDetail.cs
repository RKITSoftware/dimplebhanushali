using System.ComponentModel.DataAnnotations;

namespace Virtual_Diary.Models
{
    /// <summary>
    /// Represents details about a task.
    /// </summary>
    public class TaskDetail
    {
        /// <summary>
        /// Gets or sets the unique identifier of the task.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the task.
        /// </summary>
        [Required(ErrorMessage = "Task name is required.")]
        [StringLength(100, ErrorMessage = "Task name must be between 1 and 100 characters.", MinimumLength = 1)]
        public string TaskName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the task is completed.
        /// </summary>
        [Required(ErrorMessage = "Task Completed Status is required.")]
        public bool IsCompleted { get; set; }
    }
}
