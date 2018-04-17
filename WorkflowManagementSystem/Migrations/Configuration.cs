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
                new Item { Name = "Table 2", Description = "Rectangular table", UnitCost = 250 },
                new Item { Name = "Stage", Description = "4 by 4 white stage", UnitCost = 1000 },
                new Item { Name = "Monitor", Description = "6-inch monitor", UnitCost = 1500 },
                new Item { Name = "Spotlight", Description = null, UnitCost = 250 }
            };

            items.ForEach(s => context.Items.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            // Add client service employee examples
            var clientServiceEmployees = new List<Employee>
            {
                new Employee { UserName = "CS1" , Email = "cs1@gmail.com", FirstName = "Badr",
                    LastName = "Alsharif", PhoneNumber = "0552323909", JobTitle = EmployeeJobTitle.Director,
                    Department = Department.ClientService, EmployeeType = EmployeeType.ClientService},                
                
                new Employee { UserName = "CS2" , Email = "cs2@gmail.com", FirstName = "Moneer",
                    LastName = "Ghalib", PhoneNumber = "0504444442", JobTitle = EmployeeJobTitle.Assistant,
                    Department = Department.ClientService, EmployeeType = EmployeeType.ClientService},
            };

            foreach (var employee in clientServiceEmployees)
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
            // Add production employee examples
            var productionEmployees = new List<Employee>
            {               
                new Employee { UserName = "prod1" , Email = "prod1@gmail.com", FirstName = "Hamza",
                    LastName = "Zamil", PhoneNumber = "0500303000", JobTitle = EmployeeJobTitle.Director,
                    Department = Department.Production, EmployeeType = EmployeeType.Production},

                new Employee { UserName = "prod2" , Email = "prod2@gmail.com", FirstName = "Hashim",
                    LastName = "Bakr", PhoneNumber = "0500303234", JobTitle = EmployeeJobTitle.Assistant,
                    Department = Department.Production, EmployeeType = EmployeeType.Production},
            };

            foreach (var employee in productionEmployees)
            {
                if (userManager.FindByName(employee.UserName) == null)
                {
                    userManager.Create(employee, "user123");
                }

                var usertemp = userManager.FindByName(employee.UserName);
                if (!userManager.IsInRole(usertemp.Id, roles[4]))
                {
                    userManager.AddToRole(usertemp.Id, roles[4]);
                }
            }

            // Add finance employee examples
            var financeEmployees = new List<Employee>
            {
                new Employee { UserName = "fin1" , Email = "fin1@gmail.com", FirstName = "Abdullah",
                    LastName = "Ismail", PhoneNumber = "0505280001", JobTitle = EmployeeJobTitle.Director,
                    Department = Department.Finance, EmployeeType = EmployeeType.Finance},

                new Employee { UserName = "fin2" , Email = "fin2@gmail.com", FirstName = "Abdulrahman",
                    LastName = "Qurashi", PhoneNumber = "0505288888", JobTitle = EmployeeJobTitle.Accountant,
                    Department = Department.Finance, EmployeeType = EmployeeType.Finance},
            };

            foreach (var employee in financeEmployees)
            {
                if (userManager.FindByName(employee.UserName) == null)
                {
                    userManager.Create(employee, "user123");
                }

                var usertemp = userManager.FindByName(employee.UserName);
                if (!userManager.IsInRole(usertemp.Id, roles[5]))
                {
                    userManager.AddToRole(usertemp.Id, roles[5]);
                }
            }

            // Add creative employee examples
            var creativeEmployees = new List<Employee>
            {
                 new Employee { UserName = "CR1" , Email = "cr1@gmail.com", FirstName = "Khalid",
                    LastName = "Madani", PhoneNumber = "0505282232", JobTitle = EmployeeJobTitle.Director,
                    Department = Department.Creative, EmployeeType = EmployeeType.Creative},

                 new Employee { UserName = "CR2" , Email = "cr2@gmail.com", FirstName = "Ali",
                    LastName = "Amoudi", PhoneNumber = "0555444342", JobTitle = EmployeeJobTitle.Designer,
                    Department = Department.Creative, EmployeeType = EmployeeType.Creative},
            };

            foreach (var employee in creativeEmployees)
            {
                if (userManager.FindByName(employee.UserName) == null)
                {
                    userManager.Create(employee, "user123");
                }

                var usertemp = userManager.FindByName(employee.UserName);
                if (!userManager.IsInRole(usertemp.Id, roles[6]))
                {
                    userManager.AddToRole(usertemp.Id, roles[6]);
                }
            }

            // Add CEO example
            var CEOEmployees = new List<Employee>
            {
                 new Employee { UserName = "CEO" , Email = "ceo@gmail.com", FirstName = "Omar",
                    LastName = "Mohamed", PhoneNumber = "0505282232", JobTitle = EmployeeJobTitle.Director,
                    Department = null, EmployeeType = EmployeeType.CEO},
            };

            foreach (var employee in CEOEmployees)
            {
                if (userManager.FindByName(employee.UserName) == null)
                {
                    userManager.Create(employee, "user123");
                }

                var usertemp = userManager.FindByName(employee.UserName);
                if (!userManager.IsInRole(usertemp.Id, roles[1]))
                {
                    userManager.AddToRole(usertemp.Id, roles[1]);
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
                new Usher {FirstName = "Basem", LastName = "Helmi", MobileNumber = "0551212900", DateOfBirth = DateTime.Parse("02/12/1996"),
                    Gender = UsherGender.Male, Nationality = "Saudi", City = "Jeddah",
                    LanguageId = languages.Single(d=>d.Name=="English").Id,CarAvailability = true, MedicalCard = null },

                new Usher {FirstName = "Layal", LastName = "Attas", MobileNumber = "0501211911", DateOfBirth = DateTime.Parse("07/20/1997"),
                    Gender = UsherGender.Female, Nationality = "Saudi", City = "Riyadh",
                    LanguageId = languages.Single(d=>d.Name=="Arabic").Id, CarAvailability = true, MedicalCard = null },

                new Usher {FirstName = "Omar", LastName = "Gamdi", MobileNumber = "0500088981", DateOfBirth = DateTime.Parse("10/10/1993"),
                    Gender = UsherGender.Male, Nationality = "Saudi", City = "Jeddah",
                    LanguageId = languages.Single(d=>d.Name=="English").Id, CarAvailability = true, MedicalCard = null },

                new Usher {FirstName = "Yaser", LastName = "Ghalib", MobileNumber = "0599988999", DateOfBirth = DateTime.Parse("12/23/1996"),
                    Gender = UsherGender.Male, Nationality = "Saudi", City = "Jeddah",
                    LanguageId = languages.Single(d=>d.Name=="English").Id, CarAvailability = true, MedicalCard = null },

                new Usher {FirstName = "Yara", LastName = "Zamil", MobileNumber = "0500088981", DateOfBirth = DateTime.Parse("11/09/1995"),
                    Gender = UsherGender.Female, Nationality = "Saudi", City = "Jeddah",
                    LanguageId = languages.Single(d=>d.Name=="English").Id, CarAvailability = true, MedicalCard = null },

                new Usher {FirstName = "Husam", LastName = "Madani", MobileNumber = "0551122281", DateOfBirth = DateTime.Parse("12/12/1996"),
                    Gender = UsherGender.Male, Nationality = "Saudi", City = "Riyadh",
                    LanguageId = languages.Single(d=>d.Name=="Arabic").Id, CarAvailability = false, MedicalCard = null }
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

                new Client {FirstName = "Souq Okaz", LastName = null, Email = "okaz@gmail.com", MobileNumber = "0557707055",
                    Street = "Al Hamra St.", District = "Al Hamra Dist.", City = "Jeddah"},

                new Client {FirstName = "MBC", LastName = null, Email = "mbc@gmail.com", MobileNumber = "0553303035",
                    Street = "Al Wasl St.", District = "Al Badaa Dist.", City = "Dubai"},

                new Client {FirstName = "Amal", LastName = "khatib", Email = "amal@gmail.com", MobileNumber = "0589589897",
                    Street = "Malik Rd.", District = "Shatea Dist.", City = "Jeddah"},
            };

            clients.ForEach(s => context.Clients.AddOrUpdate(p => p.FirstName, s));
            context.SaveChanges();

            // Add event project examples 
            var eventprojects = new List<EventProject>
            {
                new EventProject {Name = "Food festival",  EventType = EventProjectType.Festival,
                    Brief = "The client wants the event to consist of 12 booths(all providing food), a stage with a screen needs to be in the entrance.There should be high security. It is for families.",
                    Street = "Malik Rd", District = "AlShati Dist", City = "Jeddah", Status = ProjectStatus.Approved,
                    DateCreated = DateTime.Parse("07/04/2018"), ClientServiceEmployeeId = 2, ClientId = 1},

                new EventProject {Name = "Kids birthday party",  EventType = EventProjectType.Birthday,
                    Brief = "The client wants a theme of superheroes. Tables with games and seats. Should accommodate 20 children and adults.",
                    Street = "King Abdullah St.", District = "Khaldya Dist.", City = "Dubai", Status = ProjectStatus.Approved,
                    DateCreated = DateTime.Parse("07/04/2018"), ClientServiceEmployeeId = 2, ClientId = 2},

                 new EventProject {Name = "Jeddah Youth Ceremony",  EventType = EventProjectType.AwardCeremony,
                    Brief = "Client requests stage with screen. A table with all awards should be in the center of the stage. Tables and chairs that accommodates 100 people.",
                    Street = "Prince Naif St.", District = "Murjan Dist.", City = "Jeddah", Status = ProjectStatus.Pending,
                    DateCreated = DateTime.Parse("12/04/2018"), ClientServiceEmployeeId = 2, ClientId = 4},

                 new EventProject {Name = "Artsy",  EventType = EventProjectType.Exhibition,
                    Brief = "The client wants the layout of the event to portray a maze and the design should be colorful. Each section will include an artist who will be displaying his/her artworks. 5 booths serving any kinds of food is required. The entrance should include a table for selling tickets.",
                    Street = "Madina Rd.", District = "Musaadya Dist.", City = "Jeddah", Status = ProjectStatus.Pending,
                    DateCreated = DateTime.Parse("12/04/2018"), ClientServiceEmployeeId = 2, ClientId = 4},

                 new EventProject {Name = "Makkah Youth Forum",  EventType = EventProjectType.AwardCeremony,
                    Brief = "The client requires a stage in the center with a screen behind the speaker. The hall should accommodate up to 40 people; the front row should have tables in front of the chairs.",
                    Street = "Om AlQura St.", District = "Muala Dist.", City = "Makkah", Status = ProjectStatus.Pending,
                    DateCreated = DateTime.Parse("12/04/2018"), ClientServiceEmployeeId = 2, ClientId = 3},

                 new EventProject {Name = "Wedding",  EventType = EventProjectType.Wedding,
                    Brief = "The client requests round tables each with 10 chairs. A stage in the center with flowers.",
                    Street = "Malik Rd.", District = "Shati Dist.", City = "Jeddah", Status = ProjectStatus.Approved,
                    DateCreated = DateTime.Parse("12/04/2018"), ClientServiceEmployeeId = 2, ClientId = 5}
            };

            eventprojects.ForEach(s => context.EventProjects.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            //// Add project schedule examples 
            //var projectSchedules = new List<ProjectSchedule>
            //{
            //    new ProjectSchedule {Date = DateTime.Parse("10/05/2018"), StartTime = DateTime.Parse("9:00:00"), EndTime = DateTime.Parse("10:30:00"), EventProjectId = 1},
            //    new ProjectSchedule {Date = DateTime.Parse("11/05/2018"), StartTime = DateTime.Parse("9:00:00"), EndTime = DateTime.Parse("10:30:00"), EventProjectId = 1},
            //    new ProjectSchedule {Date = DateTime.Parse("20/06/2018"), StartTime = DateTime.Parse("11:00:00"), EndTime = DateTime.Parse("12:30:00"), EventProjectId = 1},
            //};
            //projectSchedules.ForEach(s => context.ProjectSchedules.AddOrUpdate(p => p.Date, s));
            //context.SaveChanges();

            // Add usher appointed examples 
            //var usherAppointeds = new List<UsherAppointed>
            //{
            //    new UsherAppointed {DateAppointed = DateTime.Parse("12/04/2018"), UsherId = 1, EventProjectId = 1, ProductionEmployeeId = 3},
            //    new UsherAppointed {DateAppointed = DateTime.Parse("12/04/2018"), UsherId = 2, EventProjectId = 1, ProductionEmployeeId = 3},
            //    new UsherAppointed {DateAppointed = DateTime.Parse("12/04/2018"), UsherId = 3, EventProjectId = 1, ProductionEmployeeId = 3},
            //    new UsherAppointed {DateAppointed = DateTime.Parse("12/04/2018"), UsherId = 4, EventProjectId = 1, ProductionEmployeeId = 3},
            //    new UsherAppointed {DateAppointed = DateTime.Parse("12/04/2018"), UsherId = 1, EventProjectId = 2, ProductionEmployeeId = 3},
            //    new UsherAppointed {DateAppointed = DateTime.Parse("12/04/2018"), UsherId = 5, EventProjectId = 2, ProductionEmployeeId = 3},
            //    new UsherAppointed {DateAppointed = DateTime.Parse("12/04/2018"), UsherId = 6, EventProjectId = 2, ProductionEmployeeId = 3},
            //    new UsherAppointed {DateAppointed = DateTime.Parse("12/04/2018"), UsherId = 6, EventProjectId = 3, ProductionEmployeeId = 3},
            //    new UsherAppointed {DateAppointed = DateTime.Parse("12/04/2018"), UsherId = 1, EventProjectId = 3, ProductionEmployeeId = 3},
            //    new UsherAppointed {DateAppointed = DateTime.Parse("12/04/2018"), UsherId = 6, EventProjectId = 4, ProductionEmployeeId = 3},
            //};
            //usherAppointeds.ForEach(s => context.UsherAppointeds.AddOrUpdate(p => p.DateAppointed, s));
            //context.SaveChanges();


            // Add criterion examples 
            var criteria = new List<Criterion>
            {
                new Criterion {Name = "Quality of Work", Description = "The quality of the work or service is up to high standards." },
                new Criterion {Name = "Communication", Description = "The level of communication and interaction." },
                new Criterion {Name = "Performance", Description = null },
                new Criterion {Name = "Overall satisfactory level", Description = "The satisfactory level of the overall result and service." },
            };

            criteria.ForEach(s => context.Criteria.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();
        }
    }
}

