using WeldingJobTrackerWebApp.Models;

namespace WeldingJobTrackerWebApp.Models.ViewModels.ViewDashboard
{
    public class DashboardViewModel
    {
        public User CurrentUser { get; set; }
        public Company Company { get; set; }
        public List<Project> Projects { get; set; }
    }
}
