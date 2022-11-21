using FluentResults;
using MaxiCrush.Api.Common;
using MaxiCrush.Application.Common.Errors;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MaxiCrush.Api.Extensions;

public static class UtilityExtensions
{
    public static AuthorizationOptions AddPermissionPolicy(this AuthorizationOptions options, string permissionName)
    {
        options.AddPolicy(permissionName, policy => policy.RequireClaim(Permissions.BasePrefix, permissionName));
        
        return options;
    }

    public static Result<Guid> GetUserId(this ClaimsPrincipal principal)
    {
        var claimValue = principal.Claims.FirstOrDefault(x => x.Type == "id")?.Value;

        if (claimValue == null)
            return Result.Fail(AppErrors.Authentication.Unauthorized);

        if (!Guid.TryParse(claimValue, out Guid userId))
            return Result.Fail(AppErrors.Authentication.Unauthorized);

        return userId;
    }
}
