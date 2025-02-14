using System.Data;
using System.Net;
using System.Net.Mail;

namespace ECOMAPP.CommonRepository
{
    public class DBSendEmail : IDisposable
    {
        #region Properties

        public string SMTPServerName { get; set; }
        public string MailFrom { get; set; }
        public string MailFromPassword { get; set; }
        private List<string> MailToList { get; set; }
        private List<string> MailCcList { get; set; }
        private List<string> MailBccList { get; set; }
        private List<string> MailAttachmentList { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int SMTPPort { get; set; }
        public bool IsBodyHTML { get; set; }
        public bool IsUseDefaultCredentials { get; set; }
        public bool IsEnableSSL { get; set; }

        private List<Attachment> MailAttachmentLists { get; set; }

        private readonly IConfiguration Configuration;

        #endregion

        #region Methods
        public void AddMailToList(string EmailAddress)
        {
            string _Email = EmailAddress.ToString().Trim().ToLower();

            if (MailToList == null)
                MailToList = new List<string>();

            if (string.IsNullOrEmpty(_Email))
                MailToList.Add(_Email);
        }
        public void AddMailBccList(string EmailAddress)
        {
            string _Email = EmailAddress.ToString().Trim().ToLower();

            if (MailBccList == null)
                MailBccList = new List<string>();

            if (_Email != "")
                MailBccList.Add(_Email);
        }

        public void AddAttachmentList(string FileName)
        {
            if (MailAttachmentList == null)
                MailAttachmentList = new List<string>();

            if (FileName != "")
                MailAttachmentList.Add(FileName.ToString().Trim());
        }

        public void AddAttachment(Attachment Attach)
        {
            if (MailAttachmentLists == null)
                MailAttachmentLists = new List<Attachment>();

            if (Attach != null)
                MailAttachmentLists.Add(Attach);
        }

        public string SendEmail()
        {
            string Message = "";
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(MailFrom);

                IConfiguration conf = (new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build());

                bool _IsLive = Convert.ToBoolean(conf["IsLive"].ToString());
                //For Production
                if (_IsLive)
                {
                    if (MailToList != null)
                    {
                        foreach (string Email in MailToList)
                            mail.To.Add(Email);
                    }
                    if (MailCcList != null)
                    {
                        foreach (string Email in MailCcList)
                            mail.CC.Add(Email);
                    }

                    if (MailBccList != null)
                    {
                        foreach (string Email in MailBccList)
                            mail.Bcc.Add(Email);
                    }
                }
                //For Testing
                else
                {
                    mail.To.Add("youremail1@gmail.com");
                    mail.To.Add("youremail2@gmail.com");
                }

                //Selecting Attachemnt Based on FileName and location
                if (MailAttachmentList != null)
                {
                    foreach (string AttachmentFileName in MailAttachmentList)
                    {
                        long filezize = new FileInfo(AttachmentFileName).Length;
                        //If file size is less than 25 mb
                        if (((filezize / 1024) / 1000) < 25)
                        {
                            mail.Attachments.Add(new Attachment(AttachmentFileName));
                        }
                    }
                }
                //Selecting Attachemnt Objects directly
                if (MailAttachmentLists != null)
                {
                    foreach (Attachment AttachmentFileName in MailAttachmentLists)
                    {
                        long fileSize = new FileInfo(AttachmentFileName.Name).Length;
                        if (((fileSize / 1024) / 1000) < 25)
                        { mail.Attachments.Add(AttachmentFileName); }
                    }
                }

                //Configuation For Mail
                SmtpClient SmtpServer = new SmtpClient(SMTPServerName.ToString().Trim());
                SmtpServer.Port = SMTPPort;
                SmtpServer.UseDefaultCredentials = IsUseDefaultCredentials;
                SmtpServer.Credentials = new System.Net.NetworkCredential(MailFrom, MailFromPassword.ToString().Trim());
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                SmtpServer.EnableSsl = IsEnableSSL;

                //Mail Format
                mail.Subject = Subject;
                mail.Body = Body;
                mail.IsBodyHtml = IsBodyHTML;

                //Sending the Mail 
                SmtpServer.Send(mail);

                Message = "Email successfully sent";
            }
            catch (Exception ex)
            {
                Message = ex.Message.ToString();
            }

            return Message;
        }
        public void Dispose()
        {
            MailToList = null;
            MailCcList = null;
            MailBccList = null;
            MailAttachmentList = null;
        }
        #endregion

        public DBSendEmail GetEmailDetails(string NotificationType, string UserName)
        {
            string ProcedureName = "Proc_Get_EmailDetails";
            DBSendEmail objDBSendEmail = new DBSendEmail();
            DataSet ds = new DataSet();

            using (DBAccess Db = new DBAccess())
            {
                Db.DBProcedureName = ProcedureName;
                Db.AddParameters("@NotificationType", NotificationType);
                Db.AddParameters("@UserName", string.IsNullOrEmpty(UserName) ? "fromWhomEmailShouldBeGone@gmail.com" : UserName);
                ds = Db.DBExecute();
                Db.Dispose();
            }
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                objDBSendEmail.SMTPServerName = item[0].ToString();
                objDBSendEmail.SMTPPort = Convert.ToInt32(item[1].ToString());
                objDBSendEmail.MailFrom = item[2].ToString();
                objDBSendEmail.MailFromPassword = item[3].ToString();
                objDBSendEmail.IsEnableSSL = Convert.ToBoolean(item[4].ToString());
                objDBSendEmail.IsBodyHTML = true;
            }
            foreach (DataRow item in ds.Tables[1].Rows)
            {
                objDBSendEmail.Subject = item[0].ToString();
                objDBSendEmail.Body = item[1].ToString();
            }
            ds.Dispose();

            return objDBSendEmail;
        }
    }
}
