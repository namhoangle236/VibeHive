namespace PlaylistManagementService
{
    public class Song
    {
        private readonly HashSet<int> voterIds = new HashSet<int>();

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Artist { get; private set; }
        public string Genre { get; private set; }
        public int DurationSeconds { get; private set; }
        public int Votes => this.voterIds.Count;

        public Song(int id, string title, string artist, string genre, int durationSeconds)
        {
            if (string.IsNullOrWhiteSpace(title) || title.Trim().Length > 200)
                throw new ArgumentException("Enter a song title of 1 to 200 characters.");
            if (string.IsNullOrWhiteSpace(artist) || artist.Trim().Length > 120)
                throw new ArgumentException("Enter an artist of 1 to 120 characters.");
            if (string.IsNullOrWhiteSpace(genre) || genre.Trim().Length > 60)
                throw new ArgumentException("Enter a genre of 1 to 60 characters.");
            if (durationSeconds < 1 || durationSeconds > 86400)
                throw new ArgumentException("Duration must be between 1 and 86400 seconds.");

            this.Id = id;
            this.Title = title.Trim();
            this.Artist = artist.Trim();
            this.Genre = genre.Trim();
            this.DurationSeconds = durationSeconds;
        }

        public void Vote(int userId)
        {
            if (userId < 0)
                throw new ArgumentException("User ID must be zero or greater.");
            if (!this.voterIds.Add(userId))
                throw new InvalidOperationException("You have already voted for this song in this playlist.");
        }
    }
}
