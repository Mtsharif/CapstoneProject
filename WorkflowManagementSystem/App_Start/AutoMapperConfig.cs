﻿using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.ViewModels;

namespace WorkflowManagementSystem.App_Start
{
    /// <summary>
    /// Configuration of AutoMapper class
    /// Add all the required mappings between model and view models here
    /// </summary>
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeViewModel>().ReverseMap().ForMember(p=>p.Roles, o=>o.Ignore());
                cfg.CreateMap<Usher, UsherViewModel>().ReverseMap();
                cfg.CreateMap<Item, ItemViewModel>().ReverseMap();
                cfg.CreateMap<EventProject, EventProjectViewModel>().ReverseMap();
                cfg.CreateMap<ProjectSchedule, ProjectScheduleViewModel>().ReverseMap();
                cfg.CreateMap<Client, ClientViewModel>().ReverseMap();
                cfg.CreateMap<Language, LanguageViewModel>().ReverseMap();
                cfg.CreateMap<Document, DocumentViewModel>().ReverseMap();
                cfg.CreateMap<CostSheet, CostSheetViewModel>().ReverseMap();
                cfg.CreateMap<CostSheetItem, CostSheetItemViewModel>().ReverseMap();
                cfg.CreateMap<EmployeeTask, EmployeeTaskViewModel>().ReverseMap();
                cfg.CreateMap<TaskAssignment, TaskAssignmentViewModel>().ReverseMap();
                cfg.CreateMap<UsherAppointed, UsherAppointedViewModel>().ReverseMap();
                cfg.CreateMap<Criterion, CriterionViewModel>().ReverseMap();
                cfg.CreateMap<UsherEvaluation, UsherEvaluationViewModel>().ReverseMap();
            });
        }
    }   
}