using WeldingJobTrackerWebApp.Models;

namespace WeldingJobTrackerWebApp.ViewModels
{
    public class DashboardViewModel
    {
        public User CurrentUser { get; set; }
        public Company Company { get; set; }
        public List<Project> Projects { get; set; }
    }
}
