using ApplicationDAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApplicationDAL.Data.Context
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
      
        public ApplicationContext(DbContextOptions options) : base(options)
        {
            
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.;Database = RouteDb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);  
        }


        public DbSet<Department> departments { get; set; }

        public DbSet<Employee> employees { get; set; }
    }
}
