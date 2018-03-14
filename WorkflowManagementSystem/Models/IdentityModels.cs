using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WorkflowManagementSystem.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, CustomRole,
    int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ClientSatisfaction> ClientSatisfactions { get; set; }
        public virtual DbSet<CostSheet> CostSheets { get; set; }
        public virtual DbSet<CostSheetItem> CostSheetItems { get; set; }
        public virtual DbSet<CostVariance> CostVariances { get; set; }
        public virtual DbSet<CostVarianceItem> CostVarianceItems { get; set; }
        public virtual DbSet<Criterion> Criteria { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EventProject> EventProjects { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ProjectSchedule> ProjectSchedules { get; set; }
        public virtual DbSet<EmployeeTask> EmployeeTasks { get; set; }
        public virtual DbSet<TaskAssignment> TaskAssignments { get; set; }
        public virtual DbSet<Usher> Ushers { get; set; }
        public virtual DbSet<UsherAppointed> UsherAppointeds { get; set; }
        public virtual DbSet<UsherEvaluation> UsherEvaluations { get; set; }
        public virtual DbSet<UsherLanguage> UsherLanguages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.ClientSatisfactions)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.EventProjects)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CostSheet>()
                .HasMany(e => e.CostSheetItems)
                .WithRequired(e => e.CostSheet)
                .HasForeignKey(e => e.CostSheetId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CostSheet>()
                .HasRequired(e => e.CostVariance)
                .WithRequiredPrincipal(e => e.CostSheet)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CostVariance>()
                .HasMany(e => e.CostVarianceItems)
                .WithRequired(e => e.CostVariance)
                .HasForeignKey(e => e.CostVarianceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CostVarianceItem>()
                .Property(e => e.ActualCost)
                .HasPrecision(6, 2);

            modelBuilder.Entity<Criterion>()
                .HasMany(e => e.ClientSatisfactions)
                .WithRequired(e => e.Criterion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Criterion>()
                .HasMany(e => e.UsherEvaluations)
                .WithRequired(e => e.Criterion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.CostSheets)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.CEOEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.ConfirmCostSheets)
                .WithRequired(e => e.FinanceEmployee)
                .HasForeignKey(e => e.FinanceEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.SubmitCostSheets)
                .WithRequired(e => e.ProductionEmployee)
                .HasForeignKey(e => e.ProductionEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.CostVariances)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.FinanceEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EventProjects)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.ClientServiceEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeTasks)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.ClientServiceEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.TaskAssignments)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.ClientServiceEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.AssignedTasks)
                .WithRequired(e => e.AnyEmployee)
                .HasForeignKey(e => e.EmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.UsherAppointeds)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.ProductionEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.UsherEvaluations)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.ProductionEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EventProject>()
                .HasMany(e => e.ClientSatisfactions)
                .WithRequired(e => e.EventProject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EventProject>()
                .HasMany(e => e.CostSheets)
                .WithRequired(e => e.EventProject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EventProject>()
                .HasMany(e => e.Documents)
                .WithRequired(e => e.EventProject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EventProject>()
                .HasMany(e => e.ProjectSchedules)
                .WithRequired(e => e.EventProject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EventProject>()
                .HasMany(e => e.EmployeeTasks)
                .WithRequired(e => e.EventProject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EventProject>()
                .HasMany(e => e.UsherAppointeds)
                .WithRequired(e => e.EventProject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Item>()
                .Property(e => e.UnitCost)
                .HasPrecision(6, 2);

            modelBuilder.Entity<Item>()
                .HasMany(e => e.CostSheetItems)
                .WithRequired(e => e.Item)
                .HasForeignKey(e => e.ItemId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Item>()
                .HasMany(e => e.CostVarianceItems)
                .WithRequired(e => e.Item)
                .HasForeignKey(e => e.ItemId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EmployeeTask>()
                .HasMany(e => e.TaskAssignments)
                .WithRequired(e => e.EmployeeTask)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usher>()
                .HasMany(e => e.UsherAppointeds)
                .WithRequired(e => e.Usher)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usher>()
                .HasMany(e => e.UsherEvaluations)
                .WithRequired(e => e.Usher)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usher>()
                .HasMany(e => e.UsherLanguages)
                .WithRequired(e => e.Usher)
                .HasForeignKey(e => e.UsherId)
                .WillCascadeOnDelete(false);
        }

    }
    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
    public class CustomUserLogin : IdentityUserLogin<int> { }

    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }

    public class CustomUserStore : UserStore<ApplicationUser, CustomRole, int,
        CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}