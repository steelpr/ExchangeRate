using ASPCore.Data.Common;
using ASPCore.Data.Models;
using ASPCore.DataProcessor.ImportDto;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ASPCore.DataProcessor
{
    public class Deserializer
    {
        private readonly IServiceScopeFactory scopeFactory;

        public Deserializer(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public async Task ImportCurrency(string xmlString)
        {
            var serializer = new XmlSerializer(typeof(CurrnecyDto[]), new XmlRootAttribute("ROWSET"));

            var deserializer = (CurrnecyDto[])serializer.Deserialize(new StringReader(xmlString));

            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<IRepository<Currency>>();

                foreach (var currnecyDto in deserializer)
                {
                    if (!IsValid(currnecyDto))
                    {
                        continue;
                    }
                    
                    var currency = new Currency
                    {
                        Gold = currnecyDto.Gold,
                        Name = currnecyDto.Name,
                        Code = currnecyDto.Code,
                        Ratio = currnecyDto.Ratio,
                        ReverseRate = currnecyDto.ReverseRate,
                        Rate = currnecyDto.Rate,
                        UpdateDate = DateTime.Parse(currnecyDto.UpdateDate).Date
                    };

                    await dbContext.AddAsync(currency);
                    await dbContext.SaveChangesAsync();

                }
                Thread.Sleep(5000);

            }
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, validationResult, true);
        }

    }
}
