using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IPv4FileSearcher
{
    public class IPv4Searcher
    {
        IFileManager fileManager;
        Options options;

        public IPv4Searcher(Options options , IFileManager fileManager)
        {
            this.options = options;
            this.fileManager = fileManager;
        }

        public void Search()
        {
            string[] ipAdressesIn = options.AccessLog;
            IPAddress addressStart = IPAddress.Parse("0.0.0.0");
            int cidr = 0;

            //проверяем и задаем стартовый адресс и маску подсети
            if (options.AddressStart != null)
            {
                addressStart = IPAddress.Parse(options.AddressStart);
                if (options.AddressMask != null)
                {
                    cidr = ConvertToCidr(options.AddressMask);
                }
            }

            //Задаем сеть
            IPNetwork ipnetwork = new IPNetwork(IPAddress.Parse("0.0.0.0"), 0);
            try
            {
                ipnetwork = new IPNetwork(addressStart, cidr);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка задания нижней границы диапазона ip, установлена нижняя граница 0");

                Console.WriteLine(ex.ToString());
                var editAdres = addressStart.GetAddressBytes();
                editAdres[3] = 0;
                IPAddress exAdres = new IPAddress(editAdres);
                ipnetwork = new IPNetwork(exAdres, cidr);
            }


            //Проверяем и задаем стартовое время
            DateTime dateStart = DateTime.MinValue;
            if (options.TimeStart != null)
            {
                dateStart = DateTime.Parse(options.TimeStart);
            }

            //Проверяем и задаем конечное время
            DateTime dateEnd = DateTime.MaxValue;
            if (options.TimeEnd != null)
            {
                dateEnd = DateTime.Parse(options.TimeEnd);
            }


            Dictionary<IPAddress, int> result = new Dictionary<IPAddress, int>();

            foreach (string item in ipAdressesIn) 
            {
                var splitItem = item.Split(':').ToList();
                try
                {
                    string date = $"{splitItem[1]}:{splitItem[2]}:{splitItem[3]}";

                    var ipRequestTime = DateTime.Parse(date);
                    if (ipRequestTime >= dateStart && ipRequestTime <= dateEnd)
                    {


                        var adress = IPAddress.Parse(splitItem[0].ToString());
                        

                        if (ipnetwork.Contains(adress))
                        {
                            if (!result.ContainsKey(adress))
                                result.Add(adress, 1);
                            else
                            {
                                result[adress]++;
                            }
                        }
     
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("При обработке возникла ошибка");
                    Console.WriteLine(ex.Message);

                }
 
            }


            
            List<string> rezList = new List<string>();
            foreach (var item in result)
            {
                string str = $"{item.Key} {item.Value}";
                rezList.Add(str);   
            }
            
            string[] output = rezList.ToArray();
            fileManager.WriteFile(options.OutputPath, output);
        }

        static int ConvertToCidr(string maskStr)
        {
            var mask = IPAddress.Parse(maskStr);
            string maskBinAsString = Convert.ToString(mask.Address, 2);
                        
            int cidr = Convert.ToString(mask.Address, 2).Count(o => o == '1');
            return cidr;
        }
    }
}
