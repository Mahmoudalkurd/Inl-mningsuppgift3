namespace ConsoleApp1 { 
    internal class Program : BiblotiksMeny

    {
        static void Main(string[] args)
        {
            BiblotiksMeny newBookManagerMethods = new BiblotiksMeny();
            Databas.Load();

            while (true)
            {
                newBookManagerMethods.BookMenuOptions();

                string chooseMenuOption = Console.ReadLine()!;

                switch (chooseMenuOption)
                {
                    case "1":
                        newBookManagerMethods.AddNewBook();

                        break;

                    case "2":
                        newBookManagerMethods.AddNewFörfattare();

                        break;

                    case "3":
                        newBookManagerMethods.EditBook();

                        break;

                    case "4":
                        newBookManagerMethods.EditFörfattare();

                        break;

                    case "5":

                       newBookManagerMethods.RemoveBook();

                        break;

                    case "6":
                        newBookManagerMethods.RemoveAuthor();

                        break;

                    case "7":
                        newBookManagerMethods.ListAll();
                        break;
                    case "8":
                        newBookManagerMethods.SokÖchFiltrera();
                        break;
                    case "9":
                        newBookManagerMethods.BetygBok();
                        break;

                    case "10":
                        Databas.Save();
                        return;

                    default:
                        Console.WriteLine("Felaktigt val, försök igen. \n");
                        break;
                }


            }

        }
    }
}
