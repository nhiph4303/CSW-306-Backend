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

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"[Premium] Expiry: {MembershipExpiry:yyyy-MM-dd} | MaxBooksAllowed : {MaxBooksAllowed}");
        }
    }
}
