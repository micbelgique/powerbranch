using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PowerBranchBack.Model;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PowerBranchBack.Services
{
    public class UserService
    {
        private UserManager<ApplicationUser> UserManager;
        private SignInManager<ApplicationUser> SignInManager;
        private Context Context;
        public UserService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            Context context
            )
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
            this.Context = context;
        }

        public async Task<string> Authenticate(string username, string password)
        {

            var result = await this.SignInManager.PasswordSignInAsync(username, password, false, false);
            if (result.Succeeded)
            {
                var user = await this.UserManager.FindByNameAsync(username);
                if (user == null) return null;

                var token = generateJwtToken(user);
                return token;
            }

            return null;
        }
        public async Task<string> Register(string username, string password, string email)
        {
            var userExist = await UserManager.FindByNameAsync(username);
            if (userExist != null || userExist?.Email == email)
                return null;
            var result = await UserManager.CreateAsync(new ApplicationUser(username, email), password);
            if (result.Succeeded)
            {
                var user = await this.UserManager.FindByNameAsync(username);
                if (user == null) return null;

                var token = generateJwtToken(user);
                return token;
            }
            return null;
        }
        public async Task<ApplicationUser> GetUser(string id)
        {
            var result = await Context.Users.Include(e => e.Events).ThenInclude(eu => eu.Event).FirstOrDefaultAsync(u => u.Id == id);
            result.PasswordHash = null;
            result.SecurityStamp = null;
            result.ConcurrencyStamp = null;
            return result;
        }

        public async Task<bool> ForgetPasswordSendEmail(string email)
        {
            var user = await UserManager.FindByEmailAsync(email);
            if (user == null)
                return false;

            var token = await UserManager.GeneratePasswordResetTokenAsync(user);
            token = Base64UrlEncoder.Encode(token);
            var link = "Voici le lien pour reset votre mot de passe: https://www.powerbranch.tech/forgetpassword/" + token;

            EmailHelper emailHelper = new EmailHelper();
            bool emailResponse = emailHelper.SendEmailPasswordReset(user.Email, link);

            if (emailResponse)
                return true;
            else
                return false;
        }

        public async Task<bool> ReserPassword(string email, string code, string password)
        {
            var user = await UserManager.FindByEmailAsync(email);
            if (user == null)
                return false;

            var resetPassResult = await UserManager.ResetPasswordAsync(user, code, password);
            if (resetPassResult.Succeeded)
            {
                return true;
            }
            return false;
        }

        private string generateJwtToken(IdentityUser user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("hMlBevhXPdc9ucEZYcxyMmZ2p11RWGteFbH56YYRoAsvGKHkAS3-Tqg_nPNA9S9V_OZE1XqTLQRNWwGc1roEtd-NatZI6AJ1tHXfQnpMJZiUW8FQKF4il2Km9Im3raVnk5A9G1l6r51C-4YsCUGrRA1oamJFvrmTe3rh2Z0OoB6L2xS9hRnw9p3US939JY7LH_zh3NhwJ3o2D91TlxrLgaCLEy0pnHfL0PItTLAb1fFnVb6OBwp33ICTWfR617ozyb6Bgvr7jhqtY_OrvsnPmGFLuhrnzUqUJNbL37zyPtbvUxMM0S1rtSwVh700fGVhKSQYbIOkl23vrk4dR2DwlQ");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
