using PRCR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRCR.Models;

namespace PRCR.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {

            return View();
        }

        //public ActionResult Dashboard()
        //{
        //    try
        //    {
        //        DashboardModel dashboardModel = new DashboardModel();
        //        if (Session["UserData"] == null)
        //        {
        //            return RedirectToAction("Login", "Authentication");
        //        }
        //        else
        //        {
        //            string userType = (string)Session["UserType"];
        //            DashboardService dashboardService = new Models.DashboardService();
        //            AuthenticateUserModel authenticateUserModel = (AuthenticateUserModel)Session["UserData"];

        //            dashboardModel = dashboardService.GetDashboardData(authenticateUserModel.PERNR, (string)Session["UserType"]);

        //            if (userType == "End User")
        //            {
        //                ViewBag.UserType = "EU";
        //            }
        //            else if (userType == "Consultant")
        //            {
        //                ViewBag.UserType = "CO"; 
        //            }
        //            else if (userType == "IT Developer")
        //            {
        //                ViewBag.UserType = "IT";
        //            }
        //            else if (userType == "Basis Consultant")
        //            {
        //                ViewBag.UserType = "BASIS";
        //            }
        //            else if (userType == "HOD")
        //            {
        //                ViewBag.UserType = "HOD";
        //            }
        //            else if (userType == "Admin")
        //            {
        //                ViewBag.UserType = "ADMIN";
        //            }

        //            dashboardModel.statusWiseData.Add(new StatusWiseData { RequestType = "PR", Status = null, TotalRequest = dashboardModel.statusWiseData.Where(x => x.RequestType.ToUpper() == "PR").Sum(x => x.TotalRequest) });
        //            dashboardModel.statusWiseData.Add(new StatusWiseData { RequestType = "CR", Status = null, TotalRequest = dashboardModel.statusWiseData.Where(x => x.RequestType.ToUpper() == "CR").Sum(x => x.TotalRequest) });
        //        }
        //        return View(dashboardModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public ActionResult Dashboard()
        {
            try
            {
                DashboardModel dashboardModel = new DashboardModel();
                if (Session["UserData"] == null)
                {
                    return RedirectToAction("Login", "Authentication");
                }
                else
                {
                    string userType = (string)Session["UserType"];
                    DashboardService dashboardService = new Models.DashboardService();
                    AuthenticateUserModel authenticateUserModel = (AuthenticateUserModel)Session["UserData"];

                    ViewBag.LocationId = Request["LocationId"];
                    ViewBag.FromToDated = Request["FromToDate"];
                    string fromDate = string.Empty, toDate = string.Empty;
                    if (Request["FromToDate"] != null)
                    {
                        fromDate = Request["FromToDate"].ToString().Split('-')[0].Trim();
                        toDate = Request["FromToDate"].ToString().Split('-')[1].Trim();
                    }

                    int? locId = null;
                    if (Request["LocationId"] != null && Request["LocationId"] != "")
                    {
                        locId = Convert.ToInt32(Request["LocationId"]);
                    }

                    dashboardModel = dashboardService.GetDashboard(authenticateUserModel.PERNR, (string)Session["UserType"], locId, fromDate, toDate);
                    if (dashboardModel.locationWiseRequestlist.Where(x => x.RequestType == "PR").ToList() != null)
                    {
                        dashboardModel.locationWiseRequestlist.Add(new LocationWiseRequest()
                        {
                            LocationName = "Total",
                            RequestType = "PR",
                            PendingCnt = dashboardModel.locationWiseRequestlist.Where(x => x.RequestType == "PR").Sum(x => x.PendingCnt),
                            WIPCnt = dashboardModel.locationWiseRequestlist.Where(x => x.RequestType == "PR").Sum(x => x.WIPCnt),
                            UATCnt = dashboardModel.locationWiseRequestlist.Where(x => x.RequestType == "PR").Sum(x => x.UATCnt),
                            ClosedCnt = dashboardModel.locationWiseRequestlist.Where(x => x.RequestType == "PR").Sum(x => x.ClosedCnt),
                            CompletedCnt = dashboardModel.locationWiseRequestlist.Where(x => x.RequestType == "PR").Sum(x => x.CompletedCnt)
                        });
                    }
                    if (dashboardModel.locationWiseRequestlist.Where(x => x.RequestType == "CR").ToList() != null)
                    {
                        dashboardModel.locationWiseRequestlist.Add(new LocationWiseRequest()
                        {
                            LocationName = "Total",
                            RequestType = "CR",
                            PendingCnt = dashboardModel.locationWiseRequestlist.Where(x => x.RequestType == "CR").Sum(x => x.PendingCnt),
                            WIPCnt = dashboardModel.locationWiseRequestlist.Where(x => x.RequestType == "CR").Sum(x => x.WIPCnt),
                            UATCnt = dashboardModel.locationWiseRequestlist.Where(x => x.RequestType == "CR").Sum(x => x.UATCnt),
                            ClosedCnt = dashboardModel.locationWiseRequestlist.Where(x => x.RequestType == "CR").Sum(x => x.ClosedCnt),
                            CompletedCnt = dashboardModel.locationWiseRequestlist.Where(x => x.RequestType == "CR").Sum(x => x.CompletedCnt)
                        });
                    }
                    if (dashboardModel.ModuleWiseRequestlist.Where(x => x.RequestType == "PR").ToList() != null)
                    {
                        dashboardModel.ModuleWiseRequestlist.Add(new ModuleWiseRequest()
                        {
                            ModuleName = "Total",
                            RequestType = "PR",
                            PendingCnt = dashboardModel.ModuleWiseRequestlist.Where(x => x.RequestType == "PR").Sum(x => x.PendingCnt),
                            WIPCnt = dashboardModel.ModuleWiseRequestlist.Where(x => x.RequestType == "PR").Sum(x => x.WIPCnt),
                            UATCnt = dashboardModel.ModuleWiseRequestlist.Where(x => x.RequestType == "PR").Sum(x => x.UATCnt),
                            ClosedCnt = dashboardModel.ModuleWiseRequestlist.Where(x => x.RequestType == "PR").Sum(x => x.ClosedCnt),
                            CompletedCnt = dashboardModel.ModuleWiseRequestlist.Where(x => x.RequestType == "PR").Sum(x => x.CompletedCnt)
                        });
                    }
                    if (dashboardModel.ModuleWiseRequestlist.Where(x => x.RequestType == "CR").ToList() != null)
                    {
                        dashboardModel.ModuleWiseRequestlist.Add(new ModuleWiseRequest()
                        {
                            ModuleName = "Total",
                            RequestType = "CR",
                            PendingCnt = dashboardModel.ModuleWiseRequestlist.Where(x => x.RequestType == "CR").Sum(x => x.PendingCnt),
                            WIPCnt = dashboardModel.ModuleWiseRequestlist.Where(x => x.RequestType == "CR").Sum(x => x.WIPCnt),
                            UATCnt = dashboardModel.ModuleWiseRequestlist.Where(x => x.RequestType == "CR").Sum(x => x.UATCnt),
                            ClosedCnt = dashboardModel.ModuleWiseRequestlist.Where(x => x.RequestType == "CR").Sum(x => x.ClosedCnt),
                            CompletedCnt = dashboardModel.ModuleWiseRequestlist.Where(x => x.RequestType == "CR").Sum(x => x.CompletedCnt)
                        });
                    }

                    if (userType == "End User")
                    {
                        ViewBag.UserType = "EU";
                    }
                    else if (userType == "Consultant")
                    {
                        ViewBag.UserType = "CO";
                    }
                    else if (userType == "IT Developer")
                    {
                        ViewBag.UserType = "IT";
                    }
                    else if (userType == "Basis Consultant")
                    {
                        ViewBag.UserType = "BASIS";
                    }
                    else if (userType == "HOD")
                    {
                        ViewBag.UserType = "HOD";
                    }
                    else if (userType == "Admin")
                    {
                        ViewBag.UserType = "ADMIN";
                    }

                    // dashboardModel.statusWiseData.Add(new StatusWiseData());
                    //dashboardModel.statusWiseData.Add(new StatusWiseData { RequestType = "PR", Status = null, TotalRequest = dashboardModel.statusWiseData.Where(x => x.RequestType.ToUpper() == "PR").Sum(x => x.TotalRequest) });
                    //dashboardModel.statusWiseData.Add(new StatusWiseData { RequestType = "CR", Status = null, TotalRequest = dashboardModel.statusWiseData.Where(x => x.RequestType.ToUpper() == "CR").Sum(x => x.TotalRequest) });
                }
                return View(dashboardModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult ChangeUserRole()
        {
            try
            {
                if (Session["UserData"] == null)
                {
                    return RedirectToAction("Login", "Authentication");
                }
                else
                {
                    AuthenticateUserModel authenticateUserModel = (AuthenticateUserModel)Session["UserData"];

                    if (Session["UserType"] == "End User")
                    {
                        ViewBag.UserType = "EU";
                    }
                    else if (Session["UserType"] == "UAT User")
                    {
                        ViewBag.UserType = "UAT";
                    }
                    else if (Session["UserType"] == "Consultant")
                    {
                        ViewBag.UserType = "CO";
                    }
                    else if (Session["UserType"] == "IT Developer")
                    {
                        ViewBag.UserType = "IT";
                    }
                    else if (Session["UserType"] == "Basis Consultant")
                    {
                        ViewBag.UserType = "BASIS";
                    }
                    else if (Session["UserType"] == "HOD")
                    {
                        ViewBag.UserType = "HOD";
                    }
                    else if (Session["UserType"] == "Admin")
                    {
                        ViewBag.UserType = "ADMIN";
                    }

                    return RedirectToAction("UserRole", "Authentication", authenticateUserModel.USERROLEMAPPING);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
