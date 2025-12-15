using Microsoft.AspNetCore.Authorization;

namespace LibraryManagementSystem.Requirements.Handlers
{
    public class MinimumMembershipHandler : AuthorizationHandler<MinimumMembershipRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            MinimumMembershipRequirement requirement)
        {
            var registeredAtClaim = context.User.FindFirst("register_at");
            if (registeredAtClaim == null)
                return Task.CompletedTask;

            var registeredAt = DateTime.Parse(registeredAtClaim.Value);
            var days = (DateTime.UtcNow - registeredAt).TotalDays;
            Console.WriteLine($"User registered at: {registeredAt}, Days: {days}, Minday: {requirement.MinimumDays}, compare: {days.CompareTo(requirement.MinimumDays)}");
            if (days.CompareTo(requirement.MinimumDays) > 0)
            {
                Console.WriteLine("MinimumMembershipRequirement succeeded");
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
