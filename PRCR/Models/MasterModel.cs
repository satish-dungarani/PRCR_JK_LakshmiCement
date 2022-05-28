using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using PRCRDBA;

namespace PRCR.Models
{
    public class MasterModel
    {
        public string MasterType { get; set; }
        public int LocationId { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string LocationDesc { get; set; }
        public int ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentType { get; set; }
        public int PriorityId { get; set; }
        public string Priority { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public int UserTypeId { get; set; }
        public string UserType { get; set; }

        public int IssueTypeId { get; set; } 
        public string IssueTypeName { get; set; }
        public string IssueTypeDesc { get; set; }


    }
}