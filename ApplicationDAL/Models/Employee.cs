using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ApplicationDAL.Models
{
    //Creation of Employee Model
    public class Employee
    {
      

        public int Id { get; set; }

        [Required(ErrorMessage ="Age is Required")]
        public int Age { get; set; }

        [Required(ErrorMessage ="Name Is Required")]
        [MinLength(10,ErrorMessage ="Name should start from 10 charcter")]
        [MaxLength(50,ErrorMessage ="Length Should Not larger than 50 charcter")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Adress is Required")]
        public string Adress { get; set; }

        [DataType(DataType.Currency)] //This will change it to currency in SQL
        public decimal Salary { get; set; }

        [Display(Name="Status")]
        public bool IsActive { get; set; }

        [EmailAddress(ErrorMessage ="Email is Required")]
        public string? EmailAdress { get; set; }

        public string? Phone { get; set; }

        public string? Img { get; set; }


        [Display(Name="Hiring Date")]
        [Column(TypeName="datetime")]
        public DateTime HireDate { get; set; } = DateTime.Now;

        public DateTime CreationDate { get; set; } = DateTime.Now;

        //RelationShip
        [ForeignKey("Department")]
        [Display(Name="Department")]
        public int DepartmentId { get; set; }

        public Department? Department { get; set; }
       

    }
}
