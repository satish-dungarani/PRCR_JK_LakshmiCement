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
    public class Communication : BaseDBA
    {
        public Communication()
        {
        }

        public int InsertUpdateCommunication(
            int communictionid,
            int refrequestid,
            string communicationtype,
            int assignedusertypeid,
            string assignedemployeecode,
            string assignedemployeename,
            char consultantlevel,
            bool isescalate,
            string personforuat,
            string systemforuat,
            string usertype,
            string remark,
            bool isuatcompleted,
            DateTime remarkdate,
            DateTime remarktime,
            string remarkempcode,
            string remarkempname,
            bool isitcompleted,
            bool isbasiscompleted
            )
        {
            try
            {
                string query = "{call SP_CR_LOG_COMMUNICATION_INSUPD (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)}";
                List<OdbcParameter> prms = new List<OdbcParameter>();
                OdbcParameter prm = new OdbcParameter("P_COMMUNICTIONID", communictionid);
                prm.Direction = ParameterDirection.InputOutput;

                prms.Add(prm);
                prms.Add(new OdbcParameter("P_REFREQUESTID", refrequestid));
                prms.Add(new OdbcParameter("P_COMMUNICATIONTYPE", communicationtype));
                prms.Add(new OdbcParameter("P_ASSIGNEDUSERTYPEID", assignedusertypeid));
                prms.Add(new OdbcParameter("P_ASSIGNEDEMPLOYEECODE", assignedemployeecode == null ? "" : assignedemployeecode));
                prms.Add(new OdbcParameter("P_ASSIGNEDEMPLOYEENAME", assignedemployeename == null ? "" : assignedemployeename));
                prms.Add(new OdbcParameter("P_CONSULTANTLEVEL", consultantlevel == '\0' ? '0' : consultantlevel));
                prms.Add(new OdbcParameter("P_ISESCALATE", isescalate ? "Y" : "N"));
                prms.Add(new OdbcParameter("P_PERSONFORUAT", personforuat == null ? "" : personforuat));
                prms.Add(new OdbcParameter("P_SYSTEMFORUAT", systemforuat == null ? "" : systemforuat));
                prms.Add(new OdbcParameter("P_USERTYPE", usertype));
                prms.Add(new OdbcParameter("P_REMARK", remark));
                prms.Add(new OdbcParameter("P_ISUATCOMPLETED", isuatcompleted ? "Y" : "N"));
                prms.Add(new OdbcParameter("P_REMARKDATE", Convert.ToDateTime(remarkdate).ToString("dd-MMM-yyyy")));
                prms.Add(new OdbcParameter("P_REMARKTIME", Convert.ToDateTime(remarktime).ToString("dd-MMM-yyyy hh:mm:ss")));
                prms.Add(new OdbcParameter("P_REMARKEMPCODE", remarkempcode));
                prms.Add(new OdbcParameter("P_REMARKEMPNAME", remarkempname));
                prms.Add(new OdbcParameter("P_ISITCOMPLETED", isitcompleted ? "Y" : "N"));
                prms.Add(new OdbcParameter("P_ISBASISCOMPLETED", isbasiscompleted ? "Y" : "N"));

                return ExecuteNoneQuery(query, prms, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable SelectCommunication(int refReqestId)
        {
            try
            {
                string query = "SELECT CM.COMMUNICTIONID,CM.REFREQUESTID,CM.COMMUNICATIONTYPE,CM.ASSIGNEDUSERTYPEID,CM.ASSIGNEDEMPLOYEECODE,CM.ASSIGNEDEMPLOYEENAME,CM.CONSULTANTLEVEL,CM.ISESCALATE,CM.PERSONFORUAT,CM.SYSTEMFORUAT,CM.USERTYPE,CM.REMARK,CM.ISUATCOMPLETED,CM.REMARKDATE,CM.REMARKTIME,CM.REMARKEMPCODE,CM.REMARKEMPNAME,D.DOCUMENTID,D.REFCOMMUNICATIONID,D.DOCUMENTTYPEID,(select DT.DOCUMENTTYPE from CR_LOG_DOCUMENTTYPE DT where DT.DOCUMENTTYPEID = D.DOCUMENTTYPEID)as \"DOCUMENTTYPE\",D.DOCUMENTPATH,D.DOCUMENTATTACHDATE,D.DOCUMENTATTACHTIME " +
                                "FROM CR_LOG_COMMUNICATION CM " +
                                "INNER JOIN CR_LOG_DOCUMENT D on  CM.COMMUNICTIONID = D.REFCOMMUNICATIONID " +
                                "WHERE CM.REFREQUESTID = " + refReqestId;
                 
                List<OdbcParameter> prms = new List<OdbcParameter>();

                return ExecuteDataSet(query, prms, CommandType.Text).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectCommunication(string type, int? requestId)
        {
            try
            {
                string usertypes = string.Empty;
                if (type == "Consultant" || type == "Basis Consultant" || type == "Admin" || type == "HOD")
                {
                    usertypes = "'End User','IT Developer','Consultant','UAT User','Basis Consultant','HOD'";
                }
                else if (type == "End User" || type == "UAT User")
                {
                    usertypes = "'End User','UAT User','Consultant'";
                }
                else if (type == "IT Developer")
                {
                    usertypes = "'Consultant','IT Developer'";
                }
                //string ecode = (string.IsNullOrEmpty(EmployeeCode)) ? "null" : EmployeeCode;
                //string query = "Select * From CR_LOG_ISSUEREQUEST WHERE REQUESTCREATEEMPLOYEECODE = NVL(" + ecode + ", REQUESTCREATEEMPLOYEECODE)";
                string id = (requestId == null) ? "null" : requestId.ToString();
                string query = "Select c.COMMUNICTIONID, c.ASSIGNEDUSERTYPEID, c.ASSIGNEDEMPLOYEECODE, c.ASSIGNEDEMPLOYEENAME , c.REMARK, c.REMARKDATE, c.REMARKTIME, c.REMARKEMPCODE, c.REMARKEMPNAME, c.ISESCALATE, c.USERTYPE, c.CONSULTANTLEVEL, c.ISUATCOMPLETED, c.ISITCOMPLETED, c.ISBASISCOMPLETED " +
                          "From CR_LOG_ISSUEREQUEST  I " +
                          "Inner Join cr_log_communication c " +
                          "On c.REFREQUESTID = I.REQUESTID  " +
                          "WHERE c.USERTYPE IN (" + usertypes + ") AND I.REQUESTID = NVL(" + id + ",I.REQUESTID) ORDER BY c.COMMUNICTIONID Desc";
                //List<OdbcParameter> prms = new List<OdbcParameter>();

                return ExecuteDataSet(query, null, CommandType.Text).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}















