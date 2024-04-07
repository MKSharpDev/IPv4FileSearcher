using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IPv4FileSearcher
{
    public class ParameterReceiver
    {
        private string[] args;
        private string[] settings = new string[] {
            "--file-log",
            "--file-output",
            "--address-start",
            "--address-mask",
            "--time-start",
            "--time-end"};

        private string[] descriptions = new string[] {
            "путь к файлу с логами",
            "путь к файлу сохранения результата",
            "нижнюю границу диапазона адресов",
            "маску подсети",
            "нижнюю границу временного интервала ",
            "верхнюю границу временного интервала."};

        public string[] Args { get { return args; } set { args = value; } }


        public ParameterReceiver(string[] args)
        {
            this.args = args;
        }

        public Dictionary<string, string> MakeDataDictionary()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();


            for (int i = 0; i < args.Length; i = i + 2)
            {
                try
                {
                    if (!settings.Contains(args[i]))
                    {
                        Console.WriteLine("Параметры заданы не верно, введите параметры в ручном режиме");
                        data = MakeDictionaryManually();
                        break;
                    }
                    if (true)
                    {

                    }
                    data.Add(args[i], args[i + 1]);
                }
                catch (Exception)
                {
                    Console.WriteLine("Параметры заданы не верно, введите параметры в ручном режиме");
                    data = MakeDictionaryManually();
                }
            }
            if (data.ContainsKey("--address-mask"))
            {
                if (!IsValidMask(data["--address-mask"]))
                {
                    Console.WriteLine("Введена не валидная маска. Введите параметры в ручном режиме");
                    data = MakeDictionaryManually();
                }
            }

            // --address-mask  нельзя использовать, если не задан address-start
            if (!data.ContainsKey("--address-start") && data.ContainsKey("--address-mask"))
            {
                data.Remove("--address-mask ");
            }


            return data;
        }

        //При ошибках в параметрах задаем параметры в ручную
        private Dictionary<string, string> MakeDictionaryManually()
        {
            Dictionary<string, string> manuallyDict = new Dictionary<string, string>();
            for (int i = 0; i < settings.Length; i++)
            {
                askValue(settings[i], descriptions[i], manuallyDict);
                if (i > 1 || i == 5)
                {
                    Console.WriteLine("Если есть другие значения введите   y ");
                    string conform = Console.ReadLine();
                    
                    if (conform.ToLower() != "y")
                    {
                        break;
                    }
                }
            }
            if (manuallyDict.ContainsKey("--address-mask"))
            {
                if (!IsValidMask(manuallyDict["--address-mask"]))
                {
                    Console.WriteLine("Введена не валидная маска. Введите параметры в ручном режиме");
                    manuallyDict = MakeDictionaryManually();
                }
            }
            return manuallyDict;
        }


        private void askValue(string temp, string description, Dictionary<string, string> manuallyDict)
        {
            Console.WriteLine($"Введите {temp} {description}");
            string value = Console.ReadLine();
            manuallyDict.Add(temp, value);
        }

        //Проверяем валидность маски подсети
        static bool IsValidMask(string mask)
        {           
            if (!IPAddress.TryParse(mask, out var addr))
                return false;
            var byteVal = addr.GetAddressBytes();
            if (byteVal.Length != 4)
                return false;
            var intVal = BitConverter.ToInt32(byteVal);
            intVal = IPAddress.NetworkToHostOrder(intVal);

            var uintVal = (uint)intVal ^ uint.MaxValue;
            var addOne = uintVal + 1;
            var ret = addOne & uintVal;

            return ret == 0;
        }
    }
}
