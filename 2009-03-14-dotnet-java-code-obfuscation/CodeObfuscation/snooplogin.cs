/// <summary>
///   (The MIT License)
///   
///   Copyright (c) 2009 M.S. Babaei
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
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Management;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace SnoopSecurity
{
    #region Kernel, Login

    class Login
    {
        #region Variables & Properties

        private static string pangram = "The quick brown fox jumps over the lazy dog.";

        private string _user = string.Empty, _pw = string.Empty, _result = string.Empty;
        
        public string result
        {
            get
            {
                return _result;
            }
        }

        public string user
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
            }
        }

        public string pw
        {
            get
            {
                return _pw;
            }
            set
            {
                _pw = value;
            }
        }

        #endregion

        public Login()
        {
            Random rnd = new Random();

            Console.Write("Establishing secure tunnel");

            for (int i = 0; i < rnd.Next(3, 8); i++)
            {
                for (int j = 0; j < rnd.Next(2, 5); j++)
                    Console.Write(".");
                Thread.Sleep(rnd.Next(250, 950));
            }

            Console.WriteLine(string.Empty);
        }

        public Login(string user, string pw)
        {
            _user = user;
            _pw = pw;
        }

        public bool Verify()
        {
            bool isValid = false;
            string[][] users = { new string[] { "root", "t00r", "{1}Hello, {0}!{1}You're always welcomed...{1}", "" }, new string[] { "NuLL3rr0r", "3rr0R/.", "{1}Hello, {0}!{1}Have a lot of fun...{1}", "" }, new string[] { "guest", "please", "{1}Nope, {0}!{1}There's no room here for you, ask the root...{1}", "banned" } };

            foreach (string[] pair in users)
            {
                if (pair[0].Equals(_user) && pair[1].Equals(_pw))
                {
                    if (pair[3] == "banned")
                    {
                        _result = string.Format(pair[2], "Dear", Environment.NewLine);
                        return false;
                    }

                    _result = string.Format(pair[2], _user, Environment.NewLine);
                    isValid = true;
                    break;
                }
            }

            if (!isValid)
                _result = "Permission denied.";

            Core.Initialize(1500);

            return isValid;
        }
    }

    #endregion


    #region Kernel, Core

    class Core
    {
        private static string[] cmdList = {
                                              "?",
                                              "clear",
                                              "logout",
                                              "set_process_priority",
                                              "run_external_app",
                                              "sysinfo_brief",
                                              "sysinfo_cpu",
                                              "sysinfo_os",
                                              "sysinfo_netdevice",
                                              "sysinfo_vistaranks",
                                              "download_url",
                                              "encrypt_text",
                                              "decrypt_text",
                                              "encrypt_file",
                                              "decrypt_file",
                                              "compress_file",
                                              "decompress_file",
                                              "repair_access_db",
                                              "img_convert_resize",
                                              "img_watermark"
                                          };

        public static void Initialize(int ms)
        {
            Console.WriteLine("Initializing...");
            Thread.Sleep(ms);
        }

        private void RunCommand(string cmd)
        {
            switch (cmd)
            {
                case "?":
                    foreach (string c in cmdList)
                        Console.WriteLine(c);
                    break;
                case "clear":
                    Console.Clear();
                    break;
                case "logout":
                    break;
                case "set_process_priority":
                    Console.WriteLine("0:Idle  1:BelowNormal  2:Normal  3:AboveNormal  4:High  5:RealTime");
                    string level = Console.ReadLine();
                    Console.WriteLine(OSTools.SetPriority(level));
                    break;
                case "run_external_app":
                    Console.Write("Path to executable: ");
                    string app = Console.ReadLine();
                    Console.Write("Arguments [Return:no arguments]: ");
                    string args = Console.ReadLine();
                    Console.WriteLine(OSTools.ExternalCommand(app, args));
                    break;
                case "sysinfo_brief":
                    Console.WriteLine(SysInfo.Brief());
                    break;
                case "sysinfo_cpu":
                    Console.WriteLine(SysInfo.CPU());
                    break;
                case "sysinfo_os":
                    Console.WriteLine(SysInfo.OS());
                    break;
                case "sysinfo_netdevice":
                    Console.WriteLine(SysInfo.NetDevice());
                    break;
                case "sysinfo_vistaranks":
                    Console.WriteLine(SysInfo.VistaRanks());
                    break;
                case "download_url":
                    Console.Write("URL: ");
                    string url = Console.ReadLine();
                    Console.Write("Save path: ");
                    string dwndPath = Console.ReadLine();
                    Console.Write("{PROXY_ADDR}:{PROXY_PORT} [Return::IE proxy settings]: ");
                    string proxy = Console.ReadLine();
                    Console.WriteLine(WebUtils.DownloadURL(url, dwndPath, proxy));
                    break;
                case "encrypt_text":
                    Console.Write("Text: ");
                    string encText = Console.ReadLine();
                    Console.Write("Key: ");
                    string encKey = Console.ReadLine();
                    Console.WriteLine(EncDec.Encrypt(encText, encKey));
                    break;
                case "decrypt_text":
                    Console.Write("Text: ");
                    string decText = Console.ReadLine();
                    Console.Write("Key: ");
                    string decKey = Console.ReadLine();
                    Console.WriteLine(EncDec.Decrypt(decText, decKey));
                    break;
                case "encrypt_file":
                    Console.Write("Source: ");
                    string encSource = Console.ReadLine();
                    Console.Write("Target: ");
                    string encTarget = Console.ReadLine();
                    Console.Write("Key: ");
                    string encFileKey = Console.ReadLine();
                    Console.WriteLine(EncDec.Encrypt(encSource, encTarget, encFileKey));
                    break;
                case "decrypt_file":
                    Console.Write("Source: ");
                    string decSource = Console.ReadLine();
                    Console.Write("Target: ");
                    string decTarget = Console.ReadLine();
                    Console.Write("Key: ");
                    string decFileKey = Console.ReadLine();
                    Console.WriteLine(EncDec.Decrypt(decSource, decTarget, decFileKey));
                    break;
                case "compress_file":
                    Console.Write("Source: ");
                    string cmpSource = Console.ReadLine();
                    Console.Write("Target: ");
                    string cmpTarget = Console.ReadLine();
                    Console.Write("Mode(fast/solid): ");
                    string cmpMode = Console.ReadLine();
                    Console.WriteLine(Zipper.Compress(cmpSource, cmpTarget, cmpMode));
                    break;
                case "decompress_file":
                    Console.Write("Source: ");
                    string dcmpSource = Console.ReadLine();
                    Console.Write("Target: ");
                    string dcmpTarget = Console.ReadLine();
                    Console.Write("Mode(fast/solid): ");
                    string dcmpMode = Console.ReadLine();
                    Console.WriteLine(Zipper.Decompress(dcmpSource, dcmpTarget, dcmpMode));
                    break;
                case "repair_access_db":
                    Console.Write(".mdb File: ");
                    string dbSource = Console.ReadLine();
                    Console.Write(".mdb Pw [Return::blank Pw]: ");
                    string dbPw = Console.ReadLine();
                    Console.WriteLine(DBTools.CompactRepairJetDB(dbSource, dbPw));
                    break;
                case "img_convert_resize":
                    Console.Write("Source Image: ");
                    string imgSource = Console.ReadLine();
                    Console.Write("Target Image: ");
                    string imgTarget = Console.ReadLine();
                    Console.Write("{WIDTH}{px|%} [Return::Original Width]: ");
                    string imgW = Console.ReadLine();
                    Console.Write("{HEIGHT}{px|%} [Return::Original Height]: ");
                    string imgH = Console.ReadLine();
                    Console.WriteLine(ImageMan.GenThumb(imgSource, imgTarget, imgW, imgH));
                    break;
                case "img_watermark":
                    Console.Write("Source Image: ");
                    string wmSource = Console.ReadLine();
                    Console.Write("Target Image: ");
                    string wmTarget = Console.ReadLine();
                    Console.Write("Watermark Text: ");
                    string wmText = Console.ReadLine();
                    Console.WriteLine(ImageMan.GenWatermark(wmSource, wmTarget, wmText));
                    break;
                default:
                    Console.WriteLine("Unknown or bad command...");
                    break;
            }

            if (cmd != "clear")
                Console.WriteLine(string.Empty);
        }

        public void GoShell()
        {
            Console.WriteLine("Type ? for help...{0}", Environment.NewLine);

            string cmd = string.Empty;
            do
            {
                Console.Write("# ");
                cmd = Console.ReadLine();

                RunCommand(cmd);
            } while (!cmd.Equals("logout"));
        }
    }

    #endregion


    #region Kernel, Boot

    static class Boot
    {
        static void Main(string[] args)
        {
            Login login;
            bool loggedIn = false;
            
            try
            {
                switch (args[0])
                {
                    case "--secure":
                        login = new Login();
                        login.user = args[1];
                        login.pw = args[2];
                        loggedIn = login.Verify();
                        Console.WriteLine(login.result);
                        break;
                    case "--fast":
                        login = new Login(args[1], args[2]);
                        loggedIn = login.Verify();
                        Console.WriteLine(login.result);
                        break;
                    case "--guest":
                        login = new Login("guest", "please");
                        loggedIn = login.Verify();
                        Console.WriteLine(login.result);
                        break;
                    case "--f0rc3r00t":
                    case "--mag1cd00r":
                        login = new Login("root", "t00r");
                        loggedIn = login.Verify();
                        Console.WriteLine(login.result);
                        break;
                    default:
                        Console.WriteLine("{0}Invalid options detected!{0}", Environment.NewLine);
                        break;
                }

                if (loggedIn)
                {
                    Core core = new Core();
                    core.GoShell();
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder(string.Empty);
                sb.Append(Environment.NewLine);
                sb.Append("NuLL3rr0r! => Hang! => B00M! => B1GBANG! => [ Hello, World! ]");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append(string.Format("Kernel panic - {0}", ex.Message));
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append("    Usage :   snooplogin.exe option[--secure|--fast] user pw");
                sb.Append(Environment.NewLine);
                sb.Append("              snooplogin.exe --guest");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                Console.WriteLine(sb.ToString());
            }
            finally
            {
                Console.WriteLine(
                                    Environment.NewLine +
                                    "Snoop(R) GNU/FakeOS(TM) - (C)2009 Snoop-Security.com" +
                                    Environment.NewLine +
                                    Environment.NewLine +
                                    "You know? It's not really an Operating System;" +
                                    Environment.NewLine +
                                    "But it's an Operating Environment!!!" +
                                    " :D"
                                 );
            }
        }
    }

    #endregion


    #region Get SysInfo

    class SysInfo
    {
        public static string[] GetRawInfo(string query, string[] items)
        {
            string[] info = new string[items.Length];

            for (int i = 0; i < info.Length; i++)
                info[i] = "Not Available...";

            try
            {
                ManagementScope ms;
                ObjectQuery oq;
                ManagementObjectSearcher mos;
                ManagementObjectCollection moc;

                if (!query.Contains("NetworkAdapterConfiguration"))
                    mos = new ManagementObjectSearcher(query);
                else
                {
                    ms = new ManagementScope(@"\\localhost\root\cimv2");
                    oq = new ObjectQuery(query);
                    mos = new ManagementObjectSearcher(ms, oq);
                }

                moc = mos.Get();

                if (!query.Contains("NetworkAdapterConfiguration"))
                    foreach (ManagementBaseObject mbo in moc)
                    {
                        for (int i = 0; i < items.Length; i++)
                        {
                            try
                            {
                                info[i] = mbo[items[i]].ToString();
                            }
                            catch
                            {
                            }
                            finally
                            {
                            }
                        }
                        break;
                    }
                else
                    foreach (ManagementObject mo in moc)
                    {
                        for (int i = 0; i < items.Length; i++)
                        {
                            try
                            {
                                if (items[i] != "IPAddress" && items[i] != "DefaultIPGateway" && items[i] != "DNSServerSearchOrder")
                                    info[i] = mo[items[i]].ToString();
                                else
                                    info[i] = string.Join(", ", (string[])(mo[items[i]]));
                            }
                            catch
                            {
                            }
                            finally
                            {
                            }
                        }
                        break;
                    }
            }
            catch
            {
            }
            finally
            {
            }

            return info;
        }

        private static string GetLayoutSpace(int len, int baseSpaceNum)
        {
            string space = string.Empty;

            for (int i = 0; i < baseSpaceNum - len; i++)
                space += " ";

            return space;
        }

        private static string FormatLayout(string title, string[] items, string[] values)
        {
            string info = string.Format("{1}[{0}]", title, Environment.NewLine);

            int maxLen = 0;

            foreach (string s in items)
                if (maxLen < s.Length)
                    maxLen = s.Length;

            maxLen += 1;

            for (int i = 0; i < items.Length; i++)
                info += string.Format("{2}{0}{3}:  {1}", items[i], values[i], Environment.NewLine, GetLayoutSpace(items[i].Length, maxLen));

            return info;
        }

        public static string CPU()
        {
            string[] items = new string[] { "Name", "NumberOfLogicalProcessors", "NumberOfCores", "MaxClockSpeed" };
            string[] values = GetRawInfo("SELECT * FROM Win32_Processor", items);

            values[3] += " MHz";

            return FormatLayout("Processor", items, values);
        }

        public static string OS()
        {
            string[] items = new string[] { "Caption", "Version", "CSDVersion", "OSArchitecture", "FreePhysicalMemory" };
            string[] values = GetRawInfo("SELECT * FROM Win32_OperatingSystem", items);

            values[4] = String.Format("{0} MB", (Convert.ToUInt64(values[4]) / 1024).ToString());

            return FormatLayout("Operating System", items, values);
        }

        public static string NetDevice()
        {
            string[] items = new string[] { "Description", "DNSHostName", "IPAddress", "DefaultIPGateway", "DNSServerSearchOrder", "MACAddress" };
            string[] values = GetRawInfo("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 'TRUE'", items);

            return FormatLayout("Network Adapter Configuration", items, values);
        }

        public static string VistaRanks()
        {
            string[] items = new string[] { "CPUScore", "MemoryScore", "GraphicsScore", "D3DScore", "DiskScore" };
            string[] values = GetRawInfo("SELECT * FROM Win32_WinSAT", items);

            return FormatLayout("Windows Experience Index", items, values);
        }

        public static string Brief()
        {
            StringBuilder sb = new StringBuilder(string.Empty);

            sb.Append(Environment.NewLine);
            sb.Append(string.Format("Up-Time           : {0}min", (Int32)((Environment.TickCount / 1000) / 60)));
            sb.Append(Environment.NewLine);
            sb.Append(string.Format("OS Version        : {0}", Environment.OSVersion));
            sb.Append(Environment.NewLine);
            sb.Append(string.Format("Framework Version : {0}", Environment.Version));
            sb.Append(Environment.NewLine);
            sb.Append(string.Format("Time Zone         : {0}", TimeZone.CurrentTimeZone.IsDaylightSavingTime(DateTime.Now) ? TimeZone.CurrentTimeZone.DaylightName : TimeZone.CurrentTimeZone.StandardName));

            return sb.ToString();
        }
    }

    #endregion


    #region Encryption / Decryption

    public class EncDec
    {
        /// <summary>
        /// Based on code at:
        /// http://www.codeproject.com/KB/security/DotNetCrypto.aspx
        /// </summary>

        public static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
        {
            try
            {
                MemoryStream ms = new MemoryStream();

                Rijndael alg = Rijndael.Create();

                alg.Key = Key;
                alg.IV = IV;

                CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);

                cs.Write(clearData, 0, clearData.Length);

                cs.Close();

                byte[] encryptedData = ms.ToArray();

                return encryptedData;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return (new byte[] { });
            }
            finally
            {
            }
        }

        public static string Encrypt(string clearText, string Password)
        {
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

            byte[] encryptedData = Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));

            return Convert.ToBase64String(encryptedData);
        }

        public static byte[] Encrypt(byte[] clearData, string Password)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

            return Encrypt(clearData, pdb.GetBytes(32), pdb.GetBytes(16));
        }

        public static string Encrypt(string fileIn, string fileOut, string Password)
        {
            try
            {
                FileStream fsIn = new FileStream(fileIn, FileMode.Open, FileAccess.Read);
                FileStream fsOut = new FileStream(fileOut, FileMode.OpenOrCreate, FileAccess.Write);

                PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

                Rijndael alg = Rijndael.Create();
                alg.Key = pdb.GetBytes(32);
                alg.IV = pdb.GetBytes(16);

                CryptoStream cs = new CryptoStream(fsOut, alg.CreateEncryptor(), CryptoStreamMode.Write);

                int bufferLen = 4096;
                byte[] buffer = new byte[bufferLen];
                int bytesRead;

                do
                {
                    bytesRead = fsIn.Read(buffer, 0, bufferLen);

                    cs.Write(buffer, 0, bytesRead);
                } while (bytesRead != 0);

                cs.Close();
                fsIn.Close();

                return "Encrypted!";
            }
            catch (FileNotFoundException ex)
            {
                return ex.Message;
            }
            catch (IOException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
            }
        }

        public static byte[] Decrypt(byte[] cipherData, byte[] Key, byte[] IV)
        {
            try
            {
                MemoryStream ms = new MemoryStream();

                Rijndael alg = Rijndael.Create();

                alg.Key = Key;
                alg.IV = IV;

                CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);

                cs.Write(cipherData, 0, cipherData.Length);

                cs.Close();

                byte[] decryptedData = ms.ToArray();

                return decryptedData;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return (new byte[] { });
            }
            finally
            {
            }
        }

        public static string Decrypt(string cipherText, string Password)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

            byte[] decryptedData = Decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));

            return System.Text.Encoding.Unicode.GetString(decryptedData);
        }

        public static byte[] Decrypt(byte[] cipherData, string Password)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

            return Decrypt(cipherData, pdb.GetBytes(32), pdb.GetBytes(16));
        }

        public static string Decrypt(string fileIn, string fileOut, string Password)
        {
            try
            {
                FileStream fsIn = new FileStream(fileIn, FileMode.Open, FileAccess.Read);
                FileStream fsOut = new FileStream(fileOut, FileMode.OpenOrCreate, FileAccess.Write);

                PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                Rijndael alg = Rijndael.Create();

                alg.Key = pdb.GetBytes(32);
                alg.IV = pdb.GetBytes(16);

                CryptoStream cs = new CryptoStream(fsOut, alg.CreateDecryptor(), CryptoStreamMode.Write);

                int bufferLen = 4096;
                byte[] buffer = new byte[bufferLen];
                int bytesRead;

                do
                {
                    bytesRead = fsIn.Read(buffer, 0, bufferLen);

                    cs.Write(buffer, 0, bytesRead);

                } while (bytesRead != 0);

                cs.Close();
                fsIn.Close();

                return "Decrypted!";
            }
            catch (FileNotFoundException ex)
            {
                return ex.Message;
            }
            catch (IOException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            finally
            {
            }
        }
    }

    #endregion


    #region Compresssion / Decompression

    public class Zipper
    {
        public static byte[] Compress(string data, string mode)
        {
            return Compress(System.Text.Encoding.Unicode.GetBytes(data), mode);
        }

        public static string DecompressToString(byte[] data, string mode)
        {
            return System.Text.Encoding.Unicode.GetString(Decompress(data, mode));
        }

        public static byte[] Compress(byte[] data, string mode)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                Stream s;

                switch (mode)
                {
                    case "solid":
                        s = new DeflateStream(ms, CompressionMode.Compress);
                        break;
                    case "fast":
                        s = new GZipStream(ms, CompressionMode.Compress);
                        break;
                    default:
                        s = new GZipStream(ms, CompressionMode.Compress);
                        break;
                }

                s.Write(data, 0, data.Length);
                s.Close();

                return ms.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static byte[] Decompress(byte[] data, string mode)
        {
            try
            {
                string result = string.Empty;
                byte[] buffer = { };

                MemoryStream ms = new MemoryStream(data);
                Stream s;

                switch (mode)
                {
                    case "solid":
                        s = new DeflateStream(ms, CompressionMode.Decompress);
                        break;
                    case "fast":
                        s = new GZipStream(ms, CompressionMode.Decompress);
                        break;
                    default:
                        s = new GZipStream(ms, CompressionMode.Decompress);
                        break;
                }

                int len = 4096;

                while (true)
                {
                    int oldLen = buffer.Length;
                    Array.Resize(ref buffer, oldLen + len);
                    int size = s.Read(buffer, oldLen, len);
                    if (size != len)
                    {
                        Array.Resize(ref buffer, buffer.Length - (len - size));
                        break;
                    }
                    if (size <= 0)
                        break;
                }
                s.Close();

                return buffer;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static string Compress(string fileIn, string fileOut, string mode)
        {
            if (BasicIO.WriteStreamToFile(fileOut, Compress(BasicIO.ReadStreamFromFile(fileIn), mode)))
            {
                int sSize = BasicIO.GetFileSize(fileIn);
                int tSize = BasicIO.GetFileSize(fileOut);
                float ratio = ((sSize - tSize) / (float)sSize) * 100;

                return string.Format("Compressed Successfully! Ratio: {0}%", ratio.ToString().Substring(0, ratio.ToString().LastIndexOf(".") + 3));
            }
            else
                return "Operation Interruppted!";
        }

        public static string Decompress(string fileIn, string fileOut, string mode)
        {
            if (BasicIO.WriteStreamToFile(fileOut, Decompress(BasicIO.ReadStreamFromFile(fileIn), mode)))
                return "UnCompressed Successfully!";
            else
                return "Operation Interruppted!";
        }
    }

    #endregion


    #region Low-Level OS Operations and Tools

    public class OSTools
    {
        public static string SetPriority(string level)
        {
            string priority = string.Empty;

            Process p = Process.GetCurrentProcess();

            switch (level)
            {
                case "0":
                    p.PriorityClass = ProcessPriorityClass.Idle;
                    priority = "Idle";
                    break;
                case "1":
                    p.PriorityClass = ProcessPriorityClass.BelowNormal;
                    priority = "BelowNormal";
                    break;
                case "2":
                    p.PriorityClass = ProcessPriorityClass.Normal;
                    priority = "Normal";
                    break;
                case "3":
                    p.PriorityClass = ProcessPriorityClass.AboveNormal;
                    priority = "AboveNormal";
                    break;
                case "4":
                    p.PriorityClass = ProcessPriorityClass.High;
                    priority = "High";
                    break;
                case "5":
                    p.PriorityClass = ProcessPriorityClass.RealTime;
                    priority = "RealTime";
                    break;
                default:
                    p.PriorityClass = ProcessPriorityClass.Normal;
                    priority = "Normal";
                    break;
            }

            return string.Format("Process priority set to {0}. Just look at the Task Manager.", priority);
        }

        public static string ExternalCommand(string app, string args)
        {
            try
            {
                Process p = new Process();
                p.StartInfo.Arguments = args;
                p.StartInfo.FileName = app;
                p.Start();
                Console.WriteLine("Close the external app to comeback here....");
                p.WaitForExit();
            }
            catch (ObjectDisposedException ex)
            {
                return ex.Message;
            }
            catch (InvalidOperationException ex)
            {
                return ex.Message;
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                return ex.Message;
            }
            catch (SystemException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
            }

            return "done!";
        }
    }

    #endregion


    #region Web Tools and Utilities

    public class WebUtils
    {
        public static string DownloadURL(string url, string target, string proxy)
        {
            try
            {
                Console.WriteLine("Connecting...");

                if (proxy != string.Empty)
                {
                    Uri proxyURI = new System.Uri(String.Format("http://{0}/", proxy));
                    WebRequest.DefaultWebProxy = new WebProxy(proxyURI);
                }

                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(url, target);
                    client.Dispose();

                    WebRequest.DefaultWebProxy = null;
                }

                return String.Format("Successfully saved to: {0}", target);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
            }
        }
    }

    #endregion


    #region Image Manipulation

    class ImageMan
    {
        private static ImageFormat GetImageFormat(string ext)
        {
            ImageFormat format;

            switch (ext)
            {
                case ".png":
                    format = ImageFormat.Png;
                    break;
                case ".jpg":
                    format = ImageFormat.Jpeg;
                    break;
                case ".jpeg":
                    format = ImageFormat.Jpeg;
                    break;
                case ".jpe":
                    format = ImageFormat.Jpeg;
                    break;
                case ".gif":
                    format = ImageFormat.Gif;
                    break;
                case ".tif":
                    format = ImageFormat.Tiff;
                    break;
                case ".tiff":
                    format = ImageFormat.Tiff;
                    break;
                case ".bmp":
                    format = ImageFormat.Bmp;
                    break;
                case ".dib":
                    format = ImageFormat.Bmp;
                    break;
                case ".rle":
                    format = ImageFormat.Bmp;
                    break;
                case ".ico":
                    format = ImageFormat.Icon;
                    break;
                case ".wmf":
                    format = ImageFormat.Wmf;
                    break;
                case ".emf":
                    format = ImageFormat.Emf;
                    break;
                default:
                    format = ImageFormat.Jpeg;
                    break;
            }

            return format;
        }

        private static int GetImageSize(string size, int orgSize)
        {
            try
            {
                if (size.Trim() == string.Empty)
                    return orgSize;

                int res;

                if (size.EndsWith("px"))
                    res = Convert.ToInt32(size.Substring(0, size.Length - 2));
                else if (size.EndsWith("%"))
                    res = (orgSize * Convert.ToInt32(size.Substring(0, size.Length - 1))) / 100;
                else
                    return orgSize;

                if (res < 1)
                    return orgSize;

                return res;
            }
            catch
            {
                return orgSize;
            }
            finally
            {
            }
        }

        public static byte[] GenThumb(byte[] buffer, ImageFormat format, string width, string height)
        {
            try
            {
                MemoryStream msOriginal = new MemoryStream(buffer);
                Image imgOriginal = new Bitmap(msOriginal);

                int w = GetImageSize(width, imgOriginal.Width);
                int h = GetImageSize(height, imgOriginal.Height);

                Image imgConverted = imgOriginal.GetThumbnailImage(w, h, null, new IntPtr());
                MemoryStream msConverted = new MemoryStream();

                imgConverted.Save(msConverted, format);

                buffer = msConverted.ToArray();

                msOriginal.Dispose();
                msConverted.Dispose();
                imgOriginal.Dispose();
                imgConverted.Dispose();

                msOriginal = null;
                msConverted = null;
                imgOriginal = null;
                imgConverted = null;

                return buffer;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
            }
        }

        public static byte[] GenWatermark(byte[] buffer, ImageFormat format, string copyright)
        {
            /// <summary>
            /// Based on code at:
            /// http://www.codeproject.com/KB/GDI-plus/watermark.aspx
            /// </summary>

            try
            {
                MemoryStream pMS = new MemoryStream(buffer);
                System.Drawing.Image imgPhoto = new System.Drawing.Bitmap(pMS);

                int phWidth = imgPhoto.Width;
                int phHeight = imgPhoto.Height;

                Bitmap bmPhoto = new Bitmap(phWidth, phHeight, imgPhoto.PixelFormat);

                bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

                Graphics grPhoto = Graphics.FromImage(bmPhoto);

                grPhoto.SmoothingMode = SmoothingMode.AntiAlias;

                grPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, phWidth, phHeight), 0, 0, phWidth, phHeight, GraphicsUnit.Pixel);

                int[] sizes = new int[] { 16, 14, 12, 10, 8, 6, 4 };

                Font crFont = null;
                SizeF crSize = new SizeF();

                for (int i = 0; i < 7; i++)
                {
                    crFont = new Font("Verdana", sizes[i], FontStyle.Bold);
                    crSize = grPhoto.MeasureString(copyright, crFont);

                    if ((ushort)crSize.Width < (ushort)phWidth)
                        break;
                }

                int yPixlesFromBottom = (int)(phHeight * .05);

                float yPosFromBottom = ((phHeight - yPixlesFromBottom) - (crSize.Height / 2));

                float xCenterOfImg = (phWidth / 2);

                StringFormat StrFormat = new StringFormat();
                StrFormat.Alignment = StringAlignment.Center;

                SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(153, 0, 0, 0));

                grPhoto.DrawString(copyright, crFont, semiTransBrush2, new PointF(xCenterOfImg + 1, yPosFromBottom + 1), StrFormat);

                SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));

                grPhoto.DrawString(copyright, crFont, semiTransBrush, new PointF(xCenterOfImg, yPosFromBottom), StrFormat);

                Bitmap bmWatermark = new Bitmap(bmPhoto);
                bmWatermark.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

                imgPhoto = bmWatermark;

                grPhoto.Dispose();

                MemoryStream wMS = new MemoryStream();
                imgPhoto.Save(wMS, format);

                imgPhoto.Dispose();

                Array.Resize(ref buffer, 0);

                buffer = wMS.ToArray();

                return buffer;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
            }
        }

        public static string GenThumb(string fileIn, string fileOut, string width, string height)
        {
            if (BasicIO.WriteStreamToFile(fileOut, GenThumb(BasicIO.ReadStreamFromFile(fileIn), GetImageFormat(BasicIO.ExtractExt(fileOut)), width, height)))
                return "Manipulated Successfully";
            else
                return "Operation Interruppted!";
        }

        public static string GenWatermark(string fileIn, string fileOut, string copyright)
        {
            if (BasicIO.WriteStreamToFile(fileOut, GenWatermark(BasicIO.ReadStreamFromFile(fileIn), GetImageFormat(BasicIO.ExtractExt(fileOut)), copyright)))
                return "Watermarked Successfully!";
            else
                return "Operation Interruppted!";
        }
    }    
    
    #endregion


    #region DB Tools

    class DBTools
    {
        private static string GetTempPath()
        {
            string path = System.IO.Path.GetTempPath();
            path += path.EndsWith(Path.DirectorySeparatorChar.ToString()) ? string.Empty : Path.DirectorySeparatorChar.ToString();
            return path;
        }

        public static string CompactRepairJetDB(string mdwFilePath, string dbPw)
        {
            /// <summary>
            /// Based on code at:
            /// http://www.codeproject.com/KB/database/mdbcompact_latebind.aspx
            /// http://www.codeproject.com/KB/database/CompactAndRepair.aspx
            /// </summary>
            
            try
            {
                string tmpFile = GetTempPath() + @"tempdb.mdb";
                string cnnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Jet OLEDB:Database Password={1};Jet OLEDB:Engine Type=5";

                string connectionString = string.Format(cnnStr, mdwFilePath, dbPw);
                string connectionStringTemp = string.Format(cnnStr, tmpFile, dbPw);

                object[] oParams;
                object objJRO = Activator.CreateInstance(Type.GetTypeFromProgID("JRO.JetEngine"));
                oParams = new object[] { connectionString, connectionStringTemp };
                objJRO.GetType().InvokeMember("CompactDatabase", System.Reflection.BindingFlags.InvokeMethod, null, objJRO, oParams);

                System.IO.File.Delete(mdwFilePath);
                System.IO.File.Move(tmpFile, mdwFilePath);

                System.Runtime.InteropServices.Marshal.ReleaseComObject(objJRO);
                objJRO = null;

                return "done!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
            }
        }
    }

    #endregion


    #region Basic I/O

    class BasicIO
    {
        public static string ExtractExt(string file)
        {
            return file.Contains(".") ? file.Substring(file.LastIndexOf(".")) : string.Empty;
        }

        public static int GetFileSize(string file)
        {
            try
            {
                return (int)new FileInfo(file).Length;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
            finally
            {
            }
        }

        public static byte[] ReadStreamFromFile(string file)
        {
            try
            {
                int len = GetFileSize(file);
                byte[] data = new byte[len];

                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    fs.Read(data, 0, len);
                    fs.Close();
                }

                return data;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
            }
        }

        public static bool WriteStreamToFile(string file, byte[] data)
        {
            try
            {
                using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(data, 0, data.Length);
                    fs.Close();
                }

                return true;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
            }
        }
    }
    
    #endregion
}