using System.ComponentModel.DataAnnotations;
using ExpenseTrackerAPI.Constants;
using ExpenseTrackerAPI.Validation;

namespace ExpenseTrackerAPI.Models.User;

public class AddRoleModel
{
    
    [Required(ErrorMessage = "Username is required"),DataType(DataType.EmailAddress)]
    public string email{get; set; }
    [Required(ErrorMessage = "Password is required"), DataType(DataType.Password)]
    public string password { get; set; }
    
    [Required(ErrorMessage = "Role is required")]
    [EnumValidation(typeof(AuthConstants.Roles), ErrorMessage = "Invalid role")]
    public AuthConstants.Roles Role { get; set; }
}