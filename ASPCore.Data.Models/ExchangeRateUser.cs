using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ASPCore.Data.Models
{
    // Add profile data for application users by adding properties to the ExchangeRateUser class
    public class ExchangeRateUser : IdentityUser
    {
        public ICollection<Comment>Comments { get; set; }

        public ICollection<UserFavoriteCurrency> userFavoriteCurrencies { get; set; }
    }
}
