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
    public class CommonDB : BaseDBA
    {
        private string connectionString;
        public CommonDB()
        {
            connectionString = "Driver={Oracle in OraClient11g_home1};Dbq=ORCLJK;Uid=pmmaint;Pwd=pmmaint";
            //connectionString = "Driver={Oracle in OraClient11g_home1};Dbq=ora11g;Uid=pmmaint;Pwd=pmmaint";
        }

        public int DeleteMaster(int pkId, string MasterName)
        {
            try
            {
                string query = "{call SP_CR_LOG_DELETEMASTER (?,?)}";

                List<OdbcParameter> prms = new List<OdbcParameter>();
                prms.Add(new OdbcParameter("MASTERID", pkId));
                prms.Add(new OdbcParameter("MASTERTBLNAME", MasterName));

                return ExecuteNoneQuery(query, prms, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string smsstring(string sms_mob, string body)
        {
            string mainstring = string.Empty;
            DataTable dt = new DataTable();
            string str11 = "select SNO,DESC1 from SMS_STRING WHERE FLG='Y' order by order1";
            //string str11 = "select SNO,DESC1 from SAA.SMS_STRING WHERE FLG='Y' order by order1";
            OdbcDataAdapter str_result = new OdbcDataAdapter(str11, connectionString);
            str_result.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= (dt.Rows.Count - 1); i++)
                {
                    if (dt.Rows[i]["SNO"].ToString() == "4")
                    {
                        mainstring = mainstring + dt.Rows[i]["DESC1"].ToString() + sms_mob;

                    }
                    else
                    {
                        if (dt.Rows[i]["SNO"].ToString() == "6")
                        {
                            mainstring = mainstring + dt.Rows[i]["DESC1"].ToString() + body;
                        }
                        else
                        {
                            mainstring = mainstring + dt.Rows[i]["DESC1"].ToString();
                        }
                    }
                }
            }
            return mainstring;
        }

        public DataTable DashboardData(string empCode, string type)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "Select Count(1) TotalRequest, I.REQUESTTYPE, S.Status, S.StatusId " +
                               "From CR_LOG_ISSUEREQUEST I " +
                               "Inner Join cr_log_status s " +
                               "On I.statusid = s.statusid ";
                if (type == "IT Developer")
                {
                    query += "Where I.requestId In (Select c.refrequestid From cr_log_communication c " +
                                   "Where I.RequestId = c.refrequestid And c.assignedemployeecode = '" + empCode + "') AND I.REFREQSTATUSID IN (5,6) ";
                }
                else if (type == "Basis Consultant" || type == "Consultant")
                {
                    query += "Inner Join cr_log_moduleusermap m " +
                               "On m.MUMID = i.refmumid ";
                    if (type == "Basis Consultant")
                    {
                        query += "Where m.basiscoemployeecode = '" + empCode + "' AND I.REFREQSTATUSID IN (14,15) ";
                    }
                    else if (type == "Consultant")
                    {
                        query += "Where m.firstcoemployeecode = '" + empCode + "' AND I.REFREQSTATUSID IN (1,3,4,8,9,10) ";
                    }
                }
                else if (type == "HOD")
                {
                    query += "Where i.hodemployeecode = '" + empCode + "' AND I.REFREQSTATUSID IN (2) ";
                }
                else if (type == "End User")
                {
                    query += "Where i.requestcreateemployeecode = '" + empCode + "'"; // AND I.REFREQSTATUSID IN (11,12) ";
                }
                query += "Group By I.REQUESTTYPE,S.Status, S.StatusId ";

                return ExecuteDataSet(query, null, CommandType.Text).Tables[0];
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable DashboardLocationWiseData(string empCode, string type)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "Select Count(1) TotalRequest,I.REQUESTTYPE,  L.LOCATIONNAME, L.LOCATIONID " +
                               "From CR_LOG_ISSUEREQUEST I " +
                               "Inner Join cr_log_location L " +
                               "On I.LOCATIONID = L.LOCATIONID ";
                if (type == "IT Developer")
                {
                    query += "Where I.requestId In (Select c.refrequestid From cr_log_communication c " +
                                   "Where I.RequestId = c.refrequestid And c.assignedemployeecode = '" + empCode + "') AND I.REFREQSTATUSID IN (5,6) ";
                }
                else if (type == "Basis Consultant" || type == "Consultant")
                {
                    query += "Inner Join cr_log_moduleusermap m " +
                               "On m.MUMID = i.refmumid ";
                    if (type == "Basis Consultant")
                    {
                        query += "Where m.basiscoemployeecode = '" + empCode + "' AND I.REFREQSTATUSID IN (14,15) ";
                    }
                    else if (type == "Consultant")
                    {
                        query += "Where m.firstcoemployeecode = '" + empCode + "' AND I.REFREQSTATUSID IN (1,3,4,8,9,10) ";
                    }
                }
                else if (type == "HOD")
                {
                    query += "Where i.hodemployeecode = '" + empCode + "' AND I.REFREQSTATUSID IN (2) ";
                }
                else if (type == "End User")
                {
                    query += "Where i.requestcreateemployeecode = '" + empCode + "' "; // AND I.REFREQSTATUSID IN (11,12) ";
                }
                query += "Group By I.REQUESTTYPE, L.LOCATIONNAME, L.LOCATIONID ";

                return ExecuteDataSet(query, null, CommandType.Text).Tables[0];
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataSet DashBoard(string EmpCode, string userType, int? LocationId, string fromDate, string toDate)
        {
            try
            {

                string id = (LocationId == null) ? "null" : LocationId.ToString();
                fromDate = string.IsNullOrEmpty(fromDate) ? DateTime.Now.AddMonths(-1).ToString("yyyy/MM/dd") : Convert.ToDateTime(fromDate).ToString("yyyy/MM/dd");
                toDate = string.IsNullOrEmpty(toDate) ? DateTime.Now.ToString("yyyy/MM/dd") : Convert.ToDateTime(toDate).ToString("yyyy/MM/dd");

                DataSet ds = new DataSet();


                // Location wise
                string query = " Select x.* from (select i.REQUESTTYPE, l.LOCATIONNAME, i.LOCATIONID, " +
                               " count ( case when s.status = 'Pending' then 1 end ) Pending,         " +
                               " count ( case when s.status = 'Completed' then 1 end ) Completed,     " +
                               " count ( case when s.status = 'Closed' then 1 end ) Closed,           " +
                               " count ( case when s.status = 'UAT' then 1 end ) UAT,                 " +
                               " count ( case when s.status = 'WIP' then 1 end ) WIP                  " +
                               " from cr_log_location l,CR_LOG_ISSUEREQUEST i,  cr_log_status s       " +
                               " where l.LOCATIONID = i.LOCATIONID and i.StatusId = s.StatusId        " +
                               " and i.createdate   BETWEEN TO_DATE ('" + fromDate + "', 'yyyy/mm/dd') " +
                               " AND TO_DATE ('" + toDate + "', 'yyyy/mm/dd')                         " +
                               " Group by i.REQUESTTYPE, l.LOCATIONNAME,i.LOCATIONID)x                " +
                               " where x.LOCATIONID = NVL(" + id + ", x.LOCATIONID)           ";

                 DataTable dt1 = new DataTable();
                 dt1 = ExecuteDataSet(query, null, CommandType.Text).Tables[0].Copy();
                ds.Tables.Add(dt1);
                
                
                // Module wise 
                string query2 =
                            " Select x.* from (select i.REQUESTTYPE, m.modulename, i.moduleid,  " +
                            " count ( case when s.status = 'Pending' then 1 end ) Pending,      " +
                            " count ( case when s.status = 'Completed' then 1 end ) Completed,  " +
                            " count ( case when s.status = 'Closed' then 1 end ) Closed,        " +
                            " count ( case when s.status = 'UAT' then 1 end ) UAT,              " +
                            " count ( case when s.status = 'WIP' then 1 end ) WIP               " +
                            " from cr_log_Module m,CR_LOG_ISSUEREQUEST i,  cr_log_status s      " +
                            " where m.moduleid = i.moduleid and i.StatusId = s.StatusId         " +
                            " and i.LocationId = NVL(" + id + ", i.LOCATIONID)                  " +
                            " and i.createdate BETWEEN TO_DATE ('" + fromDate + "','yyyy/mm/dd')  " +
                            " AND TO_DATE ('" + toDate + "', 'yyyy/mm/dd')                        " +
                            " Group by i.REQUESTTYPE, m.ModuleName,i.ModuleId)x                 ";

                DataTable dt2 = new DataTable();
                dt2 = ExecuteDataSet(query2, null, CommandType.Text).Tables[0].Copy();
                dt2.TableName = "Table1";
                ds.Tables.Add(dt2);

                return ds;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable SelectReqStatusDet(int? ReqStatusId = null)
        {
            try
            {
                string id = (ReqStatusId == null) ? "null" : ReqStatusId.ToString();
                string query = "SELECT REQSTATUSID, REQSTATUS, DESCRIPTION FROM CR_LOG_REQSTATUS_DET WHERE REQSTATUSID = NVL(" + id + ", REQSTATUSID)";

                List<OdbcParameter> prms = new List<OdbcParameter>();

                return ExecuteDataSet(query, prms, CommandType.Text).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectHodEmployee(string empCode)
        {
            try
            {
                string id = (empCode == null) ? "null" : empCode.ToString();

                string query = "SELECT * FROM (SELECT LPAD(a.hod_code, 8, '0'),b.ename FROM JKPUWB.empsup a,lcwpay.empms b where a.hod_code = substr(b.pernr,3,6) and LPAD(emp_code, 8, '0') = '" + id + "'  ORDER BY ref_no DESC) WHERE ROWNUM = 1 " +
                                "union " +
                                "SELECT * FROM (SELECT LPAD(a.REV_CODE,8,'0'),b.ename FROM JKPUWB.empsup a,lcwpay.empms b where a.REV_CODE = substr(b.pernr,3,6) and LPAD(emp_code, 8, '0') = '" + id + "'  ORDER BY ref_no DESC) WHERE ROWNUM = 1 " +
                                "union " +
                                "SELECT * FROM (SELECT LPAD(a.J_HOD_CODE, 8, '0'),b.ename FROM JKPUWB.empsup a,lcwpay.empms b where a.J_HOD_CODE = substr(b.pernr,3,6) and LPAD(emp_code, 8, '0') = '" + id + "'  ORDER BY ref_no DESC) WHERE ROWNUM = 1";

                //string query = "SELECT * FROM (SELECT LPAD(a.hod_code, 8, '0'),b.ename FROM prd.empsup a,lcwpay.empms b where a.hod_code = substr(b.pernr,3,6) and LPAD(emp_code, 8, '0') = '" + id + "'  ORDER BY ref_no DESC) WHERE ROWNUM = 1 " +
                //               "union " +
                //               "SELECT * FROM (SELECT LPAD(a.REV_CODE,8,'0'),b.ename FROM prd.empsup a,lcwpay.empms b where a.REV_CODE = substr(b.pernr,3,6) and LPAD(emp_code, 8, '0') = '" + id + "'  ORDER BY ref_no DESC) WHERE ROWNUM = 1 " +
                //               "union " +
                //               "SELECT * FROM (SELECT LPAD(a.J_HOD_CODE, 8, '0'),b.ename FROM prd.empsup a,lcwpay.empms b where a.J_HOD_CODE = substr(b.pernr,3,6) and LPAD(emp_code, 8, '0') = '" + id + "'  ORDER BY ref_no DESC) WHERE ROWNUM = 1";



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
