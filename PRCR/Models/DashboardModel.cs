using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRCR.Models
{
    public class DashboardModel
    {
        public List<StatusWiseData> statusWiseData { get; set; }
        public List<LocationWiseData> locationWiseData { get; set; }


        public List<LocationWiseRequest> locationWiseRequestlist { get; set; }
        public List<ModuleWiseRequest> ModuleWiseRequestlist { get; set; }
    }

    public class StatusWiseData
    {
        public int TotalRequest { get; set; }
        public string RequestType { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
    }

    public class LocationWiseData
    {
        public string RequestType { get; set; }
        public int TotalRequest { get; set; }
        public string LocationName { get; set; }
        public int LocationId { get; set; }
    }


    public class LocationWiseRequest
    {
        public int TotalRequest { get; set; }
        public string RequestType { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }

        public int PendingCnt { get; set; }
        public int WIPCnt { get; set; }
        public int CompletedCnt { get; set; }
        public int ClosedCnt { get; set; }
        public int UATCnt { get; set; }
         
    }

    public class ModuleWiseRequest
    {
        public int TotalRequest { get; set; }
        public string RequestType { get; set; }
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }

        public int PendingCnt { get; set; }
        public int WIPCnt { get; set; }
        public int CompletedCnt { get; set; }
        public int ClosedCnt { get; set; }
        public int UATCnt { get; set; }
         
    }
}