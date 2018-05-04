using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.ViewModels
{
    public class JoinTeamViewModel
    {

        public User User { get; set; }

        [Required]
        [Display(Name ="Select the team you want to join")]
        public string Team { get; set; }

        

        public IEnumerable<SelectListItem> Teams { get; set; }

        

    }

}
