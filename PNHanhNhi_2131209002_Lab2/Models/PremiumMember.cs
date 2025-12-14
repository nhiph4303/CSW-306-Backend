using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNHNhi_2131209002_Lab2.Models
{
    public class PremiumMember : Member
    {
        public PremiumMember() { }

        public DateTime MembershipExpiry { get; set; }

        public PremiumMember(string memberID, string name, string email, DateTime membershipExpiry)
            : base(memberID, name, email)
        {
            this.MembershipExpiry = membershipExpiry;
            this.MaxBooksAllowed = 10;
        }

        public override void BorrowBook(Book book)
        {
            Console.WriteLine($"\n[{Name}] (Premium) borrowing '{book.Title}'...");

            if (DateTime.Now > MembershipExpiry)
            {
                Console.WriteLine($"Membership for {Name} has expired!");
                return;
            }

            base.BorrowBook(book); //call default logic in member
        }

        public override void ReturnBook(Book book)
        {
            Console.WriteLine($"\n[{Name}] (Premium) returning '{book.Title}'...");
            base.ReturnBook(book);
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Membership Expiry: {MembershipExpiry:d}");
        }
    }
}
