namespace MaxiCrush.Api.Common;

public static class Permissions
{
    public const string BasePrefix = "perms"; 

    public static class Users
    {
        public const string Prefix = BasePrefix + ".users";

        public const string SetRole = Prefix + ".set_role";
        public const string Queries = Prefix + ".queries";
        public const string Commands = Prefix + ".commands";
    }

    public static class Permission
    {
        public const string Prefix = BasePrefix + ".permissions";

        public const string Queries = Prefix + ".queries";
        public const string Commands = Prefix + ".commands";
    }

    public static class Roles
    {
        public const string Prefix = BasePrefix + ".roles";

        public const string Queries = Prefix + ".queries";
        public const string Commands = Prefix + ".commands";
    }
}
