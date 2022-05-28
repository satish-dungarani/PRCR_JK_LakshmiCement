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
    public class Application : BaseDBA
    {
        public Application()
        {
        }

        public int InsertUpdateApplication(int ApplicationId, string ApplicationName)
        {
            try
            {
                string query = "{call SP_CR_LOG_APPLICATION_INSUPD (?,?)}";

                List<OdbcParameter> prms = new List<OdbcParameter>();
                OdbcParameter prm = new OdbcParameter("P_APPLICATIONID", ApplicationId);
                prm.Direction = ParameterDirection.InputOutput;

                prms.Add(prm);
                //prms.Add(new OdbcParameter("P_APPLICATIONID", ApplicationId));
                prms.Add(new OdbcParameter("P_APPLICATIONNAME", ApplicationName));

                return ExecuteNoneQuery(query, prms, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable SelectApplication(int? appId)
        {
            try
            {
                string id = (appId == null) ? "null": appId.ToString();
                string query = "Select * From CR_LOG_APPLICATION WHERE APPLICATIONID = NVL(" + id + ", APPLICATIONID)   ORDER BY APPLICATIONID DESC ";

                List<OdbcParameter> prms = new List<OdbcParameter>();
                //prms.Add(new OdbcParameter("P_LOCATIONID", locationId));

                return ExecuteDataSet(query, prms, CommandType.Text).Tables[0];
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
