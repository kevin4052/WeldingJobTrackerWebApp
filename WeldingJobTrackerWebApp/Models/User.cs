using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeldingJobTrackerWebApp.Models
{
    public class User
    {
        [Key] public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        [ForeignKey("Role")] public int RoleId { get; set; }
        public Role Role { get; set; }
        [ForeignKey("Address")] public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
