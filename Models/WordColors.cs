using System;
namespace WordApi.Models
{
    public class WordColor
    {
        public int WordColorId { get; set; }
        public string Word { get; set; }
        public string Color { get; set; }
        public DateTime TS { get; set; }
    }
}
