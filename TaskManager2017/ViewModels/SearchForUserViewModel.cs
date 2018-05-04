using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.ViewModels
{
    public class SearchForUserViewModel
    {
        [Required(ErrorMessage = "You must enter a Username")]
        public string Username { get; set; }
    }
}
