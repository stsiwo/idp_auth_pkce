using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.UI.Controllers
{
    public class ViewComponentsController : Controller
    {
        [HttpPost]
        public IActionResult GetPriorityList(int _maxPriority, bool _isDone)
        {
            System.Diagnostics.Debug.WriteLine("_isDone value: " + _isDone);
            return ViewComponent("PriorityList", new { maxPriority = _maxPriority, isDone = _isDone });
        }
    }
}
