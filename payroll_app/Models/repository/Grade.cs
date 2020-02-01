using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace payroll_app.Models.repository
{
    [Table("Grade")]
    public class Grade
    {
        public Grade()
        {
            
        }

        public Grade(string id, string gradeName, string gradeCode, int arrangeOrder)
        {
            
        }

        [Key]
        [Column("ID")]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Key]
        [MaxLength(30)]
        [Column("Grade Name")]
        [Display(Name = "Grade Name")]
        [Required]
        public string GradeName { get; set; }

        [Key]
        [MaxLength(30)]
        [Column("Grade Code")]
        [Display(Name = "Grade Code")]
        [Required]
        public string GradeCode { get; set; }

        [MaxLength(11)]
        [RegularExpression("\\d", ErrorMessage = "Can accept only digits..!!",
            MatchTimeoutInMilliseconds = 1000)]
        [Column("Arrange Order")]
        [Display(Name = "Arrange Order")]
        //[Required]
        public int ArrangeOrder { get; set; }
    }
}
