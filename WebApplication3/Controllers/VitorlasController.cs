using Microsoft.AspNetCore.Mvc;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VitorlasController : ControllerBase
    {
        private readonly DataService _data;

        public VitorlasController()
        {
            _data = new DataService();
        }

        [HttpGet("hajok")]
        public IActionResult GetHajok() => Ok(_data.Hajok);

        [HttpGet("turak")]
        public IActionResult GetTurak() => Ok(_data.Turak);

        [HttpGet("legtobbtura")]
        public IActionResult GetLegtobbetHasznaltHajo()
        {
            var legtobb = _data.Turak
                .GroupBy(t => t.HajoAzon)
                .OrderByDescending(g => g.Count())
                .First();

            var hajo = _data.Hajok.First(h => h.Azon == legtobb.Key);

            return Ok(new
            {
                Hajo = hajo.Nev,
                Tipus = hajo.Tipus,
                TurakSzama = legtobb.Count()
            });
        }

        [HttpGet("resztvevok/{nap}")]
        public IActionResult GetResztvevokNapSzerint(int nap)
        {
            int osszes = _data.Turak
                .Where(t => t.Nap == nap)
                .Sum(t => t.Szemely);

            return Ok(new { Nap = nap, OsszesResztvevo = osszes });
        }
    }
}