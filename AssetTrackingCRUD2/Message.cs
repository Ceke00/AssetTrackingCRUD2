using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTrackingCRUD2
    {
    public class Message
        {
        //Generating colorized messages 
        public static void GenerateMessage(string message, string color, bool sameLine = false)
            {
            switch (color)
                {
                case "Red": Console.ForegroundColor = ConsoleColor.Red; break;
                case "Green": Console.ForegroundColor = ConsoleColor.Green; break;
                case "Yellow": Console.ForegroundColor = ConsoleColor.Yellow; break;
                case "Cyan": Console.ForegroundColor = ConsoleColor.Cyan; break;
                case "Blue": Console.ForegroundColor = ConsoleColor.Blue; break;
                case "DarkGreen": Console.ForegroundColor = ConsoleColor.DarkGreen; break;

                }
            if (sameLine) Console.Write(message);
            else Console.WriteLine(message);
            Console.ResetColor();
            }

        //Generating menuu header with specific color
        public static void GenerateMenuHeader(string title, string color)
            {
            GenerateDivider("*", color, 54);
            GenerateMessage(title, color);
            GenerateDivider("*", color, 54);
            }

        //Generating divider with specific lenght and color
        public static void GenerateDivider(string sign, string color, int lengthString)
            {
            for (int i = 0; i < lengthString - 1; i++)
                {
                GenerateMessage(sign, color, true);
                }
            GenerateMessage(sign, color);
            }

        //Generating table header
        public static void GenerateTableHeader()
            {
            string header = "ID".PadRight(8) + "TYPE".PadRight(10) + "PURCHASE DATE".PadRight(16) + "BRAND".PadRight(12) + "MODEL".PadRight(15) + "COUNTRY".PadRight(12) + "PRICE".PadRight(10) + "CURRENCY".PadRight(10) + "PRICE(USD)";
            GenerateDivider("*", "Cyan", 104);
            GenerateMessage("RED = Older than 3 years", "Red", true);
            GenerateMessage("\tYELLOW = Older than 3 years & 3 months", "Yellow");
            GenerateDivider("-", "Cyan", 104);
            GenerateMessage(header, "Cyan");
            GenerateDivider("-", "Cyan", 104);
            }
        }
    }
