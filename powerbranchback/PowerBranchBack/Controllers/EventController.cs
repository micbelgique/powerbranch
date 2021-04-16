using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerBranchBack.Model;

namespace PowerBranchBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventController : ControllerBase
    {
        private readonly Context _context;
        private HttpClient HttpClient;

        public EventController(Context context)
        {
            _context = context;
            HttpClient = new HttpClient();

        }
        [HttpPost]
        public async Task<ActionResult> CreateEvent(Event Event)
        {
            var _event = _context.Events.FirstOrDefault(e => e.Title == Event.Title);
            var id = User.Identities.First().Claims.FirstOrDefault(c => c.Type == "id").Value;
            var user = _context.Users.Include(l => l.Events).FirstOrDefault(e => e.Id == id);
            if (!user.Staff)
                return Unauthorized();
            if (_event != null)
                return NoContent();
            Event.Id = Guid.NewGuid();
            _context.Events.Add(Event);
            await _context.SaveChangesAsync();
            return new JsonResult(Event);
        }
        [HttpGet("{idEvent}")]
        public async Task<ActionResult> AssignEventToUser(string idevent)
        {
            var id = User.Identities.First().Claims.FirstOrDefault(c => c.Type == "id").Value;
            var _event = _context.Events.FirstOrDefault(e => e.Id == Guid.Parse(idevent));
            if (_event == null)
                return NotFound();
            var user = _context.Users.Include(l => l.Events).FirstOrDefault(e => e.Id == id);
            if (user == null)
                return NotFound();
            var eventUser = _context.EventUsers.FirstOrDefault(e => e.User.Id == user.Id && e.Event.Id == _event.Id);
            if(eventUser == null)
            {
                try
                {
                    var newEventUser = new EventUser
                    {
                        User = user,
                        CreatedDate = DateTime.UtcNow,
                        Event = _event
                    };
                    _context.EventUsers.Add(newEventUser);
                    await _context.SaveChangesAsync();
                    return new JsonResult(newEventUser);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return NotFound();
        }
        [HttpGet("getevents")]
        [AllowAnonymous]
        public async Task<ActionResult> GetEvents()
        {
            var result = await HttpClient.GetAsync("https://www.meetup.com/micbelgique/events/json/");
            return new JsonResult(await result.Content.ReadAsStringAsync());

        }
    }
}
