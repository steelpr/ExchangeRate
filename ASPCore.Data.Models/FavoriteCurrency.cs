using System;
using System.Collections.Generic;
using System.Text;

namespace ASPCore.Data.Models
{
    public class FavoriteCurrency
    {
        public int Id { get; set; }

        public ICollection<UserFavoriteCurrency>  userFavoriteCurrencies { get; set; }
    }
}
