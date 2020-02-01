using Microsoft.EntityFrameworkCore;
using payroll_app.Models.repository;

namespace payroll_app.Data
{
    public class payroll_app_context:DbContext
    {
        public payroll_app_context(DbContextOptions<payroll_app_context> options):base(options)
        {
            
        }

        public DbSet<Department> Departments;
        public DbSet<WorkerType> WorkerTypes;
        public DbSet<Grade> Grades;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Department>().HasKey(s => s.Id);
            modelBuilder.Entity<Department>().HasAlternateKey(s => s.DepartmentName);
            modelBuilder.Entity<Department>().HasAlternateKey(s => s.DepartmentCode);

            modelBuilder.Entity<WorkerType>().HasKey(s => s.Id);
            modelBuilder.Entity<WorkerType>().HasAlternateKey(s => s.CategoryName);
            modelBuilder.Entity<WorkerType>().HasAlternateKey(s => s.CategoryCode);

            modelBuilder.Entity<Grade>().HasKey(s => s.Id);
            modelBuilder.Entity<Grade>().HasAlternateKey(s => s.GradeName);
            modelBuilder.Entity<Grade>().HasAlternateKey(s => s.GradeCode);
        }

        public DbSet<payroll_app.Models.repository.Department> Department { get; set; }

        public DbSet<payroll_app.Models.repository.WorkerType> WorkerType { get; set; }
        
        public DbSet<Grade> Grade { get; set; }
    }
}