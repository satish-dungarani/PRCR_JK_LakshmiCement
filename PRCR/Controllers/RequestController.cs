using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRCR.Models;
using System.IO;
using System.Configuration;

using System.Transactions;

namespace PRCR.Controllers
{
    public class RequestController : BaseController
    {
        #region Comman Actions
        /// <summary>
        /// Show List of all Request for all User type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult RequestList(string type, int? LocationId, int? ApplicationId,
             int? ModuleId, int? StatusId, int? PriorityId, string FromToDate, int? refReqStatusId, int? refIssueTypeId, string focusOn = "PR")
        {
            try
            {
                ModelState.Clear();
                if (TempData["Action"] != null)
                {
                    ViewBag.Action = TempData["Action"];
                }
                if (TempData["IssueId"] != null)
                {
                    ViewBag.IssueId = TempData["IssueId"];
                }
                ViewBag.UserType = type;
                ViewBag.FocusOn = focusOn;

                var requestModels = GetRequestData(type, LocationId, ApplicationId, ModuleId, StatusId, PriorityId, FromToDate, refReqStatusId, refIssueTypeId);

                return View(requestModels);
            }
            catch (Exception ex)
            {
                return RedirectToAction("LogError", "Base", ex);
            }
        }

        //public JsonResult RequestList(string type, int? locationId, int? applicationId,
        //     int? moduleId, int? statusId, int? priorityId, string fromtoDate)
        //{
        //    try
        //    {
        //        string fromDate = string.Empty;
        //        string toDate = string.Empty;
        //        if (!string.IsNullOrEmpty(fromtoDate))
        //        {
        //            fromDate = fromtoDate.Split('-')[0].ToString();
        //            toDate = fromtoDate.Split('-')[1].ToString();
        //        }

        //        var requestModels = GetRequestData(type, locationId, applicationId, moduleId, statusId, priorityId, fromtoDate);

        //        string PRHtmlString, CRHtmlString;

        //        ViewBag.RequestType = "PR";
        //        string data = PartialView(
        //        PRHtmlString = CommonServices.RenderPartialToString("//_AdminRequestListPartial.cshtml", ViewBag.RequestType);

        //        ViewBag.RequestType = "CR";
        //        CRHtmlString = CommonServices.RenderPartialToString("_AdminRequestListPartial", ViewBag.RequestType);

        //        return Json(new { Status = "Ok", PRTable = PRHtmlString, CRTable = CRHtmlString });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Status = "Error" });
        //        //return RedirectToAction("LogError", "Base", ex);
        //    }
        //}

