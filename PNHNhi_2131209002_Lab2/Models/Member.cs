using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNHNhi_2131209002_Lab2.Models
{
    public class Member
    {
        private string memberID;
        private string name;
        private string email;

        public string MemberID
        {
            get => memberID;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Member can not empty!");
                }
                memberID = value.Trim();
            }
        }

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Name can not be empty!");
                }
                name = value.Trim();
            }
        }

        public string Email
        {
            get => email;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Email can not be empty!");
                }
                if (!value.Contains("@"))
                {
                    throw new ArgumentException("Email is invalid (must contain @).");
                }
                email = value.Trim();
            }
        }

        // max and list
        public int MaxBooksAllowed { get; set; } = 3;

        public List<Book> BorrowedBooks { get; } = new List<Book>();

        public Member(string memberID, string name, string email)
        {
            this.MemberID = memberID;
            this.Name = name;
            this.Email = email;
        }

        public Member() {}

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Member ID: {MemberID}, Name: {Name}, Email: {Email}, Max Books: {MaxBooksAllowed}, Borrow: {BorrowedBooks.Count}");
        }
    }
}
