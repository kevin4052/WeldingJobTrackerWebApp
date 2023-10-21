using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeldingJobTrackerWebApp.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Welder"] public int WelderId { get; set; }
    }
}
