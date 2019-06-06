using IdentityServer4.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Extensions.IdentityServer4
{
    public class UserLoginFailureWithEmailEvent : Event
    {
        public UserLoginFailureWithEmailEvent(string email, string error) 
            : base(EventCategories.Authentication, 
                  "User Login Failure with Email", 
                  EventTypes.Failure, 
                  EventIds.UserLoginFailure)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}
