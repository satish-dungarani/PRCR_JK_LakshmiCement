using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRCR.Models
{
    public class RequestModel
    {
        public RequestModel()
        {
            Communication = new CommunicationModel();
            Communications = new List<CommunicationModel>();
            //Requests = new List<RequestModel>();
        }

        public int RequetsId { get; set; }
        public string IssueId { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string ContactName { get; set; }
        public string Department { get; set; }
        public long Mobile { get; set; }
        public int ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public int PriorityId { get; set; }
        public string PriorityName { get; set; }
        public string RequestType { get; set; }
        public bool? IsHODApprove { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string PurposeofNewRequirment { get; set; }
        public string OutcomeofChanges { get; set; }
        public string FileName { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public string HODEmployeeCode { get; set; }
        public string HODName { get; set; }
        public DateTime? HODARDate { get; set; }
        public DateTime? HODARTime { get; set; }
        public string UserType { get; set; }        
        public bool HasEmailTo2ndCon { get; set; }
        public bool HasSMSTo2ndCon { get; set; }
        public int RefModuleUserMappingId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime CreateTime { get; set; }
        public CommunicationModel Communication { get; set; }
        public List<CommunicationModel> Communications { get; set; }
        public List<DocumentsAttachmentModel> Documents { get; set; }
        //public List<RequestModel> Requests { get; set; }
        public DateTime? TargetIssueResolveDate { get; set; }
        public DateTime? ReviseIssueResolveDate { get; set; }
        public string RequestCreateEmployeeCode { get; set; }
        public string RequestCreateEmployeeName { get; set; }
        public int ConsultantLevel { get; set; }
        public int AssignedUserTypeId { get; set; }
        public string AssignedUserType { get; set; }
        public string AssignedEmployeeCode { get; set; }
        public string AssignedEmployeeName { get; set; }
        public bool IsITCompleted { get; set; }
        public bool IsBasisCompleted { get; set; }
        public int RefReqStatusId { get; set; }
        public string RefReqStatus { get; set; }
        public string BasisCoEmployeeName { get; set; }
        public string FirstCoEmployeeName { get; set; }
        public int? RefIssueTypeId { get; set; }
        public string RefIssueType { get; set; }
    }

    public class CommunicationModel
    {
        public CommunicationModel()
        {
            DocumentsAttachment = new DocumentsAttachmentModel();
        }
        public int CommunicationId { get; set; }
        public int RefRequestId { get; set; }
        public string IssueId { get; set; }
        public string CommunicationType { get; set; }
        public int AssignedUserTypeId { get; set; }
        public string AssignedUserType { get; set; }
        public string AssignedEmployeeCode { get; set; }
        public string AssignedEmployeeName { get; set; }
        public char ConsultantLevel { get; set; }
        public bool IsEscalate { get; set; }
        public string PersonForUAT { get; set; }
        public string SystemForUAT { get; set; }
        public string UserType { get; set; }
        public string Remark { get; set; }
        public bool IsUATCompleted { get; set; }
        public bool IsITCompleted { get; set; }
        public bool IsBasisCompleted { get; set; }
        public DateTime RemarkDate { get; set; }
        public DateTime RemarkTime { get; set; }
        public string RemarkEmpCode { get; set; }
        public string RemarkEmpName { get; set; }
        public DocumentsAttachmentModel DocumentsAttachment { get; set; }
    }

    public class DocumentsAttachmentModel
    {
        public int DocumentId { get; set; }
        public int RefCommunicationId { get; set; }
        public int RefDocumentTypeId { get; set; }
        public string DocumentType { get; set; }
        public string DocumentPath { get; set; }
        public string DocumentName { get; set; }
        public DateTime DocumentAttachDate { get; set; }
        public DateTime DocumentAttachTime { get; set; }
    }
}