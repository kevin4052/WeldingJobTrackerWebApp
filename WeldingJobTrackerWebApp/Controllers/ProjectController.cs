﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WeldingJobTrackerWebApp.Interfaces;
using WeldingJobTrackerWebApp.Models;
using WeldingJobTrackerWebApp.ViewModels;

namespace WeldingJobTrackerWebApp.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectStatusRepository _projectStatusRepository;

        public ProjectController(IProjectRepository projectRepository, IProjectStatusRepository projectStatusRepository)
        {
            _projectRepository = projectRepository;
            _projectStatusRepository = projectStatusRepository;
        }
        public async Task<IActionResult> Index()
        {
            var clients = await _projectRepository.GetAll();
            return View(clients);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var client = await _projectRepository.GetByIdAsync(id);
            return View(client);
        }

        public async Task<IActionResult> Create() 
        {
            var projectStatuses = await _projectStatusRepository.GetAll();
            ViewBag.ProjectStatuses = new SelectList(projectStatuses);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectViewModel projectVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Project save failed");
                return View(projectVM);                
            }

            var project = new Project
            {
                Name = projectVM.Name,
                ProjectStatusId = projectVM.ProjectStatus.Id,
                ClientId = projectVM?.Client?.Id ?? 0,
                Budget = projectVM?.Budget ?? 0,
                CostEstimate = projectVM?.CostEstimate ?? 0,
                Description = projectVM?.Description,
                Notes = projectVM?.Notes,
                StartDate = (DateTime)(projectVM.StartDate),
                EndDate = (DateTime)(projectVM.EndDate),
                Rate = projectVM?.Rate ?? 0,
                EstimatedHours = projectVM?.EstimatedHours ?? 0,
                EstimatedWeldingWire = projectVM?.EstimatedWeldingWire ?? 0,

            };

            _projectRepository.Add(project);
            return RedirectToAction("Index");
        }
    }
}
