using System;
using System.Collections.Generic;
using System.Linq;
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
    public class TestController : ControllerBase
    {
        private readonly Context _context;

        public TestController(Context context)
        {
            _context = context;

        }

        [HttpGet("{month}/{year}")]
        public async Task GenerateTestsForUsers(int month, int year) 
        {
            var users = _context.Users.ToList();
            foreach (var user in users)
            {
                var events = user.Events.FindAll(ev => ev.CreatedDate.Month == month && ev.CreatedDate.Year == year);
                if(events.Count > 3)
                {
                    var test = new Test
                    {
                        User = user
                    };
                    _context.Tests.Add(test);
                    await _context.SaveChangesAsync();
                    foreach (var ev in events)
                    {
                        var questionsTemp = _context.Questions.Include(q => q.Answers).Where(q => q.EventLinked.Id == ev.Event.Id);
                        foreach (var question in questionsTemp)
                        {
                            var testQuestion = new TestQuestion
                            {
                                Question = question,
                                Test = test,
                            };
                            _context.TestQuestions.Add(testQuestion);
                        }
                    }
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
