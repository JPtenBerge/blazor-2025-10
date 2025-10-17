using System.Reflection.Metadata;
using Demo.Shared.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Demo.Shared.Auth;

public class BobAuthorizationHandler : AuthorizationHandler<BobAuthorRequirement, Snack>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        BobAuthorRequirement requirement, Snack snack)
    {
        if ((context.User.Identity?.Name?.StartsWith("Bob") ?? false) && snack.Name.StartsWith("C"))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

public class BobAuthorRequirement : IAuthorizationRequirement
{
}