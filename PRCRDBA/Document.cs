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
    public class Document : BaseDBA
    {
        public Document()
        {
        }

        public int InsertUpdateDocument(
            int documentId,
            int refCommId,
            int doctypeId,
            string docPath,
            DateTime attachedDate,
            DateTime attachedTime
          ){
            try
            {
                string query = "{call SP_CR_LOG_DOCUMENT_INSUPD (?,?,?,?,?,?)}"; 
                List<OdbcParameter> prms = new List<OdbcParameter>();
                OdbcParameter prm = new OdbcParameter("P_DOCUMENTID", documentId);
                prm.Direction = ParameterDirection.InputOutput;

                prms.Add(prm);
                prms.Add(new OdbcParameter("P_REFCOMMUNICATIONID", refCommId));
                prms.Add(new OdbcParameter("P_DOCUMENTTYPEID", doctypeId));
                prms.Add(new OdbcParameter("P_DOCUMENTPATH", docPath));
                prms.Add(new OdbcParameter("P_DOCUMENTATTACHDATE", Convert.ToDateTime(attachedDate).ToString("dd-MMM-yyyy")));
                prms.Add(new OdbcParameter("P_DOCUMENTATTACHTIME", Convert.ToDateTime(attachedTime).ToString("dd-MMM-yyyy hh:mm:ss")));

                return ExecuteNoneQuery(query, prms,CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable SelectDocument(int? DocumentId)
        {
            try
            {
                string id = (DocumentId == null) ? "null" : DocumentId.ToString();
                string query = "Select * From CR_LOG_Document WHERE DocumentID = NVL(" + id + ", DocumentID)  ORDER BY DocumentID DESC ";

                List<OdbcParameter> prms = new List<OdbcParameter>();

                return ExecuteDataSet(query, prms, CommandType.Text).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectDocuments(string EmployeeCode, int? requestId)
        {
            try
            {
                //string ecode = (string.IsNullOrEmpty(EmployeeCode)) ? "null" : EmployeeCode;
                //string query = "Select * From CR_LOG_ISSUEREQUEST WHERE REQUESTCREATEEMPLOYEECODE = NVL(" + ecode + ", REQUESTCREATEEMPLOYEECODE)";
                string id = (requestId == null) ? "null" : requestId.ToString();
                EmployeeCode = string.IsNullOrEmpty(EmployeeCode) ? "null" : EmployeeCode;
                string query = "Select d.DOCUMENTID,d.DOCUMENTTYPEID, dt.DOCUMENTTYPE, d.DOCUMENTPATH, d.DOCUMENTATTACHDATE, d.DOCUMENTATTACHTIME " +
                          "From CR_LOG_ISSUEREQUEST  I " +
                          "Inner Join cr_log_communication c " +
                          "On c.REFREQUESTID = I.REQUESTID  " +
                          "Inner Join cr_log_document d " +
                          "On d.REFCOMMUNICATIONID = c.COMMUNICTIONID  " +
                          "Inner Join cr_log_documenttype dt " +
                          "On dt.DOCUMENTTYPEID = d.DOCUMENTTYPEID  " +
                          "WHERE c.REMARKEMPCODE = NVL(" + EmployeeCode + ",c.REMARKEMPCODE) AND I.REQUESTID = NVL(" + id + ",I.REQUESTID)";
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

 