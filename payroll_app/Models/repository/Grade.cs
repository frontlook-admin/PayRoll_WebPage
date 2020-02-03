using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace payroll_app.Models.repository
{
    [Table("Grade")]
    [Display(Name = "Grade", Description = "Stores Grade Details.")]
    public class Grade
    {
        public Grade(int id, string gradeName, string gradeCode, string arrangeOrder)
        {
            Id = id;
            GradeName = gradeName;
            GradeCode = gradeCode;
            ArrangeOrder = arrangeOrder;
        }

        [Key]
        [Column("ID")]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Key]
        [MaxLength(30, ErrorMessage = "Exceeded Character Limit..!!")]
        [Column("GradeName")]
        [Display(Name = "Grade Name")]
        [Required]
        public string GradeName { get; set; }

        [Key]
        [MaxLength(30, ErrorMessage = "Exceeded Character Limit..!!")]
        [Column("GradeCode")]
        [Display(Name = "Grade Code")]
        [Required]
        public string GradeCode { get; set; }

        [MaxLength(11, ErrorMessage = "Exceeded Character Limit..!!")]
        [RegularExpression("\\d", ErrorMessage = "Can accept only digits..!!",
            MatchTimeoutInMilliseconds = 1000)]
        [Column("ArrangeOrder")]
        [Display(Name = "Arrange Order")]
        //[Required]
        public string ArrangeOrder { get; set; }
    }
}
