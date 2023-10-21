using System.ComponentModel.DataAnnotations;

namespace WeldingJobTrackerWebApp.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string Street1 { get; set; } = string.Empty;
        public string? Street2 { get; set; }
        public string City { get; set; } = string.Empty;
        public string state { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = "United States";
    }
}
