using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRCR.Models;

namespace PRCR.Controllers
{
    public class MasterController : Controller
    {

        #region Master
        //
        // GET: /Master/
        CommonServices objCommon = new CommonServices();
        public ActionResult MasterList(string type)
        {
            try
            {
                List<MasterModel> masterModels = new List<MasterModel>();
                if (!string.IsNullOrEmpty(type))
                {
                    ViewBag.Type = type;
                    switch (type.ToLower())
                    {
                        case "lo":
                            ViewBag.MasterName = "Location";
                            break;
                        case "ap":
                            ViewBag.MasterName = "Application";
                            break;
                        case "mo":
                            ViewBag.MasterName = "Module";
                            break;
                        case "pr":
                            ViewBag.MasterName = "Priority";
                            break;
                        case "dt":
                            ViewBag.MasterName = "Document Type";
                            break;
                        case "is":
                            ViewBag.MasterName = "Issue Status";
                            break;
                        case "ist":
                            ViewBag.MasterName = "Issue Type";
                            break; 
                        default:
                            ViewBag.MasterName = "Master";
                            break;

                    }

                    if (type.ToLower() == "lo")
                    {
                        LocationServices locationServices = new LocationServices();
                        masterModels = locationServices.GetLocations(null);
                    }
                    else if (type.ToLower() == "ap")
                    {
                        ApplicationServices appService = new ApplicationServices();
                        masterModels = appService.GetApplication(null);
                    }
                    else if (type.ToLower() == "mo")
                    {
                        ModuleServices obj = new ModuleServices();
                        masterModels = obj.GetModule(null);
                    }
                    else if (type.ToLower() == "pr")
                    {
                        PriorityServices obj = new PriorityServices();
                        masterModels = obj.GetPriority(null);
                    }
                    else if (type.ToLower() == "dt")
                    {
                        DocumentTypeServices obj = new DocumentTypeServices();
                        masterModels = obj.GetDocumentTypes(null);
                    }
                    else if (type.ToLower() == "is")
                    {
                        StatusServices obj = new StatusServices();
                        masterModels = obj.GetStatus(null);
                    }
                    else if (type.ToLower() == "ist")
                    {
                        IssueTypeServices obj = new IssueTypeServices();
                        masterModels = obj.GetIssueTypes(null);
                    } 
                }
                return View(masterModels);
            }
            catch (Exception ex)
            {
                return RedirectToAction("LogError", "Base", ex);
            }

        }

