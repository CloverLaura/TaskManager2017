using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;
using TaskManager2017.Models;

namespace TaskManager2017.Controllers
{
    public class TasksController : Controller
    {
        private readonly TaskManager2017Context _context;

        public TasksController(TaskManager2017Context context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Task.ToListAsync());
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task
                .SingleOrDefaultAsync(m => m.TaskID == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskID,Name,Description,ProjectID,Completed,IsTaken,TakenBy")] TaskManager.Models.Task task, string addNewTask, string finish)
        {
            if (ModelState.IsValid)
            {

                string cookie = HttpContext.Request.Cookies["projectCookie"];
                int projectID = Convert.ToInt32(cookie);
                var project = _context.Project.FirstOrDefault(u => u.ProjectID == projectID);
                if (ModelState.IsValid & string.IsNullOrEmpty(finish))
                {
                    task.Completed = false;
                    task.IsTaken = false;
                    task.TakenBy = "";
                    task.CreatedBy = project.CreatedBy;
                    task.ProjectID = project.ProjectID;
                    project.TaskCount++;
                    _context.Add(task);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Create");


                }
                if(ModelState.IsValid & string.IsNullOrEmpty(addNewTask))
                {
                    task.Completed = false;
                    task.IsTaken = false;
                    task.TakenBy = "";
                    task.CreatedBy = project.CreatedBy;
                    task.ProjectID = project.ProjectID;
                    project.TaskCount++;
                    _context.Add(task);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("ViewProjects", "Project");
                }
                
            }
            return View(task);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task.SingleOrDefaultAsync(m => m.TaskID == id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskID,Name,Description,ProjectID,Completed,IsTaken,TakenBy")] TaskManager.Models.Task task)
        {
            if (id != task.TaskID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.TaskID))
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
            return View(task);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task
                .SingleOrDefaultAsync(m => m.TaskID == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Task.SingleOrDefaultAsync(m => m.TaskID == id);
            _context.Task.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.Task.Any(e => e.TaskID == id);
        }
    }
}
