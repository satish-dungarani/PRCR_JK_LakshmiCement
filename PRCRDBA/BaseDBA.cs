using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
//using Oracle.DataAccess.Client;
using System.Data.Odbc;

namespace PRCRDBA
{
    public class BaseDBA
    {
        private string connectionString = "Driver={Oracle in OraClient11g_home1};Dbq=ORCLJK;Uid=pmmaint;Pwd=pmmaint";
        //private string connectionString = "Driver={Oracle in OraClient11g_home1};Dbq=ora11g;Uid=pmmaint;Pwd=pmmaint";

        public BaseDBA()
        {
        }

        protected int ExecuteNoneQuery(string query, List<OdbcParameter> prms, CommandType ct)
        {
            int result = 0;
            try
            {
                using (OdbcConnection conn = new OdbcConnection(connectionString))
                {
                    using (OdbcCommand cmd = new OdbcCommand(query, conn))
                    {
                        foreach (var prm in prms)
                        {
                            cmd.Parameters.Add(prm);
                        }
                        cmd.CommandType = ct;

                        conn.Open();
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            result = Convert.ToInt32(cmd.Parameters[0].Value);
                        }
                        conn.Close();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected dynamic ExecuteScalar(string query, List<OdbcParameter> prms, CommandType ct)
        {
            dynamic result;
            try
            {
                using (OdbcConnection conn = new OdbcConnection(connectionString))
                {
                    using (OdbcCommand cmd = new OdbcCommand(query, conn))
                    {
                        foreach (var prm in prms)
                        {
                            cmd.Parameters.Add(prm);
                        }
                        cmd.CommandType = ct;

                        conn.Open();
                        result = cmd.ExecuteScalar();
                        conn.Close();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected DataSet ExecuteDataSet(string query, List<OdbcParameter> prms, CommandType ct)
        {
            DataSet result = new DataSet();
            try
            {
                using (OdbcConnection conn = new OdbcConnection(connectionString))
                {
                    using (OdbcCommand cmd = new OdbcCommand(query, conn))
                    {
                        if (prms != null && prms.Count > 0)
                        {
                            foreach (var prm in prms)
                            {
                                cmd.Parameters.Add(prm);
                            }
                        }
                        cmd.CommandType = ct;
                        OdbcDataAdapter da = new OdbcDataAdapter();
                        da.SelectCommand = cmd;

                        conn.Open();
                        da.Fill(result);
                        conn.Close();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
