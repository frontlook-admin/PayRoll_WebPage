using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace PayRoll_JMJPL.Models
{
    [Table("Worker Type")]
    public class WorkerType
    {
        public IEnumerable<WorkerType> WorkerTypes;

        [Key]
        [Column("ID")]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [MaxLength(30)]
        [Column("Category Name")]
        [Display(Name = "Category Name")]
        [Required]
        public string CategoryName { get; set; }

        [MaxLength(30)]
        [Column("Category Code")]
        [Display(Name = "Category Code")]
        [Required]
        public string CategoryCode { get; set; }

        [MaxLength(11)]
        [Column("Arrange Order")]
        [Display(Name = "Arrange Order")]
        //[Required]
        public int ArrangeOrder { get; set; }

    }
}