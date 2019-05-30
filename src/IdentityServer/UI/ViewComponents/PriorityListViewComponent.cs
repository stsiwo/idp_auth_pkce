using IdentityServer.Infrastracture;
using IdentityServer.Infrastracture.DataEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.UI.ViewComponents
{
    /**
     *  view components flow : user event on original View ==params==> View Component Class ==result==> View Component View ==> update original View
     * 1. View Component Class
     *     - could be any folder
     *     - must have suffix "ViewComponent" 
     *     - must inherit "ViewComponent"
     * 2. View Component View
     *     - must be Views/Shared/Components/{ViewComponentClassName}/Default.cshtml
     *     - name must match with View Component Class (without suffix)
     *     
     **/
    public class PriorityListViewComponent : ViewComponent
    {
        private readonly InMemoryDbContext _context;

        public PriorityListViewComponent(InMemoryDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int maxPriority, bool isDone)
        {
            var items = await GetItemsAsync(maxPriority, isDone);
            return View(items);
        }

        private Task<List<ToDo>> GetItemsAsync(int maxPriority, bool isDone)
        {
            return _context.ToDos.Where(x => x.IsDone == isDone && x.Priority <= maxPriority).ToListAsync();

        }
    }
}
