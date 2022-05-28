using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Oracle.DataAccess.Client;
using System.Data.Odbc;

namespace PRCRDBA
{
    public class Module : BaseDBA
    {
        public Module()
        {
        }

        public int InsertUpdateModule(int ModuleId, int ApplicationId, string ModuleName)
        {
            try
            {
                string query = "{call SP_CR_LOG_MODULE_INSUPD (?,?,?)}";

                List<OdbcParameter> prms = new List<OdbcParameter>();
                OdbcParameter prm = new OdbcParameter("P_MODULEID", ModuleId);
                prm.Direction = ParameterDirection.InputOutput;

                prms.Add(prm);
                //prms.Add(new OdbcParameter("P_MODULEID", ModuleId));
                prms.Add(new OdbcParameter("P_APPLICATIONID", ApplicationId));
                prms.Add(new OdbcParameter("P_MODULENAME", ModuleName));

                return ExecuteNoneQuery(query, prms, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable SelectModule(int? moduleId, int? applicationId = null)
        {
            try
            {
                string mid = (moduleId == null) ? "null" : moduleId.ToString();
                string aid = (applicationId == null) ? "null" : applicationId.ToString();
                string query = "Select M.MODULEID,M.MODULENAME,A.APPLICATIONID,A.APPLICATIONNAME From CR_LOG_MODULE M" +
                        " Inner Join CR_LOG_APPLICATION A" +
                        " On A.APPLICATIONID = M.APPLICATIONID" +
                        " WHERE M.MODULEID = NVL(" + mid + ", M.MODULEID)" +
                        " And A.APPLICATIONID = NVL(" + aid + ",A.APPLICATIONID)  ORDER BY M.MODULEID DESC ";

                List<OdbcParameter> prms = new List<OdbcParameter>(); 

                return ExecuteDataSet(query, prms, CommandType.Text).Tables[0];
            }
            catch (Exception ex)
            { 
                throw ex;
            }
        }
    }
}