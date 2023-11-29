using Microsoft.AspNetCore.Mvc.Rendering;
using WeldingJobTrackerWebApp.Models;

namespace WeldingJobTrackerWebApp.ViewModels
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
        public int SelectedWelderId { get; set;}
        public int SelectedAdminId { get; set; }
    }
}
