using System;
using System.Collections.Generic;
using System.Linq;

namespace PNHNhi_2131209002_Lab2.Models
{
    public class Library
    {
        public string LibraryName { get; set; }
        public List<Book> Books { get; set; }
        public List<Member> Members { get; set; }
        public List<Transaction> TransactionHistory { get; set; }

        public Library()
        {
            LibraryName = "City Library";
            Books = new List<Book>();
            Members = new List<Member>();
            TransactionHistory = new List<Transaction>();
        }

        public Library(string libraryName, List<Book> books)
        {
            LibraryName = string.IsNullOrEmpty(libraryName) ? "Unnamed Library" : libraryName;
            Books = books ?? new List<Book>();
            Members = new List<Member>();
            TransactionHistory = new List<Transaction>();
        }

        public Library(Library other)
        {
            LibraryName = other.LibraryName;
            Books = new List<Book>(other.Books);
            Members = new List<Member>(other.Members);
            TransactionHistory = new List<Transaction>(other.TransactionHistory);
        }

        public void DisplayLibraryInfo()
        {
            int totalAvailable = Books.Sum(b => b.CopiesAvailable);
            Console.WriteLine($"   Library Name: {LibraryName}");
            Console.WriteLine($"   Total Books: {Books.Count}");
            Console.WriteLine($"   Total Copies Available: {totalAvailable}");
            Console.WriteLine($"   Total Members: {Members.Count}");
            Console.WriteLine($"   Total Transactions: {TransactionHistory.Count}");
        }
    }
}
