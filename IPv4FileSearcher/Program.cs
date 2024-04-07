using IPv4FileSearcher;
using System.Net;
using System.Reflection.Metadata;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class Program
{
    private static void Main(string[] args)
    {
        ParameterReceiver dataReceiver = new ParameterReceiver(args);
        FileManager fileManager = new FileManager();
        var innDict = dataReceiver.MakeDataDictionary();
        Options options = new Options(innDict, fileManager);    
        IPv4Searcher ipv4Searcher = new IPv4Searcher(options, fileManager);

        ipv4Searcher.Search();

        Console.ReadKey();

    }

    // Каждый класс имеет строго одну зону ответственности
    // dataReceiver сделан для получения параметров
    // options для создания настроек, отделен от dataReceiver для разделения ответственности
    // FileManager реализует интерфейс IFileManager благодаря чему при расширении его можно
    //   подменить и сделать какую либо иную реализацию получения начальных
    //   значений и возврата результата 
    // IPv4Searcher реализует основную логику программы и выдает значения Ip-адресов
    //   входящих в указанный диапазон с количеством 
    //   

}