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
    public class DocumentType : BaseDBA
    {
        public DocumentType()
        {
        }

        public int InsertUpdateDocumentType(int DocumentTypeId, string DocumentTypeName)
        {
            try
            {
                string query = "{call SP_CR_LOG_DOCUMENTTYPE_INSUPD (?,?)}";

                List<OdbcParameter> prms = new List<OdbcParameter>();
                prms.Add(new OdbcParameter("P_DOCUMENTTYPEID", DocumentTypeId));
                prms.Add(new OdbcParameter("P_DOCUMENTTYPE", DocumentTypeName));

                return ExecuteNoneQuery(query, prms, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable SelectDocumentType(int? DocumentTypeId)
        {
            try
            {
                string id = (DocumentTypeId == null) ? "null" : DocumentTypeId.ToString();
                string query = "Select * From CR_LOG_DocumentType WHERE DocumentTypeID = NVL(" + id + ", DocumentTypeID)";

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