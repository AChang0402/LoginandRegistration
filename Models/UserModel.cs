#pragma warning disable CS8618
namespace LoginAndRegistration.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required(ErrorMessage = "First Name is required")]
    [MinLength(2, ErrorMessage = "First Name must be at least 2 characters.")]
    [Display(Name = "First Name: ")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required")]
    [MinLength(2, ErrorMessage = "First Name must be at least 2 characters.")]
    [Display(Name = "Last Name: ")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    [UniqueEmail]
    [Display(Name = "Email: ")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    [Display(Name = "Password: ")]
    public string Password { get; set; }

    [NotMapped]
    [Required(ErrorMessage = "Confirm Password is required")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage ="Passwords must match")]
    [Display(Name = "Confirm Password: ")]
    public string PasswordConfirm { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("Email is required!");
        }
        MyContext db = (MyContext)validationContext.GetService(typeof(MyContext));
        if (db.Users.Any(user => user.Email == value.ToString()))
        {
            return new ValidationResult("Email is associated with existing account!");
        }
        else
        {
            return ValidationResult.Success;
        }
    }
}