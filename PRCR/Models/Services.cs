using PRCRDBA;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Configuration;
//using System.Web.Mail;
using System.Web.Mvc;
using System.Web.UI;

namespace PRCR.Models
{
    public class LocationServices
    {

        #region Master's Methods
        /// <summary>
        /// Save master data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveLocation(MasterModel model)
        {
            try
            {
                int result = 0;
                Location dbLoc = new Location();
                result = dbLoc.InsertUpdateLocation(
                    model.LocationId,
                    model.LocationCode,
                    model.LocationName,
                    model.LocationDesc = string.IsNullOrEmpty(model.LocationDesc) ? "" : model.LocationDesc
                    );

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MasterModel> GetLocations(int? locationId)
        {
            try
            {
                Location location = new Location();
                List<MasterModel> masterModels = location.SelectLocation(locationId).AsEnumerable().Select(x => new MasterModel()
                {
                    MasterType = "LO",
                    LocationId = Convert.ToInt32(x["LOCATIONID"]),
                    LocationName = x["LOCATIONNAME"].ToString(),
                    LocationCode = x["CODE"].ToString(),
                    LocationDesc = x["DESCRIPTION"].ToString(),

                }).ToList();

                return masterModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }

    public class ApplicationServices
    {

        #region Master's Methods
        /// <summary>
        /// Save master data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveApplication(MasterModel model)
        {
            try
            {
                int result = 0;
                Application dbLoc = new Application();
                result = dbLoc.InsertUpdateApplication(model.ApplicationId, model.ApplicationName);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MasterModel> GetApplication(int? appId)
        {
            try
            {
                Application app = new Application();
                List<MasterModel> masterModels = app.SelectApplication(appId).AsEnumerable().Select(x => new MasterModel()
                {
                    ApplicationId = Convert.ToInt32(x[0]),
                    ApplicationName = x[1].ToString(),
                    MasterType = "AP"

                }).ToList();

                return masterModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }

    public class ModuleServices
    {

        #region Master's Methods
        /// <summary>
        /// Save master data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// 

        public int SaveModule(MasterModel model)
        {
            try
            {
                int result = 0;
                Module dbLoc = new Module();
                result = dbLoc.InsertUpdateModule(model.ModuleId, model.ApplicationId, model.ModuleName);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MasterModel> GetModule(int? moduleId)
        {
            try
            {
                Module obj = new Module();
                List<MasterModel> masterModels = obj.SelectModule(moduleId).AsEnumerable().Select(x => new MasterModel()
                {
                    MasterType = "MO",
                    ModuleId = Convert.ToInt32(x["MODULEID"]),
                    ApplicationId = Convert.ToInt32(x["APPLICATIONID"]),
                    ModuleName = x["MODULENAME"].ToString(),
                    ApplicationName = x["APPLICATIONNAME"].ToString()

                }).ToList();

                return masterModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }

    public class PriorityServices
    {

        #region Master's Methods
        /// <summary>
        /// Save master data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SavePriority(MasterModel model)
        {
            try
            {
                int result = 0;
                Priority dbLoc = new Priority();
                result = dbLoc.InsertUpdatePriority(model.PriorityId, model.Priority);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MasterModel> GetPriority(int? PriorityId)
        {
            try
            {
                Priority Priority = new Priority();
                List<MasterModel> masterModels = Priority.SelectPriority(PriorityId).AsEnumerable().Select(x => new MasterModel()
                {
                    MasterType = "PR",
                    PriorityId = Convert.ToInt32(x[0]),
                    Priority = x[1].ToString()

                }).ToList();

                return masterModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }

    public class DocumentTypeServices
    {

        #region Master's Methods
        /// <summary>
        /// Save master data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveDocumentType(MasterModel model)
        {
            try
            {
                int result = 0;
                DocumentType dbLoc = new DocumentType();
                result = dbLoc.InsertUpdateDocumentType(model.DocumentTypeId, model.DocumentType);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<MasterModel> GetDocumentTypes(int? DocumentTypeId)
        {
            try
            {
                DocumentType DocumentType = new DocumentType();
                List<MasterModel> masterModels = DocumentType.SelectDocumentType(DocumentTypeId).AsEnumerable().Select(x => new MasterModel()
                {
                    MasterType = "DT",
                    DocumentTypeId = Convert.ToInt32(x[0]),
                    DocumentType = x[1].ToString()

                }).ToList();

                return masterModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }

    public class StatusServices
    {

        #region Master's Methods
        /// <summary>
        /// Save master data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveStatus(MasterModel model)
        {
            try
            {
                int result = 0;
                Status dbLoc = new Status();
                result = dbLoc.InsertUpdateStatus(model.StatusId, model.Status);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MasterModel> GetStatus(int? StatusId)
        {
            try
            {
                Status Status = new Status();
                List<MasterModel> masterModels = Status.SelectStatus(StatusId).AsEnumerable().Select(x => new MasterModel()
                {
                    MasterType = "IS",
                    StatusId = Convert.ToInt32(x[0]),
                    Status = x[1].ToString()

                }).ToList();

                return masterModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }

    public class UserTypeServices
    {

        #region Master's Methods
        ///// <summary>
        ///// Save master data
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public int SaveUserType(MasterModel model)
        //{
        //    try
        //    {
        //        int result = 0;
        //        UserType dbLoc = new UserType();
        //        result = dbLoc.InsertUpdateUserType(model.UserTypeId, model.UserType);

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<MasterModel> GetUserTypes(int? UserTypeId)
        //{
        //    try
        //    {
        //        UserType UserType = new UserType();
        //        List<MasterModel> masterModels = UserType.SelectUserType(UserTypeId).AsEnumerable().Select(x => new MasterModel()
        //        {
        //            MasterType = "LO",
        //            UserTypeId = Convert.ToInt32(x[0]),
        //            UserType = x[1].ToString()

        //        }).ToList();

        //        return masterModels;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public MasterModel GetUserTypes(int UserTypeId)
        //{
        //    try
        //    {
        //        UserType UserType = new UserType();
        //        MasterModel masterModels = UserType.SelectUserType(UserTypeId).AsEnumerable().Select(x => new MasterModel()
        //        {
        //            MasterType = "UT",
        //            UserTypeId = Convert.ToInt32(x[0]),
        //            UserType = x[1].ToString()

        //        }).FirstOrDefault();

        //        return masterModels;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        #endregion
    }

    public class IssueTypeServices
    {

        #region Master's Methods
        /// <summary>
        /// Save master data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveIssueTypes(MasterModel model)
        {
            try
            {
                int result = 0;
                IssueType dbIssueType = new IssueType();
                result = dbIssueType.InsertUpdateIssueType(
                    model.IssueTypeId,
                    model.IssueTypeName,
                    model.IssueTypeDesc = string.IsNullOrEmpty(model.IssueTypeDesc) ? "" : model.IssueTypeDesc
                    );

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MasterModel> GetIssueTypes(int? IssueTypeId)
        {
            try
            {
                IssueType dbIssueType = new IssueType();
                List<MasterModel> masterModels = dbIssueType.SelectIssueType(IssueTypeId).AsEnumerable().Select(x => new MasterModel()
                {
                    MasterType = "IST",
                    IssueTypeId = Convert.ToInt32(x["ISSUETYPEID"]),
                    IssueTypeName = x["ISSUETYPENAME"].ToString(),
                    IssueTypeDesc = x["DESCRIPTION"].ToString(),

                }).ToList();

                return masterModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }

    public class CommunicationServices
    {

        #region Master's Methods
        /// <summary>
        /// Save master data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveCommunication(CommunicationModel model)
        {
            try
            {
                int result = 0;
                Communication dbLoc = new Communication();
                result = dbLoc.InsertUpdateCommunication(
                        model.CommunicationId,
                        model.RefRequestId,
                        model.CommunicationType,
                        model.AssignedUserTypeId,
                        model.AssignedEmployeeCode,
                        model.AssignedEmployeeName,
                        model.ConsultantLevel,
                        model.IsEscalate,
                        model.PersonForUAT,
                        model.SystemForUAT,
                        model.UserType,
                        model.Remark,
                        model.IsUATCompleted,
                        model.RemarkDate,
                        model.RemarkTime,
                        model.RemarkEmpCode,
                        model.RemarkEmpName,
                        model.IsITCompleted,
                        model.IsBasisCompleted
                    );

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CommunicationModel> GetCommunications(int refRequestId)
        {
            try
            {
                Communication Communication = new Communication();
                List<CommunicationModel> CommModel = Communication.SelectCommunication(refRequestId).AsEnumerable().Select(x => new CommunicationModel()
                {
                    CommunicationId = Convert.ToInt32(x["COMMUNICTIONID"]),
                    RefRequestId = Convert.ToInt32(x["REFREQUESTID"]),
                    CommunicationType = x["COMMUNICATIONTYPE"].ToString(),
                    AssignedUserTypeId = Convert.ToInt32(x["ASSIGNEDUSERTYPEID"]),
                    AssignedEmployeeCode = x["ASSIGNEDEMPLOYEECODE"].ToString(),
                    AssignedEmployeeName = x["ASSIGNEDEMPLOYEENAME"].ToString(),
                    ConsultantLevel = Convert.ToChar(x["CONSULTANTLEVEL"]),
                    IsEscalate = x["ISESCALATE"].ToString() == "Y" ? true : false,
                    PersonForUAT = x["PERSONFORUAT"].ToString(),
                    SystemForUAT = x["SYSTEMFORUAT"].ToString(),
                    UserType = x["USERTYPE"].ToString(),
                    Remark = x["REMARK"].ToString(),
                    IsUATCompleted = x["ISUATCOMPLETED"].ToString() == "Y" ? true : false,
                    RemarkDate = Convert.ToDateTime(x["REMARKDATE"]),
                    RemarkTime = Convert.ToDateTime(x["REMARKTIME"]),
                    RemarkEmpCode = x["REMARKEMPCODE"].ToString(),
                    RemarkEmpName = x["REMARKEMPNAME"].ToString(),
                    DocumentsAttachment = new DocumentsAttachmentModel()
                    {
                        DocumentId = Convert.ToInt32(x["DOCUMENTID"]),
                        RefCommunicationId = Convert.ToInt32(x["REFCOMMUNICATIONID"]),
                        RefDocumentTypeId = Convert.ToInt32(x["DOCUMENTTYPEID"]),
                        DocumentType = x["DOCUMENTTYPE"].ToString(),
                        DocumentPath = x["DOCUMENTPATH"].ToString(),
                        DocumentAttachDate = Convert.ToDateTime(x["DOCUMENTATTACHDATE"]),
                        DocumentAttachTime = Convert.ToDateTime(x["DOCUMENTATTACHTIME"])
                    }

                }).ToList();

                return CommModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CommunicationModel> GetCommunications(string type, int? requestId)
        {
            try
            {
                Communication Communication = new Communication();
                List<CommunicationModel> CommModel = Communication.SelectCommunication(type, requestId).AsEnumerable().Select(y => new CommunicationModel()
                    {
                        CommunicationId = Convert.ToInt32(y["COMMUNICTIONID"]),
                        ConsultantLevel = Convert.ToChar(y["CONSULTANTLEVEL"]),
                        AssignedUserTypeId = Convert.ToInt32(y["ASSIGNEDUSERTYPEID"]),
                        AssignedEmployeeCode = Convert.ToString(y["ASSIGNEDEMPLOYEECODE"]),
                        Remark = Convert.ToString(y["REMARK"]),
                        RemarkDate = Convert.ToDateTime(y["REMARKDATE"]),
                        RemarkTime = Convert.ToDateTime(y["REMARKTIME"]),
                        RemarkEmpCode = Convert.ToString(y["REMARKEMPCODE"]),
                        RemarkEmpName = Convert.ToString(y["REMARKEMPNAME"]),
                        IsEscalate = y["ISESCALATE"].ToString() == "Y" ? true : false,
                        UserType = Convert.ToString(y["USERTYPE"]),
                    }).ToList();

                return CommModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }

    public class DocumentServices
    {

        #region Master's Methods
        /// <summary>
        /// Save master data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveDocument(DocumentsAttachmentModel model)
        {
            try
            {
                int result = 0;
                Document dbLoc = new Document();
                result = dbLoc.InsertUpdateDocument(model.DocumentId, model.RefCommunicationId, model.RefDocumentTypeId, model.DocumentPath, model.DocumentAttachDate, model.DocumentAttachTime);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentsAttachmentModel> GetDocuments(int? DocumentId)
        {
            try
            {
                Document Document = new Document();
                List<DocumentsAttachmentModel> masterModels = Document.SelectDocument(DocumentId).AsEnumerable().Select(x => new DocumentsAttachmentModel()
                {
                    DocumentId = Convert.ToInt32(x[0]),
                    RefCommunicationId = Convert.ToInt32(x[1]),
                    RefDocumentTypeId = Convert.ToInt32(x[2]),
                    DocumentPath = x[3].ToString(),
                    DocumentAttachDate = Convert.ToDateTime(x[4]),
                    //DocumentAttachTime = x[5].ToString()
                }).ToList();

                return masterModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentsAttachmentModel> GetDocuments(string EmployeeCode, int? DocumentId)
        {
            try
            {
                Document Document = new Document();
                List<DocumentsAttachmentModel> masterModels = Document.SelectDocuments(EmployeeCode, DocumentId).AsEnumerable().Select(x => new DocumentsAttachmentModel()
                {
                    DocumentId = Convert.ToInt32(x["DOCUMENTID"]),
                    RefDocumentTypeId = Convert.ToInt32(x["DOCUMENTTYPEID"]),
                    DocumentType = Convert.ToString(x["DOCUMENTTYPE"]),
                    DocumentPath = Convert.ToString(x["DOCUMENTPATH"]),
                    DocumentName = string.IsNullOrEmpty(Convert.ToString(x["DOCUMENTPATH"])) ? "" : x["DOCUMENTPATH"].ToString().Split('\\')[3],
                    DocumentAttachDate = Convert.ToDateTime(x["DOCUMENTATTACHDATE"]),
                    DocumentAttachTime = Convert.ToDateTime(x["DOCUMENTATTACHTIME"])
                }).ToList();

                return masterModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }

    public class IssueRequestServices
    {

        #region Master's Methods
        /// <summary>
        /// Save master data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// 
        public int SaveIssueRequest(RequestModel model)
        {
            try
            {
                int result = 0;
                IssueRequest dbLoc = new IssueRequest();
                result = dbLoc.InsertUpdateIssueRequest(
                            model.RequetsId,
                            model.IssueId,
                            model.LocationId,
                            model.ContactName,
                            model.Department,
                            model.Mobile,
                            model.ApplicationId,
                            model.ModuleId,
                            model.PriorityId,
                            model.RequestType,
                            model.IsHODApprove,
                            model.Subject,
                            model.Description,
                            model.PurposeofNewRequirment,
                            model.OutcomeofChanges,
                            model.StatusId,
                            model.HODEmployeeCode,
                            model.HODARDate,
                            model.HODARTime,
                            model.UserType,
                            model.HasEmailTo2ndCon,
                            model.HasSMSTo2ndCon,
                            model.RefModuleUserMappingId,
                            model.CreateDate,
                            model.CreateTime,
                            model.TargetIssueResolveDate,
                            model.ReviseIssueResolveDate,
                            model.RequestCreateEmployeeCode,
                            model.RefReqStatusId,
                            model.RefIssueTypeId
                    );

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RequestModel> GetIssueRequests(string EmployeeCode, int? requestId = null, int? locationId = null, int? applicationId = null,
            int? moduleid = null, int? statusId = null, int? priorityId = null, string fromDate = null, string toDate = null, int? refReqStatusId = null, int? refIssueTypeId = null)
        {
            try
            {
                IssueRequest IssueRequest = new IssueRequest();
                DateTime? dt = null;
                List<RequestModel> RequestModels = IssueRequest.
                    SelectIssueRequest(EmployeeCode, requestId, locationId, applicationId,
                    moduleid, statusId, priorityId, fromDate, toDate, refReqStatusId, refIssueTypeId).AsEnumerable().Select(x => new RequestModel()
                {
                    RequetsId = Convert.ToInt32(x["REQUESTID"]),
                    IssueId = x["ISSUECODE"].ToString(),
                    LocationId = Convert.ToInt32(x["LOCATIONID"]),
                    LocationName = Convert.ToString(x["LOCATIONNAME"]),
                    ContactName = x["CONTACTNAME"].ToString(),
                    Department = x["DEPARTMENT"].ToString(),
                    Mobile = Convert.ToInt64(x["MOBILE"]),
                    ApplicationId = Convert.ToInt32(x["APPLICATIONID"]),
                    ApplicationName = Convert.ToString(x["APPLICATIONNAME"]),
                    ModuleId = Convert.ToInt32(x["MODULEID"]),
                    ModuleName = x["MODULENAME"] == DBNull.Value || x["MODULENAME"] == "" || x["MODULENAME"] == null ? "" : Convert.ToString(x["MODULENAME"]),
                    PriorityId = Convert.ToInt32(x["PRIORITYID"]),
                    PriorityName = x["PRIORITY"].ToString(),
                    RequestType = x["REQUESTTYPE"].ToString(),
                    IsHODApprove = Convert.ToChar(x["ISHODAPPROVE"]) == 'Y' ? true : false,
                    Subject = x["SUBJECT"].ToString(),
                    Description = x["DESCRIPTION"].ToString(),
                    PurposeofNewRequirment = x["PURPOSEOFNEWREQUIRMENT"] == DBNull.Value || x["PURPOSEOFNEWREQUIRMENT"] == "" || x["PURPOSEOFNEWREQUIRMENT"] == null ? "" : x["PURPOSEOFNEWREQUIRMENT"].ToString(),
                    OutcomeofChanges = x["OUTCOMEOFCHANGES"] == DBNull.Value || x["OUTCOMEOFCHANGES"] == "" || x["OUTCOMEOFCHANGES"] == null ? "" : x["OUTCOMEOFCHANGES"].ToString(),
                    StatusId = Convert.ToInt32(x["STATUSID"]),
                    Status = x["STATUS"].ToString(),
                    HODEmployeeCode = x["HODEMPLOYEECODE"] == DBNull.Value || x["HODEMPLOYEECODE"] == "" || x["HODEMPLOYEECODE"] == null ? "" : x["HODEMPLOYEECODE"].ToString(),
                    HODName = x["HODEMPLOYEENAME"] == DBNull.Value || x["HODEMPLOYEENAME"] == "" || x["HODEMPLOYEENAME"] == null ? "" : x["HODEMPLOYEENAME"].ToString(),
                    HODARDate = x["HODARDATE"] == DBNull.Value || x["HODARDATE"] == "" || x["HODARDATE"] == null ? dt : Convert.ToDateTime(x["HODARDATE"]),
                    HODARTime = x["HODARTIME"] == DBNull.Value || x["HODARTIME"] == "" || x["HODARTIME"] == null ? dt : Convert.ToDateTime(x["HODARTIME"].ToString().Substring(0, 18).Replace('.', ':') + (x["HODARTIME"].ToString().Contains("AM") ? " AM" : " PM")),
                    UserType = x["USERTYPE"].ToString(),
                    HasEmailTo2ndCon = Convert.ToChar(x["HASEMAILTO2NDCON"]) == 'Y' ? true : false,
                    HasSMSTo2ndCon = Convert.ToChar(x["HASSMSTO2NDCON"]) == 'Y' ? true : false,
                    RefModuleUserMappingId = Convert.ToInt32(x["REFMUMID"]),
                    CreateDate = Convert.ToDateTime(x["CREATEDATE"]),
                    CreateTime = Convert.ToDateTime(x["CREATETIME"].ToString().Substring(0, 18).Replace('.', ':') + (x["CREATETIME"].ToString().Contains("AM") ? " AM" : " PM")),
                    TargetIssueResolveDate = x["TARGETISSUERESOLVEDATE"] == DBNull.Value || x["TARGETISSUERESOLVEDATE"] == "" || x["TARGETISSUERESOLVEDATE"] == null ? dt : Convert.ToDateTime(x["TARGETISSUERESOLVEDATE"]),
                    ReviseIssueResolveDate = x["REVISEISSUERESOLVEDATE"] == DBNull.Value || x["REVISEISSUERESOLVEDATE"] == "" || x["REVISEISSUERESOLVEDATE"] == null ? dt : Convert.ToDateTime(x["REVISEISSUERESOLVEDATE"]),
                    RequestCreateEmployeeCode = x["REQUESTCREATEEMPLOYEECODE"].ToString(),
                    AssignedUserTypeId = Convert.ToInt32(x["ASSIGNEDUSERTYPEID"]),
                    RefReqStatusId = Convert.ToInt32(x["REFREQSTATUSID"]),
                    RefReqStatus = x["REFREQSTATUS"].ToString(),    //((Enums.RequestStatus)Convert.ToInt32(x["REFREQSTATUSID"])).ToString()
                    AssignedEmployeeName = x["ASSIGNEDEMPLOYEENAME"] == null ? null : x["ASSIGNEDEMPLOYEENAME"].ToString(),
                    RequestCreateEmployeeName = x["REQUESTCREATEEMPLOYEENAME"].ToString(),
                    FirstCoEmployeeName = x["FIRSTCOEMPLOYEENAME"].ToString(),
                    BasisCoEmployeeName = x["BASISCOEMPLOYEENAME"] == null ? null : x["BASISCOEMPLOYEENAME"].ToString()
                }).ToList();

                return RequestModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RequestModel> GetConsultantIssueRequests(string EmployeeCode, int? requestId = null, int? locationId = null, int? applicationId = null,
            int? moduleid = null, int? statusId = null, int? priorityId = null, string fromDate = null, string toDate = null, int? refReqStatusId = null, int? refIssueTypeId = null)
        {
            try
            {
                IssueRequest IssueRequest = new IssueRequest();
                DateTime? dt = null;
                List<RequestModel> RequestModels = IssueRequest.SelectConsultantIssueRequest(EmployeeCode, requestId, locationId, applicationId,
                    moduleid, statusId, priorityId, fromDate, toDate, refReqStatusId, refIssueTypeId).AsEnumerable().Select(x => new RequestModel()
                {
                    RequetsId = Convert.ToInt32(x["REQUESTID"]),
                    IssueId = x["ISSUECODE"].ToString(),
                    LocationId = Convert.ToInt32(x["LOCATIONID"]),
                    LocationName = Convert.ToString(x["LOCATIONNAME"]),
                    ContactName = x["CONTACTNAME"].ToString(),
                    Department = x["DEPARTMENT"].ToString(),
                    Mobile = Convert.ToInt64(x["MOBILE"]),
                    ApplicationId = Convert.ToInt32(x["APPLICATIONID"]),
                    ApplicationName = Convert.ToString(x["APPLICATIONNAME"]),
                    ModuleId = Convert.ToInt32(x["MODULEID"]),
                    ModuleName = x["MODULENAME"] == null ? "" : Convert.ToString(x["MODULENAME"]),
                    PriorityId = Convert.ToInt32(x["PRIORITYID"]),
                    PriorityName = x["PRIORITY"].ToString(),
                    RequestType = x["REQUESTTYPE"].ToString(),
                    IsHODApprove = Convert.ToChar(x["ISHODAPPROVE"]) == 'Y' ? true : false,
                    Subject = x["SUBJECT"].ToString(),
                    Description = x["DESCRIPTION"].ToString(),
                    PurposeofNewRequirment = x["PURPOSEOFNEWREQUIRMENT"] == null ? "" : x["PURPOSEOFNEWREQUIRMENT"].ToString(),
                    OutcomeofChanges = x["OUTCOMEOFCHANGES"] == null ? "" : x["OUTCOMEOFCHANGES"].ToString(),
                    StatusId = Convert.ToInt32(x["STATUSID"]),
                    Status = x["STATUS"].ToString(),
                    HODEmployeeCode = x["HODEMPLOYEECODE"].ToString(),
                    HODARDate = x["HODARDATE"] == DBNull.Value ? dt : Convert.ToDateTime(x["HODARDATE"]),
                    HODARTime = x["HODARTIME"] == DBNull.Value ? dt : Convert.ToDateTime(x["HODARTIME"].ToString().Substring(0, 18).Replace('.', ':') + (x["HODARTIME"].ToString().Contains("AM") ? " AM" : " PM")),
                    UserType = x["USERTYPE"].ToString(),
                    HasEmailTo2ndCon = Convert.ToChar(x["HASEMAILTO2NDCON"]) == 'Y' ? true : false,
                    HasSMSTo2ndCon = Convert.ToChar(x["HASSMSTO2NDCON"]) == 'Y' ? true : false,
                    RefModuleUserMappingId = Convert.ToInt32(x["REFMUMID"]),
                    CreateDate = Convert.ToDateTime(x["CREATEDATE"]),
                    CreateTime = Convert.ToDateTime(x["CREATETIME"].ToString().Substring(0, 18).Replace('.', ':') + (x["CREATETIME"].ToString().Contains("AM") ? " AM" : " PM")),
                    TargetIssueResolveDate = x["TARGETISSUERESOLVEDATE"] == DBNull.Value ? dt : Convert.ToDateTime(x["TARGETISSUERESOLVEDATE"]),
                    ReviseIssueResolveDate = x["REVISEISSUERESOLVEDATE"] == DBNull.Value ? dt : Convert.ToDateTime(x["REVISEISSUERESOLVEDATE"]),
                    RequestCreateEmployeeCode = x["REQUESTCREATEEMPLOYEECODE"].ToString(),
                    ConsultantLevel = Convert.ToInt32(x["CONSULTANTLEVEL"]),
                    AssignedUserTypeId = Convert.ToInt32(x["ASSIGNEDUSERTYPEID"]),
                    //AssignedUserType = Convert.ToString(x["ASSIGNEDUSERTYPE"]),
                    AssignedEmployeeCode = Convert.ToString(x["ASSIGNEDEMPLOYEECODE"]),
                    // AssignedEmployeeName = authModel.GetEmployee(Convert.ToString(x["ASSIGNEDEMPLOYEECODE"])).ENAME,  //Authentication().FirstOrDefault().Text.ToString(),
                    RefReqStatusId = Convert.ToInt32(x["REFREQSTATUSID"]),
                    RefReqStatus = x["REFREQSTATUS"].ToString(),    //((Enums.RequestStatus)Convert.ToInt32(x["REFREQSTATUSID"])).ToString()
                    AssignedEmployeeName = x["ASSIGNEDEMPLOYEENAME"] == null ? null : x["ASSIGNEDEMPLOYEENAME"].ToString(),
                    RequestCreateEmployeeName = x["REQUESTCREATEEMPLOYEENAME"].ToString(),
                    FirstCoEmployeeName = x["FIRSTCOEMPLOYEENAME"].ToString(),
                    BasisCoEmployeeName = x["BASISCOEMPLOYEENAME"] == null ? null : x["BASISCOEMPLOYEENAME"].ToString(),
                    RefIssueTypeId = Convert.ToInt32(x["REFISSUETYPEID"]),
                    RefIssueType = x["ISSUETYPE"] == null ? null : x["ISSUETYPE"].ToString()
                }).ToList();

                //AuthenticationService authModel = new AuthenticationService();
                //foreach (var item in RequestModels)
                //{
                //    item.AssignedEmployeeName = !string.IsNullOrEmpty(item.AssignedEmployeeCode) ? authModel.GetEmployee(item.AssignedEmployeeCode).ENAME : null;
                //}

                return RequestModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RequestModel> GetITDeveloperIssueRequests(string EmployeeCode, int? requestId = null, int? locationId = null, int? applicationId = null,
            int? moduleid = null, int? statusId = null, int? priorityId = null, string fromDate = null, string toDate = null, int? refReqStatusId = null, int? refIssueTypeId = null)
        {
            try
            {
                IssueRequest IssueRequest = new IssueRequest();
                DateTime? dt = null;
                List<RequestModel> RequestModels = IssueRequest.SelectITDeveloperIssueRequest(EmployeeCode, requestId, locationId, applicationId,
                    moduleid, statusId, priorityId, fromDate, toDate, refReqStatusId, refIssueTypeId).AsEnumerable().Select(x => new RequestModel()
                {
                    RequetsId = Convert.ToInt32(x["REQUESTID"]),
                    IssueId = x["ISSUECODE"].ToString(),
                    LocationId = Convert.ToInt32(x["LOCATIONID"]),
                    LocationName = Convert.ToString(x["LOCATIONNAME"]),
                    ContactName = x["CONTACTNAME"].ToString(),
                    Department = x["DEPARTMENT"].ToString(),
                    Mobile = Convert.ToInt64(x["MOBILE"]),
                    ApplicationId = Convert.ToInt32(x["APPLICATIONID"]),
                    ApplicationName = Convert.ToString(x["APPLICATIONNAME"]),
                    ModuleId = Convert.ToInt32(x["MODULEID"]),
                    ModuleName = x["MODULENAME"] == null ? "" : Convert.ToString(x["MODULENAME"]),
                    PriorityId = Convert.ToInt32(x["PRIORITYID"]),
                    PriorityName = x["PRIORITY"].ToString(),
                    RequestType = x["REQUESTTYPE"].ToString(),
                    IsHODApprove = Convert.ToChar(x["ISHODAPPROVE"]) == 'Y' ? true : false,
                    Subject = x["SUBJECT"].ToString(),
                    Description = x["DESCRIPTION"].ToString(),
                    PurposeofNewRequirment = x["PURPOSEOFNEWREQUIRMENT"] == null ? "" : x["PURPOSEOFNEWREQUIRMENT"].ToString(),
                    OutcomeofChanges = x["OUTCOMEOFCHANGES"] == null ? "" : x["OUTCOMEOFCHANGES"].ToString(),
                    StatusId = Convert.ToInt32(x["STATUSID"]),
                    Status = Convert.ToString(x["STATUS"]),
                    HODEmployeeCode = x["HODEMPLOYEECODE"] == null ? "" : x["HODEMPLOYEECODE"].ToString(),
                    HODARDate = x["HODARDATE"] == DBNull.Value ? dt : Convert.ToDateTime(x["HODARDATE"]),
                    HODARTime = x["HODARTIME"] == DBNull.Value ? dt : Convert.ToDateTime(x["HODARTIME"].ToString().Substring(0, 18).Replace('.', ':') + (x["HODARTIME"].ToString().Contains("AM") ? " AM" : " PM")),
                    UserType = x["USERTYPE"].ToString(),
                    HasEmailTo2ndCon = Convert.ToChar(x["HASEMAILTO2NDCON"]) == 'Y' ? true : false,
                    HasSMSTo2ndCon = Convert.ToChar(x["HASSMSTO2NDCON"]) == 'Y' ? true : false,
                    RefModuleUserMappingId = Convert.ToInt32(x["REFMUMID"]),
                    CreateDate = Convert.ToDateTime(x["CREATEDATE"]),
                    CreateTime = Convert.ToDateTime(x["CREATETIME"].ToString().Substring(0, 18).Replace('.', ':') + (x["CREATETIME"].ToString().Contains("AM") ? " AM" : " PM")),
                    TargetIssueResolveDate = x["TARGETISSUERESOLVEDATE"] == DBNull.Value ? dt : Convert.ToDateTime(x["TARGETISSUERESOLVEDATE"]),
                    ReviseIssueResolveDate = x["REVISEISSUERESOLVEDATE"] == DBNull.Value ? dt : Convert.ToDateTime(x["REVISEISSUERESOLVEDATE"]),
                    RequestCreateEmployeeCode = x["REQUESTCREATEEMPLOYEECODE"].ToString(),
                    AssignedUserTypeId = Convert.ToInt32(x["ASSIGNEDUSERTYPEID"]),
                    AssignedEmployeeCode = Convert.ToString(x["ASSIGNEDEMPLOYEECODE"]),
                    IsITCompleted = Convert.ToString(x["ISITCOMPLETED"]) == "Y" ? true : false,
                    RefReqStatusId = Convert.ToInt32(x["REFREQSTATUSID"]),
                    RefReqStatus = x["REFREQSTATUS"].ToString(),    //((Enums.RequestStatus)Convert.ToInt32(x["REFREQSTATUSID"])).ToString()
                    AssignedEmployeeName = x["ASSIGNEDEMPLOYEENAME"] == null ? null : x["ASSIGNEDEMPLOYEENAME"].ToString(),
                    RequestCreateEmployeeName = x["REQUESTCREATEEMPLOYEENAME"].ToString(),
                    FirstCoEmployeeName = x["FIRSTCOEMPLOYEENAME"].ToString(),
                    BasisCoEmployeeName = x["BASISCOEMPLOYEENAME"] == null ? null : x["BASISCOEMPLOYEENAME"].ToString(),
                    RefIssueTypeId = Convert.ToInt32(x["REFISSUETYPEID"]),
                    RefIssueType = x["ISSUETYPE"] == null ? null : x["ISSUETYPE"].ToString()
                }).ToList();

                return RequestModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RequestModel> GetBasisConsultantIssueRequests(string EmployeeCode, int? requestId = null, int? locationId = null, int? applicationId = null,
            int? moduleid = null, int? statusId = null, int? priorityId = null, string fromDate = null, string toDate = null, int? refReqStatusId = null, int? refIssueTypeId = null)
        {
            try
            {
                IssueRequest IssueRequest = new IssueRequest();
                DateTime? dt = null;
                List<RequestModel> RequestModels = IssueRequest.SelectBasisConsultantIssueRequest(EmployeeCode, requestId, locationId, applicationId,
                    moduleid, statusId, priorityId, fromDate, toDate, refReqStatusId, refIssueTypeId).AsEnumerable().Select(x => new RequestModel()
                {
                    RequetsId = Convert.ToInt32(x["REQUESTID"]),
                    IssueId = x["ISSUECODE"].ToString(),
                    LocationId = Convert.ToInt32(x["LOCATIONID"]),
                    LocationName = Convert.ToString(x["LOCATIONNAME"]),
                    ContactName = x["CONTACTNAME"].ToString(),
                    Department = x["DEPARTMENT"].ToString(),
                    Mobile = Convert.ToInt64(x["MOBILE"]),
                    ApplicationId = Convert.ToInt32(x["APPLICATIONID"]),
                    ApplicationName = Convert.ToString(x["APPLICATIONNAME"]),
                    ModuleId = Convert.ToInt32(x["MODULEID"]),
                    ModuleName = x["MODULENAME"] == null ? "" : Convert.ToString(x["MODULENAME"]),
                    PriorityId = Convert.ToInt32(x["PRIORITYID"]),
                    PriorityName = x["PRIORITY"].ToString(),
                    RequestType = x["REQUESTTYPE"].ToString(),
                    IsHODApprove = Convert.ToChar(x["ISHODAPPROVE"]) == 'Y' ? true : false,
                    Subject = x["SUBJECT"].ToString(),
                    Description = x["DESCRIPTION"].ToString(),
                    PurposeofNewRequirment = x["PURPOSEOFNEWREQUIRMENT"] == null ? "" : x["PURPOSEOFNEWREQUIRMENT"].ToString(),
                    OutcomeofChanges = x["OUTCOMEOFCHANGES"] == null ? "" : x["OUTCOMEOFCHANGES"].ToString(),
                    StatusId = Convert.ToInt32(x["STATUSID"]),
                    Status = x["STATUS"].ToString(),
                    HODEmployeeCode = x["HODEMPLOYEECODE"] == null ? "" : x["HODEMPLOYEECODE"].ToString(),
                    HODARDate = x["HODARDATE"] == DBNull.Value ? dt : Convert.ToDateTime(x["HODARDATE"]),
                    HODARTime = x["HODARTIME"] == DBNull.Value ? dt : Convert.ToDateTime(x["HODARTIME"].ToString().Substring(0, 18).Replace('.', ':') + (x["HODARTIME"].ToString().Contains("AM") ? " AM" : " PM")),
                    UserType = x["USERTYPE"].ToString(),
                    HasEmailTo2ndCon = Convert.ToChar(x["HASEMAILTO2NDCON"]) == 'Y' ? true : false,
                    HasSMSTo2ndCon = Convert.ToChar(x["HASSMSTO2NDCON"]) == 'Y' ? true : false,
                    RefModuleUserMappingId = Convert.ToInt32(x["REFMUMID"]),
                    CreateDate = Convert.ToDateTime(x["CREATEDATE"]),
                    CreateTime = Convert.ToDateTime(x["CREATETIME"].ToString().Substring(0, 18).Replace('.', ':') + (x["CREATETIME"].ToString().Contains("AM") ? " AM" : " PM")),
                    TargetIssueResolveDate = x["TARGETISSUERESOLVEDATE"] == DBNull.Value ? dt : Convert.ToDateTime(x["TARGETISSUERESOLVEDATE"]),
                    ReviseIssueResolveDate = x["REVISEISSUERESOLVEDATE"] == DBNull.Value ? dt : Convert.ToDateTime(x["REVISEISSUERESOLVEDATE"]),
                    RequestCreateEmployeeCode = x["REQUESTCREATEEMPLOYEECODE"].ToString(),
                    AssignedUserTypeId = Convert.ToInt32(x["ASSIGNEDUSERTYPEID"]),
                    IsBasisCompleted = Convert.ToString(x["ISBASISCOMPLETED"]) == "Y" ? true : false,
                    RefReqStatusId = Convert.ToInt32(x["REFREQSTATUSID"]),
                    RefReqStatus = x["REFREQSTATUS"].ToString(),    //((Enums.RequestStatus)Convert.ToInt32(x["REFREQSTATUSID"])).ToString()
                    AssignedEmployeeName = x["ASSIGNEDEMPLOYEENAME"] == null ? null : x["ASSIGNEDEMPLOYEENAME"].ToString(),
                    RequestCreateEmployeeName = x["REQUESTCREATEEMPLOYEENAME"].ToString(),
                    FirstCoEmployeeName = x["FIRSTCOEMPLOYEENAME"].ToString(),
                    BasisCoEmployeeName = x["BASISCOEMPLOYEENAME"] == null ? null : x["BASISCOEMPLOYEENAME"].ToString(),
                    RefIssueTypeId = Convert.ToInt32(x["REFISSUETYPEID"]),
                    RefIssueType = x["ISSUETYPE"] == null ? null : x["ISSUETYPE"].ToString()
                }).ToList();

                return RequestModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RequestModel> GetHODIssueRequests(string EmployeeCode, int? requestId = null, int? locationId = null, int? applicationId = null,
            int? moduleid = null, int? statusId = null, int? priorityId = null, string fromDate = null, string toDate = null, int? refReqStatusId = null, int? refIssueTypeId = null)
        {
            try
            {
                IssueRequest IssueRequest = new IssueRequest();
                DateTime? dt = null;
                List<RequestModel> RequestModels = IssueRequest.SelectHODIssueRequest(EmployeeCode, requestId, locationId, applicationId,
                    moduleid, statusId, priorityId, fromDate, toDate, refReqStatusId, refIssueTypeId).AsEnumerable().Select(x => new RequestModel()
                {
                    RequetsId = Convert.ToInt32(x["REQUESTID"]),
                    IssueId = x["ISSUECODE"].ToString(),
                    LocationId = Convert.ToInt32(x["LOCATIONID"]),
                    LocationName = Convert.ToString(x["LOCATIONNAME"]),
                    ContactName = x["CONTACTNAME"].ToString(),
                    Department = x["DEPARTMENT"].ToString(),
                    Mobile = Convert.ToInt64(x["MOBILE"]),
                    ApplicationId = Convert.ToInt32(x["APPLICATIONID"]),
                    ApplicationName = Convert.ToString(x["APPLICATIONNAME"]),
                    ModuleId = Convert.ToInt32(x["MODULEID"]),
                    ModuleName = x["MODULENAME"] == null ? "" : Convert.ToString(x["MODULENAME"]),
                    PriorityId = Convert.ToInt32(x["PRIORITYID"]),
                    PriorityName = x["PRIORITY"].ToString(),
                    RequestType = x["REQUESTTYPE"].ToString(),
                    IsHODApprove = Convert.ToChar(x["ISHODAPPROVE"]) == 'Y' ? true : false,
                    Subject = x["SUBJECT"].ToString(),
                    Description = x["DESCRIPTION"].ToString(),
                    PurposeofNewRequirment = x["PURPOSEOFNEWREQUIRMENT"] == null ? null : x["PURPOSEOFNEWREQUIRMENT"].ToString(),
                    OutcomeofChanges = x["OUTCOMEOFCHANGES"] == null ? "" : x["OUTCOMEOFCHANGES"].ToString(),
                    StatusId = Convert.ToInt32(x["STATUSID"]),
                    HODEmployeeCode = x["HODEMPLOYEECODE"] == null ? "" : x["HODEMPLOYEECODE"].ToString(),
                    HODARDate = x["HODARDATE"] == DBNull.Value ? dt : Convert.ToDateTime(x["HODARDATE"]),
                    HODARTime = x["HODARTIME"] == DBNull.Value ? dt : Convert.ToDateTime(x["HODARTIME"].ToString().Substring(0, 18).Replace('.', ':') + (x["HODARTIME"].ToString().Contains("AM") ? " AM" : " PM")),
                    UserType = x["USERTYPE"].ToString(),
                    HasEmailTo2ndCon = Convert.ToChar(x["HASEMAILTO2NDCON"]) == 'Y' ? true : false,
                    HasSMSTo2ndCon = Convert.ToChar(x["HASSMSTO2NDCON"]) == 'Y' ? true : false,
                    RefModuleUserMappingId = Convert.ToInt32(x["REFMUMID"]),
                    CreateDate = Convert.ToDateTime(x["CREATEDATE"]),
                    CreateTime = Convert.ToDateTime(x["CREATETIME"].ToString().Substring(0, 18).Replace('.', ':') + (x["CREATETIME"].ToString().Contains("AM") ? " AM" : " PM")),
                    TargetIssueResolveDate = x["TARGETISSUERESOLVEDATE"] == DBNull.Value ? dt : Convert.ToDateTime(x["TARGETISSUERESOLVEDATE"]),
                    ReviseIssueResolveDate = x["REVISEISSUERESOLVEDATE"] == DBNull.Value ? dt : Convert.ToDateTime(x["REVISEISSUERESOLVEDATE"]),
                    RequestCreateEmployeeCode = x["REQUESTCREATEEMPLOYEECODE"].ToString(),
                    AssignedUserTypeId = Convert.ToInt32(x["ASSIGNEDUSERTYPEID"]),
                    RefReqStatusId = Convert.ToInt32(x["REFREQSTATUSID"]),
                    RefReqStatus = x["REFREQSTATUS"].ToString(),    //((Enums.RequestStatus)Convert.ToInt32(x["REFREQSTATUSID"])).ToString()
                    AssignedEmployeeName = x["ASSIGNEDEMPLOYEENAME"] == null ? null : x["ASSIGNEDEMPLOYEENAME"].ToString(),
                    RequestCreateEmployeeName = x["REQUESTCREATEEMPLOYEENAME"].ToString(),
                    FirstCoEmployeeName = x["FIRSTCOEMPLOYEENAME"].ToString(),
                    BasisCoEmployeeName = x["BASISCOEMPLOYEENAME"] == null ? null : x["BASISCOEMPLOYEENAME"].ToString(),
                    RefIssueTypeId = Convert.ToInt32(x["REFISSUETYPEID"]),
                    RefIssueType = x["ISSUETYPE"] == null ? null : x["ISSUETYPE"].ToString()
                }).ToList();

                return RequestModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RequestModel> GetAdminIssueRequests(int? requestId = null, int? locationId = null, int? applicationId = null,
            int? moduleid = null, int? statusId = null, int? priorityId = null, string fromDate = null, string toDate = null, int? refReqStatusId = null, int? refIssueTypeId = null)
        {
            try
            {
                IssueRequest IssueRequest = new IssueRequest();
                DateTime? dt = null;
                List<RequestModel> RequestModels = IssueRequest.SelectAdminIssueRequest(requestId, locationId, applicationId,
                    moduleid, statusId, priorityId, fromDate, toDate, refReqStatusId, refIssueTypeId).AsEnumerable().Select(x => new RequestModel()
                {
                    RequetsId = Convert.ToInt32(x["REQUESTID"]),
                    IssueId = x["ISSUECODE"].ToString(),
                    LocationId = Convert.ToInt32(x["LOCATIONID"]),
                    LocationName = Convert.ToString(x["LOCATIONNAME"]),
                    ContactName = x["CONTACTNAME"].ToString(),
                    Department = x["DEPARTMENT"].ToString(),
                    Mobile = Convert.ToInt64(x["MOBILE"]),
                    ApplicationId = Convert.ToInt32(x["APPLICATIONID"]),
                    ApplicationName = Convert.ToString(x["APPLICATIONNAME"]),
                    ModuleId = Convert.ToInt32(x["MODULEID"]),
                    ModuleName = x["MODULENAME"] == null ? "" : Convert.ToString(x["MODULENAME"]),
                    PriorityId = Convert.ToInt32(x["PRIORITYID"]),
                    PriorityName = x["PRIORITY"].ToString(),
                    RequestType = x["REQUESTTYPE"].ToString(),
                    IsHODApprove = Convert.ToChar(x["ISHODAPPROVE"]) == 'Y' ? true : false,
                    Subject = x["SUBJECT"].ToString(),
                    Description = x["DESCRIPTION"].ToString(),
                    PurposeofNewRequirment = x["PURPOSEOFNEWREQUIRMENT"] == null ? "" : x["PURPOSEOFNEWREQUIRMENT"].ToString(),
                    OutcomeofChanges = x["OUTCOMEOFCHANGES"] == null ? "" : x["OUTCOMEOFCHANGES"].ToString(),
                    StatusId = Convert.ToInt32(x["STATUSID"]),
                    Status = x["STATUS"].ToString(),
                    HODEmployeeCode = x["HODEMPLOYEECODE"] == null ? "" : x["HODEMPLOYEECODE"].ToString(),
                    HODARDate = x["HODARDATE"] == DBNull.Value ? dt : Convert.ToDateTime(x["HODARDATE"]),
                    HODARTime = x["HODARTIME"] == DBNull.Value ? dt : Convert.ToDateTime(x["HODARTIME"].ToString().Substring(0, 18).Replace('.', ':') + (x["HODARTIME"].ToString().Contains("AM") ? " AM" : " PM")),
                    UserType = x["USERTYPE"].ToString(),
                    HasEmailTo2ndCon = Convert.ToChar(x["HASEMAILTO2NDCON"]) == 'Y' ? true : false,
                    HasSMSTo2ndCon = Convert.ToChar(x["HASSMSTO2NDCON"]) == 'Y' ? true : false,
                    RefModuleUserMappingId = Convert.ToInt32(x["REFMUMID"]),
                    CreateDate = Convert.ToDateTime(x["CREATEDATE"]),
                    CreateTime = Convert.ToDateTime(x["CREATETIME"].ToString().Substring(0, 18).Replace('.', ':') + (x["CREATETIME"].ToString().Contains("AM") ? " AM" : " PM")),
                    TargetIssueResolveDate = x["TARGETISSUERESOLVEDATE"] == DBNull.Value ? dt : Convert.ToDateTime(x["TARGETISSUERESOLVEDATE"]),
                    ReviseIssueResolveDate = x["REVISEISSUERESOLVEDATE"] == DBNull.Value ? dt : Convert.ToDateTime(x["REVISEISSUERESOLVEDATE"]),
                    RequestCreateEmployeeCode = x["REQUESTCREATEEMPLOYEECODE"].ToString(),
                    AssignedUserTypeId = Convert.ToInt32(x["ASSIGNEDUSERTYPEID"]),
                    RefReqStatusId = Convert.ToInt32(x["REFREQSTATUSID"]),
                    RefReqStatus = x["REFREQSTATUS"].ToString(),    //((Enums.RequestStatus)Convert.ToInt32(x["REFREQSTATUSID"])).ToString()
                    AssignedEmployeeName = x["ASSIGNEDEMPLOYEENAME"] == null ? null : x["ASSIGNEDEMPLOYEENAME"].ToString(),
                    RequestCreateEmployeeName = x["REQUESTCREATEEMPLOYEENAME"].ToString(),
                    FirstCoEmployeeName = x["FIRSTCOEMPLOYEENAME"].ToString(),
                    BasisCoEmployeeName = x["BASISCOEMPLOYEENAME"] == null ? null : x["BASISCOEMPLOYEENAME"].ToString(),
                    RefIssueTypeId = Convert.ToInt32(x["REFISSUETYPEID"]),
                    RefIssueType = x["ISSUETYPE"] == null ? null : x["ISSUETYPE"].ToString()
                }).ToList();

                return RequestModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        #endregion
    }

    public class AuthenticationService
    {
        public AuthenticateUserModel GetEmployee(string EmpCode, string Password)
        {
            try
            {
                Authentication authentication = new Authentication();
                DataSet ds = authentication.SelectEmployee(EmpCode, Password);
                if (ds != null)
                {
                    AuthenticateUserModel authenticateUserModel = ds.Tables[0].AsEnumerable().Select(x => new AuthenticateUserModel()
                    {
                        PERNR = x["PERNR"] == null ? null : Convert.ToString(x["PERNR"]),
                        BANKL = x["BANKL"] == null ? null : Convert.ToString(x["BANKL"]),
                        BANKN = x["BANKN"] == null ? null : Convert.ToString(x["BANKN"]),
                        CATG = x["CATG"] == null ? null : Convert.ToString(x["CATG"]),
                        DEPT = x["DEPT"] == null ? null : Convert.ToString(x["DEPT"]),
                        DESIG = x["DESIG"] == null ? null : Convert.ToString(x["DESIG"]),
                        DIVISION = x["DIVISION"] == null ? null : Convert.ToString(x["DIVISION"]),
                        //DOB = x["LAST_PROM"] == null ? DateTime.Parse(null) : Convert.ToDateTime(x["DOB"]),
                        //DOC = Convert.ToDateTime(x["DOC"]),
                        //DOJ = Convert.ToDateTime(x["DOJ"]),
                        //DOJJK = Convert.ToDateTime(x["DOJJK"]),
                        //DOL =  Convert.ToDateTime(x["DOL"]),
                        //DOR =  Convert.ToDateTime(x["DOR"]),
                        EEPFN = x["EEPFN"] == null ? null : Convert.ToString(x["EEPFN"]),
                        EEPNN = x["EEPNN"] == null ? null : Convert.ToString(x["EEPNN"]),
                        EEVPF = x["EEVPF"] == null ? null : Convert.ToString(x["EEVPF"]),
                        EGRP = x["EGRP"] == null ? null : Convert.ToString(x["EGRP"]),
                        EMAIL = x["EMAIL"] == null ? null : Convert.ToString(x["EMAIL"]),
                        EMPPFBAS = x["EMPPFBAS"] == null ? null : Convert.ToString(x["EMPPFBAS"]),
                        ENAME = x["ENAME"] == null ? null : Convert.ToString(x["ENAME"]),
                        ERPFBAS = x["ERPFBAS"] == null ? null : Convert.ToString(x["ERPFBAS"]),
                        ESGRP = x["ESGRP"] == null ? null : Convert.ToString(x["ESGRP"]),
                        EVPFA = x["EVPFA"] == null ? null : Convert.ToString(x["EVPFA"]),
                        FANAME = x["FANAME"] == null ? null : Convert.ToString(x["FANAME"]),
                        GENDER = x["GENDER"] == null ? null : Convert.ToString(x["GENDER"]),
                        ICNUM = x["ICNUM"] == null ? null : Convert.ToString(x["ICNUM"]),
                        JOB = x["JOB"] == null ? null : Convert.ToString(x["JOB"]),
                        //LAST_PROM = Convert.ToDateTime(x["LAST_PROM"]),
                        LINKCD = x["LINKCD"] == null ? null : Convert.ToString(x["LINKCD"]),
                        LOC_CD = x["LOC_CD"] == null ? null : Convert.ToString(x["LOC_CD"]),
                        LOCATION = x["LOCATION"] == null ? null : Convert.ToString(x["LOCATION"]),
                        MARITAL = x["MARITAL"] == null ? null : Convert.ToString(x["MARITAL"]),
                        MOBILE = x["MOBILE"] == null ? null : Convert.ToString(x["MOBILE"]),
                        NATIO = x["NATIO"] == null ? null : Convert.ToString(x["NATIO"]),
                        ORGEH = x["ORGEH"] == null ? null : Convert.ToString(x["ORGEH"]),
                        PENBAS = x["PENBAS"] == null ? null : Convert.ToString(x["PENBAS"]),
                        PENID = x["PENID"] == null ? null : Convert.ToString(x["PENID"]),
                        PMODE = x["PMODE"] == null ? null : Convert.ToString(x["PMODE"]),
                        PNFLG = x["PNFLG"] == null ? null : Convert.ToString(x["PNFLG"]),
                        PRSAREA = x["PRSAREA"] == null ? null : Convert.ToString(x["PRSAREA"]),
                        PRSSUBAREA = x["PRSSUBAREA"] == null ? null : Convert.ToString(x["PRSSUBAREA"]),
                        QUAL = x["QUAL"] == null ? null : Convert.ToString(x["QUAL"]),
                        RECSTAT = x["RECSTAT"] == null ? null : Convert.ToString(x["RECSTAT"]),
                        RELIGION = x["RELIGION"] == null ? null : Convert.ToString(x["RELIGION"]),
                        SAREA_CD = x["SAREA_CD"] == null ? null : Convert.ToString(x["SAREA_CD"]),
                        STELL = x["STELL"] == null ? null : Convert.ToString(x["STELL"]),
                        SUP = x["SUP"] == null ? null : Convert.ToString(x["SUP"]),
                        TSTID = x["TSTID"] == null ? null : Convert.ToString(x["TSTID"])
                    }).FirstOrDefault();

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        authenticateUserModel.USERROLEMAPPING = ds.Tables[1].AsEnumerable().Select(x => new UserRoleMappingModel()
                        {
                            URMId = Convert.ToInt32(x["URMId"]),
                            EmployeeCode = Convert.ToString(x["EMPLOYEECODE"]),
                            EmployeeName = Convert.ToString(x["EMPLOYEENAME"]),
                            IsAdmin = Convert.ToChar(x["ISADMIN"]) == 'Y' ? true : false,
                            IsBasisConsultant = Convert.ToChar(x["ISBASISCONSULTANT"]) == 'Y' ? true : false,
                            IsConsultant = Convert.ToChar(x["ISCONSULTANT"]) == 'Y' ? true : false,
                            IsITDeveloper = Convert.ToChar(x["ISITDEVELOPER"]) == 'Y' ? true : false,
                            IsHOD = Convert.ToChar(x["ISHOD"]) == 'Y' ? true : false,
                            IsEndUser = Convert.ToChar(x["ISENDUSER"]) == 'Y' ? true : false

                        }).FirstOrDefault();
                    }

                    return authenticateUserModel;
                }
                else { return null; }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AuthenticateUserModel GetEmployee(string EmpCode)
        {
            try
            {
                Authentication authentication = new Authentication();
                DataTable dt = authentication.SelectEmployee(EmpCode);
                if (dt != null)
                {
                    DateTime? dattim = null;
                    AuthenticateUserModel authenticateUserModel = dt.AsEnumerable().Select(x => new AuthenticateUserModel()
                    {
                        PERNR = x["PERNR"] == null ? null : Convert.ToString(x["PERNR"]),
                        BANKL = x["BANKL"] == null ? null : Convert.ToString(x["BANKL"]),
                        BANKN = x["BANKN"] == null ? null : Convert.ToString(x["BANKN"]),
                        CATG = x["CATG"] == null ? null : Convert.ToString(x["CATG"]),
                        DEPT = x["DEPT"] == null ? null : Convert.ToString(x["DEPT"]),
                        DESIG = x["DESIG"] == null ? null : Convert.ToString(x["DESIG"]),
                        DIVISION = x["DIVISION"] == null ? null : Convert.ToString(x["DIVISION"]),
                        DOB = x["DOB"] == DBNull.Value ? dattim : Convert.ToDateTime(x["DOB"]),
                        DOC = x["DOC"] == DBNull.Value ? dattim : Convert.ToDateTime(x["DOC"]),
                        DOJ = x["DOJ"] == DBNull.Value ? dattim : Convert.ToDateTime(x["DOJ"]),
                        DOJJK = x["DOJJK"] == DBNull.Value ? dattim : Convert.ToDateTime(x["DOJJK"]),
                        DOL = x["DOL"] == DBNull.Value ? dattim : Convert.ToDateTime(x["DOL"]),
                        DOR = x["DOR"] == DBNull.Value ? dattim : Convert.ToDateTime(x["DOR"]),
                        EEPFN = x["EEPFN"] == null ? null : Convert.ToString(x["EEPFN"]),
                        EEPNN = x["EEPNN"] == null ? null : Convert.ToString(x["EEPNN"]),
                        EEVPF = x["EEVPF"] == null ? null : Convert.ToString(x["EEVPF"]),
                        EGRP = x["EGRP"] == null ? null : Convert.ToString(x["EGRP"]),
                        EMAIL = x["EMAIL"] == null ? null : Convert.ToString(x["EMAIL"]),
                        EMPPFBAS = x["EMPPFBAS"] == null ? null : Convert.ToString(x["EMPPFBAS"]),
                        ENAME = x["ENAME"] == null ? null : Convert.ToString(x["ENAME"]),
                        ERPFBAS = x["ERPFBAS"] == null ? null : Convert.ToString(x["ERPFBAS"]),
                        ESGRP = x["ESGRP"] == null ? null : Convert.ToString(x["ESGRP"]),
                        EVPFA = x["EVPFA"] == null ? null : Convert.ToString(x["EVPFA"]),
                        FANAME = x["FANAME"] == null ? null : Convert.ToString(x["FANAME"]),
                        GENDER = x["GENDER"] == null ? null : Convert.ToString(x["GENDER"]),
                        ICNUM = x["ICNUM"] == null ? null : Convert.ToString(x["ICNUM"]),
                        JOB = x["JOB"] == null ? null : Convert.ToString(x["JOB"]),
                        LAST_PROM = x["LAST_PROM"] == DBNull.Value ? dattim : Convert.ToDateTime(x["LAST_PROM"]),
                        LINKCD = x["LINKCD"] == null ? null : Convert.ToString(x["LINKCD"]),
                        LOC_CD = x["LOC_CD"] == null ? null : Convert.ToString(x["LOC_CD"]),
                        LOCATION = x["LOCATION"] == null ? null : Convert.ToString(x["LOCATION"]),
                        MARITAL = x["MARITAL"] == null ? null : Convert.ToString(x["MARITAL"]),
                        MOBILE = x["MOBILE"] == null ? null : Convert.ToString(x["MOBILE"]),
                        NATIO = x["NATIO"] == null ? null : Convert.ToString(x["NATIO"]),
                        ORGEH = x["ORGEH"] == null ? null : Convert.ToString(x["ORGEH"]),
                        PENBAS = x["PENBAS"] == null ? null : Convert.ToString(x["PENBAS"]),
                        PENID = x["PENID"] == null ? null : Convert.ToString(x["PENID"]),
                        PMODE = x["PMODE"] == null ? null : Convert.ToString(x["PMODE"]),
                        PNFLG = x["PNFLG"] == null ? null : Convert.ToString(x["PNFLG"]),
                        PRSAREA = x["PRSAREA"] == null ? null : Convert.ToString(x["PRSAREA"]),
                        PRSSUBAREA = x["PRSSUBAREA"] == null ? null : Convert.ToString(x["PRSSUBAREA"]),
                        QUAL = x["QUAL"] == null ? null : Convert.ToString(x["QUAL"]),
                        RECSTAT = x["RECSTAT"] == null ? null : Convert.ToString(x["RECSTAT"]),
                        RELIGION = x["RELIGION"] == null ? null : Convert.ToString(x["RELIGION"]),
                        SAREA_CD = x["SAREA_CD"] == null ? null : Convert.ToString(x["SAREA_CD"]),
                        STELL = x["STELL"] == null ? null : Convert.ToString(x["STELL"]),
                        SUP = x["SUP"] == null ? null : Convert.ToString(x["SUP"]),
                        TSTID = x["TSTID"] == null ? null : Convert.ToString(x["TSTID"])
                    }).FirstOrDefault();

                    return authenticateUserModel;
                }
                else { return null; }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class UserRoleMappingService
    {
        public int SaveUserRoleMapping(UserRoleMappingModel model)
        {
            try
            {
                UserRoleMap userRoleMap = new UserRoleMap();
                return userRoleMap.InsertUpdateUserRoleMap(model.URMId, model.EmployeeCode, model.EmployeeName,
                    model.IsAdmin ? 'Y' : 'N', model.IsConsultant ? 'Y' : 'N',
                    model.IsITDeveloper ? 'Y' : 'N', model.IsBasisConsultant ? 'Y' : 'N', model.IsHOD ? 'Y' : 'N', model.IsEndUser ? 'Y' : 'N');
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserRoleMappingModel> GetUserRoleMapping(int? urmId)
        {
            try
            {
                UserRoleMap userRoleMap = new UserRoleMap();
                return userRoleMap.SelectUserRoleMap(urmId).AsEnumerable().Select(x => new UserRoleMappingModel()
                {
                    URMId = Convert.ToInt32(x["URMId"]),
                    EmployeeCode = Convert.ToString(x["EMPLOYEECODE"]),
                    EmployeeName = Convert.ToString(x["EMPLOYEENAME"]),
                    IsAdmin = Convert.ToChar(x["ISADMIN"]) == 'Y' ? true : false,
                    IsBasisConsultant = Convert.ToChar(x["ISBASISCONSULTANT"]) == 'Y' ? true : false,
                    IsConsultant = Convert.ToChar(x["ISCONSULTANT"]) == 'Y' ? true : false,
                    IsITDeveloper = Convert.ToChar(x["ISITDEVELOPER"]) == 'Y' ? true : false,
                    IsHOD = Convert.ToChar(x["ISHOD"]) == 'Y' ? true : false,
                    IsEndUser = Convert.ToChar(x["ISENDUSER"]) == 'Y' ? true : false
                }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class ModuleUserMappingService
    {
        public int SaveModuleUserMapping(ModuleUserMappingModel model)
        {
            try
            {
                ModuleUserMap moduleUserMap = new ModuleUserMap();
                return moduleUserMap.InsertUpdateModuleUserMap(model.MUMId, model.LocationId, model.ApplicationId,
                    model.ModuleId, model.Consultant1EmployeeCode, model.HasEmail1 ? 'Y' : 'N', model.HasSMS1 ? 'Y' : 'N',
                    model.Consultant2EmployeeCode, model.HasEmail2 ? 'Y' : 'N', model.HasSMS2 ? 'Y' : 'N',
                    model.Consultant3EmployeeCode, model.HasEmail3 ? 'Y' : 'N', model.HasSMS3 ? 'Y' : 'N',
                    model.BasisConsultantEmployeeCode, model.HasEmail4 ? 'Y' : 'N', model.HasSMS4 ? 'Y' : 'N');
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ModuleUserMappingModel> GetModuleUserMapping(int? mumId)
        {
            try
            {
                ModuleUserMap moduleUserMap = new ModuleUserMap();
                return moduleUserMap.SelectModuleUserMap(mumId).AsEnumerable().Select(x => new ModuleUserMappingModel()
                {
                    MUMId = Convert.ToInt32(x["MUMID"]),
                    LocationId = Convert.ToInt32(x["LOCATIONID"]),
                    LocationName = Convert.ToString(x["LOCATIONNAME"]),
                    ApplicationId = Convert.ToInt32(x["APPLICATIONID"]),
                    ApplicationName = Convert.ToString(x["APPLICATIONNAME"]),
                    ModuleId = Convert.ToInt32(x["MODULEID"]),
                    ModuleName = Convert.ToString(x["MODULENAME"]),
                    Consultant1EmployeeCode = Convert.ToString(x["FIRSTCOEMPLOYEECODE"]),
                    Consultant1EmployeeName = Convert.ToString(x["FIRSTCOEMPLOYEENAME"]),
                    HasEmail1 = Convert.ToChar(x["FIRSTCOHASEMAIL"]) == 'Y' ? true : false,
                    HasSMS1 = Convert.ToChar(x["FIRSTCOHASSMS"]) == 'Y' ? true : false,
                    Consultant2EmployeeCode = Convert.ToString(x["SECCOEMPLOYEECODE"]),
                    Consultant2EmployeeName = Convert.ToString(x["SECCOEMPLOYEENAME"]),
                    HasEmail2 = Convert.ToChar(x["SECCOHASEMAIL"]) == 'Y' ? true : false,
                    HasSMS2 = Convert.ToChar(x["SECCOHASSMS"]) == 'Y' ? true : false,
                    Consultant3EmployeeCode = Convert.ToString(x["THIRDCOEMPLOYEECODE"]),
                    Consultant3EmployeeName = Convert.ToString(x["THIRDCOEMPLOYEENAME"]),
                    HasEmail3 = Convert.ToChar(x["THIRDCOHASEMAIL"]) == 'Y' ? true : false,
                    HasSMS3 = Convert.ToChar(x["THIRDCOHASSMS"]) == 'Y' ? true : false,
                    BasisConsultantEmployeeCode = Convert.ToString(x["BASISCOEMPLOYEECODE"]),
                    BasisConsultantEmployeeName = Convert.ToString(x["BASISCOEMPLOYEENAME"]),
                    HasEmail4 = Convert.ToChar(x["BASISCOHASEMAIL"]) == 'Y' ? true : false,
                    HasSMS4 = Convert.ToChar(x["BASISCOHASSMS"]) == 'Y' ? true : false
                }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ModuleUserMappingModel GetModuleUserMapping(int locationId, int applicationId, int moduleId)
        {
            try
            {
                ModuleUserMap moduleUserMap = new ModuleUserMap();
                return moduleUserMap.SelectModuleUserMap(locationId, applicationId, moduleId).AsEnumerable().Select(x => new ModuleUserMappingModel()
                {
                    MUMId = Convert.ToInt32(x["MUMID"]),
                    LocationId = Convert.ToInt32(x["LOCATIONID"]),
                    LocationName = Convert.ToString(x["LOCATIONNAME"]),
                    ApplicationId = Convert.ToInt32(x["APPLICATIONID"]),
                    ApplicationName = Convert.ToString(x["APPLICATIONNAME"]),
                    ModuleId = Convert.ToInt32(x["MODULEID"]),
                    ModuleName = Convert.ToString(x["MODULENAME"]),
                    Consultant1EmployeeCode = Convert.ToString(x["FIRSTCOEMPLOYEECODE"]),
                    HasEmail1 = Convert.ToChar(x["FIRSTCOHASEMAIL"]) == 'Y' ? true : false,
                    HasSMS1 = Convert.ToChar(x["FIRSTCOHASSMS"]) == 'Y' ? true : false,
                    SMS1 = Convert.ToString(x["FIRSTCOMOBILE"]),
                    Email1 = Convert.ToString(x["FIRSTCOEMAIL"]),
                    Consultant2EmployeeCode = Convert.ToString(x["SECCOEMPLOYEECODE"]),
                    HasEmail2 = Convert.ToChar(x["SECCOHASEMAIL"]) == 'Y' ? true : false,
                    HasSMS2 = Convert.ToChar(x["SECCOHASSMS"]) == 'Y' ? true : false,
                    SMS2 = Convert.ToString(x["SECCOMOBILE"]),
                    Email2 = Convert.ToString(x["SECCOEMAIL"]),
                    Consultant3EmployeeCode = Convert.ToString(x["THIRDCOEMPLOYEECODE"]),
                    HasEmail3 = Convert.ToChar(x["THIRDCOHASEMAIL"]) == 'Y' ? true : false,
                    HasSMS3 = Convert.ToChar(x["THIRDCOHASSMS"]) == 'Y' ? true : false,
                    SMS3 = Convert.ToString(x["THIRDCOMOBILE"]),
                    Email3 = Convert.ToString(x["THIRDCOEMAIL"]),
                    BasisConsultantEmployeeCode = Convert.ToString(x["BASISCOEMPLOYEECODE"]),
                    HasEmail4 = Convert.ToChar(x["BASISCOHASEMAIL"]) == 'Y' ? true : false,
                    HasSMS4 = Convert.ToChar(x["BASISCOHASSMS"]) == 'Y' ? true : false,
                    SMS4 = Convert.ToString(x["BASISCOMOBILE"]),
                    Email4 = Convert.ToString(x["BASISCOEMAIL"])
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ModuleUserMappingModel GetConsultant(int locationId, int applicationId, int moduleId)
        {
            try
            {
                ModuleUserMap moduleUserMap = new ModuleUserMap();
                return moduleUserMap.SelectConsultant(locationId, applicationId, moduleId).AsEnumerable().Select(x => new ModuleUserMappingModel()
                {
                    MUMId = Convert.ToInt32(x["MUMID"]),
                    Consultant1EmployeeCode = Convert.ToString(x["FIRSTCOEMPLOYEECODE"]),
                    Consultant1EmployeeName = Convert.ToString(x["FIRSTCOEMPLOYEENAME"]),

                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class DashboardService
    {
        public DashboardModel GetDashboardData(string empCode, string type)
        {
            try
            {
                CommonDB dashboard = new CommonDB();
                DashboardModel dashboardModel = new DashboardModel();
                dashboardModel.statusWiseData = dashboard.DashboardData(empCode, type).AsEnumerable().Select(x => new StatusWiseData()
                {
                    RequestType = Convert.ToString(x["RequestType"]),
                    Status = Convert.ToString(x["Status"]),
                    StatusId = Convert.ToInt32(x["StatusId"]),
                    TotalRequest = Convert.ToInt32(x["TotalRequest"])
                }).ToList();

                dashboardModel.locationWiseData = dashboard.DashboardLocationWiseData(empCode, type).AsEnumerable().Select(x => new LocationWiseData()
                {
                    LocationName = Convert.ToString(x["LOCATIONNAME"]),
                    LocationId = Convert.ToInt32(x["LOCATIONID"]),
                    RequestType = Convert.ToString(x["RequestType"]),
                    TotalRequest = Convert.ToInt32(x["TotalRequest"])
                }).ToList();

                return dashboardModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DashboardModel GetDashboard(string empCode,string usertype, int? LocationId, string fromDate, string toDate )
        {
            try
            {
                CommonDB dashboard = new CommonDB();

                DataSet ds = new DataSet(); 
                DashboardModel dashboardModel = new DashboardModel();
                ds = dashboard.DashBoard(empCode, usertype, LocationId, fromDate, toDate);
                dashboardModel.locationWiseRequestlist = ds.Tables[0].AsEnumerable().Select(x => new LocationWiseRequest()
                {
                    RequestType = Convert.ToString(x["REQUESTTYPE"]),
                    LocationId = Convert.ToInt32(x["LOCATIONID"]),
                    LocationName = Convert.ToString(x["LOCATIONNAME"]),
                    PendingCnt = Convert.ToInt32(x["Pending"]),
                    WIPCnt = Convert.ToInt32(x["WIP"]),
                    UATCnt = Convert.ToInt32(x["UAT"]),
                    CompletedCnt = Convert.ToInt32(x["Completed"]),
                    ClosedCnt = Convert.ToInt32(x["Closed"])
                }).ToList();

                dashboardModel.ModuleWiseRequestlist = ds.Tables[1].AsEnumerable().Select(x => new ModuleWiseRequest()
                {
                    RequestType = Convert.ToString(x["REQUESTTYPE"]),
                    ModuleId = Convert.ToInt32(x["MODULEID"]),
                    ModuleName = Convert.ToString(x["MODULENAME"]),
                    PendingCnt = Convert.ToInt32(x["Pending"]),
                    WIPCnt = Convert.ToInt32(x["WIP"]),
                    UATCnt = Convert.ToInt32(x["UAT"]),
                    CompletedCnt = Convert.ToInt32(x["Completed"]),
                    ClosedCnt = Convert.ToInt32(x["Closed"])
                }).ToList();
                 
                return dashboardModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    public class CommonServices
    {
        public static string RenderPartialToString(string controlName, object viewData)
        {
            ViewPage viewPage = new ViewPage() { ViewContext = new ViewContext() };

            viewPage.ViewData = new ViewDataDictionary(viewData);
            //viewPage. = viewBag;
            viewPage.Controls.Add(viewPage.LoadControl(controlName));

            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                using (HtmlTextWriter tw = new HtmlTextWriter(sw))
                {
                    viewPage.RenderControl(tw);
                }
            }

            return sb.ToString();
        }

        public static string Nl2Br(string s)
        {
            if (!string.IsNullOrWhiteSpace(s))
            {
                return s.Replace("\r\n", "<br />").Replace("\n", "<br />");
            }
            else
            {
                return string.Empty;
            }
        }

        public static List<SelectListItem> docTypes;
        public static List<SelectListItem> userType;

        public static List<SelectListItem> GetEmployees()
        {
            try
            {
                Authentication authentication = new Authentication();
                DataTable dt = authentication.SelectEmployee();

                List<SelectListItem> selectItems = new List<SelectListItem>();
                foreach (DataRow rw in dt.Rows)
                {
                    selectItems.Add(new SelectListItem { Text = rw[0].ToString() + " - " + rw[1].ToString(), Value = rw[0].ToString() });
                }
                return selectItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<SelectListItem> GetEmployees(string type)
        {
            try
            {
                UserRoleMap userRoleMap = new UserRoleMap();
                DataTable dt = userRoleMap.SelectUserRoleMap(type);
                List<SelectListItem> selectItems = new List<SelectListItem>();
                foreach (DataRow rw in dt.Rows)
                {
                    selectItems.Add(new SelectListItem { Text = rw[0].ToString() + " - " + rw[1].ToString(), Value = rw[0].ToString() });
                }
                return selectItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<SelectListItem> GetHodEmployees(string empCode)
        {
            try
            {
                CommonDB comm = new CommonDB();
                DataTable dt = comm.SelectHodEmployee(empCode);
                List<SelectListItem> selectItems = new List<SelectListItem>();
                foreach (DataRow rw in dt.Rows)
                {
                    selectItems.Add(new SelectListItem { Text = rw[0].ToString() + " - " + rw[1].ToString(), Value = rw[0].ToString() });
                }
                return selectItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<SelectListItem> GetEmployeesForRoleMapping()
        {
            try
            {
                UserRoleMap userRoleMap = new UserRoleMap();
                DataTable dt = userRoleMap.SelectEmployeeForRoleMap();
                List<SelectListItem> selectItems = new List<SelectListItem>();
                foreach (DataRow rw in dt.Rows)
                {
                    selectItems.Add(new SelectListItem { Text = rw[0].ToString() + " - " + rw[1].ToString(), Value = rw[0].ToString() });
                }
                return selectItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<SelectListItem> GetDocumentType()
        {
            try
            {
                DocumentType documentType = new DocumentType();
                DataTable dt = documentType.SelectDocumentType(null);
                return CreateListItems(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<SelectListItem> GetApplicationList()
        {
            try
            {
                Application application = new Application();
                DataTable dt = application.SelectApplication(null);
                return CreateListItems(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<SelectListItem> GetLocationList()
        {
            try
            {
                Location location = new Location();
                DataTable dt = location.SelectLocation(null);
                return CreateListItems(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<SelectListItem> GetModuleList(int applicationId)
        {
            try
            {
                Module module = new Module();
                DataTable dt = module.SelectModule(null, applicationId);
                return CreateListItems(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<SelectListItem> GetPriorityList()
        {
            try
            {
                Priority priority = new Priority();
                DataTable dt = priority.SelectPriority(null);
                return CreateListItems(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<SelectListItem> GetUserTypeList(string isforassignto = "N", int? mumid = null)
        {
            try
            {
                UserType userType = new UserType();
                DataTable dt = userType.SelectUserType(null, isforassignto);
                if (isforassignto == "Y" && mumid != null)
                {
                    ModuleUserMappingService obj = new ModuleUserMappingService();
                    bool IsBasis = string.IsNullOrEmpty(obj.GetModuleUserMapping(mumid).FirstOrDefault().BasisConsultantEmployeeCode);
                    if (IsBasis)
                    {
                        dt.Rows.Remove(dt.Select("USERTYPE = 'Basis Consultant' ")[0]);
                    }
                }
                return CreateListItems(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<SelectListItem> GetStatusList()
        {
            try
            {
                Status status = new Status();
                DataTable dt = status.SelectStatus(null);
                var list = CreateListItems(dt);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<SelectListItem> GetIssueTypeList()
        {
            try
            {
                IssueType issueType = new IssueType();
                DataTable dt = issueType.SelectIssueType(null);
                var list = CreateListItems(dt);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static List<SelectListItem> CreateListItems(DataTable dt)
        {
            try
            {
                List<SelectListItem> selectItems = new List<SelectListItem>();
                foreach (DataRow rw in dt.Rows)
                {
                    selectItems.Add(new SelectListItem { Text = rw[1].ToString(), Value = rw[0].ToString() });
                }
                return selectItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteMasterRecord(int pkId, string MasterName)
        {
            try
            {
                bool result = false;
                CommonDB obj = new CommonDB();
                result = obj.DeleteMaster(pkId, MasterName) > 0;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void send_mail_to_vendor_otp(string toEmail, string subject, string body, Attachment file = null, string[] ccEmails = null)
        {
            try
            {
                MailMessage Message = new MailMessage();
                Message.From = new MailAddress("automail@lc.jkmail.com", "JK LAKSHMI CEMENT");
                Message.To.Add(toEmail);
                if (ccEmails != null)
                {
                    Message.CC.Add(String.Join(",", ccEmails.Where(s => !string.IsNullOrEmpty(s))));
                }
                if (file != null)
                {
                    Message.Attachments.Add(file);                  
                }
                Message.IsBodyHtml = true;
                Message.Body = body + "<BR><BR>Regards,<BR>JK LAKSHMI CEMENT<br> http://jklakshmicementsrm.com/prcr <br>This is a system generated mail, please do not reply<br>";
                Message.Subject = subject;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "10.10.5.1";
                smtp.Send(Message);
                Message.Dispose();
            }
            catch (Exception)
            {
            }
        }

        public static void sms(string mobile, string subject)
        {
            try
            {
                string body = "Dear Sir/Madam , New Issue Request Generated for " + subject;

                CommonDB common = new CommonDB();
                string WFLocation = common.smsstring(mobile, body);
                string location = WFLocation;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(location);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string b64decode(string StrDecode)
        {
            string decodedString;
            decodedString = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(StrDecode));
            return decodedString;
        }

        public static string b64encode(string StrEncode)
        {
            string encodedString;
            encodedString = (Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(StrEncode)));
            return (encodedString);
        }

        public static List<SelectListItem> GetReqStatusDet()
        {
            try
            {
                CommonDB common = new CommonDB();
                DataTable dt = common.SelectReqStatusDet();
                return CreateListItems(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}