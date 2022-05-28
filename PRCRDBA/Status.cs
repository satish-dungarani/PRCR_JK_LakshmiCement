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
    public class Status : BaseDBA
    {
        public Status()
        {
        }

        public int InsertUpdateStatus(int StatusId, string StatusName)
        {
            try
            {
                string query = "{call SP_CR_LOG_STATUS_INSUPD (?,?)}"; 

                List<OdbcParameter> prms = new List<OdbcParameter>();
                prms.Add(new OdbcParameter("P_STATUSID", StatusId));
                prms.Add(new OdbcParameter("P_STATUS", StatusName));

                return ExecuteNoneQuery(query, prms,CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable SelectStatus(int? StatusId)
        {
            try
            {
                string id = (StatusId == null) ? "null" : StatusId.ToString();
                string query = "SELECT STATUSID, STATUS FROM CR_LOG_STATUS WHERE STATUSID = NVL(" + id + ", STATUSID)";

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
