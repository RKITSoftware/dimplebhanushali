using System;
using System.Collections.Generic;

namespace Virtual_Diary.Models
{
    /// <summary>
    /// Represents a diary entry with content, creation date, and associated tasks.
    /// </summary>
    public class DiaryEntry
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public List<TaskDetail> Tasks { get; set; } = new List<TaskDetail>();

        /// <summary>
        /// A predefined list of sample diary entries for demonstration purposes.
        /// </summary>
        public static List<DiaryEntry> lstDiaryEntries = new List<DiaryEntry>
        {
            // Simple Diary Entries
            new DiaryEntry
            {
                Id = 1,
                Content = "Today was a great day!",
                DateCreated = DateTime.Now.AddDays(-3), // 3 days ago
                Tasks = new List<TaskDetail>
                {
                    new TaskDetail { Id = 1, TaskName = "Write diary entry", IsCompleted = true },
                    new TaskDetail { Id = 2, TaskName = "Go for a walk", IsCompleted = false },
                }
            },
            new DiaryEntry
            {
                Id = 2,
                Content = "Worked on a new project.",
                DateCreated = DateTime.Now.AddDays(-2), // 2 days ago
                Tasks = new List<TaskDetail>
                {
                    new TaskDetail { Id = 1, TaskName = "Meeting with the team", IsCompleted = true },
                    new TaskDetail { Id = 2, TaskName = "Code review", IsCompleted = true },
                }
            },
            new DiaryEntry
            {
                Id = 3,
                Content = "Explored new hiking trails.",
                DateCreated = DateTime.Now.AddDays(-2),
                Tasks = new List<TaskDetail>
                {
                    new TaskDetail { Id = 1, TaskName = "Hike in the mountains", IsCompleted = true },
                    new TaskDetail { Id = 2, TaskName = "Capture scenic views", IsCompleted = false },
                }
            },
            new DiaryEntry
            {
                Id = 4,
                Content = "Relaxing weekend at home.",
                DateCreated = DateTime.Now.AddDays(-1), // 1 day ago
                Tasks = new List<TaskDetail>
                {
                    new TaskDetail { Id = 1, TaskName = "Read a book", IsCompleted = true },
                    new TaskDetail { Id = 2, TaskName = "Watch a movie", IsCompleted = true },
                }
            },
            new DiaryEntry
            {
                Id = 5,
                Content = "Productive day at work.",
                DateCreated = DateTime.Now.AddDays(-1),
                Tasks = new List<TaskDetail>
                {
                    new TaskDetail { Id = 1, TaskName = "Finish project report", IsCompleted = true },
                    new TaskDetail { Id = 2, TaskName = "Prepare for client meeting", IsCompleted = true },
                }
            },
            new DiaryEntry
            {
                Id = 6,
                Content = "Experimented with new recipes.",
                DateCreated = DateTime.Now,
                Tasks = new List<TaskDetail>
                {
                    new TaskDetail { Id = 1, TaskName = "Cook a new dish", IsCompleted = true },
                    new TaskDetail { Id = 2, TaskName = "Bake a dessert", IsCompleted = false },
                }
            },
            new DiaryEntry
            {
                Id = 7,
                Content = "Visited a local art exhibition.",
                DateCreated = DateTime.Now,
                Tasks = new List<TaskDetail>
                {
                    new TaskDetail { Id = 1, TaskName = "Explore art gallery", IsCompleted = true },
                    new TaskDetail { Id = 2, TaskName = "Attend art workshop", IsCompleted = false },
                }
            },
            new DiaryEntry
            {
                Id = 8,
                Content = "Gardening in the backyard.",
                DateCreated = DateTime.Now,
                Tasks = new List<TaskDetail>
                {
                    new TaskDetail { Id = 1, TaskName = "Plant flowers", IsCompleted = true },
                    new TaskDetail { Id = 2, TaskName = "Trim bushes", IsCompleted = false },
                }
            },
            new DiaryEntry
            {
                Id = 9,
                Content = "Exercise and fitness routine.",
                DateCreated = DateTime.Now,
                Tasks = new List<TaskDetail>
                {
                    new TaskDetail { Id = 1, TaskName = "Morning jog", IsCompleted = true },
                    new TaskDetail { Id = 2, TaskName = "Strength training", IsCompleted = true },
                }
            },
            new DiaryEntry
            {
                Id = 10,
                Content = "Learning a new musical instrument.",
                DateCreated = DateTime.Now,
                Tasks = new List<TaskDetail>
                {
                    new TaskDetail { Id = 1, TaskName = "Practice guitar", IsCompleted = true },
                    new TaskDetail { Id = 2, TaskName = "Learn new chords", IsCompleted = false },
                }
            },
         };

    }
}