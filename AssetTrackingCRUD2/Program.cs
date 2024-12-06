//# Asset Tracking with database and Entity Framework Core
//Level 1
//Create a console app that have the following classes and objects: 
//Laptop Computers 
//Mobile Phones 
//Create the appropriate fields, constructors and properties for each object: purchase date, price, model name etc. 
//All assets needs to be stored in database using Entity Framework Core with Create and Read functionality. 
//Level 2
//Create a program to create a list of assets (inputs) where the final result is to write the following to the console: 
// • Sorted list with Class as primary (computers first, then phones) 
// • Then sorted by purchase date 
// • Mark any item *RED* if purchase date is less than 3 months away from 3 years. 
//Your application should handle FULL CRUD. 
//Level 3
//Add offices to the model: 
//You should be able to place items in 3 different offices around the world which will use the appropriate currency 
//for that country. You should be able to input values in dollars and convert them to each currency (based on 
//todays currency charts) 
//When you write the list to the console: 
// • Sorted first by office 
// • Then Purchase date 
// • Items *RED* if date less than 3 months away from 3 years 
// • Items *Yellow* if date less than 6 months away from 3 years

//TypeOfAsset (enum)?
//DbManager?

//Skapa AssetManager
//Hämta assets från Db
//Visa meny - View Assets, 


using AssetTrackingCRUD2;
class Program
{
    public static void Main()
    {
        AssetManager assetManager = new AssetManager();
        Console.WriteLine();
        Message.GenerateMenuHeader("***********  WELCOME TO THE ASSET MANAGER  ***********", "Green");
        Console.WriteLine();
        UserInterface ui = new UserInterface(assetManager);
        ui.ShowMainMenu();
    }
}