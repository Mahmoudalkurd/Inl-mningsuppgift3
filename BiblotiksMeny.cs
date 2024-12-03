namespace ConsoleApp1
{
    public class BiblotiksMeny
    {
        
        public void AddNewBook()
        {
            Console.WriteLine("Skriv titeln på boken som du vill lägga till: ");
            string newBookTitle = Console.ReadLine()!;
            Console.WriteLine("vilken gernre ?");
            string gerner = Console.ReadLine()!;

            int publiceringsår;
            while (true)
            {
                Console.WriteLine("vilken publiceringsår ?");
                string årstring = Console.ReadLine()!;

                if (årstring.All(char.IsDigit))
                {
                    publiceringsår = Convert.ToInt32(årstring);
                    break;
                }
                else
                {
                    Console.WriteLine("Ogiltig inmatning. Vänligen ang endast siffror för publiceringsår.");

                }
            }

            Författare? författare;
            while (true)
            {
                foreach (Författare author in Databas.GetAuthors())
                {
                    Console.WriteLine($"{author.Id} - {author.Namn}");
                }
                Console.WriteLine("vilken författaren?");
                string authorString = Console.ReadLine()!;
                if (authorString.All(char.IsDigit))
                {
                    int id = Convert.ToInt32(authorString);
                    författare = Databas.FindAuthor(id);
                    if (författare != null)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ogiltig inmatning");
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltig inmatning");
                }

            }

            int newBookISBN;

            while (true)
            {
                Console.WriteLine("Skriv ISBN-numret på boken som du vill lägga till: ");
                string newBookISBNStrin = Console.ReadLine()!;

                if (newBookISBNStrin.All(char.IsDigit))
                {
                    newBookISBN = Convert.ToInt32(newBookISBNStrin);
                    break;
                }
                else
                {
                    Console.WriteLine("Ogiltig inmatning. Vänligen ange endast siffror för ISBN.");

                }
            }



            if (!BookTitleExists(newBookTitle) && !BookISBNExists(newBookISBN))
            {
                Book newBook = new Book(Databas.BookNextId(), newBookTitle, newBookISBN, gerner, publiceringsår, författare.Namn);
                Databas.InsertBook(newBook);
                Console.WriteLine($"Du har lagt till en ny bok - Titel: {newBookTitle}, Författare: {författare.Namn}, ISBN: {newBookISBN}");
            }
        }

        public void AddNewFörfattare()
        {
            Console.WriteLine("Skriv författaresnamn som du vill lägga till: ");
            string namn = Console.ReadLine()!;
            Console.WriteLine("vilken land ?");
            string land = Console.ReadLine()!;
            Boolean authorAlreadyExists = false;
            foreach (Författare f in Databas.GetAuthors())
            {
                if (f.Namn.Equals(namn))
                {
                    Console.WriteLine("redan exists");
                    authorAlreadyExists = true;
                }
            }

            if (!authorAlreadyExists) {
                Författare författare = new Författare(namn, Databas.AuthorNextId(), land);
                Databas.InsertAuthor(författare);
                Console.WriteLine($"Author {namn} created");
            }
        }

        public void EditBook()
        {
            int bookId;

            while (true)
            {
                Console.WriteLine("Enter the book id you wish to edit: ");
                string idString = Console.ReadLine()!;

                if (idString.All(char.IsDigit))
                {
                    bookId = Convert.ToInt32(idString);
                    if (bookId >= 0)
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Book id must be a number and higher than 0.");
                }
            }

            Book? book = Databas.FindBook(bookId);

            if (book == null)
            {
                Console.WriteLine("Book does not exists");
                return;
            }

            

            string newTitle;

            while (true)
            {
                Console.WriteLine($"Skriv ny titeln ({book.Title}): ");
                newTitle = Console.ReadLine()!;
                if(newTitle.Equals(book.Title) || !BookTitleExists(newTitle))
                {
                    break;
                } else
                {
                    Console.WriteLine("Book title already exists");
                }
            }

            book.Title = newTitle;

            Console.WriteLine($"Skriv ny titeln ({book.Title}): ");
            string newBookTitle = Console.ReadLine()!;

            int newBookISBN;

            while (true)
            {
                Console.WriteLine($"Skriv nytt ISBN-numret ({book.ISBN}): ");
                string newBookISBNStrin = Console.ReadLine()!;

                if (newBookISBNStrin.All(char.IsDigit))
                {
                    newBookISBN = Convert.ToInt32(newBookISBNStrin);
                    if (newBookISBN.Equals(book.ISBN) || !BookISBNExists(newBookISBN))
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltig inmatning. Vänligen ange endast siffror för ISBN.");

                }
            }
            book.ISBN = newBookISBN;
            Console.WriteLine($"vilken gernre ({book.Genre}) ?");
            string gerner = Console.ReadLine()!;

            int publiceringsår;
            while (true)
            {
                Console.WriteLine($"vilken publiceringsår ({book.Publiceringsår}) ?");
                string årstring = Console.ReadLine()!;

                if (årstring.All(char.IsDigit))
                {
                    publiceringsår = Convert.ToInt32(årstring);
                    break;
                }
                else
                {
                    Console.WriteLine("Ogiltig inmatning. Vänligen ang endast siffror för publiceringsår.");

                }
            }

            book.Publiceringsår = publiceringsår;

            Författare? författare;
            while (true)
            {
                foreach(Författare author in Databas.GetAuthors())
                {
                    Console.WriteLine($"{author.Id} - {author.Namn}");
                }
                Console.WriteLine("vilken författaren?");
                string authorString = Console.ReadLine()!;
                if (authorString.All(char.IsDigit))
                {
                    int id = Convert.ToInt32(authorString);
                    författare = Databas.FindAuthor(id);
                    if (författare != null)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ogiltig inmatning");
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltig inmatning");
                }

            }

            book.BookFörfattare = författare.Namn;


            Console.WriteLine($"Book {book.Title} has been upadted");
        }

        public void EditFörfattare()
        {
            int authorId;

            while (true)
            {
                Console.WriteLine("Enter the author id you wish to edit: ");
                string idString = Console.ReadLine()!;

                if (idString.All(char.IsDigit))
                {
                    authorId = Convert.ToInt32(idString);
                    if (authorId >= 0)
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Author id must be a number and higher than 0.");
                }
            }

            Författare? författare = Databas.FindAuthor(authorId);

            if (författare == null)
            {
                Console.WriteLine("Author does not exists");
                return;
            }

            Boolean authorAlreadyExists = false;
            while (true)
            {
                Console.WriteLine($"Skriv ny författaresnamn ({författare.Namn}): ");
                string namn = Console.ReadLine()!;
                if (namn.Equals(författare.Namn))
                {
                    break;
                }
                foreach (Författare f in Databas.GetAuthors())
                {
                    if (f.Namn.Equals(namn))
                    {
                        Console.WriteLine("redan exists");
                        authorAlreadyExists = true;
                    }
                }
                if (!authorAlreadyExists)
                {
                    författare.Namn = namn;
                    break;
                }
            }
            Console.WriteLine($"Ny land ({författare.Land})");
            string land = Console.ReadLine()!;
            författare.Land = land;
            Console.WriteLine($"Author {författare.Namn} updated");

        }

        public void RemoveBook()
        {
            int bookId;

            while (true)
            {
                Console.WriteLine("Enter the book id you wish to edit: ");
                string idString = Console.ReadLine()!;

                if (idString.All(char.IsDigit))
                {
                    bookId = Convert.ToInt32(idString);
                    if (bookId >= 0)
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Book id must be a number and higher than 0.");
                }
            }

            Book book;

            if (Databas.FindBook(bookId) == null)
            {
                Console.WriteLine("Book id does not exists");
                return;
            }
            book = Databas.FindBook(bookId)!;

            Databas.RemoveBook(book);
        }

        public void RemoveAuthor()
        {
            int authorId;

            while (true)
            {
                Console.WriteLine("Enter the author id you wish to edit: ");
                string idString = Console.ReadLine()!;

                if (idString.All(char.IsDigit))
                {
                    authorId = Convert.ToInt32(idString);
                    if (authorId >= 0)
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Author id must be a number and higher than 0.");
                }
            }
            Författare författare;
            if (Databas.FindAuthor(authorId) == null)
            {
                Console.WriteLine("Author id does not exists");
                return;
            }

            författare = Databas.FindAuthor(authorId)!;
            Databas.RemoveAuthor(författare);
        }
        public void BookMenuOptions()
        {
            Console.WriteLine("\nVälj ett alternativ:");
            Console.WriteLine("1 - Lägg till en bok");
            Console.WriteLine("2 - lägg till en ny författare");
            Console.WriteLine("3 - uppdatera bokdetaljar");
            Console.WriteLine("4 - uppdatera författaredetaljar");
            Console.WriteLine("5 - Ta bort en bok");
            Console.WriteLine("6 - Ta bort en författare");
            Console.WriteLine("7 - Lista alla böcker och författare");
            Console.WriteLine("8 - Söka och filtera alla böcker");
            Console.WriteLine("9 - Lägga till ett betyg till en bok");
            Console.WriteLine("10 - Avsluta och spara detta");
        }

        public void ListAll()
        {
            Console.WriteLine("Books: [id]Title(ISBN) - ´Publiceringsår - Författare / Grade");
            foreach (Book b in Databas.GetBooks())
            {
                double grade = 0;
                if (b.Recensioner.Count > 0)
                {
                    grade = b.Recensioner.Average();
                }
                Console.WriteLine($"[{b.Id}]{b.Title} ({b.ISBN}) - {b.Publiceringsår} - {b.BookFörfattare} / {grade}");
            }

            Console.WriteLine("Authors: [id]Namn - Land");
            foreach (Författare f in Databas.GetAuthors())
            {
                Console.WriteLine($"[{f.Id}]{f.Namn} - {f.Land}");
            }
        }

        public void SokÖchFiltrera()
        {
            int filter;
            while (true)
            {
                Console.WriteLine("Filter, 1-genre 2-författare,3-publiceringar:");
                string filterString = Console.ReadLine()!;
                if (filterString.All(char.IsDigit))
                {
                    filter = Convert.ToInt32(filterString);
                    if (filter > 0 && filter <= 3)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Dålit filter");
                    }
                }
            }
            Console.WriteLine("Filter text");
            string filterText = Console.ReadLine()!;

            int betyg;
            while (true)
            {
                Console.WriteLine("Betyg filter(0 to 5)? ");
                string betygString = Console.ReadLine()!;
                if (betygString.All(char.IsDigit))
                {
                    betyg = Convert.ToInt32(betygString);
                    if (filter >= 0 && filter <= 5)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Grade not valid");
                    }
                }
            }

            int sort;
            while (true)
            {
                Console.WriteLine("Sort 1-publiceringar 2-titel,3-författare:");
                string sortString = Console.ReadLine()!;
                if (sortString.All(char.IsDigit))
                {
                    sort = Convert.ToInt32(sortString);
                    if (sort > 0 && sort <= 3)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Dålit sort");
                    }
                }
            }
            IEnumerable<Book> books = Databas.Search(filter, filterText, betyg, sort);
            Console.WriteLine("Books: [id]Title(ISBN) - ´Publiceringsår - Författare / Grade");
            foreach (Book b in books)
            {
                double grade = 0;
                if (b.Recensioner.Count > 0)
                {
                    grade = b.Recensioner.Average();
                }
                Console.WriteLine($"[{b.Id}]{b.Title} ({b.ISBN}) - {b.Publiceringsår} - {b.BookFörfattare} / {grade}");
            }

        }

        public void BetygBok()
        {
            int bookId;

            while (true)
            {
                Console.WriteLine("Enter the book id you wish to edit: ");
                string idString = Console.ReadLine()!;

                if (idString.All(char.IsDigit))
                {
                    bookId = Convert.ToInt32(idString);
                    if (bookId >= 0)
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Book id must be a number and higher than 0.");
                }
            }

            Book book;

            if (Databas.FindBook(bookId) == null)
            {
                Console.WriteLine("Book id does not exists");
                return;
            }
            book = Databas.FindBook(bookId)!;

            int grade;
            while (true)
            {
                Console.WriteLine($"Grade book {book.Title} from 1 to 5");
                string gradeString = Console.ReadLine()!;
                if (gradeString.All(char.IsDigit))
                {
                    grade = Convert.ToInt32(gradeString);
                    if (grade > 0 && grade <= 5)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Grade must be between 1 and 5");
                    }
                }
            }
            book.Recensioner.Add(grade);
        }

        private Boolean BookTitleExists(string title)
        {
            foreach (Book book in Databas.GetBooks())
            {
                if (book.Title.Equals(title) )
                {
                    Console.WriteLine("Den här bokentiteln finns redan i biblioteket.");
                    return true;
                }
            }
            return false;
        }

        private Boolean BookISBNExists(int isbn)
        {
            foreach (Book book in Databas.GetBooks())
            {
                if (book.ISBN == isbn)
                {
                    Console.WriteLine("Den här ISBN-numret finns redan i biblioteket.");
                    return true;
                }
            }
            return false;
        }
            

    }
}