namespace ConsoleApp1
{
    internal class Book
    {
        public string Title { get; set; }
      
        public int ISBN { get; set; }
        public int Id { get; set; }
        public string Genre { get; set; }
        public int Publiceringsår { get; set; }

        public List<int> Recensioner { get; set; }

        public string BookFörfattare { get; set; }

        public Book(int id, string title, int isbn, string genre, int publiceringsår, string bookFörfattare)
        {
            Recensioner = [0];
            Id = id;
            ISBN = isbn;
            Title = title;
            Genre = genre;
            Publiceringsår = publiceringsår;
            BookFörfattare = bookFörfattare;
        }

    }
}
