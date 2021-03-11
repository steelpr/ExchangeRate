using System;
using System.Collections.Generic;
using System.Text;

namespace ASPCore.Data.Models
{
    public class UserFavoriteCurrency
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ExchangeRateUser ExchangeRateUser { get; set; }

        public int CurrencyId { get; set; }

        public Currency Currency  { get; set; }
    }
}
