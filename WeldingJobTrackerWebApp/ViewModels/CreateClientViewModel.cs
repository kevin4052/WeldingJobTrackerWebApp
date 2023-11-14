using WeldingJobTrackerWebApp.Models;

namespace WeldingJobTrackerWebApp.ViewModels
{
    public class CreateClientViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile? Image { get; set; }
        public int AddressId { get; set; }
        public Address? Address { get; set; }
    }
}
