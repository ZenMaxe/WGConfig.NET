using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WGConfig.NET.Interfaces;
using WGConfig.NET.Utils;

namespace WGConfig.NET.Validators
{
    public class Config : IValidator
    {
        public string Error = String.Empty;
        
        
        public bool Validate(string data, out string err)
        {
            err = string.Empty;
            bool checker = Task.Run(() => Checker(data)).Result;
            if (!checker)
            {
                err = Error;
                return false;
            }
         
            return true;
        }

        public bool ValidateConfigFile(string filePath, out string err)
        {
            err = string.Empty;
            
            
            if (!File.Exists(filePath))
            {
                err = "File Not Exist!";
                return false;
            }

            string data = Task.Run(() => FileHelper.FileToString(filePath)).Result;
            bool checker = Task.Run(() => Checker(data)).Result;
            if (!checker)
                return false;
            
            return true;
        }
        public async Task<bool> ValidateAsync(string data)
        {
            bool checker =  await Checker(data);
            if (!checker)
                return false;
            return true;
        }

        public async Task<bool> ValidateConfigFileAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return false;
            }
            
            string data = await FileHelper.FileToString(filePath);
            bool checker = await Checker(data);
            if (!checker)
                return false;
            return true;
        }

        private async Task<bool> Checker(string data)
        {

            if (!(data.Contains("[Interface]") && data.Contains("[Peer]")))
            {
                Error = "Interface/Peer Section is Not Exist in Config!";
                return false;
            }

            var config = new Models.Config();
            using (var datastream = new StringReader(data))
            {
                string line;
                while ((line = await datastream.ReadLineAsync()) != null)
                {
                    var parts = line.Split(new[] { ' ', '=' }, 2, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length != 2)
                    {
                        continue;
                    }
                    switch (parts[0])
                    {
                        case "PrivateKey":
                            config.PrivateKey = parts[1];
                            break;
                        case "Address":
                            if (!IpAddress.Validate(parts[1]))
                            {
                                Error = "Address Is Not Correct!";
                                return false;
                            }
                            config.Address = parts[1];
                            break;
                        case "DNS":
                            config.DNS = parts[1];
                            break;
                        case "MTU":
                            config.MTU = parts[1];
                            break;
                        case "PublicKey":
                            config.PublicKey = parts[1];
                            break;
                        case "PresharedKey":
                            config.PresharedKey = parts[1];
                            break;
                        case "Endpoint":
                            if (!IpAddress.Validate(parts[1]))
                            {
                                Error = "Endpoint Is Not Correct!";
                                return false;
                            }
                            config.Endpoint = parts[1];
                            break;
                        case "AllowedIPs":
                            config.AllowedIPs = parts[1];
                            var ips = parts[1].Split(',');
                            foreach (var i in ips)
                            {
                                if (!IpAddress.Validate(i))
                                {
                                    if (i.Equals(""))
                                        continue;
                                    Error = "IpAddress Is Not Correct!";
                                    return false;
                                }
                                    
                            }
                            
                            break;
                        case "AllowedApps":
                            var apps = parts[1].Split(',');
                            int commaCount = parts[1].Count(c => c == ',');
                            if (apps.Length - 1 != commaCount)
                            {
                                Error = "AllowedApps is Incorrect!";
                                return false;
                            }
                            config.AllowedApps = parts[1];
                            break;
                        case "DisallowedIPs":
                            var Dips = parts[1].Split(',');
                            foreach (var i in Dips)
                            {
                                if (!IpAddress.Validate(i))
                                {
                                    Error = "IpAddress Is Not Correct!";
                                    return false;
                                }
                                    
                            }
                            config.DisallowedIPs = parts[1];
                            break;
                        default:
                            Error = "Config Is Not Correct!";
                            return false;
                            break;
                    }
                }
            }

            return true;
            
        }

        
    }
}