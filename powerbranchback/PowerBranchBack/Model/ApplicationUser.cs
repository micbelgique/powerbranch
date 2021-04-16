using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PowerBranchBack.Model
{
    public class ApplicationUser : IdentityUser
    {
        public bool Staff { get; set; }
        public List<EventUser> Events { get; set; }
        public ApplicationUser(string username, string email) : base(username)
        {
            this.Email = email;
        }
        public ApplicationUser()
        {
        }
    }
}
