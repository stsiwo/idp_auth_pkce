using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.UI.Filters;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityServer.UI.Controllers
{
    [SecurityHeaders]
    public class AccountController : Controller
    {
        private readonly TestUserStore _users;
        // IIdentityServiceInterationService: a service that UI can communicate with IdentityServer
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthorizationCodeStore _schemeProvider;
        private readonly IEventService _events; 

        public AccountController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthorizationCodeStore schemeProvider,
            IEventService events,
            TestUserStore users = null)
        {
            _users = users; 

            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _events = events;
        }


        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(returnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login()
        {
            return View();
        }
    }
}
