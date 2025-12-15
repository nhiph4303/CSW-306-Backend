using Microsoft.AspNetCore.Authorization;

namespace LibraryManagementSystem.Requirements.Handlers
{
    public class ActiveUserHandler : AuthorizationHandler<ActiveUserRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ActiveUserRequirement requirement)
        {
            var isActiveClaim = context.User.FindFirst("is_active");

            if (isActiveClaim != null &&
                bool.TryParse(isActiveClaim.Value, out bool isActive) &&
                isActive)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
