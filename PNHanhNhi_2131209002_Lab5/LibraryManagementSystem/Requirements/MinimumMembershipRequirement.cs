using Microsoft.AspNetCore.Authorization;

namespace LibraryManagementSystem.Requirements
{
    public class MinimumMembershipRequirement : IAuthorizationRequirement
    {
        public int MinimumDays { get; }
        public MinimumMembershipRequirement(int minimumDays)
        {
            MinimumDays = minimumDays;
        }
    }
}
