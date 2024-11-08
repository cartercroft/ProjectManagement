﻿using DataLayerAbstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Models;
using ProjectManagement.Public.Models;
using ProjectManagement.Repositories;
using ProjectManagement.Services;

namespace ProjectManagement.API.Controllers
{
    public class ProjectController : APIControllerBase<int, ProjectViewModel, Project, ProjectService, ProjectRepository>
    {
        private readonly ProjectService _projectService;
        private readonly UserManager<User> _userManager;
        public ProjectController(ProjectService projectService, UserManager<User> userManager) : base(projectService)
        {
            _projectService = projectService;
            _userManager = userManager;
        }
        public override ProjectViewModel Save(ProjectViewModel viewModel)
        {
            var userIdString = _userManager.GetUserId(HttpContext.User);
            return _projectService.Save(viewModel, Guid.Parse(userIdString));
        }
        [Authorize(Roles = "Admin")]
        public override IEnumerable<ProjectViewModel> GetAll()
        {
            return base.GetAll();
        }
        [Authorize]
        [HttpGet]
        public List<ProjectViewModel> GetProjectsForCurrentUser()
        {
            var userIdString = _userManager.GetUserId(HttpContext.User);
            return _projectService.GetProjectsForUser(Guid.Parse(userIdString));
        }
    }
}
