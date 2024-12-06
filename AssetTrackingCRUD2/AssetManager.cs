using AssetTrackingCRUD2;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTrackingCRUD2
    {
    public class AssetManager()
        {
        MyDbContext MyDb = new MyDbContext();

        //Adding a new asset
        public void AddAsset(Asset asset)
            {
            MyDb.Assets.Add(asset);
            MyDb.SaveChanges();
            }

        //Updating a specific asset
        public void EditAsset(int id, DateTime newPurchasedDate, string newBrand, string newModel, Country newCountry, decimal newPriceInUSD)
            {
            //Getting new currency
            Currency newCurrency = Price.GetCurrency(newCountry);
            //Asset to update
            var updatedAsset = MyDb.Assets.Where(a => a.Id == id).FirstOrDefault();

            //Updating and saving changes
            if (updatedAsset != null)
                {
                updatedAsset.PurchasedDate = newPurchasedDate;
                updatedAsset.Brand = newBrand;
                updatedAsset.Model = newModel;
                updatedAsset.Country = newCountry;
                updatedAsset.Price.Value = newPriceInUSD;
                updatedAsset.Price.Currency = newCurrency;
                Message.GenerateMessage("Saving changes...", "Yellow");
                MyDb.SaveChanges();
                }
            else
                {
                Console.WriteLine("Asset not found.");
                }
            }

        //Removes an asset with a specific id
        public void DeleteAsset(int id)
            {
            var assetToDelete = MyDb.Assets.Where(a => a.Id == id).FirstOrDefault();

            if (assetToDelete != null)
                {
                MyDb.Assets.Remove(assetToDelete);
                MyDb.SaveChanges();
                Message.GenerateMessage("The asset was successfully deleted!", "Green");
                }
            else
                {
                Console.WriteLine("Asset not found.");
                }
            }
        }
    }
