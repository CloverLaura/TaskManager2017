using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.ViewModels
{
    public class AddViewModel
    {
        [Required (ErrorMessage="You must enter a name")]
        [Display (Name="Project title")]
        public string Name { get; set; }

        [Required (ErrorMessage ="you must enter a description")]
        [Display (Name = "Descritpion")]
        public string Description { get; set; }

    }
}
