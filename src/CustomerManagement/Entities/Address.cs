using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Entities
{
    public class Address
    {
        [Required]
        public int AddressId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string AddressLine { get; set; } = String.Empty;

        [MaxLength(100)]
        public string? AddressLine2 { get; set; }

        [RegularExpression("^Shipping|Billing$")]
        public string? AddressType { get; set; }

        [MaxLength(50)]
        public string? City { get; set; }

        [MinLength(4)]
        [MaxLength(6)]
        [RegularExpression("^[0-9]*$")]
        public string? PostalCode { get; set; }

        [MaxLength(20)]
        public string? State { get; set; }

        [MaxLength(100)]
        [RegularExpression("^United States|Canada$")]
        public string? Country { get; set; }

    }
}

