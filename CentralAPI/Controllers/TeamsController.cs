using CentralAPI.Data;
using CentralAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CentralAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        // DB access
        private readonly dbContext _context;

        public TeamsController(dbContext context)
        {
            _context = context;
        }

        // Methods
        [HttpPost]
        public IActionResult PostTeam(Team teamObj)
        {
            if (ModelState.IsValid)
            {
                if (teamObj != null)
                {
                    _context.Teams.Add(teamObj);
                    _context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            var teamsResponse = _context.Teams;
            return Ok(teamsResponse);
        }

        //[Authorize]
        [HttpGet]
        [Route("api/[controller]/{teamId}")]
        public IActionResult GetTeams(int teamId)
        {
            var teamsResponse = _context.Teams.FirstOrDefault(x => x.Id == teamId);
            return Ok(teamsResponse);
        }

        [HttpGet]
        [Route("api/[controller]/active")]
        public async Task<ActionResult<IEnumerable<Team>>> GetActiveTeams()
        {
            var teamsResponse = _context.Teams.Where(x => x.IsActive == true).ToList();
            return Ok(teamsResponse);
        }
    }
}
