using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AssetTrackingCRUD2
    {
    public class UserInterface
        {
        MyDbContext MyDb = new MyDbContext();
        public AssetManager assetManager;

        public UserInterface(AssetManager assetManager)
            {
            this.assetManager = assetManager;
            }

        //Main menu choices
        public void ShowMainMenu()
            {
            while (true)
                {
                try
                    {
                    Message.GenerateMenuHeader("********************  MAIN MENU  *********************", "Yellow");
                    Message.GenerateMessage("Pick an option:", "Cyan");
                    Console.WriteLine("(1) Show Asset List \n(2) Add Asset \n(3) Update Asset \n(4) Delete Asset \n(5) Show Statistics \n(6) Quit");
                    Message.GenerateDivider("-", "Yellow", 54);

                    //Users choice
                    string choice = Console.ReadLine().Trim();
                    switch (choice)
                        {
                        case "1":
                            ShowAssetList();
                            break;
                        case "2":
                            AddNewAsset();
                            break;
                        case "3":
                            EditAsset();
                            break;
                        case "4":
                            DeleteAsset();
                            break;
                        case "5":
                            ShowAssetStats();
                            break;
                        case "6":
                            QuitApp();
                            return;
                        default:
                            Console.Clear();
                            Message.GenerateDivider("*", "Red", 54);
                            Message.GenerateMessage("Invalid option. Write 1, 2, 3, 4, 5 or 6.", "Red");
                            Message.GenerateDivider("*", "Red", 54);
                            break;
                        }
                    }
                catch (Exception e) { Message.GenerateMessage("An error ocurred: " + e.Message, "Red"); }
                }
            }

        //Showing asset table by selected order
        private void ShowAssetList()
            {
            Console.Clear();
            Message.GenerateMenuHeader("*****************  COMPANY ASSETS  *******************", "Cyan");
            while (true)
                {
                try
                    {
                    int choice = GetIntInput("Sort assets by (1) Country, (2) Type, (3) Id (or 0 for main menu)? ", 3);
                    if (choice == 0) { Console.Clear(); break; }
                    Message.GenerateTableHeader();


                    if (choice == 1)
                        {
                        //Ordering by country and then date
                        var StoredAssets = MyDb.Assets.AsNoTracking().OrderBy(t => t.Country).ThenBy(o => o.PurchasedDate).ToList();
                        foreach (Asset asset in StoredAssets)
                            {
                            Console.WriteLine(asset.ToString());
                            }
                        Message.GenerateDivider("*", "Cyan", 104);

                        }
                    else if (choice == 2)
                        {
                        //Ordering by the asset type and by date
                        var StoredAssets = MyDb.Assets.AsNoTracking().OrderBy(a => EF.Property<string>(a, "asset_type")).ThenBy(o => o.PurchasedDate).ToList();
                        foreach (Asset asset in StoredAssets)
                            {
                            Console.WriteLine(asset.ToString());
                            }
                        Message.GenerateDivider("*", "Cyan", 104);

                        }
                    else if (choice == 3)
                        {
                        ShowAssetListId();
                        }
                    }
                catch (Exception e) { Message.GenerateMessage("An error ocurred: " + e.Message, "Red"); }
                }
            }
        //Showing asset list ordered by ID
        private void ShowAssetListId()
            {
            var StoredAssets = MyDb.Assets.AsNoTracking().OrderBy(a => a.Id).ToList();
            foreach (Asset asset in StoredAssets)
                {
                Console.WriteLine(asset.ToString());
                }
            Message.GenerateDivider("*", "Cyan", 104);

            }

        //Showing some statistics of the assets
        private void ShowAssetStats()
            {
            Console.Clear();
            Message.GenerateMenuHeader("*******************  STATISTICS  *********************", "Cyan");
            Console.WriteLine("Wait some seconds for new statistics.");

            var storedAssets = MyDb.Assets.AsNoTracking().ToList();

            //Counting Assets
            int totalAssets = storedAssets.Count();
            int totalPhones = storedAssets.Count(a => a.GetAssetType() == "Phone");
            int totalComputers = storedAssets.Count(a => a.GetAssetType() == "Computer");

            //Assets in different offices
            int assetsUsa = storedAssets.Count(a => a.Country.ToString() == "usa");
            int assetsSweden = storedAssets.Count(a => a.Country.ToString() == "sweden");
            int assetsGermany = storedAssets.Count(a => a.Country.ToString() == "germany");
            //Purchase cost in USD
            string totalCost = storedAssets.Sum(a => a.Price.Value).ToString("F0");
            string germanyCost = storedAssets.Where(a => a.Country.ToString() == "germany").Sum(a => a.Price.Value).ToString("F0");
            string usaCost = storedAssets.Where(a => a.Country.ToString() == "usa").Sum(a => a.Price.Value).ToString("F0");
            string swedenCost = storedAssets.Where(a => a.Country.ToString() == "sweden").Sum(a => a.Price.Value).ToString("F0");

            //Age of assets
            int totalNewAssets = storedAssets.Count(a => a.PurchasedDate.AddYears(3) > DateTime.Now);
            int totalOldAssets = storedAssets.Count(a => a.PurchasedDate.AddYears(3).AddMonths(3) > DateTime.Now && a.PurchasedDate.AddYears(3) < DateTime.Now);
            int totalVeryOldAssets = storedAssets.Count(a => a.PurchasedDate.AddYears(3).AddMonths(3) < DateTime.Now);

            //Age of assets USA
            int usaNewAssets = storedAssets.Where(a => a.Country.ToString() == "usa").Count(a => a.PurchasedDate.AddYears(3) > DateTime.Now);
            int usaOldAssets = storedAssets.Where(a => a.Country.ToString() == "usa").Count(a => a.PurchasedDate.AddYears(3).AddMonths(3) > DateTime.Now && a.PurchasedDate.AddYears(3) < DateTime.Now);
            int usaVeryOldAssets = storedAssets.Where(a => a.Country.ToString() == "usa").Count(a => a.PurchasedDate.AddYears(3).AddMonths(3) < DateTime.Now);

            //Age of assets Sweden
            int swedenNewAssets = storedAssets.Where(a => a.Country.ToString() == "sweden").Count(a => a.PurchasedDate.AddYears(3) > DateTime.Now);
            int swedenOldAssets = storedAssets.Where(a => a.Country.ToString() == "sweden").Count(a => a.PurchasedDate.AddYears(3).AddMonths(3) > DateTime.Now && a.PurchasedDate.AddYears(3) < DateTime.Now);
            int swedenVeryOldAssets = storedAssets.Where(a => a.Country.ToString() == "sweden").Count(a => a.PurchasedDate.AddYears(3).AddMonths(3) < DateTime.Now);

            //Age of assets Germany
            int germanyNewAssets = storedAssets.Where(a => a.Country.ToString() == "germany").Count(a => a.PurchasedDate.AddYears(3) > DateTime.Now);
            int germanyOldAssets = storedAssets.Where(a => a.Country.ToString() == "germany").Count(a => a.PurchasedDate.AddYears(3).AddMonths(3) > DateTime.Now && a.PurchasedDate.AddYears(3) < DateTime.Now);
            int germanyVeryOldAssets = storedAssets.Where(a => a.Country.ToString() == "germany").Count(a => a.PurchasedDate.AddYears(3).AddMonths(3) < DateTime.Now);

            //Presenting statistics to user
            Message.GenerateDivider("-", "Blue", 104);
            Message.GenerateMessage("Nr of Assets", "Cyan");
            Console.WriteLine("There are " + totalAssets + " assets in total - " + totalPhones + " phones and " + totalComputers + " computers.");

            Message.GenerateDivider("-", "Blue", 104);
            Message.GenerateMessage("Location of Assets", "Cyan");
            Console.WriteLine("USA office: ".PadRight(16) + assetsUsa);
            Console.WriteLine("Swedish office: ".PadRight(16) + assetsSweden);
            Console.WriteLine("German office: ".PadRight(16) + assetsGermany);

            Message.GenerateDivider("-", "Blue", 104);
            Message.GenerateMessage("Cost of Assets in USD", "Cyan");
            Console.WriteLine("US office: ".PadRight(16) + usaCost + " USD");
            Console.WriteLine("Swedish office: ".PadRight(16) + swedenCost + " USD");
            Console.WriteLine("German office: ".PadRight(16) + germanyCost + " USD");
            Console.WriteLine("Total cost: ".PadRight(16) + totalCost + " USD");

            Message.GenerateDivider("-", "Blue", 104);
            Message.GenerateMessage("Age of assets", "Cyan");
            Console.WriteLine("New (less than 3 years): " + totalNewAssets);
            Console.WriteLine("Old (3 years to 3 years & 3 months): " + totalOldAssets);
            Console.WriteLine("Very old (more than 3 years & 3 month): " + totalVeryOldAssets);

            Message.GenerateDivider("-", "Blue", 104);
            Message.GenerateMessage("Age of assets - in different offices", "Cyan");
            Console.WriteLine("USA ".PadRight(10) + "New: " + usaNewAssets + " Old: ".PadLeft(8) + usaOldAssets + " Very old: ".PadLeft(12) + usaVeryOldAssets);
            Console.WriteLine("Sweden ".PadRight(10) + "New: " + swedenNewAssets + " Old: ".PadLeft(8) + swedenOldAssets + " Very old: ".PadLeft(12) + swedenVeryOldAssets);
            Console.WriteLine("Germany ".PadRight(10) + "New: " + germanyNewAssets + " Old: ".PadLeft(8) + germanyOldAssets + " Very old: ".PadLeft(12) + germanyVeryOldAssets);
            Message.GenerateDivider("-", "Blue", 104);
            Message.GenerateDivider("*", "Cyan", 104);

            while (true)
                {
                try
                    {
                    int choice = GetIntInput("Press (0) for Main Menu. ", 0);
                    if (choice == 0) { Console.Clear(); break; }
                    }
                catch (Exception e) { Console.WriteLine(e.ToString()); }
                }
            }


        //Creating new assets
        private void AddNewAsset()
            {
            Console.Clear();
            Message.GenerateMenuHeader("*******************  ADD NEW ASSET  ******************", "Cyan");

            while (true)
                {
                try
                    {
                    int choice = GetIntInput("Press (1) to enter new asset (or 0 for main menu) ", 1);
                    if (choice == 0) { Console.Clear(); break; }
                    if (choice == 1)
                        {
                        Message.GenerateMessage("Describe the asset!", "Green");

                        while (true)
                            {

                            //Getting properties of the asset
                            TypeOfAsset assetType = GetEnumInput<TypeOfAsset>("What kind of asset? Computer or phone? ", false);
                            string brand = GetStringInput("What brand is the " + assetType + "? ", false);
                            string model = GetStringInput("What model is the " + brand + "? ", false);
                            decimal priceInUSD = GetDecimalInput("What did the " + assetType + " cost(in USD)? ", false);
                            Country country = GetEnumInput<Country>("Where is the " + assetType + "? USA, Germany or Sweden? ", false);
                            DateTime purchaseDate = GetDateInput("When was the " + assetType + " bought? Format date yyyy-MM-dd. ", false);
                            Currency currency = Price.GetCurrency(country);

                            //Creates different assets
                            try
                                {
                                if (assetType == TypeOfAsset.computer)
                                    {
                                    assetManager.AddAsset(new Computer(new Price(priceInUSD, currency), purchaseDate, brand, model, country));
                                    Console.Clear();
                                    Message.GenerateDivider("!", "Cyan", 54);
                                    Message.GenerateMessage("The " + assetType + " was added to the database!", "Yellow");
                                    Message.GenerateDivider("!", "Cyan", 54);
                                    Console.WriteLine();
                                    break;
                                    }
                                else if (assetType == TypeOfAsset.phone)
                                    {
                                    assetManager.AddAsset(new Phone(new Price(priceInUSD, currency), purchaseDate, brand, model, country));
                                    Console.Clear();
                                    Message.GenerateDivider("!", "Green", 54);
                                    Message.GenerateMessage("The " + assetType + " was added!", "Green");
                                    Message.GenerateDivider("!", "Green", 54);
                                    Console.WriteLine();
                                    break;
                                    }
                                else { throw new Exception("Could not add the asset"); }
                                }
                            catch (Exception e) { Message.GenerateMessage(e.Message, "Red"); break; }
                            }
                        }
                    }
                catch (Exception e) { Message.GenerateMessage("An error ocurred: " + e.Message, "Red"); }
                }
            }

        //Editing assets. Select one asset to edit from list
        private void EditAsset()
            {
            Console.Clear();
            Message.GenerateMenuHeader("*******************  EDIT ASSET  *********************", "Cyan");
            Message.GenerateTableHeader();
            ShowAssetListId();
            //What ID do you want to update

            while (true)
                {
                try
                    {
                    int inputId = GetInputId("Enter the ID of the asset you want to edit (or 0 for Main Menu): ");
                    if (inputId == 0) { Console.Clear(); break; }
                    if (MyDb.Assets.AsNoTracking().Any(a => a.Id == inputId))
                        {
                        Asset chosenAsset = MyDb.Assets.AsNoTracking().Where(a => a.Id == inputId).FirstOrDefault();
                        Message.GenerateDivider("-", "Cyan", 54);

                        //Showing current data before input
                        //allowPreviousValue is marked as true => user can press Enter to keep current value
                        Message.GenerateMessage("Press ENTER to keep current value!", "Cyan");

                        //Purchase Date
                        DateTime currentPurchasedDate = chosenAsset.PurchasedDate;
                        Console.WriteLine("Current date of purchase: " + currentPurchasedDate.ToString("yyyy-MM-dd"));
                        DateTime newPurchasedDate = GetDateInput("Enter a new purchase date (yyyy-mm-dd): ", true, currentPurchasedDate);
                        //Brand
                        string currentBrand = chosenAsset.Brand;
                        Console.WriteLine("Current Brand: " + currentBrand);
                        string newBrand = GetStringInput("Enter a new brand: ", true, currentBrand);
                        //Model
                        string currentModel = chosenAsset.Model;
                        Console.WriteLine("Current model: " + currentModel);
                        string newModel = GetStringInput("Enter a new model: ", true, currentModel);

                        //Country enum. Controling new input
                        Country currentCountry = chosenAsset.Country;
                        Console.WriteLine("Asset location: " + currentCountry.ToString());
                        Country newCountry = GetEnumInput<Country>("Enter new location (Sweden, USA or Germany): ", true, currentCountry);
                        //Price in USD
                        decimal currentPriceInUSD = chosenAsset.Price.Value;
                        Console.WriteLine("Current price in USD: " + currentPriceInUSD.ToString());
                        decimal newPriceInUSD = GetDecimalInput("Enter new price (in USD): ", true, currentPriceInUSD);

                        //Updating asset
                        assetManager.EditAsset(inputId, newPurchasedDate, newBrand, newModel, newCountry, newPriceInUSD);
                        //Confirmation to user

                        Console.Clear();
                        Message.GenerateDivider("!", "Green", 54);
                        Message.GenerateMessage("The " + chosenAsset.GetAssetType() + " was updated!", "Green");
                        Message.GenerateDivider("!", "Green", 54);

                        Console.WriteLine();

                        break;
                        }
                    else { Message.GenerateMessage("That is not a correct ID, choose an existing ID from the list.", "Red"); }
                    }
                catch (Exception e) { Message.GenerateMessage(e.Message, "Red"); }
                }
            }

        //Deleting assets. Select one asset to delete from list
        private void DeleteAsset()
            {
            Console.Clear();
            Message.GenerateMenuHeader("******************  DELETE ASSET  ********************", "Red");
            Message.GenerateTableHeader();
            ShowAssetListId();

            while (true)
                {
                try
                    {
                    int inputId = GetInputId("Enter the ID of the asset you want to delete (or 0 for Main Menu): ");
                    if (inputId == 0) { Console.Clear(); break; }
                    if (MyDb.Assets.AsNoTracking().Any(a => a.Id == inputId))
                        {
                        Asset chosenAsset = MyDb.Assets.AsNoTracking().Where(a => a.Id == inputId).FirstOrDefault();
                        Message.GenerateDivider("-", "Red", 54);

                        //Confirm deletion
                        Message.GenerateMessage("Are you sure you want to delete the following asset?", "Yellow");
                        Console.WriteLine(inputId + ". " + chosenAsset.Brand + " " + chosenAsset.Model);
                        int choice = GetIntInput("Enter (0) NO // (1) YES ", 1);
                        if (choice == 0) { Console.Clear(); break; }
                        else if (choice == 1)
                            {
                            assetManager.DeleteAsset(inputId);
                            Console.Clear();
                            //Confirmation to user
                            Message.GenerateDivider("!", "Green", 54);
                            Message.GenerateMessage("The asset was successfully deleted!", "Green");
                            Message.GenerateDivider("!", "Green", 54);
                            Console.WriteLine();
                            break;
                            }
                        }
                    else
                        {
                        Message.GenerateMessage("That is not a correct ID, choose an existing ID from the list.", "Red");
                        }
                    }
                catch (Exception e)
                    {
                    Message.GenerateMessage(e.Message, "Red");
                    }
                }
            }

        private void QuitApp()
            {
            Message.GenerateMessage("Exiting program...", "Yellow");
            }


        //VALIDATION METHODS
        //Validate int input from user, return correct int
        private static int GetInputId(string prompt)
            {
            while (true)
                {
                try
                    {
                    Message.GenerateMessage(prompt, "Yellow", true);
                    string input = Console.ReadLine().Trim();
                    if (!string.IsNullOrEmpty(input))
                        {
                        if (int.TryParse(input, out int id))
                            {
                            if (id >= 0) return id;
                            else throw new ArgumentOutOfRangeException("Not a correct id.");
                            }
                        else
                            {
                            throw new FormatException("Only enter numbers.");
                            }
                        }
                    else
                        {
                        throw new ArgumentException("No input registered. Try again!");
                        }
                    }

                catch (Exception e)
                    {
                    Message.GenerateMessage(e.Message, "Red");
                    }
                }
            }


        //Validate int input from user, return correct int
        private static int GetIntInput(string prompt, int nrOfChoices)
            {
            while (true)
                {
                try
                    {
                    Message.GenerateMessage(prompt, "Yellow", true);
                    string input = Console.ReadLine().Trim();
                    if (!string.IsNullOrEmpty(input))
                        {
                        if (int.TryParse(input, out int choice))
                            {
                            if (choice >= 0 && choice <= nrOfChoices) return choice;
                            else throw new ArgumentOutOfRangeException("Not a correct choice.");
                            }
                        else
                            {
                            throw new FormatException("Only enter numbers.");
                            }
                        }
                    else
                        {
                        throw new ArgumentException("No input registered. Try again!");
                        }
                    }

                catch (Exception e)
                    {
                    Message.GenerateMessage(e.Message, "Red");
                    }
                }
            }

        // Validate DateTime input, return a correct date.
        // If allowPreviousValue=true and input is empty, current value is returned.
        private static DateTime GetDateInput(string prompt, bool allowPreviousValue, DateTime currentValue = default(DateTime))
            {
            while (true)
                {
                try
                    {
                    Message.GenerateMessage(prompt, "Yellow", true);
                    string input = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(input) && allowPreviousValue) { return currentValue; }

                    if (!string.IsNullOrEmpty(input))
                        {
                        if (DateTime.TryParse(input, out DateTime date))
                            {
                            if (date > DateTime.Now.Date)
                                {
                                throw new FormatException("The date you entered is in the future.");
                                }
                            else return date;
                            }
                        else
                            {
                            throw new FormatException("Not a correct date. Format yyyy-MM-dd.");
                            }
                        }
                    else
                        {
                        throw new ArgumentException("No input registered. Try again!");
                        }
                    }

                catch (Exception e)
                    {
                    Message.GenerateMessage(e.Message, "Red");
                    }
                }
            }

        //Validate string input, return a string
        // If allowPreviousValue=true and input is empty, current value is returned.
        private static string GetStringInput(string prompt, bool allowPreviousValue, string currentValue = "")
            {
            while (true)
                {
                try
                    {
                    Message.GenerateMessage(prompt, "Yellow", true);
                    string input = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(input) && allowPreviousValue) { return currentValue; }

                    if (!string.IsNullOrEmpty(input))
                        {
                        return input;
                        }
                    else
                        {
                        throw new ArgumentException("No input registered. Try again!");
                        }
                    }
                catch (Exception e)
                    {
                    Message.GenerateMessage(e.Message, "Red");
                    }
                }
            }

        //Getting input of enum type
        public static T GetEnumInput<T>(string prompt, bool allowPreviousValue, T currentValue = default(T)) where T : struct
            {
            while (true)
                {
                try
                    {
                    Message.GenerateMessage(prompt, "Yellow", true);
                    string input = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(input) && allowPreviousValue) { return currentValue; }

                    if (!string.IsNullOrEmpty(input))
                        {
                        if (int.TryParse(input, out _))
                            {
                            throw new Exception("Not valid input, Don't use numbers.");
                            }
                        if (Enum.TryParse(input, true, out T result))

                            {
                            Message.GenerateMessage("You entered " + result, "Green");
                            return result;
                            }
                        else
                            {
                            throw new Exception($"Not a correct choice, try again! Valid choices are: {string.Join(", ", Enum.GetNames(typeof(T)))}");
                            }
                        }
                    else
                        {
                        throw new ArgumentException("No input registered. Try again!");
                        }
                    }
                catch (Exception e)
                    {
                    Message.GenerateMessage(e.Message, "Red");
                    }
                }
            }

        //Getting input of decimal type (price)
        private static decimal GetDecimalInput(string prompt, bool allowPreviousValue, decimal currentValue = default)
            {
            while (true)
                {
                try
                    {
                    Message.GenerateMessage(prompt, "Yellow", true);
                    string input = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(input) && allowPreviousValue) { return currentValue; }

                    if (!string.IsNullOrEmpty(input))
                        {
                        if (decimal.TryParse(input, out decimal price))
                            {
                            return price;
                            }
                        else
                            {
                            throw new FormatException("Not a correct number.");
                            }
                        }
                    else
                        {
                        throw new ArgumentException("No input registered. Try again!");
                        }
                    }
                catch (Exception e)
                    {
                    Message.GenerateMessage(e.Message, "Red");
                    }
                }
            }
        }
    }
