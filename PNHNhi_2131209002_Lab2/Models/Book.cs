using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNHNhi_2131209002_Lab2.Models
{
    public class Book
    {
        private string isbn;
        private string title;
        private string author;
        private int year;
        private int copiesAvailable;

        public string ISBN
        {
            get => isbn;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("ISBN can not be empty!");
                }
                isbn = value;
            }
        }

        public string Title
        {
            get => title;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Title can not be empty!");
                }
                title = value;
            }
        }

        public string Author
        {
            get => author;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Author can not be empty!");
                }
                author = value;
            }
        }

        public int Year
        {
            get => year;
            set
            {
                if (value < 1000 || value > DateTime.Now.Year)
                {
                    throw new ArgumentException("Year must be between 1000 and current year!");
                }
                year = value;
            }
        }

        public int CopiesAvailable
        {
            get => copiesAvailable;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Copies available can not be < 0!");
                }
                copiesAvailable = value;
            }
        }
        public Book()
        {
            ISBN = "Unknown";
            Title = "Untitled";
            Author = "Unknown";
            Year = DateTime.Now.Year;
            CopiesAvailable = 0;
        }
        public Book(string isbn, string title, string author, int year, int copiesAvailable)
        {
            this.ISBN = isbn;
            this.Title = title;
            this.Author = author;
            this.Year = year;
            this.CopiesAvailable = copiesAvailable;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"ISBN:{ISBN},Title:{Title}, Author:{Author}, Year:{Year}, CopiesAvailble:{copiesAvailable}");
        }
    }
}
