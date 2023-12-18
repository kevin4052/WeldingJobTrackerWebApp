using Microsoft.AspNetCore.Mvc.Rendering;

namespace WeldingJobTrackerWebApp.Models.ViewModels.ViewProject
{
    public class EditProjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public int SelectedTeamId { get; set; }
        public List<SelectListItem> TeamSelectList { get; set; }
        public int SelectedProjectStatusId { get; set; }
        public List<SelectListItem>? ProjectStatusSelectList { get; set; }
    }
}
