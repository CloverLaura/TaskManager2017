using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;
using TaskManager.ViewModels;
using TaskManager2017.Models;

namespace TaskManager2017.Controllers
{
    public class UsersController : Controller
    {
        private readonly TaskManager2017Context _context;

        public UsersController(TaskManager2017Context context)
        {
            _context = context;
        }

        // GET: Users
        /*public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .SingleOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }*/

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,FirstName,LastName,Password,Email,UserID,LoggedOn")] User user, string confirmPassword)
        {
            /*var userC = from m in _context.User
                        select m;
            userC = userC.Where(s => s.Username == user.Username);
            await userC.LoadAsync();*/
            var userC = _context.User.FirstOrDefault(u => u.Username == user.Username);
            if (userC != null)
            {
                ModelState.AddModelError("Username", "Useranme already exsists");
                return View(user);
            }
            /*var userE =
                _context.User
                .Where(r => r.Email == user.Email);
            userC = userC.Where(s => s.Email == user.Email);
            await userC.LoadAsync();*/
            var userE = _context.User.FirstOrDefault(u => u.Email == user.Email);
            if (userE != null)
            {
                ModelState.AddModelError("Email", "Email already registered");
                return View(user);
            }

            if (ModelState.IsValid & confirmPassword == user.Password)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                Response.Cookies.Append("userCookie", user.UserID.ToString());
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Home", "Users");
            }
            
            
            if(confirmPassword != user.Password)
            {
                ModelState.AddModelError("Password", "Passwords do not match");
            }
            return View(user);
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

        // GET: Users/Edit/5
        /*public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.SingleOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Username,FirstName,LastName,Password,Email,UserID,LoggedOn")] User user)
        {
            if (id != user.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .SingleOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.SingleOrDefaultAsync(m => m.UserID == id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.UserID == id);
        }*/
    }
}
