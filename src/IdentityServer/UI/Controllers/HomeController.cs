using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Infrastracture;
using IdentityServer.Infrastracture.DataEntity;
using Microsoft.Extensions.Logging;
using IdentityServer.UI.ViewModels.Account;

namespace IdentityServer.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly InMemoryDbContext _context;
        private readonly ILogger _logger;

        public HomeController(InMemoryDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;

        }

        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl)
        {
            return View();

            // need to call this to populate data for in-memory ef database 
            //            _context.Database.EnsureCreatedAsync();
            //            List<ToDo> toDoList = _context.ToDos.ToList();
            //            System.Diagnostics.Debug.WriteLine("# of todo list: " + _context.ToDos.Count());
            //            _logger.LogInformation("# of todo list: {Count}", toDoList.Count);
            //            return View(toDoList);
        }

    }
}
