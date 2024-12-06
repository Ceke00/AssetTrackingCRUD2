using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;
using System.Runtime.CompilerServices;
using AssetTrackingCRUD2;

namespace AssetTrackingCRUD2
    {
    public abstract class Asset
        {
        public int Id { get; set; }
        public DateTime PurchasedDate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public Price Price { get; set; }
        public Country Country { get; set; }

        [NotMapped]
        public bool IsOld => CalculateIsOld();

        [NotMapped]
        public bool IsVeryOld => CalculateIsVeryOld();

        private bool CalculateIsOld()
            {
            DateTime timeLimit = PurchasedDate.AddYears(3);
            return DateTime.Now >= timeLimit && DateTime.Now < timeLimit.AddMonths(3);
            }

        private bool CalculateIsVeryOld()
            {
            DateTime criticalLimit = PurchasedDate.AddYears(3).AddMonths(3);
            return DateTime.Now >= criticalLimit;
            }

        public override string ToString()
            {
            if (IsOld)
                {
                Console.ForegroundColor = ConsoleColor.Red;
                }
            else if (IsVeryOld)
                {
                Console.ForegroundColor = ConsoleColor.Yellow;
                }
            else
                {
                Console.ResetColor();
                }
            return Id.ToString().PadRight(8) + GetAssetType().PadRight(10) + PurchasedDate.ToString("yyyy-MM-dd").PadRight(16) + Brand.PadRight(12) + Model.PadRight(15) + Country.ToString().PadRight(12) + Price.ConvertFromUSD().ToString("F2").ToString().PadRight(10) + Price.Currency.ToString().PadRight(10) + Price.Value;
            }

        public virtual string GetAssetType() { return ""; }
        }
    }