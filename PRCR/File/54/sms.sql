CREATE TABLE SMS_STRING ( 
  SNO     NUMBER (5), 
  ORDER1  NUMBER (4), 
  DESC1   VARCHAR2 (100), 
  NAME    VARCHAR2 (10), 
  FLG     CHAR (1), 
  TYPE    NUMBER (1) ) ; 

------------------

  private string smsstring(string sms_mob, string body)
    {
        DataTable dt = new DataTable();
        string str11 = "select SNO,DESC1 from SAA.SMS_STRING WHERE FLG='Y' order by order1";
        OdbcDataAdapter str_result = new OdbcDataAdapter(str11, con);
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

----------------------------------

 protected void sms()
    {
        string body = "Your OTP no for SRM Portal is-" + remdom_password + " Do not share with any one.";

        Session["remdom_password"] = remdom_password;
        //  string WFLocation = "http://203.92.40.186:8443/Sun3/Send_SMS2x?user=jklc&password=jklc1234&sender=JKLCLT&text=" + body + "&PhoneNumber=" + vendor_sms + "";
        string WFLocation = smsstring(vendor_sms, body);
        string location = WFLocation;
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(location);
        HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

    }
--------------