using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UltraPlay_evaluation.Data;
using UltraPlay_evaluation.Data.Entities;
using UltraPlay_evaluation.Utils;

namespace UltraPlay_evaluation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UltraPlayController : ControllerBase
    {
        private readonly UltraPlay_EvalContext _evalContext;
        private readonly IMapper _mapper;
        public UltraPlayController(UltraPlay_EvalContext evalContext, IMapper mapper)
        {
            _evalContext = evalContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetMatch/{id}")]
        public ActionResult GetMatch(string id)
        {
            if (!int.TryParse(id, out int matchID))            
                return NoContent();            

            Models.Match match = _mapper.Map<Match, Models.Match>(_evalContext.Matches.FirstOrDefault(x => x.ID == matchID));
            
            match.InactiveBets = _evalContext.Bets.Where(x => x.MatchID == matchID && !x.IsActive).ToList();
            match.InactiveBets.ForEach(x => x.Odds = _evalContext.Odds.Where(y => y.BetID == x.ID).ToList());

            match.ActiveBets = _evalContext.Bets.Where(x => x.MatchID == matchID && x.IsActive).ToList();            
            match.ActiveBets.ForEach(x => x.Odds = _evalContext.Odds.Where(y => y.BetID == x.ID && y.IsActive).ToList());
            if (match == null)
                return NoContent();
            
            return Ok(match);
        }

        [HttpGet]
        [Route("GetUpcomingMatches")]
        public async Task<ActionResult> GetUpcomingMatches()
        {
            var upcomingMatches = _evalContext
                .Matches
                .Where(x => x.StartDate > DateTime.Now && x.StartDate <= DateTime.Now.AddDays(1))
                .ToList();

            List<Models.Match> result = new();

            foreach (var match in upcomingMatches) 
            {
                var mapped = _mapper.Map<Match, Models.Match>(match);
                mapped.PreviewBets = new();
                var svbs = await _evalContext.Procedures.GetPreviewBetsAsync(match.ID);

                foreach (var betType in svbs.GroupBy(x => x.BetName))
                {
                    Bet currentBet = _mapper.Map<GetPreviewBetsResult, Bet>(svbs.First(x => x.BetName == betType.Key));
                    if (betType.All(x => !x.SpecialBetValue.HasValue))                    
                        currentBet.Odds = betType.Select(x => _mapper.Map<GetPreviewBetsResult, Odd>(x)).ToList();                    
                    else                    
                        currentBet.Odds = betType.GroupBy(x => x.SpecialBetValue).First().Select(x => _mapper.Map<GetPreviewBetsResult, Odd>(x)).ToList();                    

                    mapped.PreviewBets.Add(currentBet);
                }

                result.Add(mapped);                
            }            

            if (result.Count > 0)
                return Ok(result);
            else 
                return NoContent();
        }
    }
}