using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNHNhi_2131209002_Lab2.Models
{
    public class ReturnTransaction : Transaction
    {

        public Book BookReturned { get; set; }

        public ReturnTransaction (Book book, Member member) : base(member)
        {
            BookReturned = book ?? throw new ArgumentNullException(nameof(book));
        }

        public override void Execute()
        {
            Console.WriteLine($"\n[Return] {Member.Name} → '{BookReturned.Title}'");

            // check actually borrowed
            if (!Member.BorrowedBooks.Contains(BookReturned)) {
                Console.WriteLine("Member did not borrow this book!");
                return;
            }

            //update data
            Member.BorrowedBooks.Remove(BookReturned);
            BookReturned.CopiesAvailable++;

            Console.WriteLine($"Returned successfully. Available copies: {BookReturned.CopiesAvailable}");
        }
    }
}
