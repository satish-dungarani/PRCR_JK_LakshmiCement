using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRCR.Models
{
    public class ModuleUserMappingModel
    {
        public int MUMId { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string Consultant1EmployeeCode { get; set; }
        public string Consultant1EmployeeName { get; set; }
        public bool HasEmail1 { get; set; }
        public bool HasSMS1 { get; set; }
        public string Email1 { get; set; }
        public string SMS1 { get; set; }
        public string Consultant2EmployeeCode { get; set; }
        public string Consultant2EmployeeName { get; set; }
        public bool HasEmail2 { get; set; }
        public bool HasSMS2 { get; set; }
        public string Email2 { get; set; }
        public string SMS2 { get; set; }
        public string Consultant3EmployeeCode { get; set; }
        public string Consultant3EmployeeName { get; set; }
        public bool HasEmail3 { get; set; }
        public bool HasSMS3 { get; set; }
        public string Email3 { get; set; }
        public string SMS3 { get; set; }
        public string BasisConsultantEmployeeCode { get; set; }
        public string BasisConsultantEmployeeName { get; set; }
        public bool HasEmail4 { get; set; }
        public bool HasSMS4 { get; set; }
        public string Email4 { get; set; }
        public string SMS4 { get; set; }
    }
}