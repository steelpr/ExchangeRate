using System;
using System.Collections.Generic;
using System.Text;

namespace ASPCore.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string Content { get; set; }

        public string UserEmail { get; set; }

        public string UserId { get; set; }
        public ExchangeRateUser User{ get; set; }
    }
}
