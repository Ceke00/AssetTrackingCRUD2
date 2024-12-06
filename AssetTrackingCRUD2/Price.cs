using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTrackingCRUD2
    {
    public class Price
        {
        public Price(decimal value, Currency currency)
            {
            Value = value;
            Currency = currency;
            }


        public decimal Value { get; set; }

        public Currency Currency { get; set; }

        //Convert from USD to EURO and SEK
        public decimal ConvertFromUSD()
            {
            decimal rateEURO = 0.91M;
            decimal rateSEK = 10.397M;
            decimal newValue = Value;
            if (Currency == Currency.EUR)
                {
                newValue = Value * rateEURO;
                }
            if (Currency == Currency.SEK)
                {
                newValue = Value * rateSEK;
                }
            return newValue;
            }


        public static Currency GetCurrency(Country country)
            {
            switch (country)
                {
                case Country.usa: return Currency.USD;
                case Country.sweden: return Currency.SEK;
                case Country.germany: return Currency.EUR;
                default:
                    throw new ArgumentException("Invalid country");
                }
            }
        }
    }
