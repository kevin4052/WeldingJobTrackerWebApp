using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeldingJobTrackerWebApp.Models
{
    public class Company
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Employees { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
