namespace PlaylistManagementService
{
    public abstract class Playlist
    {
        protected readonly List<Song> songs = new List<Song>();

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int CreatedBy { get; private set; }
        public abstract bool IsCollaborative { get; }

        public Playlist(int id, string name, int createdBy)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Trim().Length > 100)
                throw new ArgumentException("Enter a playlist name of 1 to 100 characters.");
            if (createdBy < 0)
                throw new ArgumentException("Creator user ID must be zero or greater.");

            this.Id = id;
            this.Name = name.Trim();
            this.CreatedBy = createdBy;
        }

        public virtual bool CanAccess(int userId)
        {
            return userId == this.CreatedBy;
        }

        public void CheckAccess(int userId)
        {
            if (userId < 0)
                throw new ArgumentException("User ID must be zero or greater.");
            if (!this.CanAccess(userId))
                throw new UnauthorizedAccessException("Only the creator or an invited collaborator can use this playlist.");
        }

        public virtual List<int> GetCollaborators()
        {
            return new List<int>();
        }

        public IReadOnlyList<Song> GetSongs()
        {
            return this.songs.AsReadOnly();
        }

        public void AddSong(Song song, int userId)
        {
            this.CheckAccess(userId);

            if (this.songs.Any(item => item.Id == song.Id))
                throw new InvalidOperationException("This song is already in the playlist.");

            // Each playlist keeps its own votes, even when songs share a catalog ID.
            this.songs.Add(new Song(song.Id, song.Title, song.Artist, song.Genre, song.DurationSeconds));
        }

        public void RemoveSong(int songId, int userId)
        {
            this.CheckOwner(userId);
            this.songs.Remove(this.FindSong(songId));
        }

        protected void CheckOwner(int userId)
        {
            if (userId < 0)
                throw new ArgumentException("User ID must be zero or greater.");
            if (userId != this.CreatedBy)
                throw new UnauthorizedAccessException("Only the playlist creator can do this.");
        }

        protected Song FindSong(int songId)
        {
            return this.songs.FirstOrDefault(song => song.Id == songId)
                ?? throw new KeyNotFoundException("The song was not found in this playlist.");
        }

        public abstract void InviteCollaborator(int userId, int collaboratorUserId);
        public abstract void VoteSong(int songId, int userId);
    }
}
