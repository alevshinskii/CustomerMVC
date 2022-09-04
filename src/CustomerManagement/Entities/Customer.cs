using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Entities
{
    public class Customer
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(50)]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Phone]
        [MaxLength(15)]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        [MaxLength(255)]
        public string? Email { get; set; }

        [Range(0, Int32.MaxValue)]
        public decimal? TotalPurchasesAmount { get; set; }

        public List<Address> Addresses { get; set; } = new();

        public List<Note> Notes { get; set; } = new();
    }
}