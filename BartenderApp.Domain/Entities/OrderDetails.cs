using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BartenderApp.Domain.Entities
{
    public class OrderDetails
    {
        [Required(ErrorMessage = "Please Enter your First Name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter your Last Name")]
        [Display(Name = "First Name")]
        public string LastName { get; set; }

    }
}
