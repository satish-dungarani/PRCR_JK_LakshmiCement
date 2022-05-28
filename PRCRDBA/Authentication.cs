using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PRCRDBA
{
    public class Authentication : BaseDBA
    {
        public DataSet SelectEmployee(string EmpCode, string Password)
        {
            try
            {
                string query = "Select * From jkpuwb.lv_login Where EmpCode = '" + EmpCode + "' And ENC_PASSWORD = '" + Password + "'";
                DataTable dt = new DataTable();
                dt = ExecuteDataSet(query, null, CommandType.Text).Tables[0];
                if (dt.Rows.Count == 1)
                {
                    DataSet ds = new DataSet();
                    query = "Select * From lcwpay.empms Where PERNR = '00" + EmpCode + "'";
                    ds.Tables.Add(ExecuteDataSet(query, null, CommandType.Text).Tables[0].Copy());
                    ds.Tables[0].TableName = "EMPLOYEE";
                    
                    query = "Select * From cr_log_userrolemap Where EMPLOYEECODE = '00" + EmpCode + "'";
                    ds.Tables.Add(ExecuteDataSet(query, null, CommandType.Text).Tables[0].Copy());
                    ds.Tables[1].TableName = "USERROLEMAP";
                    
                    return ds;
                }
                else
                {
                    return null;
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectEmployee()
        {
            try
            {
                string query = "Select PERNR,ENAME From lcwpay.empms Where CATG In ('OS','CA') And RECSTAT = 'C'";
                return ExecuteDataSet(query, null, CommandType.Text).Tables[0];
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable SelectEmployee(string EmpCode)
        {
            try
            {
                string query = "Select * From lcwpay.empms Where PERNR = '"+ EmpCode +"'";
                return ExecuteDataSet(query, null, CommandType.Text).Tables[0];
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
