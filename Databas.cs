using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace ConsoleApp1
{
    internal class LibraryData
    {
        public List<Författare> Authors { get; set; }
        public List<Book> Books { get; set; }
        public int BookIds = 0;
        public int FörfattareIds = 0;

        public LibraryData() {
            Books = new List<Book>();
            Authors = new List<Författare>();
        }
    }

    internal class Databas
    {
        private static LibraryData databas;
        public static void Load()
        {
            try
            {
                using (StreamReader reader = new StreamReader("LibraryData.json"))
                {
                    var options = new JsonSerializerOptions { IncludeFields = true };
                    databas = JsonSerializer.Deserialize<LibraryData>(reader.ReadToEnd(), options);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Error loading json data. Starting a new one");
                databas = new LibraryData();
            }

        }

        public static void Save()
        {
            var options = new JsonSerializerOptions { IncludeFields = true };

            string data = JsonSerializer.Serialize(databas, options);
            try
            {
                using (StreamWriter writer = new StreamWriter("LibraryData.json"))
                {
                    writer.Write(data);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.StackTrace);
            }
        }

        public static void InsertBook(Book book)
        {
            databas.Books.Add(book);
        }

        public static void InsertAuthor(Författare författare)
        {
            databas.Authors.Add(författare);
        }

        public static void RemoveBook(Book book)
        {
            databas.Books.Remove(book);
        }

        public static void RemoveAuthor(Författare frattare)
        {
            databas.Authors.Remove(frattare);
        }

        public static Book? FindBook(int id)
        {
            foreach(Book b in databas.Books)
            {
                if (b.Id == id)
                {
                    return b;
                }
            }
            return null;
        }

        public static Författare? FindAuthor(int id)
        {
            foreach(Författare f in databas.Authors)
            {
                if (f.Id == id)
                {
                    return f;
                }
            }
            return null;
        }

        public static IEnumerable<Book> Search(int filter, string filterText, int betyg, int sort)
        {
            IEnumerable<Book> books;

            if (filter == 1)
            {
                books = from book in databas.Books where book.Genre.StartsWith(filterText) select book;
            }
            else if (filter == 2)
            {
                books = from book in databas.Books where book.BookFörfattare.StartsWith(filterText) select book;
            }
            else
            {
                int ar = 0;
                if (filterText.All(char.IsDigit))
                {
                    ar = Convert.ToInt32(filterText);
                }

                books = from book in databas.Books where book.Publiceringsår == ar select book;
            }


            List<Book> result = books.ToList().FindAll(book =>
            {
                double avg = 0;
                if (book.Recensioner.Count > 0)
                {
                    avg = book.Recensioner.Average();
                }
                return betyg >= avg;
            });
           
            

            if (sort == 1)
            {
                books = result.OrderBy((book) => book.Publiceringsår);
            } else if (sort == 2)
            {
                books = result.OrderBy(book => book.Title);
            } else
            {
                books = result.OrderBy(book => book.BookFörfattare);
            }
            return books;
        }

        public static List<Book> GetBooks()
        {
            return databas.Books;
        }

        public static List<Författare> GetAuthors()
        {
            return databas.Authors;
        }

        public static int BookNextId()
        {
            return databas.BookIds++;
        }

        public static int AuthorNextId()
        {
            return databas.FörfattareIds++;
        }

    }
}
