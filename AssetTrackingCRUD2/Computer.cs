using AssetTrackingCRUD2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTrackingCRUD2
    {
    public class Computer : Asset
        {
        public Computer() { }
        public Computer(Price price, DateTime purchasedDate, string brand, string model, Country country)
            {
            Price = price;
            PurchasedDate = purchasedDate;
            Brand = brand;
            Model = model;
            Country = country;
            }
        public override string GetAssetType()
            {
            return "Computer";
            }
        }
    }
