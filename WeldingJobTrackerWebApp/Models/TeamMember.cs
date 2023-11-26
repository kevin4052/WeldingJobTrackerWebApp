using System.ComponentModel.DataAnnotations.Schema;

namespace WeldingJobTrackerWebApp.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        [ForeignKey("Team")] public int TeamId { get; set; }
        public Team Team { get; set; }
        [ForeignKey("User")] public string UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("TeamRole")] public int RoleId { get; set; }
        public TeamRole Role { get; set; }
    }
}
