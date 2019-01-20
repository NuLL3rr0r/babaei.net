#!/usr/bin/perl

#  (The MIT License)
#
#  Copyright (c) 2007 Mamadou Babaei
#
#  Permission is hereby granted, free of charge, to any person obtaining a copy
#  of this software and associated documentation files (the "Software"), to deal
#  in the Software without restriction, including without limitation the rights
#  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
#  copies of the Software, and to permit persons to whom the Software is
#  furnished to do so, subject to the following conditions:
#
#  The above copyright notice and this permission notice shall be included in
#  all copies or substantial portions of the Software.
#
#  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
#  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
#  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
#  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
#  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
#  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
#  THE SOFTWARE.


use strict;
use CGI ':standard';
use CGI::Carp 'fatalsToBrowser';

my $browser = $ENV{'HTTP_USER_AGENT'};
my $ip = $ENV{'REMOTE_ADDR'};
my $url = "http://" . $ENV{'HTTP_HOST'} . $ENV{'REQUEST_URI'};
my $date = localtime(time);

if ($browser =~ m/Googlebot/) {
    my $to = qq("Admin Name" <adminid\@somedomain.com>);
    my $from =  qq("Your Name" <someid\@somedomain.com>);
    my $subject =  "Googlebot Deteced";
    my $body;
    
    $body .= "<br /><br /><br /><strong>Page Crawled By Googlebot</strong>";
    $body .= "<br /><blockquote>$url</blockquote>";
    $body .= "<br /><br /><strong>By Google Server At</strong>";
    $body .= "<br /><blockquote>$ip</blockquote>";
    $body .= "<br /><br /><strong>Crawl Date</strong>";
    $body .= "<br /><blockquote>$date</blockquote>";
    
    open (MAIL, "|/usr/lib/sendmail -t");

    print MAIL "To: $to\n";
    print MAIL "From: $from\n";
    print MAIL "Subject: $subject\n";
    print MAIL "Content-type: text/html; charset=utf-8\n\n";
    print MAIL "$body";

    close(MAIL);
}

print "Content-Type: text/html; charset=utf-8\n\n";

print "<html>";
print "<head>";
print qq(<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />);
print "<title>Googlebot Listener</title>";
print "</head>";
print "<body>";
print "<h2>Hello Googlebot</h2>";
print qq(<p><strong><em>When googlebot comes (if it does!) to pages that contains this code, you get an e-mail sent to the address you specify.</strong></em></p>
);
print "<br /><br />";
print "<h2>Server Variables</h2>";
print "<blockquote>";

my $result;

$result = qq(<table border="0" cellpadding="10">);

foreach (keys %ENV) {
    $result .= qq(<tr valign="top">);
    $result .= qq(<td>);
    $result .= qq(<strong>$_</strong>:);
    $result .= qq(</td>);
    $result .= qq(<td>);
    $result .= qq(<em>$ENV{$_}</em>);
    $result .= qq(</td>);
    $result .= qq(</tr>);
}

$result .= qq(</table>);

print "$result";

print "</blockquote>";
print "</body>";
print "</html>";

