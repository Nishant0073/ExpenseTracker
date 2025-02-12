using ExpenseTrackerAPI.Models.User;

namespace ExpenseTrackerAPI.Service;

public interface IUserService
{
    public Task<string> GetToken(LoginModel loginModel);
}