using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager2017.ViewModels
{
    public class CreateProjectViewModel
    {
        public IEnumerable<SelectListItem> Teams { get; set; }

        public string TeamName { get; set; }
    }
}
