using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNHNhi_2131209002_Lab2.Models
{
    public class BorrowTransaction : Transaction
    {
        public Book BookBorrowed {  get; set; }

        public BorrowTransaction (Member member, Book book) : base(member)
        {
            BookBorrowed = book ?? throw new ArgumentNullException(nameof(book));
        }
        public override void Execute()
        {
            Console.WriteLine($"\n[Borrow] {Member.Name} -> '{BookBorrowed.Title}'");

            // check available
            if (BookBorrowed.CopiesAvailable <= 0)
            {
                Console.WriteLine("Book is out of stock");
                return;
            }

            // check max
            if (Member.BorrowedBooks.Count >= Member.MaxBooksAllowed)
            {
                Console.WriteLine($"{Member.Name} reached the limit ({Member.MaxBooksAllowed}).");
                return;
            }

            // update data
            BookBorrowed.CopiesAvailable--;
            Member.BorrowedBooks.Add(BookBorrowed);

            Console.WriteLine($"Borrowed successfully. Remaining copies: {BookBorrowed.CopiesAvailable}");
        }
    }
}
