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
        private int maxBooksAllowed;
        private List<Book> borrwedBooks;

        public string MemberID
        {
            get => memberID;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Member can not empty!");
                }
                memberID = value;
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
                name = value;
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
                email = value;
            }
        }

        public void DisplayInfo()
        {

        }
    }
}
