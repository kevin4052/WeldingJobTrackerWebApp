namespace WeldingJobTrackerWebApp.Models.ViewModels.ViewAccount
{
    public class DetailAccountViewModal
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public Address? Address { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public List<Team>? Teams { get; set; }
        public List<Project>? Projects { get; set; }
    }
}
