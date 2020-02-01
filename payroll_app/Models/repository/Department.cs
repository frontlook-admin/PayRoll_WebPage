using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace payroll_app.Models.repository
{
    [Table("Department")]
    public class Department
    {
        //public IEnumerable<Department> Departments;

        public Department()
        {

        }

        public Department(int id, string departmentName, string departmentCode, int arrangeOrder)
        {
            Id = id;
            DepartmentName = departmentName;
            DepartmentCode = departmentCode;
            ArrangeOrder = arrangeOrder;
        }

        [Key]
        [Column("ID")]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Key]
        [MaxLength(30)]
        [Column("Department Name")]
        [Display(Name = "Department Name")]
        [Required]
        public string DepartmentName { get; set; }

        [Key]
        [MaxLength(30)]
        [Column("Department Code")]
        [Display(Name = "Department Code")]
        [Required]
        public string DepartmentCode { get; set; }

        [MaxLength(11)]
        [Column("Arrange Order")]
        [Display(Name = "Arrange Order")]
        [RegularExpression("\\d", ErrorMessage = "Can accept only digits..!!",
            MatchTimeoutInMilliseconds = 1000)]
        //[Required]
        public int ArrangeOrder { get; set; }
    }
}
