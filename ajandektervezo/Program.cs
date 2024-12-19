using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Diagnostics;

namespace ajandektervezo
{
    internal class Program
    {
        static int money = 0;
        static List<string> giftName = new List<string>();
        static List<string> giftCategory = new List<string>();
        static List<int> giftPrice = new List<int>();
        static void Main(string[] args)
        {
            bool run = true;
            while (money == 0)
            {
                Console.Clear();
                setBudget();
            }
            while (run)
            {
                Console.Write("1. Ajándéklista kezelése\n2. Költségvetés kezelése\n3. Statisztika\n4. Kilépés\n\nVálasszon egy opciót: ");
                try
                {
                    int option = int.Parse(Console.ReadLine());
                    switch (option)
                    {
                        case 1:
                            Console.Clear();
                            Console.Write("1. Ajándék hozzáadása\n2. Ajándék törlése\n3. Ajándéklista megtekintése\n4. Vissza\n\nVálasszon egy opciót: ");
                            int optionX = int.Parse(Console.ReadLine());
                            switch (optionX)
                            {
                                case 1:
                                    Console.Clear();
                                    addGift();
                                    break;
                                case 2:
                                    Console.Clear();
                                    removeGift();
                                    break;
                                case 3:
                                    Console.Clear();
                                    Console.Write("1. Ajándékok megtekintése\n2. Ajándékok kategorizálása\n\nVálasszon egy opciót: ");
                                    int optionX2 = int.Parse(Console.ReadLine());
                                    switch (optionX2)
                                    {
                                        case 1:
                                            Console.Clear();
                                            viewGifts();
                                            break;
                                        case 2:
                                            Console.Clear();
                                            categorizeGifts();
                                            break;
                                        case 3:
                                            Console.Clear();
                                            break;
                                        default:
                                            Console.Clear();
                                            Console.WriteLine("Érvénytelen érték!\n");
                                            break;
                                    }
                                    break;
                                case 4:
                                    Console.Clear();
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Érvénytelen érték!\n");
                                    break;
                            }
                            break;
                        case 2:
                            Console.Clear();
                            Console.Write("1. Költségvetés beállítása\n2. Költségvetés megtekintése\n3. Vissza\n\nVálasszon egy opciót: ");
                            int optionY = int.Parse(Console.ReadLine());
                            switch (optionY)
                            {
                                case 1:
                                    Console.Clear();
                                    setBudget();
                                    break;
                                case 2:
                                    Console.Clear();
                                    checkBudget();
                                    break;
                                case 3:
                                    Console.Clear();
                                    break;
                                default:
                                    Console.Clear();
                                    break;
                            }

                            break;
                        case 3:
                            Console.Clear();
                            checkMostExpensive();
                            break;
                        case 4:
                            Console.Clear();
                            Console.WriteLine("Kellemes karácsonyt!");
                            Thread.Sleep(3000);
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Érvénytelen érték!\n");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Az opció csak szám lehet!\n");              
                }
            }
        }
        static void setBudget()
        {
            Console.Write("Adja meg az ajándékozási költségvetésed (Ft): ");
            try
            {
                int budget = int.Parse(Console.ReadLine());
                if (budget < 0)
                {
                    throw new Exception("A pénzed nem lehet negatív!\n");
                }
                money = budget;
                Console.Clear();
            }
            catch (FormatException)
            {
                Console.WriteLine("A pénz csak szám lehet!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void checkBudget()
        {
            Console.WriteLine($"A költségvetésed: {money}\n");
        }
        static void viewGifts()
        {
            if (giftName.Count == 0)
            {
                Console.WriteLine("Az ajándéklista üres!\n");
                return;
            }
            for (int i = 0; i < giftName.Count; i++)
            {
                Console.WriteLine($"Ajándék neve: {giftName[i]}\nAjándék ára: {giftPrice[i]}\nAjándék kategóriája: {giftCategory[i]}\n");
            }
        }
        static void categorizeGifts()
        {
            if (giftName.Count == 0)
            {
                Console.WriteLine("Az ajándéklista üres!\n");
                return;
            }

            List<string> uniqueCategories = new List<string>();
            foreach (string category in giftCategory)
            {
                if (!uniqueCategories.Contains(category))
                {
                    uniqueCategories.Add(category);
                }
            }
            foreach (string category in uniqueCategories)
            {
                Console.WriteLine($"Kategória: {category}");
                for (int i = 0; i < giftCategory.Count; i++)
                {
                    if (giftCategory[i] == category)
                    {
                        Console.WriteLine($"  - {giftName[i]}");
                    }
                }
            }
            Console.WriteLine();
        }
        static void addGift()
        {
            try
            {
                Console.Write("Adja meg az ajándék nevét: ");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new Exception("A név nem lehet üres!\n");
                }
                giftName.Add(name);
                Console.Write("Adja meg az ajándék kategóriáját: ");
                string category = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(category))
                {
                    throw new Exception("A kategória nem lehet üres!\n");
                }
                giftCategory.Add(category);
                Console.Write("Adja meg az ajándék árát: ");
                int price = int.Parse(Console.ReadLine());
                if (price < 0)
                {
                    throw new Exception("Az ajándék ára nem lehet negatív!\n");
                }
                if (price > money)
                {
                    throw new Exception("Az ajándék meghaladja a költségvetésed!\n");
                }
                giftPrice.Add(price);
                int budget = money - price;
                money = budget;
            }
            catch (FormatException)
            {
                Console.WriteLine("Érvénytelen bemenet!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void removeGift()
        {
            try
            {
                Console.Write("Adja meg az ajándék nevét: ");
                string name = Console.ReadLine();
                if (!giftName.Contains(name))
                {
                    Console.Clear();
                    Console.WriteLine("Az ajándék nem található a listában!\n");
                    return;
                }
                int index = giftName.IndexOf(name);
                money += giftPrice[index];
                giftName.RemoveAt(index);
                giftCategory.RemoveAt(index);
                giftPrice.RemoveAt(index);
            }
            catch (FormatException)
            {
                Console.WriteLine("Az ajándék neve nem lehet csak betű!\n");
            }
        }
        static void checkMostExpensive()
        {
            if (giftPrice.Count == 0)
            {
                Console.WriteLine("Az ajándéklista üres!\n");
                return;
            }
            Console.WriteLine($"A legdrágább ajándék: {giftName[giftPrice.IndexOf(giftPrice.Max())]} ({giftPrice.Max()} Ft)");
        }
    }
}
