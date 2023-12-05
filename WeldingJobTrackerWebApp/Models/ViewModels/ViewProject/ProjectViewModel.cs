﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace WeldingJobTrackerWebApp.Models.ViewModels.ViewProject
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedByUserId { get; set; }
        public List<SelectListItem>? ProjectStatusSelectList { get; set; }
        public List<SelectListItem>? UserSelectList { get; set; }
        public List<SelectListItem>? ClientSelectList { get; set; }
        public string SelectedProjectStatus { get; set; }
        public string SelectedUser { get; set; }
        public string SelectedClient { get; set; }
        public string SelectedManagerId { get; set; }
        public Client? Client { get; set; }
        public int Budget { get; set; } = 0;
        public int CostEstimate { get; set; } = 0;
        public string? Description { get; set; }
        public string? Notes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Rate { get; set; } = 0;
        public int EstimatedHours { get; set; } = 0;
        public int totalHours { get; set; } = 0;
        public int EstimatedWeldingWire { get; set; } = 0;
        public int TotalWeldingWire { get; set; } = 0;
    }
}