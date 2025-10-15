using System.Security.Cryptography.X509Certificates;
using System;
using PNHNhi_2131209002_Lab2.Models;
using System.Runtime.CompilerServices;

namespace PNHNhi_2131209002_Lab2{
    public class Program
    {
        public static void Main(string[] args)
        {
            Book b1 = new Book();
            Book b2 = new Book("978-0132350884", "Clean Code", "Robert C. Martin", 2008, 5);

            b1.DisplayInfo();
            b2.DisplayInfo();
        }
    }
}
