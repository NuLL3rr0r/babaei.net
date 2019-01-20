<!--
  (The MIT License)

  Copyright (c) 2007 Mamadou Babaei

  Permission is hereby granted, free of charge, to any person obtaining a copy
  of this software and associated documentation files (the "Software"), to deal
  in the Software without restriction, including without limitation the rights
  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
  copies of the Software, and to permit persons to whom the Software is
  furnished to do so, subject to the following conditions:

  The above copyright notice and this permission notice shall be included in
  all copies or substantial portions of the Software.

  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
  THE SOFTWARE.
-->


<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="System.Net.Mail" %>
<%@ Import Namespace="System.IO" %>

<script type="text/C#" runat="server">
    private string UidGen(int len)
    {
        Random rnd = new Random();
        String id = string.Empty;
        int min = 0, max = 0;

        for (int i = 0; i < len; i++)
        {
            switch (rnd.Next(3))
            {
                case 0:
                    min = 48;
                    max = 58;
                    break;
                case 1:
                    min = 65;
                    max = 91;
                    break;
                case 2:
                    min = 97;
                    max = 123;
                    break;
                default:
                    break;
            }
            id = String.Concat(id, Convert.ToChar(rnd.Next(min, max)));
        }

        return id;
    }

    private bool SendMail(string to, string from, string subject, string body)
    {
        try
        {
            using (MailMessage msg = new MailMessage(from, to, subject, body))
            {
                msg.BodyEncoding = Encoding.UTF8;
                msg.SubjectEncoding = Encoding.UTF8;
                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.High;
                
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new System.Net.NetworkCredential("some.id@gmail.com", "pw");
                smtp.EnableSsl = true;
                smtp.Send(msg);
            }
        }
        catch (FormatException ex)
        {
            Error(ex.Message);
            return false;
        }
        catch (SmtpException ex)
        {
            Error(ex.Message);
            return false;
        }
        finally
        {
        }

        return true;
    }
    
    private void Error(string err)
    {
        Response.Write(err);
    }
</script>

<%
    string fileName = Request.QueryString["filename"];
    string mail = Request.QueryString["mail"];
    string uid = Request.QueryString["uid"];
        
    string path = Server.MapPath("~");
    path += path.EndsWith("\\") ? string.Empty : "\\";
    string parent = path.Substring(0, path.LastIndexOf("\\", path.Length - 2) + 1);
    string filePath = parent + "files\\" + fileName;
    string regList = parent + "list\\" + "list.txt";
    string regTemp = parent + "list\\" + "temp.txt";
    
    
    if (fileName != string.Empty && fileName != null && mail != string.Empty && mail != null)
    {
        if (File.Exists(filePath))
        {
            try
            {
                using (StreamReader sr = new StreamReader(regList))
                {
                    while (true)
                    {
                        uid = UidGen(30);
                        bool isDup = false;
                        while (sr.Peek() >= 0)
                        {
                            if (sr.ReadLine().IndexOf(uid) > -1)
                            {
                                isDup = true;
                            }
                        }
                        if (!isDup)
                        {
                            break;
                        }
                    }
                }
                
                using (StreamWriter sw = new StreamWriter(regList, true, Encoding.UTF8))
                {
                    sw.WriteLine(uid);
                }

                string link = String.Format("http://www.somedomain.com/securedown.pl?filename={0}&uid={1}", fileName, uid);
                
                if (SendMail(mail, "someid@somedomain.com", "Download Link", "<strong>Download Link:<strong><br/>" + link))
                    Response.Write("<br /><center><h3>Thanks for your registration<br/>The download link sent to your inbox<br/>Just follow it...</h3></center>");
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }
            finally
            {
            }
        }
        else
        {
            Error("File not found: " + fileName);
        }
    }
    else if (fileName != string.Empty && fileName != null && uid != string.Empty && uid != null)
    {
        try
        {
            bool isReg = false;

            string list = string.Empty;
            
            using (StreamReader sr = new StreamReader(regList))
            {
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    if (line.IndexOf(uid) > -1)
                    {
                        line = string.Empty;
                        isReg = true;
                    }
                    else
                        list += (line + " ");
                }
                sr.Close();
            }

            if (isReg)
            {
                if (File.Exists(filePath))
                {
                    int fileSize = (int)new FileInfo(filePath).Length;
                    byte[] buffer = new byte[fileSize];

                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    {
                        fs.Read(buffer, 0, fileSize);
                    }

                    Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                    Response.ContentType = "application/octet-stream";
                    Response.BinaryWrite(buffer);
                    
                    string[] lineList = list.Split(' ');

                    using (StreamWriter sw = new StreamWriter(regTemp, false, Encoding.UTF8))
                    {
                        foreach (string i in lineList)
                        {
                            if (i != string.Empty)
                                sw.WriteLine(i);
                        }
                        sw.Flush();
                        sw.Close();
                    }

                    File.Delete(regList);
                    File.Move(regTemp, regList);

                    Response.End();
                }
                else
                    Error("<br /><center><h3>File not found: " + fileName + "</h3></center>");
            }
            else
                Error("<br /><center><h3>You're not registered</h3></center>");
        }
        catch (Exception ex)
        {
            Error(ex.Message);
        }
        finally
        {
        }
    }
    else
    {
        Response.Write("<br /><center><h3>Error Parsing Parameters</h3></center>");
    }    
%>

