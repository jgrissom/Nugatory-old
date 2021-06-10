using System.ComponentModel.DataAnnotations;
using System;
namespace WordApi.Models
{
    public class WordColor
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string Color { get; set; }
    }
}
