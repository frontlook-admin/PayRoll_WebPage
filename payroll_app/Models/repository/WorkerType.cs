using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace payroll_app.Models.repository
{
    [Table("WorkerType")]
    [Display(Name = "Worker Type", Description = "Stores Worker Type Details.")]
    public class WorkerType
    {
        //public IEnumerable<WorkerType> WorkerTypes;

        public WorkerType(int id, string categoryName, string categoryCode, string arrangeOrder)
        {
            Id = id;
            CategoryName = categoryName;
            CategoryCode = categoryCode;
            ArrangeOrder = arrangeOrder;
        }

        [Key]
        [Column("ID")]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Key]
        [MaxLength(30, ErrorMessage = "Exceeded Character Limit..!!")]
        [Column("CategoryName")]
        [Display(Name = "Category Name")]
        [Required]
        public string CategoryName { get; set; }

        [Key]
        [MaxLength(30, ErrorMessage = "Exceeded Character Limit..!!")]
        [Column("CategoryCode")]
        [Display(Name = "Category Code")]
        [Required]
        public string CategoryCode { get; set; }

        [MaxLength(11, ErrorMessage = "Exceeded Character Limit..!!")]
        [RegularExpression("\\d", ErrorMessage = "Can accept only digits..!!",
            MatchTimeoutInMilliseconds = 1000)]
        [Column("ArrangeOrder")]
        [Display(Name = "Arrange Order")]
        //[Required]
        public string ArrangeOrder { get; set; }

    }
}
