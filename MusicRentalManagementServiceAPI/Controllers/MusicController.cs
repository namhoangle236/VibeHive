using Microsoft.AspNetCore.Mvc;

namespace MusicRentalManagementServiceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusicController : Controller
    {
        //saving all albums into list
        public static List<Music> albums = new List<Music>();

        //album ID counter
        public static int IdCounter = 1;

        //add new album
        [HttpPost]
        public ActionResult AddAlbum(Music album)
        {
            album.AlbumId = IdCounter++;

            //album should automatically be able to rent when added to catalog
            album.IsAvailable = true;
            albums.Add(album);
            return Ok("Album successfully added");
        }

        //view all albums
        [HttpGet]
        public ActionResult<List<Music>> GetAlbums()
        {
            return Ok(albums);
        }

        //can only delete albums not actively being rented
        [HttpDelete("{id}")]
        public ActionResult DeleteAlbum(int id)
        {

            var album = albums.FirstOrDefault(a => a.AlbumId == id);
            if (album == null)
            {
                return NotFound("Album not found");
            }

            if (!album.IsAvailable)
            {
                return BadRequest("Cannot delete an album that is currently being rented");
            }

            albums.Remove(album);
            return Ok("Removed album successfully");
        }
    }
}
