using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BowlingGame.Helpers;
using BowlingGame.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BowlingGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BowlingGameController : ControllerBase
    {
        private BowlingGameService BowlingGameService;
        public BowlingGameController(IOptions<GameConfig> config,  IFrame frame)
        {
            BowlingGameService = new BowlingGameService(config, frame); 
        }
        /// <summary>
        /// Gets the total score for bowling game
        /// If a specific frame is specified it displays the score other wise displays all scores in
        /// </summary>
        /// <param name="scores"></param>
        /// <param name="frameNo"></param>
        /// <returns></returns>
        // GET api/values
        [HttpGet]
        //[ArrayInput("scores", Separator = ',')]
        //[Authorize(Roles = "Administrator,Accountant")]
        [AllowAnonymous]
        public ActionResult<List<string>> Get(string scores, int? frameNo=null)
        {
            try
            {
                List<string> results = new List<string>();
                string error = Validation.CheckScoreValidity(scores, frameNo);
                if (error.Length > 0)
                {
                    return BadRequest(error);
                }
                BowlingGameService.SetScoreString(scores);
                results.Add("Total Score = " + BowlingGameService.TotalScore().ToString());
                results.AddRange(BowlingGameService.ScoresByFrame(frameNo));
                return Ok(results);
            }
            catch(Exception ex)
            {
                return BadRequest(ex); 
            }
        }
    }
}
