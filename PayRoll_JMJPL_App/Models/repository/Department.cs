using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PayRoll_JMJPL_App.Models.repository
{
    [Table("Department")]
    public class Department
    {
        [Key]
        [Column("ID")]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [MaxLength(30)]
        [Column("Department Name")]
        [Display(Name = "Department Name")]
        [Required]
        public string DepartmentName { get; set; }

        [MaxLength(30)]
        [Column("Department Code")]
        [Display(Name = "Department Code")]
        [Required]
        public string DepartmentCode { get; set; }

        [Column("Arrange Order")]
        [Display(Name = "Arrange Order")]
        //[Required]
        public int ArrangeOrder { get; set; }
    }
}
