using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace payroll_app.Models.repository
{
    [Table("Worker Type")]
    public class WorkerType
    {
        //public IEnumerable<WorkerType> WorkerTypes;

        public WorkerType()
        {

        }

        public WorkerType(int id, string categoryName, string categoryCode, int arrangeOrder)
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
        [MaxLength(30)]
        [Column("Category Name")]
        [Display(Name = "Category Name")]
        [Required]
        public string CategoryName { get; set; }

        [Key]
        [MaxLength(30)]
        [Column("Category Code")]
        [Display(Name = "Category Code")]
        [Required]
        public string CategoryCode { get; set; }

        [RegularExpression("\\d", ErrorMessage = "Can accept only digits..!!",
            MatchTimeoutInMilliseconds = 1000)]
        [Column("Arrange Order")]
        [Display(Name = "Arrange Order")]
        //[Required]
        public int ArrangeOrder { get; set; }

    }
}
