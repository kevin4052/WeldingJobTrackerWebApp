using WeldingJobTrackerWebApp.Models;

namespace WeldingJobTrackerWebApp.ViewModels
{
    public class EditClientViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
    }
}
