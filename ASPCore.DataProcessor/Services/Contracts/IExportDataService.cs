using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace ASPCore.DataProcessor.Services.Contracts
{
    public interface IExportDataService
    {
        Task ExportCurrency();
    }
}
