namespace PlaylistManagementService
{
    public interface CommandInterface
    {
        void Execute(Playlist playlist);
    }

    public class AddSongCommand : CommandInterface
    {
        private readonly Song song;
        private readonly int userId;

        public AddSongCommand(Song song, int userId)
        {
            this.song = song;
            this.userId = userId;
        }

        public void Execute(Playlist playlist)
        {
            playlist.AddSong(this.song, this.userId);
        }
    }

    public class RemoveSongCommand : CommandInterface
    {
        private readonly int songId;
        private readonly int userId;

        public RemoveSongCommand(int songId, int userId)
        {
            this.songId = songId;
            this.userId = userId;
        }

        public void Execute(Playlist playlist)
        {
            playlist.RemoveSong(this.songId, this.userId);
        }
    }

    public class VoteSongCommand : CommandInterface
    {
        private readonly int songId;
        private readonly int userId;

        public VoteSongCommand(int songId, int userId)
        {
            this.songId = songId;
            this.userId = userId;
        }

        public void Execute(Playlist playlist)
        {
            playlist.VoteSong(this.songId, this.userId);
        }
    }
}