        public PartialViewResult MasterData(int id, string type)
        {
            try
            {
                MasterModel masterModel = new MasterModel();

                if (id > 0)
                {
                    if (type.ToLower() == "lo")
                    {
                        LocationServices Loc = new LocationServices();
                        masterModel = Loc.GetLocations(id).FirstOrDefault();
                    }
                    else if (type.ToLower() == "ap")
                    {
                        ApplicationServices appService = new ApplicationServices();
                        masterModel = appService.GetApplication(id).FirstOrDefault();
                    }
                    else if (type.ToLower() == "mo")
                    {
                        ModuleServices mod = new ModuleServices();
                        masterModel = mod.GetModule(id).FirstOrDefault();
                    }
                    else if (type.ToLower() == "pr")
                    {
                        PriorityServices obj = new PriorityServices();
                        masterModel = obj.GetPriority(id).FirstOrDefault();
                    }
                    else if (type.ToLower() == "dt")
                    {
                        DocumentTypeServices obj = new DocumentTypeServices();
                        masterModel = obj.GetDocumentTypes(id).FirstOrDefault();
                    }
                    else if (type.ToLower() == "is")
                    {
                        StatusServices obj = new StatusServices();
                        masterModel = obj.GetStatus(id).FirstOrDefault();
                    }
                    else if (type.ToLower() == "ist")
                    {
                        IssueTypeServices obj = new IssueTypeServices();
                        masterModel = obj.GetIssueTypes(id).FirstOrDefault(); 
                    } 
                }

                if (type.ToLower() == "lo")
                {
                    ViewBag.MasterName = "Location";
                }
                else if (type.ToLower() == "ap")
                {
                    ViewBag.MasterName = "Application";
                }
                else if (type.ToLower() == "mo")
                {
                    ViewBag.MasterName = "Module";
                }
                else if (type.ToLower() == "pr")
                {
                    ViewBag.MasterName = "Priority";
                }
                else if (type.ToLower() == "dt")
                {
                    ViewBag.MasterName = "Document Type";
                }
                else if (type.ToLower() == "is")
                {
                    ViewBag.MasterName = "Issue Status";
                }
                else if (type.ToLower() == "ist")
                {
                    ViewBag.MasterName = "Issue Type";
                } 

                masterModel.MasterType = type.ToLower();
                ViewBag.Type = type;
                return PartialView("_MasterDataPartial", masterModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult SaveMaster(MasterModel model)
        {
            try
            {
                int result = 0;
                string strMsg = string.Empty;
                if (model.MasterType.ToLower() == "lo")
                {
                    LocationServices obj = new LocationServices();
                    result = obj.SaveLocation(model);
                }
                else if (model.MasterType.ToLower() == "ap")
                {
                    ApplicationServices obj = new ApplicationServices();
                    result = obj.SaveApplication(model);
                }
                else if (model.MasterType.ToLower() == "mo")
                {
                    ModuleServices obj = new ModuleServices();
                    result = obj.SaveModule(model);
                }
                else if (model.MasterType.ToLower() == "pr")
                {
                    PriorityServices obj = new PriorityServices();
                    result = obj.SavePriority(model);
                }
                else if (model.MasterType.ToLower() == "dt")
                {
                    DocumentTypeServices obj = new DocumentTypeServices();
                    result = obj.SaveDocumentType(model);

                    CommonServices.docTypes = CommonServices.GetDocumentType();
                }
                else if (model.MasterType.ToLower() == "is")
                {
                    StatusServices obj = new StatusServices();
                    result = obj.SaveStatus(model);
                }
                else if (model.MasterType.ToLower() == "ist")
                {
                    IssueTypeServices obj = new IssueTypeServices();
                    result = obj.SaveIssueTypes(model);
                } 

                if (result == -1)
                {
                    strMsg = "Duplicate record is not allowed!";
                }
                else if (result > 0)
                {
                    strMsg = "Successfully saved!";
                }
                else
                {
                    strMsg = "Please contact to Administrator and try again later!";
                }

                return Json(new { Result = result, Message = strMsg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return RedirectToAction("LogError", "Base", ex);
            }
        }

        [HttpDelete]
        public ActionResult DeleteMaster(int pkId, string MasterName)
        {
            try
            {
                objCommon.DeleteMasterRecord(pkId, MasterName);

                return Json(new { Message = "Successfully delete!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return RedirectToAction("LogError", "Base", ex);
            }
        }

        #endregion

        #region Mapping
        public ActionResult UserRoleMappingList()
        {
            try
            {
                ModelState.Clear();
                UserRoleMappingService userRoleMappingService = new UserRoleMappingService();
                List<UserRoleMappingModel> userRoleMappingModels = userRoleMappingService.GetUserRoleMapping(null);
                return View(userRoleMappingModels);
            }
            catch (Exception ex)
            {
                return RedirectToAction("LogError", "Base", ex);
            }
        }

        public ActionResult UserRoleMapping(string op, int? urmId)
        {
            try
            {
                TempData["Action"] = op;
                UserRoleMappingModel userRoleMappingModel = new UserRoleMappingModel();
                UserRoleMappingService userRoleMappingService = new UserRoleMappingService();
                if (urmId != null)
                {
                    userRoleMappingModel = userRoleMappingService.GetUserRoleMapping(urmId).FirstOrDefault();
                }
                else
                {
                    userRoleMappingModel.IsEndUser = true;
                }
                return View(userRoleMappingModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("LogError", "Base", ex);
            }
        }

        [HttpPost]
        public JsonResult UserRoleMapping(UserRoleMappingModel model, string op)
        {
            string message = string.Empty;
            string status = string.Empty;
            try
            {
                TempData["Action"] = op;
                UserRoleMappingService userRoleMappingService = new UserRoleMappingService();
                model.URMId = Convert.ToInt32(userRoleMappingService.SaveUserRoleMapping(model));
                message = "User role mapping successfully saved.";
                status = "Ok";
            }
            catch (Exception ex)
            {
                message = "Please contact to Administrator and try again later!";
                status = "Error";
            }
            return Json(new { Message = message, Status = status }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ModuleUserMappingList()
        {
            try
            {
                ModuleUserMappingService moduleUserMappingService = new ModuleUserMappingService();
                List<ModuleUserMappingModel> moduleUserMappingModel = moduleUserMappingService.GetModuleUserMapping(null);
                return View(moduleUserMappingModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("LogError", "Base", ex);
            }
        }

        public ActionResult ModuleUserMapping(string op, int? mumId)
        {
            try
            {
                TempData["Action"] = op;
                //ViewBag.Location = CommonServices.GetLocationList();
                //ViewBag.Application = CommonServices.GetApplicationList();
                //ViewBag.Module = new List<SelectListItem>();
                ModuleUserMappingModel moduleUserMappingModel = new ModuleUserMappingModel();
                if (mumId != null)
                {
                    ModuleUserMappingService moduleUserMappingService = new ModuleUserMappingService();
                    moduleUserMappingModel = moduleUserMappingService.GetModuleUserMapping(mumId).FirstOrDefault();
                    //ViewBag.Module = CommonServices.GetModuleList(moduleUserMappingModel.ApplicationId);
                }
                return View(moduleUserMappingModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("LogError", "Base", ex);
            }
        }

        [HttpPost]
        public ActionResult ModuleUserMapping(ModuleUserMappingModel model, string op)
        {
            try
            {
                TempData["Action"] = op;
                string strMsg = string.Empty;
                ModuleUserMappingService moduleUserMappingService = new ModuleUserMappingService();
                model.MUMId = moduleUserMappingService.SaveModuleUserMapping(model);
                if (model.MUMId == -1)
                {
                    strMsg = "Duplicate user mapping isn't allow for this module on this Location!";
                }
                else if (model.MUMId > 0)
                {
                    strMsg = "Module user Mapping Successfully Saved!";
                }
                else
                {
                    strMsg = "Please contact to Administrator and try again later!";
                }

                return Json(new { Result = "OK", mumid = model.MUMId, msg = strMsg }, JsonRequestBehavior.AllowGet);
                 
            }
            catch (Exception)
            {
                return Json(new { Result = "ERROR",  msg = "Please contact to Administrator and try again later!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Common
        [HttpGet]
        public PartialViewResult ModuleDD(int id)
        {
            try
            {
                MasterModel masterModel = new MasterModel();
                masterModel.ApplicationId = id;
                return PartialView("_ModuleDDListPartial", masterModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
