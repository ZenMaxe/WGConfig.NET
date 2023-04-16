namespace WGConfig.NET.Models
{
    public class Config
    {
        public string PrivateKey { get; set; }
        public string Address { get; set; }
        public string DNS { get; set; } = "";
        public string MTU { get; set; } = "";
        public string PublicKey { get; set; }
        public string PresharedKey { get; set; }
        public string Endpoint { get; set; }
        public string AllowedIPs { get; set; } = "";
        public string AllowedApps { get; set; } = "";
        public string DisallowedIPs { get; set; } = "";
    }
}