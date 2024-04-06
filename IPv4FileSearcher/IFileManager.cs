using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPv4FileSearcher
{
    public interface IFileManager
    {
        public void ReadFile(string path);
        public void WriteFile(string path);
    }
}
