using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;

namespace PRCRDBA
{
    public class IssueType : BaseDBA
    {
        public int InsertUpdateIssueType(int IssueTypeId, string IssueTypeName, string description)
        {
            try
            {
                string query = "{call SP_CR_LOG_ISSUETYPE_INSUPD (?,?,?)}";

                List<OdbcParameter> prms = new List<OdbcParameter>();
                OdbcParameter prm = new OdbcParameter("P_ISSUETYPEID", IssueTypeId);
                prm.Direction = ParameterDirection.InputOutput;
                prms.Add(prm);
                prms.Add(new OdbcParameter("P_ISSUETYPENAME", IssueTypeName));
                prms.Add(new OdbcParameter("P_DESCRIPTION", description)); 

                return ExecuteNoneQuery(query, prms, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable SelectIssueType(int? IssueTypeId)
        {
            try
            {
                string id = (IssueTypeId == null) ? "null" : IssueTypeId.ToString();
                string query = "SELECT ISSUETYPEID, ISSUETYPENAME, DESCRIPTION FROM CR_LOG_ISSUETYPE WHERE ISSUETYPEID = NVL(" + id + ", ISSUETYPEID)";

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
