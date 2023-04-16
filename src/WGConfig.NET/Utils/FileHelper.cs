using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WGConfig.NET.Utils
{
    public static class FileHelper
    {
        public async static Task<string> FileToString(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string? data = await sr.ReadToEndAsync();
                if (data != null)
                    return data;
                return "";
            }
            

        }
    }
}