namespace WorkflowManagementSystem.Migrations
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WorkflowManagementSystem.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WorkflowManagementSystem.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(WorkflowManagementSystem.Models.ApplicationDbContext context)
        {
            //These are the roles included in the application.
            string[] roles = { "Admin", "CEO", "Event Planner", "Client Service Employee", "Production Employee", "Finance Employee", "Creative Employee" };

            // Admin user login information
            string adminEmail = "admin@gmail.com";
            string adminUserName = "admin";
            string adminPassword = "admin123";

            var roleStore = new CustomRoleStore(context);
            var roleManager = new RoleManager<CustomRole, int>(roleStore);

            foreach (var role in roles)
            {
                if (!roleManager.RoleExists(role))
                {
                    roleManager.Create(new CustomRole { Name = role });
                }
            }

            // Define admin user
            var userStore = new CustomUserStore(context);
            var userManager = new ApplicationUserManager(userStore);

            //TODO Change the type of the admin user
            var admin = new ApplicationUser
            {
                UserName = adminUserName,
                Email = adminEmail,
                EmailConfirmed = true,
                LockoutEnabled = false
            };

            // Create admin user
            if (userManager.FindByName(admin.UserName) == null)
            {
                userManager.Create(admin, adminPassword);
            }

            // Add admin user to admin role
            // roles[0] is "Admin"
            var user = userManager.FindByName(admin.UserName);
            if (!userManager.IsInRole(user.Id, roles[0]))
            {
                userManager.AddToRole(admin.Id, roles[0]);
            }

            // Add item examples 
            var items = new List<Item>
            {
                new Item { Name = "Chair", Description = "Normal sitting chair", UnitCost = 20},
                new Item { Name = "Table 1", Description = "Circular table", UnitCost = 200 },
                new Item { Name = "Table 2", Description = "Rectangular table", UnitCost = 250 }
            };

            items.ForEach(s => context.Items.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            // Add employee examples 
            var employees = new List<Employee>
            {
                new Employee { UserName = "user1" , Email = "user1@gmail.com", FirstName = "Badr",
                    LastName = "Alsharif", PhoneNumber = "0552323909", JobTitle = EmployeeJobTitle.Director,
                    Department = Department.ClientService, EmployeeType = EmployeeType.ClientService},
                new Employee { UserName = "user2" , Email = "user2@gmail.com", FirstName = "Hamza",
                    LastName = "Zamil", PhoneNumber = "0500303000", JobTitle = EmployeeJobTitle.Assistant,
                    Department = Department.ClientService, EmployeeType = EmployeeType.ClientService},
                new Employee { UserName = "user3" , Email = "user3@gmail.com", FirstName = "Abdullah",
                    LastName = "Ismail", PhoneNumber = "0505280001", JobTitle = EmployeeJobTitle.EventPlanner,
                    Department = Department.ClientService, EmployeeType = EmployeeType.ClientService},
            };

            foreach (var employee in employees)
            {
                if (userManager.FindByName(employee.UserName) == null)
                {
                    userManager.Create(employee, "user123");
                }

                var usertemp = userManager.FindByName(employee.UserName);
                if (!userManager.IsInRole(usertemp.Id, roles[3]))
                {
                    userManager.AddToRole(usertemp.Id, roles[3]);
                }
            }

            // Add language examples
            var languages = new List<Language>
            {
                new Language {Name = "English"},
                new Language {Name = "Arabic"}
            };

            languages.ForEach(s => context.Languages.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            // Add usher examples 
            var ushers = new List<Usher>
            {
                new Usher {FirstName = "Basem", LastName = "Helmi", MobileNumber = "0551212900", DateOfBirth = null,
                    Gender = UsherGender.Male, Nationality = "Saudi", City = "Jeddah",
                    LanguageId = languages.Single(d=>d.Name=="English").Id,CarAvailability = true, MedicalCard = null },

                new Usher {FirstName = "Layal", LastName = "Attas", MobileNumber = "0501211911", DateOfBirth = null,
                    Gender = UsherGender.Male, Nationality = "Saudi", City = "Riyadh",
                    LanguageId = languages.Single(d=>d.Name=="Arabic").Id, CarAvailability = true, MedicalCard = null }
            };

            ushers.ForEach(s => context.Ushers.AddOrUpdate(p => p.FirstName, s));
            context.SaveChanges();

            // Add client examples 
            var clients = new List<Client>
            {
                new Client {FirstName = "STC", LastName = null, Email = "stc@gmail.com", MobileNumber = "0550002000",
                    Street = "Malik Rd", District = "Shatea", City = "Jeddah"},

                new Client {FirstName = "Abdullah", LastName = "Sharif", Email = "asharif@gmail.com", MobileNumber = "0550001023",
                    Street = "Malik Rd", District = "Zahraa", City = "Jeddah"},
            };

            clients.ForEach(s => context.Clients.AddOrUpdate(p => p.FirstName, s));
            context.SaveChanges();

            //// Add event project examples 
            //var eventprojects = new List<EventProject>
            //{
            //    new EventProject {Name = "Food festival",  EventType = EventProjectType.Festival,
            //        Brief = "The client wants the event to consist of 12 booths(all providing food), a stage with a screen needs to be in the entrance.There should be high security. It is for families.",
            //        Street = "Malik Rd", District = "AlShati Dist", City = "Jeddah", Status = ProjectStatus.Approved,
            //        Presentation = null, EventReportTemplate = null, EventReport = null, ThreeDModel = null,
            //        DateCreated = null, ClientServiceEmployeeId = employees.Single(e => e.FirstName == "Badr").Id,
            //        ClientId = clients.Single(c => c.FirstName == "STC").ClientId}
            //};

            //eventprojects.ForEach(s => context.EventProjects.AddOrUpdate(p => p.Name, s));
            //context.SaveChanges();
        }
    }
}

