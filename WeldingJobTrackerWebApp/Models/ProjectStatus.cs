using System.ComponentModel.DataAnnotations;

namespace WeldingJobTrackerWebApp.Models
{
    public class ProjectStatus
    {
        [Key] public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
