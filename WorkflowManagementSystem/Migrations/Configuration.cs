namespace WorkflowManagementSystem.Migrations
{
    using Microsoft.AspNet.Identity;
    using System;
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
            string[] roles = { "Admin", "CEO", "Client Service Employee", "Production Employee", "Finance Employee", "Creative Employee", "Event Planner" };

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

            // Add item sample
            AddItems(context);

            // Add language sample
            AddLanguages(context);
        }

        private void AddItems(ApplicationDbContext context)
        {
            context.Items.AddOrUpdate(
                  p => p.Name, 
                  new Item { Name = "Chair", Description = "Normal sitting chair", UnitCost = 20},
                  new Item { Name = "Table1", Description = "Circular table", UnitCost = 200 },
                  new Item { Name = "Table 2", Description = "Rectangular table", UnitCost = 250 }
                  );
        }

        private void AddLanguages(ApplicationDbContext context)
        {
            context.Languages.AddOrUpdate(
                p => p.Name,
                new Language { Name = "English" },
                new Language { Name = "Arabic" }
                );
        }

    }
}
