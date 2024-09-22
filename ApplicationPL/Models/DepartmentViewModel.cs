using ApplicationBLL.Interfaces;
using ApplicationDAL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ApplicationPL.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        //Front End Validation to ensure that code is enterd
        [Required(ErrorMessage = "Code Is Required")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        [MaxLength(25), MinLength(2)]
        public string Name { get; set; }

        [DisplayName("Date Of Creation")] // Display name for front
        public DateTime? DateOfCreation { get; set; }

        //RelationShip
        public ICollection<Employee>? Employees { get; set; }

        public int NumberOfEmployees => Employees.Count;

        public string StatusColor => NumberOfEmployees <= 3 ? "Green" : "Red";

        public string Message => NumberOfEmployees > 3 ? $"{Name} Department Has More than 3 Employee Please Move {NumberOfEmployees - 3} to Another Department" : "";

    }
}
