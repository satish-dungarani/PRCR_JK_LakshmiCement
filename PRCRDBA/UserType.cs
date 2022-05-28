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
    public class UserType : BaseDBA
    {
        public UserType()
        {
        }

        //public int InsertUpdateUserType(int UserTypeId, string UserType)
        //{
        //    try
        //    {
        //        string query = "{call SP_CR_LOG_USERTYPE_INSUPD (?,?)}";

        //        List<OdbcParameter> prms = new List<OdbcParameter>();
        //        prms.Add(new OdbcParameter("P_USERTYPEID", UserTypeId));
        //        prms.Add(new OdbcParameter("P_USERTYPE", UserType));

        //        return ExecuteNoneQuery(query, prms,CommandType.StoredProcedure);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        public DataTable SelectUserType(int? usertypeId,string isforassignto)
        {
            try
            {
                string id = (usertypeId == null) ? "null" : usertypeId.ToString();

                string query = string.Empty;
                if (isforassignto == "Y")
                {
                    query = "Select USERTYPEID,USERTYPE From CR_LOG_USERTYPE WHERE USERTYPEID = NVL(" + id + ", USERTYPEID) And USERTYPE NOT IN ('Admin','HOD')";
                }
                else {
                    query = "Select USERTYPEID,USERTYPE From CR_LOG_USERTYPE WHERE USERTYPEID = NVL(" + id + ", USERTYPEID)";
                }
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
