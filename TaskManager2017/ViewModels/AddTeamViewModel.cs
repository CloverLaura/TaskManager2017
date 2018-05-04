using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.ViewModels
{
    public class AddTeamViewModel
    {
        [Required (ErrorMessage = "You must enter a name for your team")]
        [Display (Name ="Team name:")]
        public string Name { get; set; }

        [Display(Name = "Brief description of team (optional)")]
        public string Description { get; set; }

    }
}
