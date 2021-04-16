using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using PowerBranchBack.Model;
using PowerBranchBack.Services;
using System.Linq;
using System.Threading.Tasks;

namespace PowerBranchBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly Context _context;
        private UserService userService;

        public UserController(Context context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            userService = new UserService(userManager, signInManager, _context);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> CreateRegisteredUser(RegisteredUser registeredUser)
        {
            var _registeredUser = _context.RegisteredUsers.FirstOrDefault(e => e.Email == registeredUser.Email);
            if (_registeredUser != null)
                return Unauthorized();
            _context.RegisteredUsers.Add(registeredUser);
            await _context.SaveChangesAsync();

            return new JsonResult(registeredUser);
        }
        [HttpGet]

        public async Task<ActionResult> ReturnUser()
        {
            if (Request.Headers[HeaderNames.Authorization].Count > 0)
            {
                var id = User.Identities.First().Claims.FirstOrDefault(c => c.Type == "id").Value;
                var user = await userService.GetUser(id);
                return new JsonResult(user);
            }
            return NotFound();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("forgetPassword")]
        public async Task<ActionResult> ForgetPassword(string email)
        {
            var result = await userService.ForgetPasswordSendEmail(email);
            if (result)
            {
                return new JsonResult(result);
            }
            return NotFound();
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("resetPassword")]
        public async Task<ActionResult> ResetPassword(string email, string code, string password)
        {
            var token = Base64UrlEncoder.Decode(code);
            var result = await userService.ReserPassword(email, token, password);
            if (result)
                return new JsonResult(result);
            return NotFound();
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("auth")]
        public async Task<ActionResult> Auth([FromBody] UserFromAngular user)
        {
            var result = await userService.Authenticate(user.Username, user.Password);
            if (result != null)
            {
                return new JsonResult(result);
            }
            return Unauthorized();
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] UserFromAngular user)
        {
            var result = await userService.Register(user.Username, user.Password, user.Email);
            if (result != null)
            {
                return new JsonResult(result);
            }
            return Unauthorized();
        }
    }
}
