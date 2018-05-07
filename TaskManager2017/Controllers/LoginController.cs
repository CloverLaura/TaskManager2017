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
                        return RedirectToAction("Home");
                    }
                    return View(signInViewModel);
                }
            }
            return View(signInViewModel);
        }
            
           
        

        [HttpGet]
        public IActionResult SignUp()
        {
            SignUpTaskViewModel signUpTaskViewModel = new SignUpTaskViewModel();
            return View(signUpTaskViewModel);
        }


        [HttpPost]
        public IActionResult SignUp(SignUpTaskViewModel signUpTaskViewModel)
        {
            UserData userdata = new UserData();
            if (!userdata.IsValidUsername(signUpTaskViewModel.Username))
            {
                ModelState.AddModelError("Username", "Username already taken");
            }
            List<User> users = userdata.AllUsersToList();
            foreach(User u in users)
            {
                if(signUpTaskViewModel.Email == u.Email)
                {
                    ModelState.AddModelError("Email", "Email already registered");
                    return View(signUpTaskViewModel);
                }
            }
            if (ModelState.IsValid)
            {

            User newUser = new User
            {
                Username = signUpTaskViewModel.Username,
                FirstName = signUpTaskViewModel.FirstName,
                LastName = signUpTaskViewModel.LastName,
                Email = signUpTaskViewModel.Email,
                Password = signUpTaskViewModel.Password,
            };

            UserData userData = new UserData();
            //ctx.Add(newUser);
            //ctx.SaveChanges();
            userData.Add(newUser);
            User user = userData.GetByEmail(signUpTaskViewModel.Email);
            Response.Cookies.Append("userCookie", user.UserID.ToString());

            return RedirectToAction("Home", new { email = signUpTaskViewModel.Email });

            }
            return View(signUpTaskViewModel);
            
        }

        

       

        public IActionResult Home()
        {
            //UserData userData = new UserData();
            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            var user = _context.User.FirstOrDefault(u => u.UserID == userID);
            user.LoggedOn = true;
            _context.SaveChangesAsync();

            return View(user);
        }

        public IActionResult SearchForUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchForUser(SearchForUserViewModel searchForUserViewModel)
        {
            if (ModelState.IsValid)
            {
                //UserData userData = new UserData();
                List<User> users = _context.User.ToList();
                //List<User> users = userData.AllUsersToList();
                foreach (User u in users)
                {
                    if (u.Username == searchForUserViewModel.Username)
                    {
                        return RedirectToAction("ViewSearchedUser", new { id = u.UserID });
                    }
                }
                ModelState.TryAddModelError("Username", "Username not found");
                return View(searchForUserViewModel);
            }
            ModelState.AddModelError("Username", "You must enter a Username");
            return View(searchForUserViewModel);
        }

        public IActionResult ViewSearchedUser(int id)
        {
            //UserData userData = new UserData();
            //User user = userData.GetById(id);
            var user = _context.User.FirstOrDefault(u => u.UserID == id);
            return View(user);
        }

        public IActionResult SignOut()
        {
            UserData userData = new UserData();
            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            User user = userData.GetById(userID);
            user.LoggedOn = false;

            return RedirectToAction("SignIn", "Login");
        }

       
    }

    
}
