 protected void send_mail_to_vendor_otp()
    {
        try
        {
            MailMessage Msg = new MailMessage();
            Msg.From = "automail@lc.jkmail.com";
            Msg.To = vendor_mail;
            Msg.Subject = "SRM OTP";

            Msg.Body = "Dear Sir/Madam your OTP no for SRM Portal------->:" + remdom_password;
            SmtpMail.SmtpServer = "10.10.5.1";
            SmtpMail.Send(Msg);
            Msg = null;

        }
        catch
        {
            update_flg = "X";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "id", "alert('Error While Send mail');", true);
        }
    }


-----------------------

   MailMessage Message = new MailMessage();
                    Message.From = new MailAddress("automail@lc.jkmail.com", "JK LAKSHMI CEMENT");
                    if (read_pernr["EMAIL"].ToString() == "" || read_pernr["EMAIL"].ToString() == " ")
                    {
                        Message.To.Add("shailesh@lc.jkmail.com");
                    }
                    else
                    {
                        //Message.To.Add("shailesh@lc.jkmail.com");
                        Message.To.Add(read_pernr["EMAIL"].ToString());
                    }
                    //Message.Bcc.Add("rupesh.joshi@novelerp.com");
                    //Message.Bcc.Add("shailesh@lc.jkmail.com");
                    //Message.Bcc.Add("akshat@lc.jkmail.com");
                    Message.IsBodyHtml = true;
                    Message.Body = strbody + tblhead3 + "<BR><BR>Regards,<BR>JK LAKSHMI CEMENT<br>This is a system generated mail, please do not reply<br>" + Emailbody;
                    Message.Subject = subject;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "10.10.5.1";
                    smtp.Send(Message);
                    Message.Dispose();