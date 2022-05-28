using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.Data.Odbc;

namespace PRCRDBA
{
    public class Location : BaseDBA
    {
        public Location()
        {
        }

        public int InsertUpdateLocation(int locationId, string code, string locationName,string description)
        {
            try
            {
                string query = "{call SP_CR_LOG_LOCATION_INSUPD (?,?,?,?)}";
                
                List<OdbcParameter> prms = new List<OdbcParameter>();
                OdbcParameter prm = new OdbcParameter("P_LOCATIONID", locationId);
                prm.Direction = ParameterDirection.InputOutput;
                prms.Add(prm);
                prms.Add(new OdbcParameter("P_LOCATIONNAME", locationName));
                prms.Add(new OdbcParameter("P_DESCRIPTION", description));
                prms.Add(new OdbcParameter("P_CODE", code));

                return ExecuteNoneQuery(query, prms,CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable SelectLocation(int? locationId)
        {
            try
            {
                string id = (locationId == null) ? "null" : locationId.ToString();
                string query = "Select LOCATIONID, LOCATIONNAME, DESCRIPTION, CODE From CR_LOG_LOCATION WHERE LOCATIONID = NVL(" + id + ", LOCATIONID) ORDER BY  LOCATIONID DESC ";

                List<OdbcParameter> prms = new List<OdbcParameter>();
                //prms.Add(new OdbcParameter("P_LOCATIONID", locationId));

                return ExecuteDataSet(query, prms, CommandType.Text).Tables[0];
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
