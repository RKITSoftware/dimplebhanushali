using ServiceStack;
using System;

namespace Historical_Events.Models
{
    public class HistoricalEvent
    {
        public int Id { get; set; }
        public int PublishDate { get; set; }
        public string HeadlineCategory { get; set; }
        public string HeadlineText { get; set; }
    }
}