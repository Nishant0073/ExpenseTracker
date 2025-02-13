namespace ExpenseTrackerAPI.Constants;

public class AuthConstants
{
    public enum Roles
    {
        Admin,
        User
    }

    public const Roles defaultRole = Roles.User;
    public const string defaultEmail = "user1@email.com";
    public const string defaultUsername = "user1";
    public const string defaultPassword = "password";
}
