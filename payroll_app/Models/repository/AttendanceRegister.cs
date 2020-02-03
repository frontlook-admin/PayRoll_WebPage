using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace payroll_app.Models.repository
{
    [Table("AttendanceRegister")]
    [Display(Name = "Attendance Register",Description = "Registers Employee Attendance")]
    public class AttendanceRegister
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("Id")]
        public int EmployeeId { get; set; }

        public bool Attendance { get; set; }

        [Column("AttendanceTime")]
        [Display(Name = "Attendance Time",AutoGenerateField = true)]
        [DisplayFormat(DataFormatString = "{0:dd} {0:MMMM},{0:yyyy} {dddd}, {0:h:mm:ss} {0:tt}")]
        [Editable(allowEdit:false)]
        [Timestamp]
        public byte[] AttendanceTime { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employees { get; set; }
    }
}
