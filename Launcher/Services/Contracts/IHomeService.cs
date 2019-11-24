using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Launcher.Services.Contracts
{
    public interface IHomeService
    {
        IList<string> ExportDirectories { get; }
        Task Download(string url, string destination);
    }
}