        public List<RequestModel> GetRequestData(string type, int? locationId = null, int? applicationId = null,
             int? moduleId = null, int? statusId = null, int? priorityId = null, string fromtoDate = null, int? refReqStatusId = null, int? refIssueTypeId = null)
        {
            try
            {
                List<RequestModel> requestModels = new List<RequestModel>();
                IssueRequestServices issueRequestServices = new IssueRequestServices();
                AuthenticateUserModel authenticateUserModel = (AuthenticateUserModel)Session["UserData"];

                string fromDate = string.Empty;
                string toDate = string.Empty;
                if (!string.IsNullOrEmpty(fromtoDate))
                {
                    fromDate = fromtoDate.Split('-')[0].ToString();
                    toDate = fromtoDate.Split('-')[1].ToString();
                }


                if (type.ToUpper() == "EU" || type.ToUpper() == "UAT")
                {
                    requestModels = issueRequestServices.GetIssueRequests(authenticateUserModel.PERNR, null, locationId, applicationId, moduleId, statusId, priorityId, fromDate, toDate, refReqStatusId, refIssueTypeId);
                }
                else if (type.ToUpper() == "CO")
                {
                    requestModels = issueRequestServices.GetConsultantIssueRequests(authenticateUserModel.PERNR, null, locationId, applicationId, moduleId, statusId, priorityId, fromDate, toDate, refReqStatusId, refIssueTypeId).ToList();   //.Where(x => x.StatusId == 1 || x.StatusId == 2 || x.StatusId == 5).ToList(); // if search is not used
                }
                else if (type.ToUpper() == "IT")
                {
                    requestModels = issueRequestServices.GetITDeveloperIssueRequests(authenticateUserModel.PERNR, null, locationId, applicationId, moduleId, statusId, priorityId, fromDate, toDate, refReqStatusId, refIssueTypeId).ToList();  //.Where(x => x.StatusId == 1 || x.StatusId == 2).ToList();// if search is not used
                }
                else if (type.ToUpper() == "BASIS")
                {
                    requestModels = issueRequestServices.GetBasisConsultantIssueRequests(authenticateUserModel.PERNR, null, locationId, applicationId, moduleId, statusId, priorityId, fromDate, toDate, refReqStatusId, refIssueTypeId).ToList();  //.Where(x => x.StatusId == 1 || x.StatusId == 2).ToList();// if search is not used
                }
                else if (type.ToUpper() == "HOD")
                {
                    requestModels = issueRequestServices.GetHODIssueRequests(authenticateUserModel.PERNR, null, locationId, applicationId, moduleId, statusId, priorityId, fromDate, toDate, refReqStatusId, refIssueTypeId);
                }
                else if (type.ToUpper() == "ADMIN")
                {
                    requestModels = issueRequestServices.GetAdminIssueRequests(null, locationId, applicationId, moduleId, statusId, priorityId, fromDate, toDate, refReqStatusId, refIssueTypeId);
                }

                return requestModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool SaveCommunicationAndAttachDocument(RequestModel model, HttpPostedFileBase file, string docTypeName)
        {
            try
            {
                bool result = false;
                AuthenticateUserModel authModel = new AuthenticateUserModel();
                System.Net.Mail.Attachment attachment = file != null ? new System.Net.Mail.Attachment(file.InputStream, file.FileName) : null;
                if (Session["UserData"] != null)
                {
                    authModel = Session["UserData"] as AuthenticateUserModel;
                }

                CommunicationServices communicationServices = new CommunicationServices();
                model.Communication.RefRequestId = model.RequetsId;

                if (model.RequestType == "CR" && model.Communication.UserType == "End User")
                {
                    model.Communication.AssignedUserTypeId = Convert.ToInt32(CommonServices.userType.Where(x => x.Text == "HOD").FirstOrDefault().Value);
                    model.Communication.AssignedUserType = "HOD";
                }
                else if (model.Communication.UserType != "Consultant")
                {

                    if (model.Communication.IsITCompleted || model.Communication.IsBasisCompleted || model.Communication.UserType == "End User" || model.Communication.UserType == "UAT User")
                    {
                        model.Communication.AssignedUserTypeId = Convert.ToInt32(CommonServices.userType.Where(x => x.Text == "Consultant").FirstOrDefault().Value);
                        model.Communication.AssignedUserType = "Consultant";
                    }
                    else
                    {
                        model.Communication.AssignedUserTypeId = Convert.ToInt32(CommonServices.userType.Where(x => x.Text == model.Communication.UserType).FirstOrDefault().Value);
                        model.Communication.AssignedUserType = model.Communication.UserType;
                    }

                }

                model.Communication.RemarkDate = System.DateTime.Now.Date;
                model.Communication.RemarkTime = System.DateTime.Now;
                model.Communication.CommunicationType = model.RequestType;
                model.Communication.RemarkEmpCode = authModel.PERNR;
                model.Communication.RemarkEmpName = authModel.ENAME;
                model.Communication.Remark = CommonServices.Nl2Br(model.Communication.Remark);

                model.Communication.CommunicationId = communicationServices.SaveCommunication(model.Communication);

                if (file != null)
                {
                    string filePath = ConfigurationManager.AppSettings["filePath"].ToString() + "\\File\\";
                    if (!Directory.Exists(filePath + model.RequetsId))
                    {
                        Directory.CreateDirectory(filePath + model.RequetsId);
                    }
                    //if (!System.IO.File.Exists(Server.MapPath("\\Files\\" + model.RequetsId + "\\" + file.FileName)))
                    //{
                    //    file.SaveAs(Server.MapPath("\\Files\\" + model.RequetsId + "\\" + file.FileName));
                    //}

                    file.SaveAs(filePath + model.RequetsId + "\\" + file.FileName);

                    DocumentServices documentServices = new DocumentServices();
                    model.Communication.DocumentsAttachment.RefCommunicationId = model.Communication.CommunicationId;
                    model.Communication.DocumentsAttachment.DocumentAttachDate = System.DateTime.Now.Date;
                    model.Communication.DocumentsAttachment.DocumentAttachTime = System.DateTime.Now;
                    model.Communication.DocumentsAttachment.DocumentPath = "\\File\\" + model.RequetsId + "\\" + file.FileName;
                    model.Communication.DocumentsAttachment.RefDocumentTypeId = Convert.ToInt32(CommonServices.docTypes.Where(x => x.Text.ToLower() == docTypeName.ToLower()).FirstOrDefault().Value);

                    model.Communication.DocumentsAttachment.DocumentId = documentServices.SaveDocument(model.Communication.DocumentsAttachment);
                }

                if (model.Communication.CommunicationId > 0)
                {
                    RequestModel insertedModel = new RequestModel();
                    IssueRequestServices issueReqModel = new IssueRequestServices();
                    insertedModel = issueReqModel.GetIssueRequests(null, model.RequetsId).FirstOrDefault();
                    model.IssueId = insertedModel.IssueId;

                    ModuleUserMappingService moduleUserMappingService = new ModuleUserMappingService();
                    if (model.UserType == "End User")
                    {
                        if (model.RequestType == "CR")
                        {
                            AuthenticationService authenticationService = new AuthenticationService();
                            AuthenticateUserModel authenticateUserModel = authenticationService.GetEmployee(model.HODEmployeeCode);
                            CommonServices.send_mail_to_vendor_otp(authenticateUserModel.EMAIL, model.IssueId + " - " + model.Subject, model.Communication.Remark, attachment);
                        }

                        if (model.RequestCreateEmployeeCode != null)
                        {
                            AuthenticationService authenticationService = new AuthenticationService();
                            AuthenticateUserModel authenticateUserModel = authenticationService.GetEmployee(model.RequestCreateEmployeeCode);
                            CommonServices.send_mail_to_vendor_otp(authenticateUserModel.EMAIL, model.IssueId + " - " + model.Subject, model.Communication.Remark, attachment);
                        }

                        ModuleUserMappingModel moduleUserMappingModel = moduleUserMappingService.GetModuleUserMapping(model.LocationId, model.ApplicationId, model.ModuleId);

                        if (moduleUserMappingModel.HasEmail1)
                        {
                            string[] emailIds = new string[3];
                            if (moduleUserMappingModel.HasEmail2)
                            {
                                emailIds[0] = moduleUserMappingModel.Email2;
                            }
                            if (moduleUserMappingModel.HasEmail3)
                            {
                                emailIds[1] = moduleUserMappingModel.Email3;
                            }
                            if (moduleUserMappingModel.HasEmail4)
                            {
                                emailIds[2] = moduleUserMappingModel.Email4;
                            }
                            CommonServices.send_mail_to_vendor_otp(moduleUserMappingModel.Email1, model.IssueId + " - " + model.Subject, model.Communication.Remark, attachment, emailIds);
                        }
                        if (moduleUserMappingModel.HasSMS1)
                        {
                            CommonServices.sms(moduleUserMappingModel.SMS1, model.IssueId + " - " + model.Subject);
                        }
                        if (moduleUserMappingModel.HasSMS3 && !string.IsNullOrWhiteSpace(moduleUserMappingModel.SMS3))
                        {
                            CommonServices.sms(moduleUserMappingModel.SMS3, model.IssueId + " - " + model.Subject);
                        }

                        if (model.PriorityId != 1)
                        {
                            if (moduleUserMappingModel.HasEmail2)
                            {
                                CommonServices.send_mail_to_vendor_otp(moduleUserMappingModel.Email2, model.IssueId + " - " + model.Subject, model.Communication.Remark, attachment);
                            }
                            if (moduleUserMappingModel.HasSMS2)
                            {
                                CommonServices.sms(moduleUserMappingModel.SMS2, model.IssueId + " - " + model.Subject);
                            }
                        }
                    }
                    else if (model.UserType == "Consultant")
                    {
                        ModuleUserMappingModel moduleUserMappingModel = moduleUserMappingService.GetModuleUserMapping(model.LocationId, model.ApplicationId, model.ModuleId);

                        if (!string.IsNullOrEmpty(moduleUserMappingModel.Consultant2EmployeeCode))
                        {
                            if (model.HasEmailTo2ndCon)
                            {
                                CommonServices.send_mail_to_vendor_otp(moduleUserMappingModel.Email1, model.IssueId + " - " + model.Subject, model.Communication.Remark, attachment);
                            }
                            if (model.HasSMSTo2ndCon)
                            {
                                CommonServices.sms(moduleUserMappingModel.SMS1, model.IssueId + " - " + model.Subject);
                            }
                        }

                        if (model.Communication.AssignedUserTypeId == 2)
                        {
                            if (moduleUserMappingModel.HasEmail1)
                            {
                                CommonServices.send_mail_to_vendor_otp(moduleUserMappingModel.Email1, model.IssueId + " - " + model.Subject, model.Communication.Remark, attachment);
                            }
                            if (moduleUserMappingModel.HasSMS1)
                            {
                                CommonServices.sms(moduleUserMappingModel.SMS1, model.IssueId + " - " + model.Subject);
                            }

                            if (moduleUserMappingModel.HasEmail3)
                            {
                                CommonServices.send_mail_to_vendor_otp(moduleUserMappingModel.Email3, model.IssueId + " - " + model.Subject, model.Communication.Remark, attachment);
                            }
                            if (moduleUserMappingModel.HasSMS3)
                            {
                                CommonServices.sms(moduleUserMappingModel.SMS3, model.IssueId + " - " + model.Subject);
                            }

                            if (model.PriorityId != 1)
                            {
                                if (moduleUserMappingModel.HasEmail2)
                                {
                                    CommonServices.send_mail_to_vendor_otp(moduleUserMappingModel.Email2, model.IssueId + " - " + model.Subject, model.Communication.Remark, attachment);
                                }
                                if (moduleUserMappingModel.HasSMS2)
                                {
                                    CommonServices.sms(moduleUserMappingModel.SMS2, model.IssueId + " - " + model.Subject);
                                }
                            }
                        }
                        else if (model.Communication.AssignedUserTypeId == 4)
                        {
                            if (moduleUserMappingModel.HasEmail4)
                            {
                                CommonServices.send_mail_to_vendor_otp(moduleUserMappingModel.Email4, model.IssueId + " - " + model.Subject, model.Communication.Remark, attachment);
                            }
                            if (moduleUserMappingModel.HasSMS4)
                            {
                                CommonServices.sms(moduleUserMappingModel.SMS4, model.IssueId + " - " + model.Subject);
                            }
                        }
                        else if (model.Communication.AssignedUserTypeId == 1)
                        {
                            AuthenticationService authenticationService = new AuthenticationService();
                            AuthenticateUserModel authenticateUserModel = authenticationService.GetEmployee(model.Communication.AssignedEmployeeCode);
                            if (!string.IsNullOrWhiteSpace(authenticateUserModel.EMAIL))
                            {
                                CommonServices.send_mail_to_vendor_otp(authenticateUserModel.EMAIL, model.IssueId + " - " + model.Subject, model.Communication.Remark, attachment);
                            }
                            if (!string.IsNullOrWhiteSpace(authenticateUserModel.MOBILE))
                            {
                                CommonServices.sms(authenticateUserModel.MOBILE, model.IssueId + " - " + model.Subject);
                            }
                        }
                        else if (model.Communication.AssignedUserTypeId == 3)
                        {
                            AuthenticationService authenticationService = new AuthenticationService();
                            AuthenticateUserModel authenticateUserModel = authenticationService.GetEmployee(model.RequestCreateEmployeeCode);
                            if (!string.IsNullOrWhiteSpace(authenticateUserModel.EMAIL))
                            {
                                CommonServices.send_mail_to_vendor_otp(authenticateUserModel.EMAIL, model.IssueId + " - " + model.Subject, model.Communication.Remark, attachment);
                            }
                            if (!string.IsNullOrWhiteSpace(authenticateUserModel.MOBILE))
                            {
                                CommonServices.sms(authenticateUserModel.MOBILE, model.IssueId + " - " + model.Subject);
                            }
                        }

                        if (model.StatusId == (int)Enums.IssueStatus.Completed || model.StatusId == (int)Enums.IssueStatus.Closed)
                        {
                            AuthenticationService authenticationService = new AuthenticationService();
                            AuthenticateUserModel authenticateUserModel = authenticationService.GetEmployee(model.RequestCreateEmployeeCode);
                            if (!string.IsNullOrWhiteSpace(authenticateUserModel.EMAIL))
                            {
                                CommonServices.send_mail_to_vendor_otp(authenticateUserModel.EMAIL, model.IssueId + " - " + model.Subject, model.Communication.Remark, attachment);
                            }
                        }

                    }
                    else if (model.UserType == "IT Developer")
                    {
                        ModuleUserMappingModel moduleUserMappingModel = moduleUserMappingService.GetModuleUserMapping(model.LocationId, model.ApplicationId, model.ModuleId);

                        if (moduleUserMappingModel.HasEmail1)
                        {
                            CommonServices.send_mail_to_vendor_otp(moduleUserMappingModel.Email1, "IT Developer Remark", model.Communication.Remark, attachment);
                        }
                        if (moduleUserMappingModel.HasSMS1)
                        {
                            CommonServices.sms(moduleUserMappingModel.SMS1, "IT Developer Remark");
                        }
                    }
                    else if (model.UserType == "Basis Consultant")
                    {
                        ModuleUserMappingModel moduleUserMappingModel = moduleUserMappingService.GetModuleUserMapping(model.LocationId, model.ApplicationId, model.ModuleId);

                        if (moduleUserMappingModel.HasEmail1)
                        {
                            CommonServices.send_mail_to_vendor_otp(moduleUserMappingModel.Email1, "Basis Consultant Remark", model.Communication.Remark, attachment);
                        }
                        if (moduleUserMappingModel.HasSMS1)
                        {
                            CommonServices.sms(moduleUserMappingModel.SMS1, "Basis Consultant Remark");
                        }
                    }
                    result = true;
                }

                return result;
            }
            catch (Exception ex)
            {
                this.LogError(ex);
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult Communication(RequestModel model, HttpPostedFileBase IssueFile, string type)
        {
            try
            {
                AuthenticateUserModel authenticateUserModel = new AuthenticateUserModel();
                authenticateUserModel = (AuthenticateUserModel)Session["UserData"];
                if (type == "CO")
                {
                    model.Communication.UserType = "Consultant";
                    if (model.ReviseIssueResolveDate != null)
                    {
                        model.RefReqStatusId = (int)Enums.RequestStatus.Request_Revised_Consultant;
                    }
                    else if (model.Communication.IsEscalate)
                    {
                        model.RefReqStatusId = (int)Enums.RequestStatus.Request_Escalated_Consultant;
                    }
                    else if (model.Communication.AssignedUserTypeId == 2 && model.Communication.AssignedEmployeeCode != authenticateUserModel.PERNR)
                    {
                        model.RefReqStatusId = (int)Enums.RequestStatus.Request_Assigned_Other_Consultant;
                    }
                    else if (model.Communication.AssignedUserTypeId == 1)
                    {
                        model.RefReqStatusId = (int)Enums.RequestStatus.Request_Assigned_ITDeveloper;
                    }
                    else if (model.Communication.AssignedUserTypeId == 3)
                    {
                        model.RefReqStatusId = (int)Enums.RequestStatus.Request_Assigned_UAT;
                    }
                    else if (model.Communication.AssignedUserTypeId == 4)
                    {
                        model.RefReqStatusId = (int)Enums.RequestStatus.Request_Assigned_Basis;
                    }

                    if (model.StatusId == (int)Enums.IssueStatus.Closed)
                    {
                        model.RefReqStatusId = (int)Enums.RequestStatus.Request_Closed;
                    }
                    else if (model.StatusId == (int)Enums.IssueStatus.Completed)
                    {
                        model.RefReqStatusId = (int)Enums.RequestStatus.Request_Completed;
                    }

                }
                else if (type == "IT")
                {
                    model.Communication.UserType = "IT Developer";
                    if (model.Communication.IsITCompleted)
                    {
                        model.RefReqStatusId = (int)Enums.RequestStatus.Request_Completed_ITDeveloper;
                    }
                    else
                    {
                        model.RefReqStatusId = (int)Enums.RequestStatus.Request_WIP_ITDeveloper;
                    }
                }
                else if (type == "BASIS")
                {
                    model.Communication.UserType = "Basis Consultant";
                    if (model.Communication.IsBasisCompleted)
                    {
                        model.RefReqStatusId = (int)Enums.RequestStatus.Request_Confirmed_Basis;
                    }
                    else
                    {
                        model.RefReqStatusId = (int)Enums.RequestStatus.Request_WIP_Basis;
                    }
                }
                else if (type == "UAT")
                {
                    model.Communication.UserType = "UAT User";
                    if (model.Communication.IsUATCompleted)
                    {
                        model.RefReqStatusId = (int)Enums.RequestStatus.Request_Confirmed_UAT;
                    }
                    else
                    {
                        model.RefReqStatusId = (int)Enums.RequestStatus.Request_WIP_UAT;
                    }
                }
                else if (type == "HOD")
                {
                    model.Communication.UserType = "HOD";
                    if ((bool)model.IsHODApprove)
                    {
                        model.RefReqStatusId = (int)Enums.RequestStatus.Request_Approved_Assigned_Consultant;
                    }
                    else
                    {
                        model.RefReqStatusId = (int)Enums.RequestStatus.Request_Rejected_HOD;
                    }
                }

                model.UserType = model.Communication.UserType;

                //if (ModelState.IsValid)
                if (model != null)
                {
                    IssueRequestServices issueRequestServices = new IssueRequestServices();

                    if (type == "HOD")
                    {
                        //AuthenticateUserModel authenticateUserModel = new AuthenticateUserModel();
                        //authenticateUserModel = (AuthenticateUserModel)Session["UserData"];
                        model.HODEmployeeCode = authenticateUserModel.PERNR;
                        model.HODARDate = System.DateTime.Now.Date;
                        model.HODARTime = System.DateTime.Now;
                        model.Communication.ConsultantLevel = '0';
                    }

                    //if (type == "HOD" || type == "CO")
                    //{
                    //    model.RequetsId = issueRequestServices.SaveIssueRequest(model);
                    //}

                    model.RequetsId = issueRequestServices.SaveIssueRequest(model);

                    if (model != null && model.RequetsId > 0)
                    {
                        SaveCommunicationAndAttachDocument(model, IssueFile, model.Communication.DocumentsAttachment.DocumentType);
                    }
                }

                return RedirectToAction("RequestList", new { type = type });
            }
            catch (Exception ex)
            {
                return RedirectToAction("LogError", "Base", ex);
            }
        }

        public ActionResult GetConsultant(int LocationId, int ApplicationId, int ModuleID)
        {
            try
            {
                string _emp = string.Empty;
                AuthenticationService _authModel = new AuthenticationService();
                ModuleUserMappingService _mumModel = new ModuleUserMappingService();

                var mum = _mumModel.GetConsultant(LocationId, ApplicationId, ModuleID);
                AuthenticateUserModel emp = new AuthenticateUserModel();
                if (mum != null)
                {
                    emp = _authModel.GetEmployee(mum.Consultant1EmployeeCode);
                    return Json(new { result = "OK", MUMID = mum.MUMId, EMP = emp.PERNR + " - " + emp.ENAME }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = "NO", msg = "No consultant found!" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                //return RedirectToAction("LogError", "Base", ex);
                return Json(new { result = "ERROR", msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region End User Actions
        /// <summary>
        /// Create Request Page Show 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult CreateRequest(string type)
        {
            try
            {
                AuthenticateUserModel authModel = new AuthenticateUserModel();
                string userType = string.Empty;
                if (Session["UserData"] != null)
                {
                    authModel = Session["UserData"] as AuthenticateUserModel;
                }
                if (Session["UserType"] != null)
                {
                    userType = Session["UserType"].ToString();
                }

                RequestModel model = new RequestModel();
                model.RequestType = !string.IsNullOrEmpty(type) && type.ToLower() == "pr" ? "PR" : "CR";
                model.Department = authModel.DEPT;
                model.ContactName = authModel.ENAME;
                model.RequestCreateEmployeeCode = authModel.PERNR;
                model.RequestCreateEmployeeName = authModel.ENAME;
                model.Mobile = !string.IsNullOrWhiteSpace(authModel.MOBILE) ? Convert.ToInt64(authModel.MOBILE) : model.Mobile;

                return View("CreateRequest", model);
            }
            catch (Exception ex)
            {
                this.LogError(ex);
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult CreateRequest(RequestModel model, HttpPostedFileBase IssueFile)
        {

            try
            {
                //using (TransactionScope scope = new TransactionScope())
                //{
                ModelState.Clear();
                TempData["Action"] = "SAVE";
                AuthenticateUserModel authModel = new AuthenticateUserModel();
                string userType = string.Empty;
                if (Session["UserData"] != null)
                {
                    authModel = Session["UserData"] as AuthenticateUserModel;
                }
                if (Session["UserType"] != null)
                {
                    userType = Session["UserType"].ToString();
                }
                if (ModelState.IsValid)
                {
                    IssueRequestServices issueRequestServices = new IssueRequestServices();
                    if (model.RequetsId == 0)
                    {
                        if (model.RefModuleUserMappingId == 0)
                        {
                            ViewBag.Action = "WARNING";
                            return View(model);
                        }
                        else
                        {
                            model.IssueId = model.RequestType + System.DateTime.Now.Year.ToString();
                            model.HODEmployeeCode = string.IsNullOrEmpty(model.HODEmployeeCode) ? "" : model.HODEmployeeCode.PadLeft(8, '0');
                            model.OutcomeofChanges = string.IsNullOrEmpty(model.OutcomeofChanges) ? "" : model.OutcomeofChanges;
                            model.PurposeofNewRequirment = string.IsNullOrEmpty(model.PurposeofNewRequirment) ? "" : model.PurposeofNewRequirment;
                            //model.RefModuleUserMappingId = 0;
                            model.CreateDate = DateTime.Now.Date;
                            model.CreateTime = DateTime.Now;
                            model.UserType = userType;
                            model.RequestCreateEmployeeCode = authModel.PERNR;
                            model.Communication.Remark = model.Description;
                            model.StatusId = Convert.ToInt32(CommonServices.GetStatusList().Where(x => x.Text == "Pending").FirstOrDefault().Value);
                            model.RefReqStatusId = model.RequestType == "PR" ? (int)Enums.RequestStatus.Request_Created_Assigned_Consultant : (int)Enums.RequestStatus.Request_Created_Assigned_HOD;
                            model.RequetsId = issueRequestServices.SaveIssueRequest(model);
                        }


                    }


                    if (model != null && model.RequetsId > 0)
                    {

                        model.Communication.UserType = userType;
                        SaveCommunicationAndAttachDocument(model, IssueFile, "user");
                        TempData["IssueId"] = model.IssueId;
                    }
                    else
                    {
                        ViewBag.Action = "ERROR";
                        return View(model);
                    }

                }
                // scope.Complete();
                return RedirectToAction("RequestList", "Request", new { type = "EU" });
            }
            //}
            catch (Exception ex)
            {
                return RedirectToAction("LogError", "Base", ex);
            }

        }

        public ActionResult UATRequestRemark(int requestId, string op, string type)
        {
            try
            {
                IssueRequestServices issueRequestServices = new IssueRequestServices();
                AuthenticateUserModel authenticateUserModel = (AuthenticateUserModel)Session["UserData"];
                RequestModel model = issueRequestServices.GetIssueRequests(authenticateUserModel.PERNR, requestId).FirstOrDefault();
                CommunicationServices communicationService = new CommunicationServices();
                model.Communications = communicationService.GetCommunications("UAT User", requestId);
                DocumentServices documentServices = new DocumentServices();
                model.Documents = documentServices.GetDocuments(null, requestId);
                model.RequestType = type;
                if (!string.IsNullOrEmpty(op) && op.ToLower() == "view")
                {
                    ViewBag.Action = "View";
                }
                else if (!string.IsNullOrEmpty(op) && op.ToLower() == "edit")
                {
                    ViewBag.Action = "Edit";
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("LogError", "Base", ex);
            }
        }
        #endregion

        #region Consultant Request
        /// <summary>
        /// Consultant Request
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        public ActionResult ConsultantRequestRemark(int requestId, string op, string type, string level)
        {
            try
            {
                IssueRequestServices issueRequestServices = new IssueRequestServices();
                AuthenticateUserModel authenticateUserModel = (AuthenticateUserModel)Session["UserData"];
                RequestModel model = issueRequestServices.GetConsultantIssueRequests(authenticateUserModel.PERNR, requestId).FirstOrDefault();
                model.Communication.AssignedUserTypeId = model.AssignedUserTypeId;
                model.Communication.AssignedEmployeeCode = model.AssignedEmployeeCode;
                CommunicationServices communicationService = new CommunicationServices();
                model.Communications = communicationService.GetCommunications("Consultant", requestId);
                DocumentServices documentServices = new DocumentServices();
                model.Documents = documentServices.GetDocuments(null, requestId);
                model.RequestType = type;
                model.Communication.ConsultantLevel = Convert.ToChar(level);
                ViewBag.ConsultantLevel = level;

                if (!string.IsNullOrEmpty(op) && op.ToLower() == "view")
                {
                    ViewBag.Action = "View";
                }
                else if (!string.IsNullOrEmpty(op) && op.ToLower() == "edit")
                {
                    ViewBag.Action = "Edit";
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("LogError", "Base", ex);
            }
        }
        #endregion

        #region IT Developer Request
        /// <summary>
        /// IT Developer Request
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        public ActionResult ITRequestRemark(int requestId, string op, string type)
        {
            try
            {

                IssueRequestServices issueRequestServices = new IssueRequestServices();
                AuthenticateUserModel authenticateUserModel = (AuthenticateUserModel)Session["UserData"];
                RequestModel model = issueRequestServices.GetITDeveloperIssueRequests(authenticateUserModel.PERNR, requestId).FirstOrDefault();
                model.Communication.AssignedEmployeeCode = model.AssignedEmployeeCode;
                CommunicationServices communicationService = new CommunicationServices();
                model.Communications = communicationService.GetCommunications("IT Developer", requestId);
                DocumentServices documentServices = new DocumentServices();
                model.Documents = documentServices.GetDocuments(null, requestId);
                model.RequestType = type;

                if (!string.IsNullOrEmpty(op) && op.ToLower() == "view")
                {
                    ViewBag.Action = "View";
                }
                else if (!string.IsNullOrEmpty(op) && op.ToLower() == "edit")
                {
                    ViewBag.Action = "Edit";
                }
                return View(model);
            }
            catch (Exception ex)
            {

                return RedirectToAction("LogError", "Base", ex);
            }
        }
        #endregion

        #region Basis Consultant Request
        /// <summary>
        /// Basis Consultant Request
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        public ActionResult BasisRequestRemark(int requestId, string op, string type)
        {
            try
            {
                IssueRequestServices issueRequestServices = new IssueRequestServices();
                AuthenticateUserModel authenticateUserModel = (AuthenticateUserModel)Session["UserData"];
                RequestModel model = issueRequestServices.GetBasisConsultantIssueRequests(authenticateUserModel.PERNR, requestId).FirstOrDefault();
                CommunicationServices communicationService = new CommunicationServices();
                model.Communications = communicationService.GetCommunications("Basis Consultant", requestId);
                DocumentServices documentServices = new DocumentServices();
                model.Documents = documentServices.GetDocuments(null, requestId);
                model.RequestType = type;

                if (!string.IsNullOrEmpty(op) && op.ToLower() == "view")
                {
                    ViewBag.Action = "View";
                }
                else if (!string.IsNullOrEmpty(op) && op.ToLower() == "edit")
                {
                    ViewBag.Action = "Edit";
                }
                return View(model);
            }
            catch (Exception ex)
            {

                return RedirectToAction("LogError", "Base", ex);
            }
        }
        #endregion

        #region HOD Request
        /// <summary>
        /// HOD Request
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        public ActionResult HODRequestRemark(int requestId, string op, string type)
        {
            try
            {
                IssueRequestServices issueRequestServices = new IssueRequestServices();
                AuthenticateUserModel authenticateUserModel = (AuthenticateUserModel)Session["UserData"];
                RequestModel model = issueRequestServices.GetHODIssueRequests(authenticateUserModel.PERNR, requestId).FirstOrDefault();
                CommunicationServices communicationService = new CommunicationServices();
                model.Communications = communicationService.GetCommunications("HOD", requestId);
                DocumentServices documentServices = new DocumentServices();
                model.Documents = documentServices.GetDocuments(null, requestId);
                model.RequestType = type;

                if (!string.IsNullOrEmpty(op) && op.ToLower() == "view")
                {
                    ViewBag.Action = "View";
                }
                else if (!string.IsNullOrEmpty(op) && op.ToLower() == "edit")
                {
                    ViewBag.Action = "Edit";
                }
                return View(model);
            }
            catch (Exception ex)
            {

                return RedirectToAction("LogError", "Base", ex);
            }
        }
        #endregion

        #region Admin Request
        /// <summary>
        /// Admin Request
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>

        public ActionResult AdminRequestRemark(int requestId, string type)
        {
            try
            {
                IssueRequestServices issueRequestServices = new IssueRequestServices();
                AuthenticateUserModel authenticateUserModel = (AuthenticateUserModel)Session["UserData"];
                RequestModel model = issueRequestServices.GetAdminIssueRequests(requestId, null, null, null, null, null, null, null).FirstOrDefault();
                model.Communication.AssignedUserTypeId = model.AssignedUserTypeId;
                model.Communication.AssignedEmployeeCode = model.AssignedEmployeeCode;
                CommunicationServices communicationService = new CommunicationServices();
                model.Communications = communicationService.GetCommunications("Admin", requestId);
                DocumentServices documentServices = new DocumentServices();
                model.Documents = documentServices.GetDocuments(null, requestId);
                model.RequestType = type;
                ViewBag.Action = "View";
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("LogError", "Base", ex);
            }
        }
        #endregion

    }
}
