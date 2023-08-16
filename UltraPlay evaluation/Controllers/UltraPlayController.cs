using Microsoft.AspNetCore.Mvc;
using UltraPlay_evaluation.Data;
using UltraPlay_evaluation.Data.Entities;

namespace UltraPlay_evaluation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UltraPlayController : ControllerBase
    {
        private readonly UltraPlay_EvalContext _evalContext;
        public UltraPlayController(UltraPlay_EvalContext evalContext)
        {
            _evalContext = evalContext;
        }

        [HttpGet]
        [Route("GetMatch/{id}")]
        public ActionResult GetMatch(string id)
        {
            if (!int.TryParse(id, out int matchID))            
                return NoContent();            

            Match match = _evalContext.Matches.FirstOrDefault(x => x.ID == matchID);
            if (match == null)
                return NoContent();

            return Ok(match);
        }
    }
}