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
        [ForeignKey("Client")] public int ClientId { get; set; }
        public Client? Client {  get; set; }
        public int Rate {  get; set; }
        public int EstimatedHours {  get; set; }
        public int totalHours {  get; set; } 
        public int EstimatedWeldingWire {  get; set; }
        public int TotalWeldingWire {  get; set; }
    }
}
