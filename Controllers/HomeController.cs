using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Models.Domain;

namespace ToDoList.Controllers;

public class HomeController : Controller
{
    private readonly MVCDemoDbContext _context;

    public HomeController(MVCDemoDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var todos = _context.Tasks.ToList();
        return View(todos);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Tasks todo)
    {
        if (ModelState.IsValid)
        {
            todo.Priority = todo.Category == "work" ? 0 : 1;
            _context.Tasks.Add(todo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        var todos = _context.Tasks.ToList();
        return View("Index", todos);
    }

    public IActionResult Delete(int id)
    {
        var task = _context.Tasks.Find(id);
        if (task == null)
        {
            return NotFound();
        }

        _context.Tasks.Remove(task);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var task = _context.Tasks.Find(id);
        if (task == null)
        {
            return NotFound();
        }

        return View(task);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Tasks todo)
    {
        if (ModelState.IsValid)
        {
            var existingTask = _context.Tasks.Find(todo.Id);
            if (existingTask == null)
            {
                return NotFound();
            }

            existingTask.Task = todo.Task;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(todo);
    }


    public IActionResult ToggleStatus(int id)
    {
        var task = _context.Tasks.Find(id);
        if (task == null)
        {
            return NotFound();
        }

        task.Status = !task.Status;
        _context.SaveChanges();

        return RedirectToAction("Index");
    }





    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
