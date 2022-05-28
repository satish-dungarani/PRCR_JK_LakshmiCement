using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PRCR
{
    public class Enums
    {
        public enum RequestStatus
        {
            [Display(Name = "New PR Request Created  and Assigned to Consultant")]
            Request_Created_Assigned_Consultant = 1,
            // Hod Status
            [Display(Name = "New CR Request Created  and Assigned to HOD")]
            Request_Created_Assigned_HOD = 2,
            [Display(Name = "CR Request Rejected")]
            Request_Rejected_HOD = 3,
            [Display(Name = "CR Request Approved and Assigned to Consultant")]
            Request_Approved_Assigned_Consultant = 4,
            // IT Developer status
            [Display(Name = "Request Assigned to IT Developer")]
            Request_Assigned_ITDeveloper = 5,
            [Display(Name = "Request In Process by IT Developer")]
            Request_WIP_ITDeveloper = 6,
            Request_Completed_ITDeveloper = 7,
            // Con Status
            Request_Assigned_Other_Consultant = 8,
            Request_Revised_Consultant = 9,
            Request_Escalated_Consultant = 10,
            // UAT status
            Request_Assigned_UAT = 11,
            Request_WIP_UAT = 12,
            Request_Confirmed_UAT = 13,
            // Basis Status
            Request_Assigned_Basis = 14,
            Request_WIP_Basis = 15,
            Request_Confirmed_Basis = 16,
            // Request Status
            Request_Closed = 17,
            Request_Completed = 18

        }

        public enum IssueStatus
        {
            Pending = 1,
            WIP = 2,
            Completed = 3,
            Closed = 4,
            UAT = 5
        }
    } 
}