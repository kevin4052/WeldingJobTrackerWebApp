using System.ComponentModel.DataAnnotations;
using WeldingJobTrackerWebApp.Data.Enum;

namespace WeldingJobTrackerWebApp.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public RoleType Type { get; set; }
    }
}
