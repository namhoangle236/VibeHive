using Microsoft.AspNetCore.Mvc;

namespace MusicRentalManagementServiceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalsController : Controller
    {
        //list to manage active rentals
        public static List<Rental> rentals = new List<Rental>();

        //using userId and albumID to establish a rental
        [HttpPost]
        public ActionResult RentAlbum(int userId, int albumId)
        {
            //creating new rental object
            //ReturnDate is nullable so value is omitted here
            //null value establishes that it is being rented

            var album = MusicController.albums.FirstOrDefault(a => a.AlbumId == albumId);
            if (album == null)
            {
                return NotFound("Album not found..");
            }

            if (!album.IsAvailable)
            {
                return BadRequest("Album is not available to rent");
            }

            album.IsAvailable = false;

            var rental = new Rental()
            {
                UserID = userId,
                AlbumID = albumId,
                RentalDate = DateTime.Now
            };

            rentals.Add(rental);
            return Ok("Successfully rented the album!");
        }

        //viewing all rentals
        [HttpGet]
        public ActionResult<List<Rental>> GetRentals()
        {
            return Ok(rentals);
        }

        //return an album
        //updating return date
        //every album has a unique id, so userId not necessary
        [HttpPost("{albumId}/return")]
        public ActionResult ReturnAlbum(int albumId)
        {
            var rental = rentals.FirstOrDefault(r => r.AlbumID == albumId && r.ReturnDate == null);

            if (rental == null)
            {
                return NotFound("Rental not found..");
            }

            rental.ReturnDate = DateTime.Now;

            var album = MusicController.albums.FirstOrDefault(a=> a.AlbumId == albumId);
            if (album != null)
            {
                album.IsAvailable = true;
            }

            return Ok("Album returned successfully!");
        }
    }
}
