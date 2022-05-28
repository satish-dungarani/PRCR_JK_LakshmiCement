using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRCRDBA
{
    public class UserRoleMap : BaseDBA
    {
        public int InsertUpdateUserRoleMap(int urmId, string employeeCode, string employeName, char isAdmin,
            char isConsultant, char isITDeveloper, char isBasisConsultant, char ishod, char isenduser)
        {
            try
            {
                string query = "{call SP_CR_LOG_USERROLEMAP_INSUPD (?,?,?,?,?,?,?,?,?)}";

                List<OdbcParameter> prms = new List<OdbcParameter>();
                OdbcParameter urmIdPrm = new OdbcParameter("P_URMID", urmId);

                urmIdPrm.Direction = ParameterDirection.InputOutput;
                prms.Add(urmIdPrm);
                prms.Add(new OdbcParameter("P_EMPLOYEECODE", employeeCode));
                prms.Add(new OdbcParameter("P_EMPLOYEENAME", employeName));
                prms.Add(new OdbcParameter("P_ISADMIN", isAdmin));
                prms.Add(new OdbcParameter("P_ISCONSULTANT", isConsultant));
                prms.Add(new OdbcParameter("P_ISITDEVELOPER", isITDeveloper));
                prms.Add(new OdbcParameter("P_ISBASISCONSULTANT", isBasisConsultant));
                prms.Add(new OdbcParameter("P_ISHOD", ishod));
                prms.Add(new OdbcParameter("P_ISENDUSER", isenduser));

                urmId = ExecuteNoneQuery(query, prms, CommandType.StoredProcedure);

                return urmId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectUserRoleMap(int? urmId)
        {
            try
            {
                string id = urmId == null ? "null" : urmId.ToString();
                string query = "SELECT URMID, EMPLOYEECODE,EMPLOYEENAME,ISADMIN,ISCONSULTANT,ISITDEVELOPER,ISBASISCONSULTANT,ISHOD,ISENDUSER FROM CR_LOG_USERROLEMAP WHERE URMID = NVL(" + id + ",URMID)";

                return ExecuteDataSet(query, null, CommandType.Text).Tables[0];
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        public DataTable SelectUserRoleMap(string type)
        {
            try
            {
                string cond = string.Empty;
                if (type.ToLower() == "admin")
                {
                    cond = "WHERE ISADMIN = 'Y'";
                }
                else if (type.ToLower() == "consultant")
                {
                    cond = "WHERE ISCONSULTANT = 'Y'";
                }
                else if (type.ToLower() == "itdeveloper")
                {
                    cond = "WHERE ISITDEVELOPER = 'Y'";
                }
                else if (type.ToLower() == "basisconsultant")
                {
                    cond = "WHERE ISBASISCONSULTANT = 'Y'";
                }
                else if (type.ToLower() == "hod")
                {
                    cond = "WHERE ISHOD = 'Y'";
                }
                else if (type.ToLower() == "enduser")
                {
                    cond = "WHERE ISENDUSER = 'Y'";
                } 

                string query = "Select EMPLOYEECODE,EMPLOYEENAME From CR_LOG_USERROLEMAP " + cond;

                return ExecuteDataSet(query, null, CommandType.Text).Tables[0];
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable SelectEmployeeForRoleMap()
        {
            try
            {
                string query = "SELECT E.PERNR,E.ENAME FROM LCWPAY.EMPMS E LEFT JOIN CR_LOG_USERROLEMAP R ON E.PERNR = R.EMPLOYEECODE WHERE R.EMPLOYEECODE IS NULL;";

                return ExecuteDataSet(query, null, CommandType.Text).Tables[0];
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
