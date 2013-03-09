#!/usr/bin/perl

#  (The MIT License)
#
#  Copyright (c) 2007 M.S. Babaei
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

use lib "../cgi-lib";
use String;
use String::Random;

my $fileName = param('filename');
my $mail = param('mail');
my $uid = param('uid');

my $filePath = "../files/" . $fileName;
my $regList = "../list/list.txt";

my $httpHeader = "Content-Type: text/html; charset=utf-8\n\n";


if (FoundXSS($fileName)) {
    print $httpHeader;
    print qq(<br /><center><h3>Alice is not in Wonderland.</h3></center>);
    exit;
}

if ($fileName ne '' and $mail ne '') {
    print $httpHeader;

    if (-e $filePath) {
        open (REG, "<:utf8", $regList) || Error ("register", "your mail address");
        flock (REG, 1);
        my @lines = <REG>;
        close(REG);

        my $list = join("", @lines);

        while (1) {
            $uid = UidGen(30);
            my $isDup = $list =~ m/$uid/;
            if ($isDup == 0) {
                last;
            }
        }
                
        open (REG, ">>:utf8", $regList) || Error ("register", "your mail address");
        flock (REG, 2);
        print REG qq($uid\n);
        close (REG);    
    
        my $link = qq(http://www.somedomain.com/securedown.pl?filename=$fileName&uid=$uid);
    
        SendMail($mail, qq("Your Name" <someid\@somedomain.com>), "Download Link", qq(<strong>Download Link:<strong><br/>$link));
        print qq(<br /><center><h3>Thanks for your registration<br/>The download link sent to your inbox<br/>Just follow it...</h3></center>);
        exit;
     }
     else {
        Error('find', $fileName);
     }
}
elsif ($fileName ne '' and $uid ne '') {
    open (REG, "<:utf8", $regList) || ErrorH ("find", "the registeration information");
    flock (REG, 1);
    my @lines = <REG>;
    close(REG);
    
    my $isReg = 0;
    
    foreach (@lines) {
        if ($_ =~ m/$uid/) {
            $isReg = 1;
            last;
        }
    }
    
    if (!$isReg) {
        print $httpHeader;
        print qq(<br /><center><h3>You're not registered</h3></center>);
        exit;
    }

    if (-e $filePath) {
        open (FILE, $filePath) || ErrorH ("open", "$fileName");
        flock (FILE, 1);

        binmode(FILE);
        binmode(STDOUT);

        my $cgi = new CGI;
        print $cgi->header( -type => 'application/octet-stream', -attachment => qq($fileName) );

        my $buffer;
    
        while (read(FILE, $buffer, 100 * 2**10)) {
            print STDOUT $buffer;
        }
        
        close(FILE);

        open (REG, ">:utf8", $regList);
        flock (REG, 2);
        foreach(@lines) {
            $_ =~ s/$uid\n//;
            print REG $_;
        }
        close (REG);
    }
    else {
        ErrorH ('find', $fileName);
    }
}
else {
    print $httpHeader;
    print qq(<br /><center><h3>Error Parsing Parameters</h3></center>);
}

sub SendMail {
    my $to = $_[0];
    my $from =  $_[1];
    my $subject =  $_[2];
    my $body =  $_[3];
    
    open (MAIL, "|/usr/lib/sendmail -t") || Error('open', 'mail program');

    print MAIL "To: $to\n";
    print MAIL "From: $from\n";
    print MAIL "Subject: $subject\n";
    print MAIL "Content-type: text/html; charset=utf-8\n\n";
    print MAIL "$body";

    close(MAIL);
        
    return 1;
}

sub UidGen {
    my $pattern;
    
    for (my $i = 0; $i < $_[0]; $i++) {
        $pattern .= "s";
    }
    
    my $rnd = new String::Random;
    my $key = $rnd->randpattern($pattern);
    return $key;
}

sub FoundXSS {
    foreach (@_) {
        if ($_ =~ /[<>]/) {
            return 1;
        }
    }
    
    return 0;
}

sub Error {
    print qq(<br /><center><h3>The server cant $_[0] the $_[1]: $!\n</h3></center>);
    exit;
}

sub ErrorH {
    print $httpHeader;
    print qq(<br /><center><h3>The server can't $_[0] the $_[1]: $!\n</h3></center>);
    exit;
}

