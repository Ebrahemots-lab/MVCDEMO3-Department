using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDAL.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Code Is Required")] //Front End Validation to ensure that code is enterd
        public string Code { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }

        [DisplayName("Date Of Creation")] // Display name for front
        public DateTime? DateOfCreation { get; set; }

    }
}
