using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNHNhi_2131209002_Lab2.Models
{
    public interface IMemberActions
    {
        void BorrowBook(Book book);
        void ReturnBook(Book book);
    }
}
