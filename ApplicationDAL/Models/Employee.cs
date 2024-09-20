using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ApplicationDAL.Models
{
    //Creation of Employee Model
    public class Employee
    {
      

        public int Id { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Adress { get; set; }

        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        public string? EmailAdress { get; set; }

        public string? Phone { get; set; }

        public string? Img { get; set; }


        public DateTime HireDate { get; set; } = DateTime.Now;

        public DateTime CreationDate { get; set; } = DateTime.Now;

        //RelationShip
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public Department? Department { get; set; }
       

    }
}
