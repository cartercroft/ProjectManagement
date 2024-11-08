﻿using DataLayerAbstractions;
using Microsoft.AspNetCore.Authorization;
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
        public ProjectController(ProjectService projectService) : base(projectService)
        {
            _projectService = projectService;
        }
    }
}
