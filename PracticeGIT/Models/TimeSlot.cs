using System;

namespace PracticeGIT.Models
{
    public class TimeSlot
    {
        public int SlotId { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

    }
}