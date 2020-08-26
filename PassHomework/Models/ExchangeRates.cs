using System;
using System.Collections.Generic;

namespace PassHomework.Models
{
    public class ExchangeRates
    {
        public Dictionary<string, decimal> Rates { get; set; }
        public string Base { get; set; }
        public DateTime Date { get; set; }
    }
}
