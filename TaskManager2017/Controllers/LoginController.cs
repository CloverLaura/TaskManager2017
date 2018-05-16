using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.ViewModels;
using static TaskManager.ViewModels.SignUpTaskViewModel;
using System.Net;
using Microsoft.AspNetCore.Identity;
using System.Collections;
using TaskManager2017;
using TaskManager2017.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.Controllers
{

    public class LoginController : Controller
    {

        private readonly TaskManager2017Context _context;

        public LoginController(TaskManager2017Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            var user = _context.User.FirstOrDefault(u => u.UserID == userID);
            if (user != null)
            {
                user.LoggedOn = false;
                _context.SaveChangesAsync();
            }

            
            return View();
        }


        
        public async Task<IActionResult> SignIn(SignInViewModel signInViewModel)
        {
            if (ModelState.IsValid)
            {
                var userE = await _context.User.FirstOrDefaultAsync(m => m.Email == signInViewModel.Email);
                if(userE == null)
                {
                    ModelState.AddModelError("Email", "Email is not registered");
                }
                else
                {
                    if( userE.Password != signInViewModel.Password)
                    {
                        ModelState.AddModelError("Password", "Incorrect password");
                    }
                    else
                    {
                        Response.Cookies.Append("userCookie", userE.UserID.ToString());
                        return RedirectToAction("Home", "Users");
                    }
                    return View(signInViewModel);
                }
            }
            return View(signInViewModel);
        }

        public IActionResult SignOut()
        {
            
            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            var user = _context.User.FirstOrDefault(u => u.UserID == userID);
            user.LoggedOn = false;
            _context.SaveChangesAsync();

            return RedirectToAction("SignIn", "Login");
        }

       
    }

    
}
