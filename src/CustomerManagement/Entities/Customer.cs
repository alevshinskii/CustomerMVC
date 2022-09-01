using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Entities
{
    public class Customer
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(50)]
        public string? FirstName { get; set; } = null;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = String.Empty;

        [Phone]
        [MaxLength(15)]
        public string? PhoneNumber { get; set; } = null;

        [EmailAddress]
        [MaxLength(255)]
        public string? Email { get; set; } = null;

        [Range(0, Int32.MaxValue)]
        public decimal? TotalPurchasesAmount { get; set; } = null;

        public List<Address> Addresses { get; set; } = new List<Address>();

        public List<Note> Notes { get; set; } = new List<Note>();
    }
}