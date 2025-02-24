using System.Net.Mail;
using System.Net;

namespace ECOMAPP.CommonRepository
{
    public class DBSendEmailSE:DALBASE
    {
 


        #region MailBodyFor OTP
        public string MailOtpBody(string otp, string recipientName)
        {
            string appName = "Maha Agro Mart";
            string companyName = "Maha Agro Mart";
            string emailAddress = "support@mahaagromart.com";

            string address = "MAIDC, Krushi Udyog Bhavan E, Dinkar Rao Desai Marg, Aarey Colony, Goregaon, Mumbai, Maharashtra 400065";
            string zipCode = "400065";
            string countryName = "💖Inida";

            string htmlTemplate = $@"
                <!DOCTYPE html>
                <html lang=""en"">
                <head>
                  <meta charset=""UTF-8"" />
                  <title></title>
                  <style>
                    body {{
                      margin: 0;
                      padding: 0;
                      font-family: ""Helvetica Neue"", Helvetica, Arial, sans-serif;
                      color: #333;
                      background-color: #fff;
                    }}

                    .container {{
                      margin: 0 auto;
                      width: 100%;
                      max-width: 600px;
                      padding: 0 0px;
                      padding-bottom: 10px;
                      border-radius: 5px;
                      line-height: 1.8;
                    }}

                    .header {{
                      border-bottom: 1px solid #eee;
                    }}

                    .header a {{
                      font-size: 1.4em;
                      color: #000;
                      text-decoration: none;
                      font-weight: 600;
                    }}

                    .content {{
                      min-width: 700px;
                      overflow: auto;
                      line-height: 2;
                    }}

                    .otp {{
                      background: linear-gradient(to right, #00bc69 0, #00bc88 50%, #00bca8 100%);
                      margin: 0 auto;
                      width: max-content;
                      padding: 0 10px;
                      color: #fff;
                      border-radius: 4px;
                    }}

                    .footer {{
                      color: #aaa;
                      font-size: 0.8em;
                      line-height: 1;
                      font-weight: 300;
                    }}

                    .email-info {{
                      color: #666666;
                      font-weight: 400;
                      font-size: 13px;
                      line-height: 18px;
                      padding-bottom: 6px;
                    }}

                    .email-info a {{
                      text-decoration: none;
                      color: #00bc69;
                    }}
                  </style>
                </head>

                <body>
                  <div class=""container"">
                    <div class=""header"">
                      <a>Prove Your Identity</a>
                    </div>
                    <br />
                    <strong>Dear {recipientName},</strong>
                    <p>
                      We have received a mail verification request for your {appName} account. For
                      security purposes, please verify your identity by providing the
                      following One-Time Password (OTP).
                      <br />
                      <b>Your One-Time Password (OTP) verification code is:</b>
                    </p>
                    <h2 class=""otp"">{otp}</h2>
                    <p style=""font-size: 0.9em"">
                      <strong>One-Time Password (OTP) is valid for 3 minutes.</strong>
                      <br />
                      <br />
                      If you did not initiate this login request, please disregard this
                      message. Please ensure the confidentiality of your OTP and do not share
                      it with anyone.<br />
                      <strong>Do not forward or give this code to anyone.</strong>
                      <br />
                      <br />
                      <strong>Thank you for using {appName}.</strong>
                      <br />
                      <br />
                      Best regards,
                      <br />
                      <strong>{appName}</strong>
                    </p>

                    <hr style=""border: none; border-top: 0.5px solid #131111"" />
                    <div class=""footer"">
                      <p>This email can't receive replies.</p>
                      <p>
                        For more information about {appName} and your account, visit
                        <strong>{appName}</strong>
                      </p>
                    </div>
                  </div>
                  <div style=""text-align: center"">
                    <div class=""email-info"">
                      <span>
                        This email was sent to
                        <a href=""mailto:{emailAddress}"">{emailAddress}</a>
                      </span>
                    </div>
                    <div class=""email-info"">
                      <a href=""#"" target=""_blank"">{appName}</a> | {address} | {zipCode}, {countryName}
                    </div>
                    <div class=""email-info"">
                      &copy; 2023 {appName}. All rights reserved.
                    </div>
                  </div>
                </body>
                </html>";


            return htmlTemplate;

        }

        #endregion


        #region SendEmail 

        public bool SendEmail(string ReciversEmail, string EmailBody, string Title, string? CC = null, string? BCC = null)
        {

            IConfiguration conf = (new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build());
            string Email = conf["AppConstants:EmailAddress"].ToString();
            string Password = conf["AppConstants:Password"].ToString();

            bool ret = false;
            try
            {

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(Email),
                    Subject = Title,
                    Body = EmailBody,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(ReciversEmail);


                if (!string.IsNullOrEmpty(CC))
                {
                    mailMessage.CC.Add(CC);
                }


                if (!string.IsNullOrEmpty(BCC))
                {
                    mailMessage.Bcc.Add(BCC);
                }


                using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.Credentials = new NetworkCredential(Email, Password);
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMessage);
                    ret = true;
                }


            }
            catch (Exception ex)
            {
                ErrorLog("MailSend", "MailHandler", ex.ToString());
                ret = false;
            }

            return ret;
        }

        #endregion 


    }
}
