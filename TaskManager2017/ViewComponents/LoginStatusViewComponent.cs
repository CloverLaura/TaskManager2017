using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;
using TaskManager2017.Models;

namespace TaskManager2017.ViewComponents
{
    public class LoginStatusViewComponent : ViewComponent
    {
        private readonly TaskManager2017Context _context;

        public LoginStatusViewComponent(TaskManager2017Context context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //UserData userData = new UserData();
            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            var user = await _context.User.FirstOrDefaultAsync(m => m.UserID == userID);
            //User user = userData.GetById(userID);


            if (user == null)
            {
                return View();


            }
            if (user.LoggedOn)
            {
                return View("LoggedIn");

            }
            else
            {
                return View();
            }
        }
    }
}
