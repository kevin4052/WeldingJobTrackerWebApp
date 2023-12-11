using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeldingJobTrackerWebApp.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        [ForeignKey("Image")] public int? ImageId { get; set; }
        public Image? Image { get; set; }
        [ForeignKey("Address")] public int? AddressId { get; set; }
        public Address? Address { get; set; }
        [ForeignKey("Company")] public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
