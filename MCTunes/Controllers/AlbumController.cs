using MCTunes.Database;
using MCTunes.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCTunes.Controllers
{
    [Route("api/Album")]
    [ApiController]
    public class AlbumController : Controller
    {
        private readonly RetrieveAlbum _retrieve;
        public AlbumController(RetrieveAlbum retrieve)
        {
            _retrieve = retrieve;
        }

        [HttpGet("GetAllAlbumByBand/{idband}")]
        public async Task<ActionResult<IEnumerable<Album>>> GetAllAlbumByBand(int idband)
        {
            return _retrieve.GetAllByBand(idband).ToList();

        }

        [HttpGet("GetAlbum/{id}")]
        public async Task<ActionResult<Album>> GetAlbum(int id)
        {
            return _retrieve.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> PostAlbum(Album album)
        {
            return _retrieve.NewAlbum(album);

        }
    }
}
