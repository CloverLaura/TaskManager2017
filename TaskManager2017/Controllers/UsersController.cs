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
using TaskManager2017.ViewModels;

namespace TaskManager2017.Controllers
{
    public class UsersController : Controller
    {
        private readonly TaskManager2017dbContext _context;

        public UsersController(TaskManager2017dbContext context)
        {
            _context = context;
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,FirstName,LastName,Password,Email,UserID,LoggedOn")] User user, string confirmPassword)
        {
            
            if(confirmPassword == null)
            {
                ModelState.AddModelError("ConfirmPassword", "You must verify your password");
                return View(user);
            }
            
            var userC = await _context.User.FirstOrDefaultAsync(u => u.Username == user.Username);
            if (userC != null)
            {
                ModelState.AddModelError("Username", "Useranme already exsists");
                return View(user);
            }
           
            var userE = await _context.User.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (userE != null)
            {
                ModelState.AddModelError("Email", "Email already registered");
                return View(user);
            }

            if (ModelState.IsValid & confirmPassword == user.Password)
            {
             
                
                await _context.SaveChangesAsync();
                Response.Cookies.Append("userCookie", user.UserID.ToString());
          
                return RedirectToAction("Home", "Users");
            }
            
            
            if(confirmPassword != user.Password)
            {
                ModelState.AddModelError("Password", "Passwords do not match");
            }
            return View(user);
        }

        public async Task<IActionResult> Home()
        {
            HomeUsersViewModel homeUsersViewModel = new HomeUsersViewModel();
            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie); 
            var user =_context.User.FirstOrDefault( u => u.UserID == userID);
            homeUsersViewModel.FirstName = user.FirstName;
            homeUsersViewModel.LastName = user.LastName;
            user.LoggedOn = true;
            await _context.SaveChangesAsync();
            var custQuery = _context.Team.Where(t => t.CreatedBy == user.Username).ToList();
            List<Team> userCreatedTeams = new List<Team>();
            if (custQuery != null)
            {
                foreach(Team t in custQuery)
                {
                    userCreatedTeams.Add(t);
                }
                
                homeUsersViewModel.UserCreatedTeams = userCreatedTeams.Select(t => t.Name).Distinct();
            }


            var custQuery2 = _context.Team.Where(t => t.UserID == user.UserID).ToList();
            if (custQuery2 != null)
            {
                List<Team> teamsUserIn = new List<Team>();
                foreach (Team t in custQuery2)
                {
                    teamsUserIn.Add(t);
                }
                homeUsersViewModel.TeamsUserIn = teamsUserIn;
            }
            IQueryable < Project > custQuery3 =
                from t in _context.Project
                where t.CreatedBy == user.Username
                select t;
            if (custQuery3 != null)
            {
                List<Project> userProjects = new List<Project>();
                foreach (Project project in custQuery3)
                {
                    userProjects.Add(project);
                }
                homeUsersViewModel.UserProjects = userProjects;
            }
            
            

            IQueryable<TaskManager.Models.Task> custQuery4 =
                from t in _context.Task
                where t.TakenBy == user.Username
                select t;

            
            if (custQuery4 != null)
            {
                int completedTask = 0;
                int uncompletedTask = 0;
                List<TaskManager.Models.Task> userTasks = new List<TaskManager.Models.Task>();
                foreach (TaskManager.Models.Task task in custQuery4)
                {
                    userTasks.Add(task);
                    if (task.Completed == true)
                    {
                        completedTask++;
                    }
                    else
                    {
                        uncompletedTask++;
                    }
                }
                homeUsersViewModel.UserTasks = userTasks;
                homeUsersViewModel.CompletedTasks = completedTask;
                homeUsersViewModel.UncompletedTasks = uncompletedTask;
                homeUsersViewModel.UserName = user.Username;
            }
            

            return View(homeUsersViewModel);

            

            
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
                
                List<User> users = _context.User.ToList();
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
            
            var user = _context.User.FirstOrDefault(u => u.UserID == id);

            IQueryable<Project> custQuery =
                from t in _context.Project
                where t.CreatedBy == user.Username
                select t;
            List<Project> userProjects = new List<Project>();
            foreach(var project in custQuery)
            {
                userProjects.Add(project);
            }
            ViewBag.UserProjects = userProjects;

            IQueryable<TaskManager.Models.Task> custQuery2 =
                from t in _context.Task
                where t.TakenBy == user.Username
                select t;
            List<TaskManager.Models.Task> userTasks = new List<TaskManager.Models.Task>();
            foreach (var task in custQuery2)
            {
                userTasks.Add(task);
            }
            ViewBag.UserTasks = userTasks;

            //IQueryable<UserTeam> custQuery3 =
                //from t in _context.UserTeam
                //where t.User == user.Username
                //select t;
            //List<UserTeam> userTeam = new List<UserTeam>();
            //foreach (var team in custQuery3)
            //{
                //userTeam.Add(team);
            //}
            //ViewBag.Userteams = userTeam;

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
