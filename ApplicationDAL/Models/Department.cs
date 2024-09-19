using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ApplicationDAL.Models
{

    
    public class Department
    {


        public int Id { get; set; }

        //Front End Validation to ensure that code is enterd
        [Required(ErrorMessage ="Code Is Required")]    
        public string Code { get; set; }
     
        [Required(ErrorMessage = "Name Is Required")]
        [MaxLength(25) , MinLength(2)]
        public string Name { get; set; }
     
        [DisplayName("Date Of Creation")] // Display name for front
        public DateTime? DateOfCreation { get; set; }

        //RelationShip
        public ICollection<Employee>? Employees { get; set; }

    }
}
