using System;
using System.Reflection;

using SingleResponsibilityPrinciple.AdoNet;

namespace SingleResponsibilityPrinciple
{
    class Program
    {
        static void Main(string[] args)
        {
            var tradeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SingleResponsibilityPrinciple.trades.txt");

            var logger = new ConsoleLogger();
            var tradeValidator = new SimpleTradeValidator(logger);
            var tradeDataProvider = new StreamTradeDataProvider(tradeStream);
            //Created new URLTradeDataProvider object for Request 407.
            var URLTradeDataProvider = new URLTradeDataProvider("http://faculty.css.edu/tgibbons/trades4.txt");
            var tradeMapper = new SimpleTradeMapper();
            var tradeParser = new SimpleTradeParser(tradeValidator, tradeMapper);
            var tradeStorage = new AdoNetTradeStorage(logger);

            var tradeProcessor = new TradeProcessor(tradeDataProvider, tradeParser, tradeStorage);
            tradeProcessor.ProcessTrades();

            //Request 409. Save trades to database on remote SQL server.
            string connectSqlServer = "Data Source = athena.css.edu; Initial Catalog = CIS3285; Persist Security Info = True; User ID = tgibbons; Password = Data Source = athena.css.edu; Initial Catalog = CIS3285; Persist Security Info = True; User ID = tgibbons; Password = Saints4CSS";
            using (var connection = new System.Data.SqlClient.SqlConnection(connectSqlServer))


                Console.ReadKey();
        }
    }
}
