//using Microsoft.ApplicationBlocks.ExceptionManagement;
using System;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CompanyPortal.Manager
{
    public class MailManager : MailMessage
    {

        #region Public Methods
        /// <summary>
        /// Send formatted email
        /// </summary>
        /// <param name="smtpHost">Smtp server name</param>
        /// <param name="name">Requester Name</param>
        /// <param name="url">Consumer Name</param>
        public void SendEmail(string smtpHost, string name, string url)
        {
            string bodyContent = string.Empty;
            try
            {
                SmtpClient smtpClient = new SmtpClient(smtpHost);
                bodyContent = FormatMail(smtpHost, name, url);
                smtpClient.Send(this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Format mail body
        /// Apply template
        /// </summary>
        /// <param name="smtpHost">Smtp server name</param>
        /// <param name="name">Requester Name</param>
        /// <param name="url">Consumer Name</param>
        /// <returns> Formatted string</returns>
        private string FormatMail(string smtpHost, string name, string url)
        {

            string bodyContents = string.Empty;
            string signatureContent = string.Empty;

            Assembly objAssembly = Assembly.GetExecutingAssembly();

            string emailContent;
            AlternateView htmlView;

            using (StreamReader streamReader = new StreamReader(objAssembly.GetManifestResourceStream("CompanyPortal.Templates.Mail.html")))
            {
                bodyContents = streamReader.ReadToEnd();

                emailContent = String.Format(bodyContents, name, url);

                htmlView = AlternateView.CreateAlternateViewFromString(String.Format(bodyContents, name, url), null, "text/html");
            }

            this.AlternateViews.Add(htmlView);
            this.IsBodyHtml = true;
            return emailContent;
        }

        #endregion Private Methods
    }
}