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
        public string TaskName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the task is completed.
        /// </summary>
        public bool IsCompleted { get; set; }
    }
}
