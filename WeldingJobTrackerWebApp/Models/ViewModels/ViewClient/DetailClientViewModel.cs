using System.ComponentModel.DataAnnotations.Schema;

namespace WeldingJobTrackerWebApp.Models.ViewModels.ViewClient
{
    public class DetailClientViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public List<Project> Projects { get; set; }
    }
}
