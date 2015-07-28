/// <summary>
///   (The MIT License)
///   
///   Copyright (c) 2007 Mohammad S. Babaei
///   
///   Permission is hereby granted, free of charge, to any person obtaining a copy
///   of this software and associated documentation files (the "Software"), to deal
///   in the Software without restriction, including without limitation the rights
///   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
///   copies of the Software, and to permit persons to whom the Software is
///   furnished to do so, subject to the following conditions:
///   
///   The above copyright notice and this permission notice shall be included in
///   all copies or substantial portions of the Software.
///   
///   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
///   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
///   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
///   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
///   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
///   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
///   THE SOFTWARE.
/// </summary>


using System;
using System.Net.Mail;
using System.Text;

public partial class GooglebotDetector : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string browser = Request.ServerVariables["HTTP_USER_AGENT"];
        string ip = Request.ServerVariables["REMOTE_ADDR"];
        string url = String.Concat("http://", Request.ServerVariables["HTTP_HOST"], Request.ServerVariables["URL"], "?", Request.ServerVariables["QUERY_STRING"]);
        /*
         * if IIS 7.0
         * string url = String.Concat("http://", Request.ServerVariables["HTTP_HOST"], Request.ServerVariables["HTTP_URL"]);
        */
        DateTime dt = DateTime.Now;
        string date = dt.ToString();

        if (browser.Contains("Googlebot"))
        {
            string to = "adminid@somedomain.com";
            string from = "someid@somedomain.com";
            string subject = "Googlebot Deteced";

            StringBuilder body = new StringBuilder();

            body.Append("<br /><br /><br /><strong>Page Crawled By Googlebot</strong>");
            body.Append("<br /><blockquote>{0}</blockquote>");

            body.Append("<br /><br /><strong>By Google Server At</strong>");
            body.Append("<br /><blockquote>{1}</blockquote>");

            body.Append("<br /><br /><strong>Crawl Date</strong>");
            body.Append("<br /><blockquote>{2}</blockquote>");

            string msgBody = String.Format(body.ToString(), url, ip, date);
            
            try
            {
                using (MailMessage msg = new MailMessage(from, to, subject, msgBody))
                {
                    msg.BodyEncoding = Encoding.UTF8;
                    msg.SubjectEncoding = Encoding.UTF8;
                    msg.IsBodyHtml = true;
                    msg.Priority = MailPriority.High;

                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.Credentials = new System.Net.NetworkCredential("yourid@gmail.com", "pw");
                    smtp.EnableSsl = true;
                    smtp.Send(msg);
                }
            }
            catch (FormatException ex)
            {
                // TODO: some statements
            }
            catch (SmtpException ex)
            {
                // TODO: some statements
            }
            catch (Exception ex)
            {
                // TODO: some statements
            }
            finally
            {
                // TODO: some statements
            }
        }
    }
}

