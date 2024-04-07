using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPv4FileSearcher
{
    public class FileManager : IFileManager
    {
        public string[] ReadFile(string path)
        {
            List<string> content = new List<string>();

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        content.Add(line);
                    }
                }

                return content.ToArray();


            }
            catch (Exception ex)
            {

                Console.WriteLine("Ошибка чтения файла");
                Console.WriteLine($"Error: {ex.Message}");
            }
            return null;
        }

        public void WriteFile(string path, string[] content)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path, true))

                    foreach (string line in content)
                    {
                        writer.WriteLine(line);
                    }

            }
            catch (Exception ex)
            {

                Console.WriteLine("Ошибка записи в файл");
                Console.WriteLine($"Error: {ex.Message}");
            }

        }
    }
}
