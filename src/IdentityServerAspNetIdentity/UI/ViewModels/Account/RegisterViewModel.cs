using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.UI.ViewModels.Account
{
    public class RegisterViewModel : RegisterInputModel
    {
        public RegisterViewModel(string returnUrl)
        {
            ReturnUrl = returnUrl;
        }

        public string ReturnUrl { get; set; } = null;
    }
}
