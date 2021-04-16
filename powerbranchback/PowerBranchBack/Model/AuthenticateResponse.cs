using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerBranchBack.Model
{
    public class AuthenticateResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(IdentityUser user, string token)
        {
            Id = user.Id;
            FirstName = user.UserName;
            Email = user.NormalizedEmail;
            Token = token;
        }
    }
}
