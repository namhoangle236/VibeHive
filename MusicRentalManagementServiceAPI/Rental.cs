namespace MusicRentalManagementServiceAPI
{
    public class Rental
    {
        public int UserID { get; set; }
        public int AlbumID { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }

    }
}
