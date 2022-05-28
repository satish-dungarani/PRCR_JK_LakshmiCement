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
    public class IssueRequest : BaseDBA
    {
        public IssueRequest()
        {
        }

        public int InsertUpdateIssueRequest(
                    int requestid,
                    string issuecode,
                    int locationid,
                    string contactname,
                    string department,
                    long mobile,
                    int applicationid,
                    int moduleid,
                    int priorityid,
                    string requesttype,
                    bool? ishodapprove,
                    string subject,
                    string description,
                    string purposeofnewrequirment,
                    string outcomeofchanges,
                    int statusid,
                    string hodemployeecode,
                    DateTime? hodardate,
                    DateTime? hodartime,
                    string usertype,
                    bool hasemailto2ndcon,
                    bool hassmsto2ndcon,
                    int refmumid,
                    DateTime createdate,
                    DateTime createtime,
                    Nullable<DateTime> targetissueresolvedate,
                    Nullable<DateTime> reviseissueresolvedate,
                    string reqCreateEmpCode,
                    int refReqStatusId,
                    int? RefIssueTypeId

            )
        {
            try
            {
                string query = "{call SP_CR_LOG_ISSUEREQUEST_INSUPD (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)}";

                List<OdbcParameter> prms = new List<OdbcParameter>();
                OdbcParameter prm = new OdbcParameter("P_REQUESTID", requestid);
                prm.Direction = ParameterDirection.InputOutput;

                prms.Add(prm);
                prms.Add(new OdbcParameter("P_ISSUECODE", issuecode));
                prms.Add(new OdbcParameter("P_LOCATIONID", locationid));
                prms.Add(new OdbcParameter("P_CONTACTNAME", contactname));
                prms.Add(new OdbcParameter("P_DEPARTMENT", department));
                prms.Add(new OdbcParameter("P_MOBILE", mobile.ToString()));
                prms.Add(new OdbcParameter("P_APPLICATIONID", applicationid));
                prms.Add(new OdbcParameter("P_MODULEID", moduleid));
                prms.Add(new OdbcParameter("P_PRIORITYID", priorityid));
                prms.Add(new OdbcParameter("P_REQUESTTYPE", requesttype));
                prms.Add(new OdbcParameter("P_ISHODAPPROVE", ishodapprove == true ? 'Y' : 'N'));
                prms.Add(new OdbcParameter("P_SUBJECT", subject));
                prms.Add(new OdbcParameter("P_DESCRIPTION", description));
                prms.Add(new OdbcParameter("P_PURPOSEOFNEWREQUIRMENT", string.IsNullOrWhiteSpace(purposeofnewrequirment) ? "" : purposeofnewrequirment));
                prms.Add(new OdbcParameter("P_OUTCOMEOFCHANGES", string.IsNullOrWhiteSpace(outcomeofchanges) ? "" : outcomeofchanges));
                prms.Add(new OdbcParameter("P_STATUSID", statusid));
                prms.Add(new OdbcParameter("P_HODEMPLOYEECODE", hodemployeecode == null ? "" : hodemployeecode));
                prms.Add(new OdbcParameter("P_HODARDATE", hodardate == null ? "" : Convert.ToDateTime(hodardate).ToString("dd-MMM-yyyy")));
                prms.Add(new OdbcParameter("P_HODARTIME", hodartime == null ? "" : Convert.ToDateTime(hodartime).ToString("dd-MMM-yyyy hh:mm:ss")));
                prms.Add(new OdbcParameter("P_USERTYPE", usertype));
                prms.Add(new OdbcParameter("P_HASEMAILTO2NDCON", hasemailto2ndcon == true ? 'Y' : 'N'));
                prms.Add(new OdbcParameter("P_HASSMSTO2NDCON", hassmsto2ndcon == true ? 'Y' : 'N'));
                prms.Add(new OdbcParameter("P_REFMUMID", refmumid));
                prms.Add(new OdbcParameter("P_CREATEDATE", Convert.ToDateTime(createdate).ToString("dd-MMM-yyyy")));
                prms.Add(new OdbcParameter("P_CREATETIME", Convert.ToDateTime(createtime).ToString("dd-MMM-yyyy hh:mm:ss")));
                prms.Add(new OdbcParameter("P_TargetIssueResolveDate", targetissueresolvedate == null ? "" : Convert.ToDateTime(targetissueresolvedate).ToString("dd-MMM-yyyy")));
                prms.Add(new OdbcParameter("P_ReviseIssueResolveDate", reviseissueresolvedate == null ? "" : Convert.ToDateTime(reviseissueresolvedate).ToString("dd-MMM-yyyy")));
                prms.Add(new OdbcParameter("P_REQUESTCREATEEMPLOYEECODE", reqCreateEmpCode));
                prms.Add(new OdbcParameter("P_REFREQSTATUSID", refReqStatusId));
                prms.Add(new OdbcParameter("P_REFISSUETYPEID", RefIssueTypeId)); //under development

                return ExecuteNoneQuery(query, prms, CommandType.StoredProcedure); ;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //public DataTable SelectIssueRequest(int? RequestId)
        //{
        //    try
        //    {
        //        string id = (RequestId == null) ? "null" : RequestId.ToString();
        //        string query = "Select I.REQUESTID,I.ISSUECODE,I.LOCATIONID, L.LOCATIONNAME, I.CONTACTNAME, I.DEPARTMENT ,I.MOBILE, I.APPLICATIONID, " +
        //                  "AP.APPLICATIONNAME, I.MODULEID, M.MODULENAME, I.PRIORITYID,I.REQUESTTYPE, I.ISHODAPPROVE, I.SUBJECT, I.DESCRIPTION, " +
        //                  "I.PURPOSEOFNEWREQUIRMENT, I.OUTCOMEOFCHANGES,I.STATUSID, I.HODEMPLOYEECODE,I.HODARDATE,TO_CHAR(NVL(I.HODARTIME,'')) as HODARTIME, " +
        //                  "I.USERTYPE,I.HASEMAILTO2NDCON, I.HASSMSTO2NDCON, I.REFMUMID,I.CREATEDATE,TO_CHAR(NVL(I.CREATETIME,'')) as CREATETIME, " +
        //                  "I.TARGETISSUERESOLVEDATE, I.REVISEISSUERESOLVEDATE, I.REQUESTCREATEEMPLOYEECODE  " +
        //                  "From CR_LOG_ISSUEREQUEST  I " +
        //                  "Inner Join CR_LOG_LOCATION  L " +
        //                  "On L.LOCATIONID = I.LOCATIONID  " +
        //                  "Inner Join CR_LOG_APPLICATION  AP  " +
        //                  "On AP.APPLICATIONID = I.APPLICATIONID  " +
        //                  "Inner Join CR_LOG_MODULE  M  " +
        //                  "On M.MODULEID = I.MODULEID  " +
        //                  "WHERE REQUESTID = NVL(" + id + ", REQUESTID)";

        //        List<OdbcParameter> prms = new List<OdbcParameter>();

        //        return ExecuteDataSet(query, prms, CommandType.Text).Tables[0];
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public DataTable SelectIssueRequest(string EmployeeCode, int? RequestId = null, int? locationId = null, int? applicationId = null,
            int? moduleid = null, int? statusId = null, int? priorityId = null, string fromDate = null, string toDate = null, int? refReqStatusId = null, int? refIssueTypeId = null)
        {
            try
            {
                string id = (RequestId == null) ? "null" : RequestId.ToString();
                string lid = locationId == null ? "null" : locationId.ToString();
                string aid = applicationId == null ? "null" : applicationId.ToString();
                string mid = moduleid == null ? "null" : moduleid.ToString();
                string sid = statusId == null ? "null" : statusId.ToString();
                string pid = priorityId == null ? "null" : priorityId.ToString();
                string rsid = refReqStatusId == null ? "null" : refReqStatusId.ToString();
                string itid = refIssueTypeId == null ? "null" : refIssueTypeId.ToString(); 
                string query = "Select I.REQUESTID,I.ISSUECODE,I.LOCATIONID, L.LOCATIONNAME, I.CONTACTNAME, I.DEPARTMENT ,I.MOBILE, I.APPLICATIONID, " +
                          "AP.APPLICATIONNAME, I.MODULEID, M.MODULENAME, I.PRIORITYID, (select p.priority from cr_log_priority p where p.priorityid = I.priorityid)as \"PRIORITY\",I.REQUESTTYPE, I.ISHODAPPROVE, I.SUBJECT, I.DESCRIPTION, " +
                          "I.PURPOSEOFNEWREQUIRMENT, I.OUTCOMEOFCHANGES,I.STATUSID, (select s.status from cr_log_status s where s.statusId = I.statusId)as \"STATUS\", I.HODEMPLOYEECODE, (select hod.pernr || '-' || hod.Ename From lcwpay.empms hod where hod.pernr = I.HODEMPLOYEECODE) as HODEMPLOYEENAME,I.HODARDATE,TO_CHAR(NVL(I.HODARTIME,'')) as HODARTIME, " +
                          "I.USERTYPE,I.HASEMAILTO2NDCON, I.HASSMSTO2NDCON, I.REFMUMID,I.CREATEDATE,TO_CHAR(NVL(I.CREATETIME,'')) as CREATETIME, " +
                          "I.TARGETISSUERESOLVEDATE, I.REVISEISSUERESOLVEDATE, I.REQUESTCREATEEMPLOYEECODE, I.REFREQSTATUSID, (SELECT ds.REQSTATUS FROM CR_LOG_REQSTATUS_DET ds WHERE ds.REQSTATUSID = I.REFREQSTATUSID) AS REFREQSTATUS,  I.REFISSUETYPEID, (SELECT ist.ISSUETYPENAME FROM CR_LOG_ISSUETYPE ist WHERE ist.ISSUETYPEID = I.REFISSUETYPEID) AS ISSUETYPE, " +
                          "(Select c.ASSIGNEDUSERTYPEID From cr_log_communication c Where c.REFREQUESTID = I.REQUESTID And c.CommunictionId = (select max(co.CommunictionId) From cr_log_communication co Where co.REFREQUESTID = I.REQUESTID)) As ASSIGNEDUSERTYPEID, " +
                          "(select e.pernr || '-' || e.Ename From lcwpay.empms e where e.pernr = I.REQUESTCREATEEMPLOYEECODE) as REQUESTCREATEEMPLOYEENAME, " +
                          "(select e.pernr || '-' || e.Ename From lcwpay.empms e where e.pernr = (select mum.FIRSTCOEMPLOYEECODE from cr_log_moduleusermap mum where mum.MUMID = I.REFMUMID)) FIRSTCOEMPLOYEENAME, " +
                          "(select e.pernr || '-' || e.Ename From lcwpay.empms e where e.pernr = (select mum.BASISCOEMPLOYEECODE from cr_log_moduleusermap mum where mum.MUMID = I.REFMUMID)) BASISCOEMPLOYEENAME, " +
                          "Case When (select co.ASSIGNEDEMPLOYEECODE From cr_log_communication co " +
                          "Where co.COMMUNICTIONID = (Select max(c.COMMUNICTIONID) from cr_log_communication c Where c.REFREQUESTID = I.REQUESTID) " +
                          ") IS NULL Then '' Else (select e.pernr || '-' || e.Ename From lcwpay.empms e where e.pernr = (select co.ASSIGNEDEMPLOYEECODE From cr_log_communication co " +
                          "Where co.COMMUNICTIONID = (Select max(c.COMMUNICTIONID) from cr_log_communication c Where c.REFREQUESTID = I.REQUESTID ))) End ASSIGNEDEMPLOYEENAME " +
                          "From CR_LOG_ISSUEREQUEST  I " +
                          "Inner Join CR_LOG_LOCATION  L " +
                          "On L.LOCATIONID = I.LOCATIONID  " +
                          "Inner Join CR_LOG_APPLICATION  AP  " +
                          "On AP.APPLICATIONID = I.APPLICATIONID  " +
                          "Inner Join CR_LOG_MODULE  M  " +
                          "On M.MODULEID = I.MODULEID  " +
                          "WHERE I.REQUESTCREATEEMPLOYEECODE = NVL('" + EmployeeCode + "', I.REQUESTCREATEEMPLOYEECODE) AND REQUESTID = NVL(" + id + ", REQUESTID) " +
                            "And I.LOCATIONID = NVL(" + lid + ", I.LOCATIONID) " +
                            "And I.APPLICATIONID = NVL(" + aid + ", I.APPLICATIONID) " +
                            "And I.MODULEID = NVL(" + mid + ", I.MODULEID) " +
                            "And I.STATUSID = NVL(" + sid + ", I.STATUSID) " +
                            "And I.REFREQSTATUSID = NVL(" + rsid + ", I.REFREQSTATUSID) " +
                            "And I.REFISSUETYPEID = NVL(" + itid + ", I.REFISSUETYPEID) " +
                            "And I.PRIORITYID = NVL(" + pid + ", I.PRIORITYID) ";

                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                {
                    query += "And I.CREATEDATE BETWEEN Cast('" + fromDate + "' as Date)" +
                    "And Cast('" + toDate + "' as Date)";
                }
                query += " ORDER BY  I.REQUESTID DESC";

                return ExecuteDataSet(query, null, CommandType.Text).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectConsultantIssueRequest(string EmployeeCode, int? requestId = null, int? locationId = null, int? applicationId = null,
            int? moduleid = null, int? statusId = null, int? priorityId = null, string fromDate = null, string toDate = null, int? refReqStatusId = null, int? refIssueTypeId = null)
        {
            try
            {
                string id = (requestId == null) ? "null" : requestId.ToString();
                string lid = locationId == null ? "null" : locationId.ToString();
                string aid = applicationId == null ? "null" : applicationId.ToString();
                string mid = moduleid == null ? "null" : moduleid.ToString();
                string sid = statusId == null ? "null" : statusId.ToString();
                string pid = priorityId == null ? "null" : priorityId.ToString();
                string rsid = refReqStatusId == null ? "null" : refReqStatusId.ToString();
                string itid = refIssueTypeId == null ? "null" : refIssueTypeId.ToString();
                string query = "Select I.REQUESTID,I.ISSUECODE,I.LOCATIONID, L.LOCATIONNAME, I.CONTACTNAME, I.DEPARTMENT ,I.MOBILE, I.APPLICATIONID, " +
                          "AP.APPLICATIONNAME, I.MODULEID, M.MODULENAME, I.PRIORITYID, (select p.priority from cr_log_priority p where p.priorityid = I.priorityid)as \"PRIORITY\",I.REQUESTTYPE, I.ISHODAPPROVE, I.SUBJECT, I.DESCRIPTION, " +
                          "I.PURPOSEOFNEWREQUIRMENT, I.OUTCOMEOFCHANGES,I.STATUSID, (select s.status from cr_log_status s where s.statusId = I.statusId)as \"STATUS\", I.HODEMPLOYEECODE, (select hod.pernr || '-' || hod.Ename From lcwpay.empms hod where hod.pernr = I.HODEMPLOYEECODE) as HODEMPLOYEENAME,I.HODARDATE,TO_CHAR(NVL(I.HODARTIME,'')) as HODARTIME, " +
                          "I.USERTYPE,I.HASEMAILTO2NDCON, I.HASSMSTO2NDCON, I.REFMUMID,I.CREATEDATE,TO_CHAR(NVL(I.CREATETIME,'')) as CREATETIME, I.REFREQSTATUSID, (SELECT ds.REQSTATUS FROM CR_LOG_REQSTATUS_DET ds WHERE ds.REQSTATUSID = I.REFREQSTATUSID) AS REFREQSTATUS,  I.REFISSUETYPEID, (SELECT ist.ISSUETYPENAME FROM CR_LOG_ISSUETYPE ist WHERE ist.ISSUETYPEID = I.REFISSUETYPEID) AS ISSUETYPE, " +
                          "I.TARGETISSUERESOLVEDATE, I.REVISEISSUERESOLVEDATE, I.REQUESTCREATEEMPLOYEECODE, mu.FIRSTCOEMPLOYEECODE, mu.SECCOEMPLOYEECODE, mu.THIRDCOEMPLOYEECODE,  " +
                          "case when mu.firstcoemployeecode = '" + EmployeeCode + "' then 1  when  mu.seccoemployeecode = '" + EmployeeCode + "' then 2 when  mu.thirdcoemployeecode = '" + EmployeeCode + "' then 3 end AS CONSULTANTLEVEL, " +
                          "(Select c.ASSIGNEDUSERTYPEID From cr_log_communication c Where c.REFREQUESTID = I.REQUESTID And c.CommunictionId = (select max(co.CommunictionId) From cr_log_communication co Where co.REFREQUESTID = I.REQUESTID)) As ASSIGNEDUSERTYPEID, " +
                          "(Select c.ASSIGNEDEMPLOYEECODE From cr_log_communication c Where c.REFREQUESTID = I.REQUESTID And c.CommunictionId = (select max(co.CommunictionId) From cr_log_communication co Where co.REFREQUESTID = I.REQUESTID)) As ASSIGNEDEMPLOYEECODE, " +
                          "(select e.pernr || '-' || e.Ename From lcwpay.empms e where e.pernr = I.REQUESTCREATEEMPLOYEECODE) as REQUESTCREATEEMPLOYEENAME, " +
                          "(select e.pernr || '-' || e.Ename From lcwpay.empms e where e.pernr = (select mum.FIRSTCOEMPLOYEECODE from cr_log_moduleusermap mum where mum.MUMID = I.REFMUMID)) FIRSTCOEMPLOYEENAME, " +
                          "(select e.pernr || '-' || e.Ename From lcwpay.empms e where e.pernr = (select mum.BASISCOEMPLOYEECODE from cr_log_moduleusermap mum where mum.MUMID = I.REFMUMID)) BASISCOEMPLOYEENAME, " +
                          "Case When (select co.ASSIGNEDEMPLOYEECODE From cr_log_communication co " +
                          "Where co.COMMUNICTIONID = (Select max(c.COMMUNICTIONID) from cr_log_communication c Where c.REFREQUESTID = I.REQUESTID) " +
                          ") IS NULL Then '' Else (select e.pernr || '-' || e.Ename From lcwpay.empms e where e.pernr = (select co.ASSIGNEDEMPLOYEECODE From cr_log_communication co " +
                          "Where co.COMMUNICTIONID = (Select max(c.COMMUNICTIONID) from cr_log_communication c Where c.REFREQUESTID = I.REQUESTID ))) End ASSIGNEDEMPLOYEENAME " +
                          "From CR_LOG_ISSUEREQUEST  I " +
                          "Inner Join cr_log_moduleusermap  mu " +
                          "On mu.mumid = I.REFMUMID  " +
                          "Inner Join CR_LOG_LOCATION  L " +
                          "On L.LOCATIONID = I.LOCATIONID  " +
                          "Inner Join CR_LOG_APPLICATION  AP  " +
                          "On AP.APPLICATIONID = I.APPLICATIONID  " +
                          "Inner Join CR_LOG_MODULE  M  " +
                          "On M.MODULEID = I.MODULEID  " +
                          "WHERE '" + EmployeeCode + "' IN (mu.firstcoemployeecode,mu.seccoemployeecode,mu.thirdcoemployeecode) AND I.REQUESTID = NVL(" + id + ",I.REQUESTID) " +
                          "And I.LOCATIONID = NVL(" + lid + ", I.LOCATIONID) " +
                          "And I.APPLICATIONID = NVL(" + aid + ", I.APPLICATIONID) " +
                          "And I.MODULEID = NVL(" + mid + ", I.MODULEID) " +
                          "And I.STATUSID = NVL(" + sid + ", I.STATUSID) " +
                          "And I.REFREQSTATUSID = NVL(" + rsid + ", I.REFREQSTATUSID) " +
                          "And I.REFISSUETYPEID = NVL(" + itid + ", I.REFISSUETYPEID) " +
                          "And I.PRIORITYID = NVL(" + pid + ", I.PRIORITYID) ";

                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                {
                    query += "And I.CREATEDATE BETWEEN Cast('" + fromDate + "' as Date)" +
                    "And Cast('" + toDate + "' as Date)";
                }
                query += " ORDER BY  I.REQUESTID DESC";

                return ExecuteDataSet(query, null, CommandType.Text).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectITDeveloperIssueRequest(string EmployeeCode, int? requestId = null, int? locationId = null, int? applicationId = null,
            int? moduleid = null, int? statusId = null, int? priorityId = null, string fromDate = null, string toDate = null, int? refReqStatusId = null, int? refIssueTypeId = null)
        {
            try
            {
                string id = (requestId == null) ? "null" : requestId.ToString();
                string lid = locationId == null ? "null" : locationId.ToString();
                string aid = applicationId == null ? "null" : applicationId.ToString();
                string mid = moduleid == null ? "null" : moduleid.ToString();
                string sid = statusId == null ? "null" : statusId.ToString();
                string pid = priorityId == null ? "null" : priorityId.ToString();
                string rsid = refReqStatusId == null ? "null" : refReqStatusId.ToString();
                string itid = refIssueTypeId == null ? "null" : refIssueTypeId.ToString();
                string query = "Select distinct I.REQUESTID,I.ISSUECODE,I.LOCATIONID, L.LOCATIONNAME, I.CONTACTNAME, I.DEPARTMENT ,I.MOBILE, I.APPLICATIONID, " +
                          "AP.APPLICATIONNAME, I.MODULEID, M.MODULENAME, I.PRIORITYID, (select p.priority from cr_log_priority p where p.priorityid = I.priorityid)as \"PRIORITY\",I.REQUESTTYPE, I.ISHODAPPROVE, I.SUBJECT, I.DESCRIPTION, " +
                          "I.PURPOSEOFNEWREQUIRMENT, I.OUTCOMEOFCHANGES,I.STATUSID, (select s.status from cr_log_status s where s.statusId = I.statusId)as \"STATUS\", I.HODEMPLOYEECODE, (select hod.pernr || '-' || hod.Ename From lcwpay.empms hod where hod.pernr = I.HODEMPLOYEECODE) as HODEMPLOYEENAME,I.HODARDATE,TO_CHAR(NVL(I.HODARTIME,'')) as HODARTIME, " +
                          "I.USERTYPE,I.HASEMAILTO2NDCON, I.HASSMSTO2NDCON, I.REFMUMID,I.CREATEDATE,TO_CHAR(NVL(I.CREATETIME,'')) as CREATETIME, I.REFREQSTATUSID, (SELECT ds.REQSTATUS FROM CR_LOG_REQSTATUS_DET ds WHERE ds.REQSTATUSID = I.REFREQSTATUSID) AS REFREQSTATUS,  I.REFISSUETYPEID, (SELECT ist.ISSUETYPENAME FROM CR_LOG_ISSUETYPE ist WHERE ist.ISSUETYPEID = I.REFISSUETYPEID) AS ISSUETYPE, " +
                          "I.TARGETISSUERESOLVEDATE, I.REVISEISSUERESOLVEDATE, I.REQUESTCREATEEMPLOYEECODE, mu.FIRSTCOEMPLOYEECODE, mu.SECCOEMPLOYEECODE, mu.THIRDCOEMPLOYEECODE,  " +
                          "(Select c.ASSIGNEDUSERTYPEID From cr_log_communication c Where c.REFREQUESTID = I.REQUESTID And c.CommunictionId = (select max(co.CommunictionId) From cr_log_communication co Where co.REFREQUESTID = I.REQUESTID)) As ASSIGNEDUSERTYPEID, " +
                          "(Select c.ASSIGNEDEMPLOYEECODE From cr_log_communication c Where c.REFREQUESTID = I.REQUESTID And c.CommunictionId = (select max(co.CommunictionId) From cr_log_communication co Where co.REFREQUESTID = I.REQUESTID)) As ASSIGNEDEMPLOYEECODE, " +
                          "(Select c.ISITCOMPLETED From cr_log_communication c Where c.REFREQUESTID = I.REQUESTID And c.CommunictionId = (select max(co.CommunictionId) From cr_log_communication co Where co.REFREQUESTID = I.REQUESTID)) As ISITCOMPLETED, " +
                          "(select e.pernr || '-' || e.Ename From lcwpay.empms e where e.pernr = I.REQUESTCREATEEMPLOYEECODE) as REQUESTCREATEEMPLOYEENAME, " +
                          "(select e.pernr || '-' || e.Ename From lcwpay.empms e where e.pernr = (select mum.FIRSTCOEMPLOYEECODE from cr_log_moduleusermap mum where mum.MUMID = I.REFMUMID)) FIRSTCOEMPLOYEENAME, " +
                          "(select e.pernr || '-' || e.Ename From lcwpay.empms e where e.pernr = (select mum.BASISCOEMPLOYEECODE from cr_log_moduleusermap mum where mum.MUMID = I.REFMUMID)) BASISCOEMPLOYEENAME, " +
                          "Case When (select co.ASSIGNEDEMPLOYEECODE From cr_log_communication co " +
                          "Where co.COMMUNICTIONID = (Select max(c.COMMUNICTIONID) from cr_log_communication c Where c.REFREQUESTID = I.REQUESTID) " +
                          ") IS NULL Then '' Else (select e.pernr || '-' || e.Ename From lcwpay.empms e where e.pernr = (select co.ASSIGNEDEMPLOYEECODE From cr_log_communication co " +
                          "Where co.COMMUNICTIONID = (Select max(c.COMMUNICTIONID) from cr_log_communication c Where c.REFREQUESTID = I.REQUESTID ))) End ASSIGNEDEMPLOYEENAME " +
                          "From CR_LOG_ISSUEREQUEST  I " +
                          "Inner Join cr_log_moduleusermap  mu " +
                          "On mu.mumid = I.REFMUMID  " +
                          "Inner Join CR_LOG_LOCATION  L " +
                          "On L.LOCATIONID = I.LOCATIONID  " +
                          "Inner Join CR_LOG_APPLICATION  AP  " +
                          "On AP.APPLICATIONID = I.APPLICATIONID  " +
                          "Inner Join CR_LOG_MODULE  M  " +
                          "On M.MODULEID = I.MODULEID  " +
                    //"Inner Join CR_LOG_STATUS  S  " +
                    //"On S.STATUSID = I.STATUSID " +
                    //"Inner Join CR_LOG_COMMUNICATION C " +
                    //"On C.REFREQUESTID = I.REQUESTID " +
                          "WHERE  (Select c.ASSIGNEDEMPLOYEECODE From cr_log_communication c Where c.REFREQUESTID = I.REQUESTID And c.CommunictionId = (select max(co.CommunictionId) From cr_log_communication co Where co.REFREQUESTID = I.REQUESTID)) = '" + EmployeeCode + "'  AND I.REQUESTID = NVL(" + id + ",I.REQUESTID) " +
                          "And I.LOCATIONID = NVL(" + lid + ", I.LOCATIONID) " +
                          "And I.APPLICATIONID = NVL(" + aid + ", I.APPLICATIONID) " +
                          "And I.MODULEID = NVL(" + mid + ", I.MODULEID) " +
                          "And I.STATUSID = NVL(" + sid + ", I.STATUSID) " +
                          "And I.REFREQSTATUSID = NVL(" + rsid + ", I.REFREQSTATUSID) " +
                          "And I.REFISSUETYPEID = NVL(" + itid + ", I.REFISSUETYPEID) " +
                          "And I.PRIORITYID = NVL(" + pid + ", I.PRIORITYID) ";

                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                {
                    query += "And I.CREATEDATE BETWEEN Cast('" + fromDate + "' as Date)" +
                    "And Cast('" + toDate + "' as Date)";
                }
                query += " ORDER BY  I.REQUESTID DESC";

                return ExecuteDataSet(query, null, CommandType.Text).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectBasisConsultantIssueRequest(string EmployeeCode, int? requestId = null, int? locationId = null, int? applicationId = null,
            int? moduleid = null, int? statusId = null, int? priorityId = null, string fromDate = null, string toDate = null, int? refReqStatusId = null, int? refIssueTypeId = null)
        {
            try
            {
                string id = (requestId == null) ? "null" : requestId.ToString();
                string lid = locationId == null ? "null" : locationId.ToString();
                string aid = applicationId == null ? "null" : applicationId.ToString();
                string mid = moduleid == null ? "null" : moduleid.ToString();
                string sid = statusId == null ? "null" : statusId.ToString();
                string pid = priorityId == null ? "null" : priorityId.ToString();
                string rsid = refReqStatusId == null ? "null" : refReqStatusId.ToString();
                string itid = refIssueTypeId == null ? "null" : refIssueTypeId.ToString();
                string query = "Select I.REQUESTID,I.ISSUECODE,I.LOCATIONID, L.LOCATIONNAME, I.CONTACTNAME, I.DEPARTMENT ,I.MOBILE, I.APPLICATIONID, " +
                          "AP.APPLICATIONNAME, I.MODULEID, M.MODULENAME, I.PRIORITYID, (select p.priority from cr_log_priority p where p.priorityid = I.priorityid)as \"PRIORITY\",I.REQUESTTYPE, I.ISHODAPPROVE, I.SUBJECT, I.DESCRIPTION, " +
                          "I.PURPOSEOFNEWREQUIRMENT, I.OUTCOMEOFCHANGES,I.STATUSID, (select s.status from cr_log_status s where s.statusId = I.statusId)as \"STATUS\", I.HODEMPLOYEECODE, (select hod.pernr || '-' || hod.Ename From lcwpay.empms hod where hod.pernr = I.HODEMPLOYEECODE) as HODEMPLOYEENAME,I.HODARDATE,TO_CHAR(NVL(I.HODARTIME,'')) as HODARTIME, " +
                          "I.USERTYPE,I.HASEMAILTO2NDCON, I.HASSMSTO2NDCON, I.REFMUMID,I.CREATEDATE,TO_CHAR(NVL(I.CREATETIME,'')) as CREATETIME, I.REFREQSTATUSID, (SELECT ds.REQSTATUS FROM CR_LOG_REQSTATUS_DET ds WHERE ds.REQSTATUSID = I.REFREQSTATUSID) AS REFREQSTATUS,  I.REFISSUETYPEID, (SELECT ist.ISSUETYPENAME FROM CR_LOG_ISSUETYPE ist WHERE ist.ISSUETYPEID = I.REFISSUETYPEID) AS ISSUETYPE, " +
                          "I.TARGETISSUERESOLVEDATE, I.REVISEISSUERESOLVEDATE, I.REQUESTCREATEEMPLOYEECODE, mu.FIRSTCOEMPLOYEECODE, mu.SECCOEMPLOYEECODE, mu.THIRDCOEMPLOYEECODE,  " +
                          "(Select c.ASSIGNEDUSERTYPEID From cr_log_communication c Where c.REFREQUESTID = I.REQUESTID And c.CommunictionId = (select max(co.CommunictionId) From cr_log_communication co Where co.REFREQUESTID = I.REQUESTID)) As ASSIGNEDUSERTYPEID, " +
                          "(Select c.ISBASISCOMPLETED From cr_log_communication c Where c.REFREQUESTID = I.REQUESTID And c.CommunictionId = (select max(co.CommunictionId) From cr_log_communication co Where co.REFREQUESTID = I.REQUESTID)) As ISBASISCOMPLETED, " +
                          "(select e.pernr || '-' || e.Ename From lcwpay.empms e where e.pernr = I.REQUESTCREATEEMPLOYEECODE) as REQUESTCREATEEMPLOYEENAME, " +
                          "(select e.pernr || '-' || e.Ename From lcwpay.empms e where e.pernr = (select mum.FIRSTCOEMPLOYEECODE from cr_log_moduleusermap mum where mum.MUMID = I.REFMUMID)) FIRSTCOEMPLOYEENAME, " +
                          "(select e.pernr || '-' || e.Ename From lcwpay.empms e where e.pernr = (select mum.BASISCOEMPLOYEECODE from cr_log_moduleusermap mum where mum.MUMID = I.REFMUMID)) BASISCOEMPLOYEENAME, " +
                          "Case When (select co.ASSIGNEDEMPLOYEECODE From cr_log_communication co " +
                          "Where co.COMMUNICTIONID = (Select max(c.COMMUNICTIONID) from cr_log_communication c Where c.REFREQUESTID = I.REQUESTID) " +
                          ") IS NULL Then '' Else (select e.pernr || '-' || e.Ename From lcwpay.empms e where e.pernr = (select co.ASSIGNEDEMPLOYEECODE From cr_log_communication co " +
                          "Where co.COMMUNICTIONID = (Select max(c.COMMUNICTIONID) from cr_log_communication c Where c.REFREQUESTID = I.REQUESTID ))) End ASSIGNEDEMPLOYEENAME " +
                          "From CR_LOG_ISSUEREQUEST  I " +
                          "Inner Join cr_log_moduleusermap  mu " +
                          "On mu.mumid = I.REFMUMID  " +
                          "Inner Join CR_LOG_LOCATION  L " +
                          "On L.LOCATIONID = I.LOCATIONID  " +
                          "Inner Join CR_LOG_APPLICATION  AP  " +
                          "On AP.APPLICATIONID = I.APPLICATIONID  " +
                          "Inner Join CR_LOG_MODULE  M  " +
                          "On M.MODULEID = I.MODULEID  " +
                          "WHERE  mu.BASISCOEMPLOYEECODE =  '" + EmployeeCode + "' AND I.REQUESTID = NVL(" + id + ",I.REQUESTID) " +
                          "And I.LOCATIONID = NVL(" + lid + ", I.LOCATIONID) " +
                          "And I.APPLICATIONID = NVL(" + aid + ", I.APPLICATIONID) " +
                          "And I.MODULEID = NVL(" + mid + ", I.MODULEID) " +
                          "And I.STATUSID = NVL(" + sid + ", I.STATUSID) " +
                          "And I.REFREQSTATUSID = NVL(" + rsid + ", I.REFREQSTATUSID) " +
                          "And I.REFISSUETYPEID = NVL(" + itid + ", I.REFISSUETYPEID) " +
                          "And I.PRIORITYID = NVL(" + pid + ", I.PRIORITYID) ";

                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                {
                    query += "And I.CREATEDATE BETWEEN Cast('" + fromDate + "' as Date)" +
                    "And Cast('" + toDate + "' as Date)";
                }
                query += " ORDER BY  I.REQUESTID DESC";

                return ExecuteDataSet(query, null, CommandType.Text).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectHODIssueRequest(string EmployeeCode, int? requestId = null, int? locationId = null, int? applicationId = null,
            int? moduleid = null, int? statusId = null, int? priorityId = null, string fromDate = null, string toDate = null, int? refReqStatusId = null, int? refIssueTypeId = null)
        {
            try
            {
                string id = (requestId == null) ? "null" : requestId.ToString();
                string lid = locationId == null ? "null" : locationId.ToString();
                string aid = applicationId == null ? "null" : applicationId.ToString();
                string mid = moduleid == null ? "null" : moduleid.ToString();
                string sid = statusId == null ? "null" : statusId.ToString();
                string pid = priorityId == null ? "null" : priorityId.ToString();
                string rsid = refReqStatusId == null ? "null" : refReqStatusId.ToString();
                string itid = refIssueTypeId == null ? "null" : refIssueTypeId.ToString();
                string query = "Select I.REQUESTID,I.ISSUECODE,I.LOCATIONID, L.LOCATIONNAME, I.CONTACTNAME, I.DEPARTMENT ,I.MOBILE, I.APPLICATIONID, " +
                          "AP.APPLICATIONNAME, I.MODULEID, M.MODULENAME, I.PRIORITYID, (select p.priority from cr_log_priority p where p.priorityid = I.priorityid)as \"PRIORITY\",I.REQUESTTYPE, I.ISHODAPPROVE, I.SUBJECT, I.DESCRIPTION, " +
                          "I.PURPOSEOFNEWREQUIRMENT, I.OUTCOMEOFCHANGES,I.STATUSID, (select s.status from cr_log_status s where s.statusId = I.statusId)as \"STATUS\", I.HODEMPLOYEECODE, (select hod.pernr || '-' || hod.Ename From lcwpay.empms hod where hod.pernr = I.HODEMPLOYEECODE) as HODEMPLOYEENAME,I.HODARDATE,TO_CHAR(NVL(I.HODARTIME,'')) as HODARTIME, " +
                          "I.USERTYPE,I.HASEMAILTO2NDCON, I.HASSMSTO2NDCON, I.REFMUMID,I.CREATEDATE,TO_CHAR(NVL(I.CREATETIME,'')) as CREATETIME, I.REFREQSTATUSID, (SELECT ds.REQSTATUS FROM CR_LOG_REQSTATUS_DET ds WHERE ds.REQSTATUSID = I.REFREQSTATUSID) AS REFREQSTATUS,  I.REFISSUETYPEID, (SELECT ist.ISSUETYPENAME FROM CR_LOG_ISSUETYPE ist WHERE ist.ISSUETYPEID = I.REFISSUETYPEID) AS ISSUETYPE, " +
                          "I.TARGETISSUERESOLVEDATE, I.REVISEISSUERESOLVEDATE, I.REQUESTCREATEEMPLOYEECODE, mu.FIRSTCOEMPLOYEECODE, mu.SECCOEMPLOYEECODE, mu.THIRDCOEMPLOYEECODE,  " +
                          "(Select c.ASSIGNEDUSERTYPEID From cr_log_communication c Where c.REFREQUESTID = I.REQUESTID And c.CommunictionId = (select max(co.CommunictionId) From cr_log_communication co Where co.REFREQUESTID = I.REQUESTID)) As ASSIGNEDUSERTYPEID, " +
                          "(select e.pernr || '-' || e.Ename From lcwpay.empms e where e.pernr = I.REQUESTCREATEEMPLOYEECODE) as REQUESTCREATEEMPLOYEENAME, " +
                          "(select e.pernr || '-' || e.Ename From lcwpay.empms e where e.pernr = (select mum.FIRSTCOEMPLOYEECODE from cr_log_moduleusermap mum where mum.MUMID = I.REFMUMID)) FIRSTCOEMPLOYEENAME, " +
                          "(select e.pernr || '-' || e.Ename From lcwpay.empms e where e.pernr = (select mum.BASISCOEMPLOYEECODE from cr_log_moduleusermap mum where mum.MUMID = I.REFMUMID)) BASISCOEMPLOYEENAME, " +
                          "Case When (select co.ASSIGNEDEMPLOYEECODE From cr_log_communication co " +
                          "Where co.COMMUNICTIONID = (Select max(c.COMMUNICTIONID) from cr_log_communication c Where c.REFREQUESTID = I.REQUESTID) " +
                          ") IS NULL Then '' Else (select e.pernr || '-' || e.Ename From lcwpay.empms e where e.pernr = (select co.ASSIGNEDEMPLOYEECODE From cr_log_communication co " +
                          "Where co.COMMUNICTIONID = (Select max(c.COMMUNICTIONID) from cr_log_communication c Where c.REFREQUESTID = I.REQUESTID ))) End ASSIGNEDEMPLOYEENAME " +
                          "From CR_LOG_ISSUEREQUEST  I " +
                          "Inner Join cr_log_moduleusermap  mu " +
                          "On mu.mumid = I.REFMUMID  " +
                          "Inner Join CR_LOG_LOCATION  L " +
                          "On L.LOCATIONID = I.LOCATIONID  " +
                          "Inner Join CR_LOG_APPLICATION  AP  " +
                          "On AP.APPLICATIONID = I.APPLICATIONID  " +
                          "Inner Join CR_LOG_MODULE  M  " +
                          "On M.MODULEID = I.MODULEID  " +
                          "WHERE  I.HODEMPLOYEECODE =  '" + EmployeeCode + "' AND I.REQUESTID = NVL(" + id + ",I.REQUESTID) " +
                          "And I.LOCATIONID = NVL(" + lid + ", I.LOCATIONID) " +
                          "And I.APPLICATIONID = NVL(" + aid + ", I.APPLICATIONID) " +
                          "And I.MODULEID = NVL(" + mid + ", I.MODULEID) " +
                          "And I.STATUSID = NVL(" + sid + ", I.STATUSID) " +
                          "And I.REFREQSTATUSID = NVL(" + rsid + ", I.REFREQSTATUSID) " +
                          "And I.REFISSUETYPEID = NVL(" + itid + ", I.REFISSUETYPEID) " +
                          "And I.PRIORITYID = NVL(" + pid + ", I.PRIORITYID) ";

                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                {
                    query += "And I.CREATEDATE BETWEEN Cast('" + fromDate + "' as Date)" +
                    "And Cast('" + toDate + "' as Date)";
                }
                query += " ORDER BY  I.REQUESTID DESC";
                return ExecuteDataSet(query, null, CommandType.Text).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectAdminIssueRequest(int? RequestId = null, int? locationId = null, int? applicationId = null,
            int? moduleid = null, int? statusId = null, int? priorityId = null, string fromDate = null, string toDate = null, int? refReqStatusId = null, int? refIssueTypeId = null)
        {
            try
            {
                string id = (RequestId == null) ? "null" : RequestId.ToString();
                string lid = locationId == null ? "null" : locationId.ToString();
                string aid = applicationId == null ? "null" : applicationId.ToString();
                string mid = moduleid == null ? "null" : moduleid.ToString();
                string sid = statusId == null ? "null" : statusId.ToString();
                string pid = priorityId == null ? "null" : priorityId.ToString();
                string rsid = refReqStatusId == null ? "null" : refReqStatusId.ToString();
                string itid = refIssueTypeId == null ? "null" : refIssueTypeId.ToString();
                string query = "Select I.REQUESTID,I.ISSUECODE,I.LOCATIONID, L.LOCATIONNAME, I.CONTACTNAME, I.DEPARTMENT ,I.MOBILE, I.APPLICATIONID, " +
                          "AP.APPLICATIONNAME, I.MODULEID, M.MODULENAME, I.PRIORITYID, (select p.priority from cr_log_priority p where p.priorityid = I.priorityid)as \"PRIORITY\",I.REQUESTTYPE, I.ISHODAPPROVE, I.SUBJECT, I.DESCRIPTION, " +
                          "I.PURPOSEOFNEWREQUIRMENT, I.OUTCOMEOFCHANGES,I.STATUSID, (select s.status from cr_log_status s where s.statusId = I.statusId)as \"STATUS\", I.HODEMPLOYEECODE, (select hod.pernr || '-' || hod.Ename From lcwpay.empms hod where hod.pernr = I.HODEMPLOYEECODE) as HODEMPLOYEENAME,I.HODARDATE,TO_CHAR(NVL(I.HODARTIME,'')) as HODARTIME, " +
                          "I.USERTYPE,I.HASEMAILTO2NDCON, I.HASSMSTO2NDCON, I.REFMUMID,I.CREATEDATE,TO_CHAR(NVL(I.CREATETIME,'')) as CREATETIME, I.REFREQSTATUSID, (SELECT ds.REQSTATUS FROM CR_LOG_REQSTATUS_DET ds WHERE ds.REQSTATUSID = I.REFREQSTATUSID) AS REFREQSTATUS,  I.REFISSUETYPEID, (SELECT ist.ISSUETYPENAME FROM CR_LOG_ISSUETYPE ist WHERE ist.ISSUETYPEID = I.REFISSUETYPEID) AS ISSUETYPE, " +
                          "I.TARGETISSUERESOLVEDATE, I.REVISEISSUERESOLVEDATE, I.REQUESTCREATEEMPLOYEECODE,  " +
                          "(Select c.ASSIGNEDUSERTYPEID From cr_log_communication c Where c.REFREQUESTID = I.REQUESTID And c.CommunictionId = (select max(co.CommunictionId) From cr_log_communication co Where co.REFREQUESTID = I.REQUESTID)) As ASSIGNEDUSERTYPEID, " +
                          "(select e.pernr || '-' || e.Ename From lcwpay.empms e where e.pernr = I.REQUESTCREATEEMPLOYEECODE) as REQUESTCREATEEMPLOYEENAME, " +
                          "(select e.pernr || '-' || e.Ename From lcwpay.empms e where e.pernr = (select mum.FIRSTCOEMPLOYEECODE from cr_log_moduleusermap mum where mum.MUMID = I.REFMUMID)) FIRSTCOEMPLOYEENAME, " +
                          "(select e.pernr || '-' || e.Ename From lcwpay.empms e where e.pernr = (select mum.BASISCOEMPLOYEECODE from cr_log_moduleusermap mum where mum.MUMID = I.REFMUMID)) BASISCOEMPLOYEENAME, " +
                          "Case When (select co.ASSIGNEDEMPLOYEECODE From cr_log_communication co " +
                          "Where co.COMMUNICTIONID = (Select max(c.COMMUNICTIONID) from cr_log_communication c Where c.REFREQUESTID = I.REQUESTID) " +
                          ") IS NULL Then '' Else (select e.pernr || '-' || e.Ename From lcwpay.empms e where e.pernr = (select co.ASSIGNEDEMPLOYEECODE From cr_log_communication co " +
                          "Where co.COMMUNICTIONID = (Select max(c.COMMUNICTIONID) from cr_log_communication c Where c.REFREQUESTID = I.REQUESTID ))) End ASSIGNEDEMPLOYEENAME " +
                          "From CR_LOG_ISSUEREQUEST  I " +
                          "Inner Join CR_LOG_LOCATION  L " +
                          "On L.LOCATIONID = I.LOCATIONID  " +
                          "Inner Join CR_LOG_APPLICATION  AP  " +
                          "On AP.APPLICATIONID = I.APPLICATIONID  " +
                          "Inner Join CR_LOG_MODULE  M  " +
                          "On M.MODULEID = I.MODULEID  " +
                          "WHERE I.REQUESTID = NVL(" + id + ", I.REQUESTID) " +
                          "And I.LOCATIONID = NVL(" + lid + ", I.LOCATIONID) " +
                          "And I.APPLICATIONID = NVL(" + aid + ", I.APPLICATIONID) " +
                          "And I.MODULEID = NVL(" + mid + ", I.MODULEID) " +
                          "And I.STATUSID = NVL(" + sid + ", I.STATUSID) " +
                          "And I.REFREQSTATUSID = NVL(" + rsid + ", I.REFREQSTATUSID) " +
                          "And I.REFISSUETYPEID = NVL(" + itid + ", I.REFISSUETYPEID) " +
                          "And I.PRIORITYID = NVL(" + pid + ", I.PRIORITYID) ";

                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                {
                    query += "And I.CREATEDATE BETWEEN Cast('" + fromDate + "' as Date)" +
                    "And Cast('" + toDate + "' as Date)";
                }
                query += " ORDER BY  I.REQUESTID DESC";
                return ExecuteDataSet(query, null, CommandType.Text).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}






























