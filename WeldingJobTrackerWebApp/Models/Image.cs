using System.ComponentModel.DataAnnotations;

namespace WeldingJobTrackerWebApp.Models
{
    public class Image
    {
        [Key] public int Id { get; set; }
        [Required] public string Url { get; set; }
    }
}
