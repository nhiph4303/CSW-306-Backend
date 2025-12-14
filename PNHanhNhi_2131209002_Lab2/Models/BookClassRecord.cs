using System;

namespace PNHNhi_2131209002_Lab2.Models
{
    //class
    public class BookClass
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public BookClass(string isbn, string title, string author)
        {
            ISBN = isbn;
            Title = title;
            Author = author;
        }

        public override string ToString()
        {
            return $"[BookClass] {Title} by {Author} (ISBN: {ISBN})";
        }
    }

    //record
    public record BookRecord(string ISBN, string Title, string Author);
}
