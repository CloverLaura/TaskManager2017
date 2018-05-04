using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.ViewModels
{
    public class SignInViewModel
    {
        [Required (ErrorMessage ="You must enter an email")]
        [EmailAddress]
        [Display (Name = "Email: ")]
        public string Email { get; set; }

        [Required (ErrorMessage="You must enter a password")]
        [Display (Name ="Password: ")]
        public string Password { get; set; }

        
    }
}
