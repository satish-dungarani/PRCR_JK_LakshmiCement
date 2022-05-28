using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRCR.Models
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (Session["UserData"] == null)
                {
                    filterContext.Result = new RedirectResult(@"\Authentication\Login", true);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            base.OnActionExecuting(filterContext);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogError(Exception ex)
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", ex.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", ex.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", ex.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", ex.TargetSite != null ? ex.TargetSite.ToString() : null);
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;

            string filePath = ConfigurationManager.AppSettings["filePath"].ToString() + "\\ErrorLog";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string path =  filePath + "\\ErrorLog_"+ DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
            using (StreamWriter writer = new StreamWriter(path, true))
            { 
                writer.WriteLine(message);
                writer.Close();
            }

            return View("Error");
        }

    }
}
