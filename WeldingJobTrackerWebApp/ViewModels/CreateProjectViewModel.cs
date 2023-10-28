using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WeldingJobTrackerWebApp.Models;

namespace WeldingJobTrackerWebApp.ViewModels
{
    public class CreateProjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
        public Client? Client { get; set; }
        public int Budget { get; set; }
        public int CostEstimate { get; set; }
        public string? Description { get; set; }
        public string? Notes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Rate { get; set; }
        public int EstimatedHours { get; set; }
        public int totalHours { get; set; }
        public int EstimatedWeldingWire { get; set; }
        public int TotalWeldingWire { get; set; }
    }
}
