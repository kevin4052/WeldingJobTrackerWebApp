using Microsoft.AspNetCore.Mvc.Rendering;

namespace WeldingJobTrackerWebApp.Models.ViewModels.ViewTeam
{
    public class DetailTeamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Project> Projects { get; set; }
        public User Welder { get; set; }
        public User Admin { get; set; }
    }
}
