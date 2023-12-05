using Microsoft.AspNetCore.Mvc.Rendering;

namespace WeldingJobTrackerWebApp.Models.ViewModels.ViewTeam
{
    public class TeamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public List<SelectListItem> ProjectSelectList { get; set; }
        public List<SelectListItem> WelderSelectList { get; set; }
        public List<SelectListItem> AdminSelectList { get; set; }
        public int SelectedProjectId { get; set; }
        public string SelectedWelderId { get; set; }
        public string SelectedAdminId { get; set; }
    }
}
