using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerBranchBack.Model;
using System.Linq;

namespace PowerBranchBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ExpertController : ControllerBase
    {
        private readonly Context _context;

        public ExpertController(Context context)
        {
            _context = context;

        }

        [HttpGet]
        public ActionResult GetExpert()
        {
            return new JsonResult(_context.Experts.ToList());
        }
    }
}
