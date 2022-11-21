using FluentResults;
using System.Net;

namespace MaxiCrush.Application.Common.Errors;

public static class AppErrors
{
    public static readonly ResultError Unexpected = new("App.Unexpected", "Unexpected behavior", HttpStatusCode.BadRequest);

    public static class Authentication
    {
        public static readonly ResultError InvalidCredentials = new("Authentication.InvalidCredentials", "Invalid credentials provided", HttpStatusCode.Unauthorized);
        public static readonly ResultError DuplicateEmail = new("Authentication.DuplicateEmail", "Email already exists.", HttpStatusCode.Conflict);
        public static readonly ResultError Unauthorized = new("Authentication.Unauthorized", "You're not authenticated correctly", HttpStatusCode.Unauthorized);

        public static readonly ResultError NoConfirmationToken = new("Authentication.Confirmation.NotFound", "No confirmation token linked to the provided informations.", HttpStatusCode.BadRequest);
        public static readonly ResultError WrongConfirmationCode = new("Authentication.Confirmation.WrongCode", "Wrong confirmation code provided", HttpStatusCode.BadRequest);
        public static readonly ResultError Confirmation_UserAlreadyExist = new("Authentication.Confirmation.UserAlreadyExist", "Cannot create a confirmation code because this user is already registered.", HttpStatusCode.Unauthorized);
    }

    public static class Users
    {
        public static readonly ResultError RoleNotFound = new("Users.RoleNotFound", "This role has not been found", HttpStatusCode.NotFound);
        public static readonly ResultError NotFound = new("Users.NotFound", "No user found with the provided informations.", HttpStatusCode.NotFound);
        public static readonly ResultError RoleAlreadyGiven = new("Users.RoleAlreadyExist", "This role has already been added to this user", HttpStatusCode.BadRequest);
        public static readonly ResultError NotBanned = new("Users.NotBanned", "You're trying to unban a user that is not banned.", HttpStatusCode.Forbidden);
    }

    public static class Roles
    {
        public static readonly ResultError NotFound = new("Roles.NotFound", "No role found with the provided informations.", HttpStatusCode.NotFound);
        public static readonly ResultError DuplicateName = new("Roles.DuplicateName", "Role with this name already exists.", HttpStatusCode.Conflict);
        public static readonly ResultError NoDefaultRole = new("Roles.NoDefaultRole", "No default role was found in the database", HttpStatusCode.NotFound);
        public static readonly ResultError CannotDeleteDefaultRole = new("Roles.CannotDeleteDefaultRole", "Impossible to delete the default user role", HttpStatusCode.Forbidden);
    }

    public static class Permissions
    {
        public static readonly ResultError InsufficientPermission = new("Permissions.Insufficient", "You don't have enough permissions to perform this action", HttpStatusCode.Forbidden);
        public static readonly ResultError DuplicateName = new("Permissions.DuplicateName", "Permission with this name already exists.", HttpStatusCode.Conflict);
        public static readonly ResultError NotFound = new("Permissions.NotFound", "No permission found with the provided informations.", HttpStatusCode.NotFound);
    }
}
