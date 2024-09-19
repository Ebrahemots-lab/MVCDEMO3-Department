using ApplicationBLL.Interfaces;
using ApplicationDAL.Models;
using System.ComponentModel.DataAnnotations;


namespace ValidationLib
{
    public class UniqueCodeAttribute : ValidationAttribute
    {
        //Get the Data from database
        private IDepartmentRepository _DepartmentRepository;

        public string? ErrorMessage { get; set; }

        public UniqueCodeAttribute(IDepartmentRepository _dept)
        {
            _DepartmentRepository = _dept;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            //Conver the sended code into string 
            if (value != null)
            {
                string deptCode = (string)value;

                Department dept = _DepartmentRepository.Find(deptCode);

                if (dept == null)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(ErrorMessage);
                }

            }

            return null;
        }
    }
}
