using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApplicationPL.Models
{
    public class RegisterViewModel 
    {
        [Required(ErrorMessage ="First Name Required")]
        [MinLength(3)]
        public string Fname { get; set; }

        [Required(ErrorMessage = "Last Name Required")]
        [MinLength(3)]
        public string Lname { get; set; }

        [Required]
        [EmailAddress(ErrorMessage="Email is Required")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage ="PLease Confirm Your Password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password dosn't Match")]
        public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }
    }
}
 