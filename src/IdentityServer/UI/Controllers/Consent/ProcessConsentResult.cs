using IdentityServer.UI.ViewModels.Consent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.UI.Controllers.Consent
{
    public class ProcessConsentResult
    {
        public bool IsRedirect => RedirectUri != null;
        public string RedirectUri { get; set; }
        public string ClientId { get; set; }

        public bool ShowView => ViewModel != null;
        public ConsentViewModel ViewModel { get; set; }

        public bool HasValidationError => ValidationError != null;
        public string ValidationError { get; set; }

    }
}
