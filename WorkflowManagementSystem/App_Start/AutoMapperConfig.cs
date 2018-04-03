using WorkflowManagementSystem.Models;
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
                // TODO check if needed 
                cfg.CreateMap<Language, LanguageViewModel>().ReverseMap();
            });
        }
    }   
}