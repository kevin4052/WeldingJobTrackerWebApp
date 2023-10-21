using System.ComponentModel.DataAnnotations;

namespace WeldingJobTrackerWebApp.Models
{
    public class Welder
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }
    }
}
