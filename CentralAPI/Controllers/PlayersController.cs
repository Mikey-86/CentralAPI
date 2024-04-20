using CentralAPI.Data;
using CentralAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CentralAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        // DB access
        private readonly dbContext _context;

        public PlayersController(dbContext context)
        {
            _context = context;
        }

        // Methods
        [HttpPost]
        public async Task<ActionResult> PostPlayer([FromBody] PlayersDTO playerObj)
        {
            //Check to make sure player does not exist already
            var result = _context.Players;
            var playerBool = result.Any(x => x.IDNumber == playerObj.IDNumber);

            if (playerBool != true)
            {
                if (ModelState.IsValid)
                {
                    if (playerObj != null)
                    {
                        //Make db object
                        Player player = new Player();
                        player.Id = playerObj.Id;
                        player.FirstName = playerObj.FirstName;
                        player.LastName = playerObj.LastName;
                        player.Address = playerObj.Address;
                        player.IDNumber = playerObj.IDNumber;
                        player.Age = playerObj.Age;
                        player.DesiredTeam = playerObj.DesiredTeam;

                        _context.Players.Add(player);
                        _context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Invalid data");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState + " Player already assigned to team!");
            }
        }

        //Get Players for team
        //[Authorize]
        [HttpGet("{teamId}")]
        [Route("api/players/{teamId}")]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers(int teamId)
        {

            if (teamId != 0)
            {
                //Getting name of team first and then using that to get all players
                var response = _context.Players.Where(x => Convert.ToInt32(x.DesiredTeam) == teamId).ToList();
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
