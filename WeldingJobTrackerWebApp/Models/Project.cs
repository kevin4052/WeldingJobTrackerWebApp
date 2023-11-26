using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeldingJobTrackerWebApp.Models
{
    public class Project
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("ProjectStatus")] public int ProjectStatusId { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
        [ForeignKey("Team")] public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
