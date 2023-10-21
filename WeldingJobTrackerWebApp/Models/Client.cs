using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeldingJobTrackerWebApp.Models
{
    public class Client
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Address")] public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
