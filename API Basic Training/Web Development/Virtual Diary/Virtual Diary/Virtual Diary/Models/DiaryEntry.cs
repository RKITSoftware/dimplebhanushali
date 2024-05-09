using System;
using System.Collections.Generic;

namespace Virtual_Diary.Models
{
    /// <summary>
    /// Represents a diary entry with content, creation date, and associated tasks.
    /// </summary>
    public class DiaryEntry
    {
        /// <summary>
        /// Gets or Sets Entry Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Entry Content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or Sets Date Created Time
        /// </summary>
        public DateTime DateCreated { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or Sets Entry Tasks
        /// </summary>
        public List<TaskDetail> Tasks { get; set; } = new List<TaskDetail>();

      
    }
}