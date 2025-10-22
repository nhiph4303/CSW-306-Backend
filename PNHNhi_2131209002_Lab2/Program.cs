using System;
using System.Collections.Generic;
using PNHNhi_2131209002_Lab2.Models;

namespace PNHNhi_2131209002_Lab2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //1
            Console.WriteLine("---- Exercise 1 ----");
            Book b1 = new Book();
            Book b2 = new Book("978-0132350884", "Clean Code", "Robert C. Martin", 2008, 5);

            b1.DisplayInfo();
            b2.DisplayInfo();

            //2r) =====
            Console.WriteLine("\n---- Exercise 2 ----");
            Member m1 = new Member("M001", "Alice", "alice@example.com");
            PremiumMember m2 = new PremiumMember("P001", "Bob", "bob@example.com", DateTime.Now.AddMonths(6));

            m1.DisplayInfo();
            m2.DisplayInfo();

            //3
            Console.WriteLine("\n---- Exercise 3 ----");
            Book bookA = new Book("111", "C# in Depth", "Jon Skeet", 2019, 1);
            Member alice = new Member("M002", "Alice", "alice@example.com");
            PremiumMember bob = new PremiumMember("P002", "Bob", "bob@example.com", DateTime.Now.AddMonths(3));

            Transaction t1 = new BorrowTransaction(alice, bookA);
            Transaction t2 = new BorrowTransaction(bob, bookA);
            Transaction t3 = new ReturnTransaction(alice, bookA);

            t1.Execute(); //borrow success
            t2.Execute(); //out of stock
            t3.Execute(); //return success

            //4
            Console.WriteLine("\n---- Exercise 4 ----");
            Book bookB = new Book("222", "The Pragmatic Programmer", "Andrew Hunt", 1999, 2);
            PremiumMember premiumMember = new PremiumMember("P003", "Carol", "carol@example.com", DateTime.Now.AddMonths(12));
            Member regularMember = new Member("M003", "David", "david@example.com");

            List<Transaction> transactions = new List<Transaction>
            {
                new BorrowTransaction(regularMember, bookB),
                new BorrowTransaction(premiumMember, bookB),
                new BorrowTransaction(premiumMember, bookB), //hết sách
                new ReturnTransaction(regularMember, bookB),
                new BorrowTransaction(premiumMember, bookB)   //mượn lại
            };

            foreach (Transaction t in transactions)
            {
                t.Execute();
            }

            Console.WriteLine("\n---- Final Book Status ----");
            bookB.DisplayInfo();
            Console.WriteLine("\n---- Member Borrowed Books ----");
            Console.WriteLine($"Regular: {regularMember.BorrowedBooks.Count}");
            Console.WriteLine($"Premium: {premiumMember.BorrowedBooks.Count}");

            //5
            Console.WriteLine("\n---- Exercise 5 ----");

            Book s1 = new Book("333", "Refactoring", "Martin Fowler", 2018, 2);
            Book s2 = new Book("444", "Design Patterns", "GoF", 1994, 1);

            Member member1 = new Member("M004", "Eva", "eva@example.com");
            PremiumMember member2 = new PremiumMember("P004", "Frank", "frank@example.com", DateTime.Now.AddMonths(2));

            member1.BorrowBook(s1);
            member1.BorrowBook(s2);
            member2.BorrowBook(s1);
            member2.BorrowBook(s2); //out of stock

            member1.ReturnBook(s1);
            member2.BorrowBook(s1); //borrow again

            member1.PrintDetails();
            member2.PrintDetails();

            //6
            Console.WriteLine("\n---- Exercise 6 ----");

            //Danh sách sách mẫu
            List<Book> initialBooks = new List<Book>
            {
                new Book("111", "C# in Depth", "Jon Skeet", 2019, 3),
                new Book("222", "Clean Code", "Robert C. Martin", 2008, 2)
            };

            //constructor mặc định
            Library lib1 = new Library();
            lib1.Books.AddRange(initialBooks);
            lib1.Members.Add(new Member("M101", "Alice", "alice@mail.com"));
            lib1.TransactionHistory.Add(new BorrowTransaction(lib1.Members[0], lib1.Books[0]));

            Console.WriteLine("\n-- Library 1 (Default Constructor) --");
            lib1.DisplayLibraryInfo();

            //constructor có tham số
            Library lib2 = new Library("EIU Library", initialBooks);
            lib2.Members.Add(new PremiumMember("P202", "Bob", "bob@mail.com", DateTime.Now.AddMonths(12)));

            Console.WriteLine("\n-- Library 2 (Parameterized Constructor) --");
            lib2.DisplayLibraryInfo();

            //copy constructor
            Library lib3 = new Library(lib2);
            Console.WriteLine("\n-- Library 3 (Copied from Library 2) --");
            lib3.DisplayLibraryInfo();


            //7
            Console.WriteLine("\n---- Exercise 7 ----");

            NotificationService notify = new NotificationService();
            AdvancedNotificationService advNotify = new AdvancedNotificationService();

            //overload
            notify.SendNotification("System maintenance at midnight.");
            notify.SendNotification("Book 'Clean Code' is due tomorrow!", "Alice");
            notify.SendNotification("Library closed on weekends.", new List<string> { "Alice", "Bob", "Carol" });

            //overriding
            advNotify.SendNotification("Membership expires soon!");

            //8
            Console.WriteLine("\n---- Exercise 8: LibraryCard ----");

            Member memberA = new Member("M501", "Hanh Nhi", "hanhnhi@eiu.edu.vn");
            PremiumMember memberB = new PremiumMember("P502", "Ngoc Mai", "ngocmai@eiu.edu.vn", DateTime.Now.AddMonths(6));

            LibraryCard cardA = new LibraryCard("CARD001", memberA);
            LibraryCard cardB = new LibraryCard("CARD002", memberB);

            cardA.DisplayCardInfo();
            cardB.DisplayCardInfo();

            Console.WriteLine("\n-- Deactivating and renewing cards --");
            cardA.DeactivateCard();
            cardA.DisplayCardInfo();

            cardA.RenewCard();
            cardA.DisplayCardInfo();

            //9
            Console.WriteLine("\n---- Exercise 9 ----");

            BookClass bc1 = new BookClass("111", "Clean Code", "Robert C. Martin");
            BookClass bc2 = new BookClass("111", "Clean Code", "Robert C. Martin");

            BookRecord br1 = new BookRecord("111", "Clean Code", "Robert C. Martin");
            BookRecord br2 = new BookRecord("111", "Clean Code", "Robert C. Martin");

            //so sanh
            Console.WriteLine($"BookClass equality: {bc1 == bc2}");
            Console.WriteLine($"BookRecord equality: {br1 == br2}");

            //toString
            Console.WriteLine($"Class ToString(): {bc1}");
            Console.WriteLine($"Record ToString(): {br1}");

            //hashcode
            Console.WriteLine($"Class HashCode: {bc1.GetHashCode()}");
            Console.WriteLine($"Record HashCode: {br1.GetHashCode()}");

            //with
            BookRecord br3 = br1 with { Title = "Clean Coder" };
            Console.WriteLine($"Modified Record: {br3}");

            //mutability
            bc1.Title = "Refactoring Clean Code";

            Console.WriteLine("\n--- Final Comparison ---");
            Console.WriteLine($"BookClass (mutable): {bc1}");
            Console.WriteLine($"BookRecord (immutable): {br1}");


        }
    }
}
