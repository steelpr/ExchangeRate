using ASPCore.DataProcessor.Services.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ASPCore.DataProcessor.Services
{
    public class ExportDataService : IExportDataService
    {
        private const int startIndex = 30;
        private const int endIndex = 502;
        private const string deserializedIsSuccessful = "Deserialized {0} was successful";
        private readonly string deserializationStopped = "deserialization stopped";

        private const string CategoryGame = "Game";
        private const string CategoryHardware = "Hardware";

        private readonly WebClient web;
        private readonly Deserializer deserializer;
        private readonly ILogger logger;
               
        public ExportDataService(Deserializer deserializer,
            ILogger<TimedHostedService> logger)
        {
            this.web = new WebClient ();
            this.deserializer = deserializer;
            this.logger = logger;

        }

        public async Task ExportCurrency()
        {
            string xmlCurrency = web.DownloadString("http://www.bnb.bg/Statistics/StExternalSector/StExchangeRates/StERForeignCurrencies/index.htm?download=xml&search=&lang=EN");

            string xmlString = xmlCurrency.Remove(startIndex, endIndex);

            try
            {
                await deserializer.ImportCurrency(xmlString);
            }
            catch (Exception)
            {
                logger.LogError(deserializationStopped);
            }
        }
    }
}
