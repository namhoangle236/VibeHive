namespace MusicRentalManagementServiceAPI
{
    public class Music
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Genre {  get; set; }
        public int Year { get; set; }
        public bool IsAvailable { get; set; }
    }
}
