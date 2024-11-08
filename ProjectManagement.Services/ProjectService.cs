﻿using AutoMapper;
using DataLayerAbstractions;
using LayerAbstractions.Interfaces;
using ProjectManagement.Models;
using ProjectManagement.Public.Models;
using ProjectManagement.Repositories;

namespace ProjectManagement.Services
{
    public class ProjectService : ServiceBase<int, ProjectViewModel, Project, ProjectRepository>
    {
        private ProjectRepository _projectRepository;
        private ProjectTaskService _taskService;
        private IMapper _mapper;
        public ProjectService(ProjectRepository projectRepository,
            IMapper mapper,
            ProjectTaskService taskService) : base(projectRepository, mapper)
        {
            _mapper = mapper;
            _taskService = taskService;
            _projectRepository = projectRepository;
        }
    }
}
