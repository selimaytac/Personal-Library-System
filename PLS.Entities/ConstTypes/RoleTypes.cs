namespace PLS.Entities.ConstTypes;

public static class RoleTypes
{
    public const string Admins = SuperAdmin + "," +  Admin;
    public const string All = SuperAdmin + "," +  Admin + "," + User;
    public const string SuperAdmin = "SuperAdmin";
    public const string Admin = "Admin";
    public const string User = "User";
}