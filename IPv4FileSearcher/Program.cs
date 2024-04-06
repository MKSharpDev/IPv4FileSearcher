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
        IPv4Searcher ipv4Searcher = new IPv4Searcher(dataReceiver);

        IPv4Searcher.Search;



        Console.ReadKey();

    }

}