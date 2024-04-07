using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPv4FileSearcher
{
    public class Options
    {
        private string[] accessLog;
        private string outputPath;
        private string addressStart;
        private string addressMask;
        private string timeStart;
        private string timeEnd;

        public string[] AccessLog { get { return accessLog; } set { accessLog = value; } }
        public string OutputPath { get { return outputPath; } set { outputPath = value; } }
        public string AddressStart { get { return addressStart; } set { addressStart = value; } }
        public string AddressMask { get {  return addressMask; } set { addressMask = value; } }
        public string TimeStart { get { return timeStart; } set { timeStart = value; } }
        public string TimeEnd { get {  return timeEnd; } set {  timeEnd = value; } }

        public Options(Dictionary<string, string> parametrs, IFileManager fileManager)
        {
            if (parametrs.ContainsKey("--file-log"))
            {
                this.AccessLog = fileManager.ReadFile(parametrs["--file-log"]);
            }
            else
            {
                throw new Exception("Ошибка задания пути к файлу логов");
            }

            if (parametrs.ContainsKey("--file-output"))
            {
                this.OutputPath = parametrs["--file-output"];
            }
            else
            {
                throw new Exception("Ошибка задания пути к файлу результата");
            }

            if (parametrs.ContainsKey("--address-start"))
            {
                this.AddressStart = parametrs["--address-start"];
            }

            if (parametrs.ContainsKey("--address-mask"))
            {
                this.AddressMask = parametrs["--address-mask"];
            }

            if (parametrs.ContainsKey("--time-start"))
            {
                this.TimeStart = parametrs["--time-start"];
            }

            if (parametrs.ContainsKey("--time-end"))
            {
                this.TimeEnd = parametrs["--time-end"];
            }
        }
    }
}
