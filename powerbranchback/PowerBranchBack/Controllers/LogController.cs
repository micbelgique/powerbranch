using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerBranchBack.Model;
using System;
using System.Threading.Tasks;

namespace PowerBranchBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LogController : ControllerBase
    {
        private readonly Context _context;
        public LogController(Context context)
        {
            this._context = context;
        }

        [HttpPost]
        public Log AddLog(Log log)
        {
            log.Time = DateTime.UtcNow;
            _context.Logs.Add(log);
            _context.SaveChanges();
            return log;
        }
    }
}
