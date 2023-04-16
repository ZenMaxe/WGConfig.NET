using System;
using WGConfig.NET.Interfaces;

namespace WGConfig.NET.Validators
{
    public static class IpAddress
    {
        public static bool Validate(string data)
        {
            string ip = string.Empty;
            

            if (string.IsNullOrEmpty(data)) return false;

            if (data.Contains("/"))
            {
                ip = data.Split('/')[0];
            }
            else if (data.Contains(":"))
            {
                var ipandport = data.Split(':');
                if (!IsPortCorrect(ipandport[1]))
                {
                    return false;
                }

                ip = ipandport[0];
            }
            else
            {
                ip = data;
            }

            return isAddressCorrect(ip);
        }

        public static bool IsPortCorrect(string port) => UInt16.TryParse(port, out var x);
        private static bool isAddressCorrect(string ip)
        {
            string[] ips = ip.Split('.');
            if (ips.Length != 4) return false;
            
            foreach (var i in ips)
            {
                if (!byte.TryParse(i, out var ibyte)) return false;
            }

            return true;

        }
    }
}

