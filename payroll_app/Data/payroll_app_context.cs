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
        public DbSet<Employee> Employees;
        public DbSet<AttendanceRegister> AttendanceRegisters;

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

            modelBuilder.Entity<Employee>().HasKey(s => s.Id);
            modelBuilder.Entity<Employee>().HasAlternateKey(s => s.PrimaryMobileNo);
            modelBuilder.Entity<Employee>().HasAlternateKey(s => s.SecondaryMobileNo);
            modelBuilder.Entity<Employee>().HasAlternateKey(s => s.EmailId);

            modelBuilder.Entity<AttendanceRegister>().HasKey(s => s.Id);
        }

        public DbSet<Department> Department { get; set; }
        public DbSet<WorkerType> WorkerType { get; set; }
        public DbSet<Grade> Grade { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<AttendanceRegister> AttendanceRegister { get; set; }
    }
}