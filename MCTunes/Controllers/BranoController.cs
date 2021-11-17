using MCTunes.Database;
using MCTunes.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCTunes.Controllers
{
    [Route("api/Brano")]
    [ApiController]
    public class BranoController : Controller
    {
        private readonly RetrieveBrano _retrieve;
        public BranoController(RetrieveBrano retrieve)
        {
            _retrieve = retrieve;
        }

        [HttpGet("GetAllBraniByBand/{idband}")]
        public async Task<ActionResult<IEnumerable<Brano>>> GetAllBraniByBand(int idband)
        {
            return _retrieve.GetAllByBand(idband).ToList();

        }

        [HttpGet("GetBrano/{id}")]
        public async Task<ActionResult<Brano>> GetBrano(int id)
        {
            return _retrieve.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> PostBrano(Brano brano)
        {
            return _retrieve.NewBrano(brano);

        }
    }
}
