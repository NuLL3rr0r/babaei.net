#!/bin/sh

#  (The MIT License)
#
#  Copyright (c) 2013 M.S. Babaei
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


CLOUDFLARE_IP_RANGES_FILE_PATH="/usr/local/www/_include/cloudflare"
WWW_GROUP="www"
WWW_USER="www"

wget -q -N https://www.cloudflare.com/ips-v4 -O /var/tmp/cloudflare-ips-v4 --no-check-certificate
wget -q -N https://www.cloudflare.com/ips-v6 -O /var/tmp/cloudflare-ips-v6 --no-check-certificate

echo "# CloudFlare IP Ranges" > $CLOUDFLARE_IP_RANGES_FILE_PATH
echo "# Generated at $(date) by $0" >> $CLOUDFLARE_IP_RANGES_FILE_PATH
echo "" >> $CLOUDFLARE_IP_RANGES_FILE_PATH
awk '{ print "set_real_ip_from " $0 ";" }' /var/tmp/cloudflare-ips-v4 >> $CLOUDFLARE_IP_RANGES_FILE_PATH
awk '{ print "set_real_ip_from " $0 ";" }' /var/tmp/cloudflare-ips-v6 >> $CLOUDFLARE_IP_RANGES_FILE_PATH
echo "real_ip_header CF-Connecting-IP;" >> $CLOUDFLARE_IP_RANGES_FILE_PATH
echo "" >> $CLOUDFLARE_IP_RANGES_FILE_PATH

chown $WWW_USER:$WWW_GROUP $CLOUDFLARE_IP_RANGES_FILE_PATH

rm -rf /var/tmp/cloudflare-ips-v4
rm -rf /var/tmp/cloudflare-ips-v6

