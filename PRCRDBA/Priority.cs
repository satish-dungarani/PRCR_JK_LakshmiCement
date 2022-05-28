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
    public class Priority : BaseDBA
    {
        public Priority()
        {
        }

        public int InsertUpdatePriority(int PriorityId, string PriorityName)
        {
            try
            {
                string query = "{call SP_CR_LOG_PRIORITY_INSUPD (?,?)}";

                List<OdbcParameter> prms = new List<OdbcParameter>();
                prms.Add(new OdbcParameter("P_PRIORITYID", PriorityId));
                prms.Add(new OdbcParameter("P_PRIORITY", PriorityName));

                return ExecuteNoneQuery(query, prms, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable SelectPriority(int? PriorityId)
        {
            try
            {
                string id = (PriorityId == null) ? "null" : PriorityId.ToString();
                string query = "Select * From CR_LOG_Priority WHERE PriorityID = NVL(" + id + ", PriorityID)";

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