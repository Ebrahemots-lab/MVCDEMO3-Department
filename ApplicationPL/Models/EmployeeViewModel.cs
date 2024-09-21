using ApplicationDAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ApplicationBLL.Validation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationPL.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Age is Required")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        [MinLength(10, ErrorMessage = "Name should start from 10 charcter")]
        [MaxLength(50, ErrorMessage = "Length Should Not larger than 50 charcter")]
        //[UniqueCode(ErrorMessage = "Name Must Be Unique")]
        //[UniqueCode(ErrorMessage = "Name Must Contains Route")]
        [Remote(action: "UniqueName" , controller:"Employee",ErrorMessage ="Name Must Contain Route")]
        //[UniqueCode(ErrorMessage = "Name Must Contains Route")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Adress is Required")]
        public string Adress { get; set; }

        [DataType(DataType.Currency)] //This will change it to currency in SQL
        public decimal Salary { get; set; }

        [Display(Name = "Status")]
        public bool IsActive { get; set; }

        [EmailAddress(ErrorMessage = "Email is Required")]
        public string? EmailAdress { get; set; }

        public string? Phone { get; set; }

        public string? Img { get; set; }


        [Display(Name = "Hiring Date")]
        [Column(TypeName = "datetime")]
        public DateTime HireDate { get; set; } = DateTime.Now;

        public DateTime CreationDate { get; set; } = DateTime.Now;

        //RelationShip
        [ForeignKey("Department")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        public Department? Department { get; set; }
    }
}
