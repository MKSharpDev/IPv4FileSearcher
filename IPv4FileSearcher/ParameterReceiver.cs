using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPv4FileSearcher
{
    public class ParameterReceiver
    {
        private string[] args;


        public string[] Args { get { return args; } set { args = value; } }


        public ParameterReceiver(string[] args)
        {
            this.args = args;
        }



        public Dictionary<string, string> MakeDataDictionary()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            string[] settings = new string[] {
            "--file-log",
            "--file-output",
            "--address-start",
            "--address-mask",
            "--time-start ",
            "--time-end"};

            for (int i = 0; i < args.Length; i = i + 2)
            {
                try
                {
                    if (!settings.Contains(args[i]))
                    {
                        Console.WriteLine("Параметры заданы не верно, введите параметры в ручном режиме");
                        data = new Dictionary<string, string>();
                        break;
                    }
                    data.Add(args[i], args[i + 1]);
                }
                catch (Exception)
                {
                    Console.WriteLine("Параметры заданы не верно, введите параметры в ручном режиме");
                    data = new Dictionary<string, string>();
                }
            }

            if (!data.ContainsKey("--address-start") && data.ContainsKey("--address-mask"))
            {
                data.Remove("--address-mask ");
            }
            return data;
        }
    }
}
