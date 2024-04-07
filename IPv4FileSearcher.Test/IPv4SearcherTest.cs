
namespace IPv4FileSearcher.Test
{
    public class IPv4SearcherTest
    {
        [Fact]
        public void Test()
        {
            // arrange

            Hendler hendler = new Hendler();
            string[] list = 
                { 
                   "14.187.180.104:2000-12-09 12:00:00", "84.147.146.8:2000-12-10 12:00:30",
                   "101.234.144.10:2000-10-01 12:00:20", "101.234.144.10:2000-11-01 12:00:50",
                   "101.234.144.24:2000-10-01 12:00:50", "82.15.65.26:2011-12-01 12:00:00",
                   "7.212.217.27:2010-12-01 12:00:50"
                };
            hendler.AdressList = list;
            string[] args =
                {"--file-log", "не используется", "--file-output", "не используется",
                "--address-start" ,  "101.234.144.10", "--address-mask", "255.255.255.128" ,
                "--time-start" ,  "15.08.2000", "--time-end" , "10.01.2024"
                };

            ParameterReceiver dataReceiver = new ParameterReceiver(args);
            var innDict = dataReceiver.MakeDataDictionary();
            OutputFileManager outputFileManager = new OutputFileManager(hendler);
            Options options = new Options(innDict, outputFileManager);
            IPv4Searcher ipv4Searcher = new IPv4Searcher(options, outputFileManager);

            // act
            ipv4Searcher.Search();

            // assert
            Assert.Equal("101.234.144.10 2", hendler.Content[0]);
            Assert.Equal("101.234.144.24 1", hendler.Content[1]);

        }
    }

    //Благодаря IFileManager подменяем реализацию IFileManager получения начальных
    //   значений и возврата результата и получаем данные для теста
    public class Hendler
    {
        string[] adressList;
        string[] content;
        public string[] AdressList { get { return adressList; } set { adressList = value; } }
        public string[] Content { get { return content; } set { content = value; } }
    }
    public class OutputFileManager : IFileManager
    {
        Hendler hendler;
        public OutputFileManager(Hendler hendler)
        {
            this.hendler = hendler;
        }
        public string[] ReadFile(string path)
        {
            return hendler.AdressList;
        }

        public void WriteFile(string path, string[] content)
        {
            hendler.Content = content;
        }
    }
}