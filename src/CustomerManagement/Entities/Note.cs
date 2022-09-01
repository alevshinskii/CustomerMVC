using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Entities
{
    public class Note
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Text { get; set; }=String.Empty;
    }
}

