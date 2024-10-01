using System.ComponentModel.DataAnnotations;

namespace ApplicationPL.Models
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage ="Email Is Required")]
        [EmailAddress(ErrorMessage ="Invalid EmailAdress")]
        public string Email { get; set; }
    }
}
