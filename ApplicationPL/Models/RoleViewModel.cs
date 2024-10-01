using System.ComponentModel.DataAnnotations;

namespace ApplicationPL.Models
{
    public class RoleViewModel
    {
        [Required(ErrorMessage ="Role Name is Required")]
        public string Name { get; set; }
    }
}
