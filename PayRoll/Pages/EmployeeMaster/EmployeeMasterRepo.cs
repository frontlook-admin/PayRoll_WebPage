using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayRoll.Pages.EmployeeMaster
{
    public class EmployeeMasterRepo
    {
        public int Id, AadharNo;

        public string
            EmployeePicture,
            AdultRegistrationNo,
            EmployeeCode,
            PfNo,
            EmployeeName,
            FatherHusbandName,
            Gender,
            PermanentAddress,
            PresentAddress,
            Nominee,
            MobileNo,
            Shift,
            Designation,
            Department,
            PanNo,
            Grade,
            Category,
            Basic,
            OffDay;

        public DateTime DateOfJoining, DateOfBirth, LastWorkingDate;
        public bool Active;

    }
}