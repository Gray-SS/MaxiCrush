namespace MaxiCrush.Contracts;

public static class ApiRoutes
{
    public const string Version = "v1";
    public const string BaseUrl = "api/" + Version;
    public const string TargetId = "/{id:guid}";

    public static class Authentication
    {
        public const string Endpoint = BaseUrl + "/auth";

        public const string Register = Endpoint + "/register";
        public const string Login = Endpoint + "/login";
        public const string Confirmation = Endpoint + "/confirmation";
        public const string VerifyConfirmation = Confirmation + "/verify";

        public static string VerifyConfirmationF(string email, string code)
            => VerifyConfirmation + $"?email={email}&code={code}";
    }

    public static class Users
    {
        public const string Endpoint = BaseUrl + "/users";

        public const string AuthorizedUser = Endpoint + "/me";
        public const string SetRole = Endpoint + TargetId + "/role";

        public const string DeleteUser = Endpoint + TargetId;
        public const string UpdateUser = Endpoint + TargetId;

        public const string GetUser = Endpoint + TargetId;
        public const string GetAllUser = Endpoint;

        public static string DeleteUserF(Guid id)
            => DeleteUser.Replace(TargetId, $"/{id}");
        public static string GetUserF(Guid id)
            => GetUser.Replace(TargetId, $"/{id}");
        public static string SetRoleF(Guid id)
            => SetRole.Replace(TargetId, $"/{id}");
        public static string UpdateUserF(Guid id)
            => UpdateUser.Replace(TargetId, $"/{id}");
    }

    public static class Roles
    {
        public const string Endpoint = BaseUrl + "/roles";

        public const string Create = Endpoint;
        public const string Update = Endpoint + TargetId;
        public const string Delete = Endpoint + TargetId;
        public const string GetRole = Endpoint + TargetId;
        public const string GetAllRoles = Endpoint;

        public static string UpdateF(Guid id)
            => Update.Replace(TargetId, $"/{id}");
        public static string DeleteF(Guid id)
            => Delete.Replace(TargetId, $"/{id}");
    }

    public static class Permissions
    {
        public const string Endpoint = BaseUrl + "/permissions";

        public const string Create = Endpoint;
        public const string Update = Endpoint;
        public const string Delete = Endpoint + TargetId;
        public const string GetPermission = Endpoint + TargetId;
        public const string GetAllPermissions = Endpoint;

        public static string DeleteF(Guid id)
            => Delete.Replace(TargetId, $"/{id}");
    }
}
