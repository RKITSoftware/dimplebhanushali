namespace Virtual_Diary.Models
{
    /// <summary>
    /// Represents details about a task.
    /// </summary>
    public class TaskDetail
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public bool IsCompleted { get; set; }
    }
}