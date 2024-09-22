using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ApplicationDAL.Models
{

    
    public class Department
    {


        public int Id { get; set; }

       
        [Required]    
        public string Code { get; set; }
     
        [Required]
        public string Name { get; set; }
     
        [DisplayName] 
        public DateTime? DateOfCreation { get; set; }

        //RelationShip
        public ICollection<Employee>? Employees { get; set; }

    }
}
