using System.Collections.Generic;
using System.Threading.Tasks;

namespace WGConfig.NET.Interfaces
{
    public interface IValidator
    {
        bool ValidateConfigFile(string arg, out string arg2);
        bool Validate(string arg, out string arg2);
        Task<bool> ValidateAsync(string arg);
        Task<bool> ValidateConfigFileAsync(string arg);
    }
}