<!--
  (The MIT License)

  Copyright (c) 2007 Mohammad S. Babaei

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


<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.cs" Inherits="GooglebotDetector" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Googlebot Listener</title>
</head>
<body>
<h2>Hello Googlebot</h2>
<p><strong><em>When googlebot comes (if it does!) to pages that contains this code, you get an e-mail sent to the address you specify.</strong></em></p>
<br /><br />
<h2>Server Variables</h2>
<blockquote>
<%
    string[] server = Request.ServerVariables.AllKeys;
    System.Text.StringBuilder str = new StringBuilder();

    str.Append("<table border=\"0\" cellpadding=\"10\">");
    
    foreach (string i in server)
    {
        str.Append("<tr valign=\"top\">");
        str.Append("<td>");
        str.Append("<strong>" + i + "</strong>" + ":");
        str.Append("</td>");
        str.Append("<td>");
        str.Append("<em>" + Request.ServerVariables[i] + "</em>");
        str.Append("</td>");
        str.Append("</tr>");
    }
    
    str.Append("</table>");
    
    Response.Write(str);
%>
</blockquote>
</body>
</html>

