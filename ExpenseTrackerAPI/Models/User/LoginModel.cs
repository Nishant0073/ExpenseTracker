using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Models.User;

public class LoginModel
{
    [Required(ErrorMessage = "Username is required"),DataType(DataType.EmailAddress)]
    public string email{get; set; }
    [Required(ErrorMessage = "Password is required"), DataType(DataType.Password)]
    public string password { get; set; }
}