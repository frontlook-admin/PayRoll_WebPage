using Microsoft.EntityFrameworkCore;
using PayRoll_JMJPL_App.Models.repository;

namespace PayRoll_JMJPL_App.Data
{
    public class PayrollDbContext : DbContext
    {
        public PayrollDbContext(DbContextOptions<PayrollDbContext> options)
            : base(options)
        {
        }

        public Microsoft.EntityFrameworkCore.DbSet<Models.repository.Department> Departments { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Models.repository.WorkerType> WorkerTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            /*builder.Entity<HeadGroup>(b => b.HasKey(s => s.GroupId));
            builder.Entity("Models.repository.HeadGroups", b => { 
                b.HasKey("GroupId");
                b.HasAlternateKey("GroupName");
            });*/


            builder.Entity<Department>().HasKey(s => s.DepartmentCode);
            builder.Entity<Department>().HasAlternateKey(s => s.DepartmentName);

            builder.Entity<WorkerType>().HasKey(s => s.CategoryCode);
            builder.Entity<WorkerType>().HasKey(s => s.CategoryName);
        }
    }
}