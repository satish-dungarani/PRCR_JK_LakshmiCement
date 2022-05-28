using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRCRDBA
{
    public class ModuleUserMap : BaseDBA
    {
        public int InsertUpdateModuleUserMap(int mumId, int locationId, int applicationId, int moduleId,
            string firstCoEmployeeCode, char firstCoHasEmail, char firstCoHasSms,
            string secCoEmployeeCode, char secCoHasEmail, char secCoHasSms,
            string thirdCoEmployeeCode, char thirdCoHasEmail, char thirdCoHasSms,
            string basisCoEmployeeCode, char basisCoHasEmail, char basisCoHasSms)
        {
            try
            {
                string query = "{call SP_CR_LOG_MODULEUSERMAP_INSUPD (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)}";

                List<OdbcParameter> prms = new List<OdbcParameter>();
                var prm = new OdbcParameter("P_MUMID", mumId);
                prm.Direction = ParameterDirection.InputOutput;
                prms.Add(prm);
                prms.Add(new OdbcParameter("P_LOCATIONID", locationId));
                prms.Add(new OdbcParameter("P_APPLICATIONID", applicationId));
                prms.Add(new OdbcParameter("P_MODULEID", moduleId));
                prms.Add(new OdbcParameter("P_FIRSTCOEMPLOYEECODE", firstCoEmployeeCode));
                prms.Add(new OdbcParameter("P_FIRSTCOHASEMAIL", firstCoHasEmail));
                prms.Add(new OdbcParameter("P_FIRSTCOHASSMS", firstCoHasSms));
                prms.Add(new OdbcParameter("P_SECCOEMPLOYEECODE", secCoEmployeeCode == null ? "" : secCoEmployeeCode));
                prms.Add(new OdbcParameter("P_SECCOHASEMAIL", secCoHasEmail));
                prms.Add(new OdbcParameter("P_SECCOHASSMS", secCoHasSms));
                prms.Add(new OdbcParameter("P_THIRDCOEMPLOYEECODE", thirdCoEmployeeCode == null ? "" : thirdCoEmployeeCode));
                prms.Add(new OdbcParameter("P_THIRDCOHASEMAIL", thirdCoHasEmail));
                prms.Add(new OdbcParameter("P_THIRDCOHASSMS", thirdCoHasSms));
                prms.Add(new OdbcParameter("P_BASISCOEMPLOYEECODE", basisCoEmployeeCode == null ? "" : basisCoEmployeeCode));
                prms.Add(new OdbcParameter("P_BASISCOHASEMAIL", basisCoHasEmail));
                prms.Add(new OdbcParameter("P_BASISCOHASSMS", basisCoHasSms));

                return ExecuteNoneQuery(query, prms, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable SelectModuleUserMap(int? mumId)
        {
            try
            {
                string id = mumId == null ? "null" : mumId.ToString();
                string query = "Select mu.MUMID, mu.LOCATIONID,l.LOCATIONNAME,mu.APPLICATIONID, a.APPLICATIONNAME  ,mu.MODULEID ,m.MODULENAME " +
                          ",mu.FIRSTCOEMPLOYEECODE, (SELECT e.ENAME FROM lcwpay.empms e where e.PERNR = mu.FIRSTCOEMPLOYEECODE) AS FIRSTCOEMPLOYEENAME, mu.FIRSTCOHASEMAIL ,mu.FIRSTCOHASSMS " +
                          ",mu.SECCOEMPLOYEECODE,  (SELECT e.ENAME FROM lcwpay.empms e where e.PERNR = mu.SECCOEMPLOYEECODE) AS SECCOEMPLOYEENAME ,mu.SECCOHASEMAIL ,mu.SECCOHASSMS ,mu.THIRDCOEMPLOYEECODE,  (SELECT e.ENAME FROM lcwpay.empms e where e.PERNR = mu.THIRDCOEMPLOYEECODE) AS THIRDCOEMPLOYEENAME  ,mu.THIRDCOHASEMAIL ,mu.THIRDCOHASSMS " +
                          ",mu.BASISCOEMPLOYEECODE,  (SELECT e.ENAME FROM lcwpay.empms e where e.PERNR = mu.BASISCOEMPLOYEECODE) AS BASISCOEMPLOYEENAME  ,mu.BASISCOHASEMAIL ,mu.BASISCOHASSMS " +
                        "From CR_LOG_MODULEUSERMAP mu " +
                        "Inner Join cr_log_location l " +
                        "On l.LocationId = mu.LocationId " +
                        "Inner Join cr_log_Application a " +
                        "On a.ApplicationId = mu.ApplicationId " +
                        "Inner Join cr_log_Module m " +
                        "On m.ModuleId = mu.ModuleId WHERE MUMID = NVL(" + id + ",MUMID)  ORDER BY mu.MUMID DESC ";

                return ExecuteDataSet(query, null, CommandType.Text).Tables[0];
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable SelectModuleUserMap(int locationId, int applicationId, int moduleId)
        {
            try
            {
                //string query = "Select mu.MUMID, mu.LOCATIONID,l.LOCATIONNAME,mu.APPLICATIONID, a.APPLICATIONNAME  ,mu.MODULEID ,m.MODULENAME " +
                //          ",mu.FIRSTCOEMPLOYEECODE ,mu.FIRSTCOHASEMAIL ,mu.FIRSTCOHASSMS, e.MOBILE As FIRSTCOMOBILE,e.EMAIL As FIRSTCOEMAIL " +
                //          ",mu.SECCOEMPLOYEECODE    ,mu.SECCOHASEMAIL ,mu.SECCOHASSMS, em.MOBILE As SECCOMOBILE,em.EMAIL As SECCOEMAIL " +
                //          ",mu.THIRDCOEMPLOYEECODE  ,mu.THIRDCOHASEMAIL ,mu.THIRDCOHASSMS, emp.MOBILE As THIRDCOMOBILE,emp.EMAIL As THIRDCOEMAIL " +
                //          ",mu.BASISCOEMPLOYEECODE  ,mu.BASISCOHASEMAIL ,mu.BASISCOHASSMS, ep.MOBILE As BASISCOMOBILE,ep.EMAIL As BASISCOEMAIL " +
                //        "From CR_LOG_MODULEUSERMAP mu " +
                //        "Inner Join cr_log_location l " +
                //        "On l.LocationId = mu.LocationId " +
                //        "Inner Join cr_log_Application a " +
                //        "On a.ApplicationId = mu.ApplicationId " +
                //        "Inner Join cr_log_Module m " +
                //        "On m.ModuleId = mu.ModuleId " +
                //        "Inner Join lcwpay.empms e " +
                //        "On e.PERNR = mu.FIRSTCOEMPLOYEECODE " +
                //        "Inner Join lcwpay.empms em " +
                //        "On em.PERNR = NVL(mu.SECCOEMPLOYEECODE,em.PERNR) " +
                //        "Inner Join lcwpay.empms emp " +
                //        "On emp.PERNR = NVL(mu.THIRDCOEMPLOYEECODE,emp.PERNR) " +
                //        "Inner Join lcwpay.empms ep " +
                //        "On ep.PERNR = NVL(mu.BASISCOEMPLOYEECODE,ep.PERNR) " +
                //        "WHERE mu.LOCATIONID = " + locationId + " And  mu.APPLICATIONID = " + applicationId + " And  mu.MODULEID = " + moduleId;

                string query = "select mu.MUMID, mu.LOCATIONID,l.LOCATIONNAME,mu.APPLICATIONID, a.APPLICATIONNAME  ,mu.MODULEID ,m.MODULENAME ,mu.FIRSTCOEMPLOYEECODE ,mu.FIRSTCOHASEMAIL ,  " +
                           "   mu.FIRSTCOHASSMS, e.MOBILE As FIRSTCOMOBILE,e.EMAIL As FIRSTCOEMAIL ,mu.SECCOEMPLOYEECODE    ,mu.SECCOHASEMAIL ,  " +
                           "   mu.SECCOHASSMS, em.MOBILE As SECCOMOBILE,em.EMAIL As SECCOEMAIL ,mu.THIRDCOEMPLOYEECODE  ,mu.THIRDCOHASEMAIL ,mu.THIRDCOHASSMS, emp.MOBILE As THIRDCOMOBILE,  " +
                           "   emp.EMAIL As THIRDCOEMAIL ,mu.BASISCOEMPLOYEECODE  ,mu.BASISCOHASEMAIL ,mu.BASISCOHASSMS, ep.MOBILE  " +
                           "    As BASISCOMOBILE,ep.EMAIL As BASISCOEMAIL,e.MOBILE As FIRSTCOMOBILE,e.EMAIL As FIRSTCOEMAIL   " +
                           "    from CR_LOG_MODULEUSERMAP mu,cr_log_location l,cr_log_Application a,cr_log_Module m ,lcwpay.empms e,lcwpay.empms em,lcwpay.empms emp,lcwpay.empms ep     " +
                           "     WHERE l.LocationId = mu.LocationId  and a.ApplicationId = mu.ApplicationId and m.ModuleId = mu.ModuleId   " +
                           "     and e.PERNR(+) = mu.FIRSTCOEMPLOYEECODE and em.PERNR(+) = mu.SECCOEMPLOYEECODE  " +
                           "     and emp.PERNR(+) = mu.THIRDCOEMPLOYEECODE and   " +
                           "      ep.PERNR(+) = mu.BASISCOEMPLOYEECODE  " +
                           "     and  mu.LOCATIONID = " + locationId + " And  mu.APPLICATIONID = " + applicationId + " And  mu.MODULEID = " + moduleId + " ";

                return ExecuteDataSet(query, null, CommandType.Text).Tables[0];
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable SelectConsultant(int locationId, int applicationId, int moduleId)
        {
            try
            {
                string query = "select m.MUMID, m.FIRSTCOEMPLOYEECODE , e.ename as FIRSTCOEMPLOYEENAME from cr_log_moduleusermap m " +
                     "join lcwpay.empms e on e.pernr = m.firstcoemployeecode " +
                     "WHERE m.LOCATIONID = " + locationId + " And  m.APPLICATIONID = " + applicationId + " And  m.MODULEID = " + moduleId;

                return ExecuteDataSet(query, null, CommandType.Text).Tables[0];
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
