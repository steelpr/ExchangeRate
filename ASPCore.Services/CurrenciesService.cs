using ASPCore.Services.Contracts;
using System.Threading.Tasks;
using ASPCore.Data.Models;
using ASPCore.Data.Common;

namespace ASPCore.Services
{
    public class CurrenciesService : ICurrenciesService
    {
        private readonly IRepository<Currency> currency;

        public CurrenciesService(IRepository<Currency> currency)
        {
            this.currency = currency;
        }

        public Task Create()
        {
            var currency = new Currency
            {


            };

            return null;
        }
    }
}
