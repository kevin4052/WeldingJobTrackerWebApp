using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeldingJobTrackerWebApp.Models
{
    public class Welder
    {
        [Key] public int Id { get; set; } 
        [ForeignKey("User")] public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
