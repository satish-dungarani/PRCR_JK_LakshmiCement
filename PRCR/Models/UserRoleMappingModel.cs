using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRCR.Models
{
    public class UserRoleMappingModel
    {
        public int URMId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsConsultant { get; set; }
        public bool IsITDeveloper { get; set; }
        public bool IsBasisConsultant { get; set; }
        public bool IsHOD { get; set; }
        public bool IsEndUser { get; set; }
    }
}