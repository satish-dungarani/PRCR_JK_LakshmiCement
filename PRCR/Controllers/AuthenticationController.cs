using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRCR.Models;

namespace PRCR.Controllers
{
    public class AuthenticationController : Controller
    {
        //
        // GET: /Authentication/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult Login(AuthenticationModel model)
        {
            try
            {
                if (model != null)
                {
                    //if (model.EmpCode.ToLower() == "admin" && model.Password.ToLower() == "admin")
                    //{
                    //    Session["UserType"] = "Admin";
                    //    //return RedirectToAction("Dashboard", "Home");
                    //}
                    //else if (model.EmpCode.ToLower() == "enduser" && model.Password.ToLower() == "enduser")
                    //{
                    //    Session["UserType"] = "End User";
                    //}
                    //else if (model.EmpCode.ToLower() == "consultant1" && model.Password.ToLower() == "consultant1")
                    //{
                    //    Session["UserType"] = "Consultant";
                    //}
                    //else if (model.EmpCode.ToLower() == "consultant2" && model.Password.ToLower() == "consultant2")
                    //{
                    //    Session["UserType"] = "Consultant";
                    //}
                    //else if (model.EmpCode.ToLower() == "consultant2" && model.Password.ToLower() == "consultant3")
                    //{
                    //    Session["UserType"] = "Consultant";
                    //}
                    //else if (model.EmpCode.ToLower() == "itdeveloper" && model.Password.ToLower() == "itdeveloper")
                    //{
                    //    Session["UserType"] = "IT Developer";
                    //}
                    //else if (model.EmpCode.ToLower() == "basisconsultant" && model.Password.ToLower() == "basisconsultant")
                    //{
                    //    Session["UserType"] = "Basis Consultant";
                    //}
                    //else if (model.EmpCode.ToLower() == "hod" && model.Password.ToLower() == "hod")
                    //{
                    //    Session["UserType"] = "HOD";
                    //}

                    AuthenticateUserModel authenticateUserModel = new AuthenticateUserModel();
                    AuthenticationService authenticationService = new AuthenticationService();

                    // Password Decryption
                    string enc_password = CommonServices.b64encode(model.Password.ToUpper());

                    authenticateUserModel = authenticationService.GetEmployee(model.EmpCode, enc_password);


                    if (authenticateUserModel != null)
                    {
                        Session["UserData"] = authenticateUserModel;
                        
                        CommonServices.docTypes = CommonServices.GetDocumentType();
                        CommonServices.userType = CommonServices.GetUserTypeList();

                        if (authenticateUserModel.USERROLEMAPPING.URMId == 0)
                        {
                            Session["UserType"] = "End User";
                            Session["UserRoleCount"] = 1;
                            return RedirectToAction("Dashboard", "Home");
                        }
                        else
                        {
                            //authenticateUserModel.USERROLEMAPPING.IsHOD = true;

                            int count = 0;
                            if (authenticateUserModel.USERROLEMAPPING.IsAdmin)
                            {
                                count++;
                                Session["UserType"] = "Admin";
                            }
                            if (authenticateUserModel.USERROLEMAPPING.IsConsultant)
                            {
                                count++;
                                Session["UserType"] = "Consultant";
                            }
                            if (authenticateUserModel.USERROLEMAPPING.IsITDeveloper)
                            {
                                count++;
                                Session["UserType"] = "IT Developer";
                            }
                            if (authenticateUserModel.USERROLEMAPPING.IsBasisConsultant)
                            {
                                count++;
                                Session["UserType"] = "Basis Consultant";
                            }
                            if (authenticateUserModel.USERROLEMAPPING.IsHOD)
                            {
                                count++;
                                Session["UserType"] = "HOD";
                            }
                            if (authenticateUserModel.USERROLEMAPPING.IsEndUser)
                            {
                                count++;
                                Session["UserType"] = "End User";
                            }

                            if (count > 1)
                            {
                                Session["UserRoleCount"] = count;
                                return RedirectToAction("UserRole", authenticateUserModel.USERROLEMAPPING);
                            }
                            else
                            {
                                Session["UserRoleCount"] = 1;
                                return RedirectToAction("Dashboard", "Home");
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Result = "INVALID";
                    }
                    return View("Login");
                } 
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult UserRole(UserRoleMappingModel model)
        {
            try
            {
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult UserRole(string submit, UserRoleMappingModel model)
        {
            try
            {
                if (submit.ToLower() == "admin") { Session["UserType"] = "Admin"; }
                else if (submit.ToLower() == "consultant") { Session["UserType"] = "Consultant"; }
                else if (submit.ToLower() == "it developer") { Session["UserType"] = "IT Developer"; }
                else if (submit.ToLower() == "basis consultant") { Session["UserType"] = "Basis Consultant"; }
                else if (submit.ToLower() == "hod") { Session["UserType"] = "HOD"; }
                else if (submit.ToLower() == "end user") { Session["UserType"] = "End User"; }
                else
                {
                    return View(model);
                }

                return RedirectToAction("Dashboard", "Home");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
