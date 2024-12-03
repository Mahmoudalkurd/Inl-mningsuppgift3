namespace ConsoleApp1
{
    internal class Författare
    {
        public string Namn {  get; set; }
        public int Id { get; set; }
        public string Land {  get; set; }
        public Författare (string namn, int id, string land)
        {
            Namn = namn;
            Id = id;
            Land = land;
        }
    }
}
