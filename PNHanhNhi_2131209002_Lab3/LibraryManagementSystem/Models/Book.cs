namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }

        public Book()
        {
            Id = Guid.NewGuid().ToString();
        }


        public override string ToString()
        {
           return $"{Id}: \"{Title}\" - {Author} ({Genre})";
        }

    }
}
