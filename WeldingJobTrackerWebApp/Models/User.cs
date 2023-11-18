using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeldingJobTrackerWebApp.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        [ForeignKey("Address")] public int? AddressId { get; set; }
        public Address? Address { get; set; }
    }
}
