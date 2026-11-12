using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PlaylistManagementService.Controllers
{
    [ApiController]
    [Route("api/playlists")]
    public class PlaylistsController : ControllerBase
    {
        private static readonly List<Playlist> PlaylistList = new List<Playlist>();
        private static readonly List<Song> SongList = new List<Song> { };

        private static int NextPlaylistId = 1;
        private static int NextSongId = 0;

        // Multiple clients share these lists, so changes must happen one at a time.
        private static readonly object DataLock = new object();

        [HttpPost]
        public ActionResult CreatePlaylist(PlaylistRequest request)
        {
            lock (DataLock)
            {
                Playlist playlist;
                try
                {
                    if (request.IsCollaborative)
                        playlist = new CollaborativePlaylist(NextPlaylistId, request.Name, request.CreatedBy);
                    else
                        playlist = new PersonalPlaylist(NextPlaylistId, request.Name, request.CreatedBy);
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }

                PlaylistList.Add(playlist);
                NextPlaylistId++;
                return Ok(new { message = "Playlist created successfully!", id = playlist.Id });
            }
        }

        [HttpGet]
        public ActionResult GetPlaylists(int? userId = null)
        {
            if (userId.HasValue && userId.Value < 0)
                return BadRequest("User ID must be zero or greater.");

            lock (DataLock)
            {
                var playlists = PlaylistList
                    .Where(playlist => !userId.HasValue || playlist.CanAccess(userId.Value))
                    .Select(playlist => new
                    {
                        playlist.Id,
                        playlist.Name,
                        playlist.CreatedBy,
                        playlist.IsCollaborative,
                        Songs = playlist.GetSongs().Select(song => song.Id).ToList(),
                        CollaboratorIds = playlist.GetCollaborators()
                    }).ToList();

                return Ok(playlists);
            }
        }

        [HttpPut("{id:int}/add")]
        public ActionResult AddSong(int id, [FromQuery, BindRequired] int userId, [FromQuery, BindRequired] int songId)
        {
            if (userId < 0 || songId < 0)
                return BadRequest("User ID and song ID must be zero or greater.");

            lock (DataLock)
            {
                var playlist = PlaylistList.FirstOrDefault(item => item.Id == id);
                if (playlist == null)
                    return NotFound("Playlist not found!");
                if (!playlist.CanAccess(userId))
                    return StatusCode(403, "You do not have access to this playlist.");

                var song = SongList.FirstOrDefault(item => item.Id == songId);
                if (song == null)
                    return NotFound("Song not found!");
                if (playlist.GetSongs().Any(item => item.Id == songId))
                    return BadRequest("This song is already in the playlist.");

                CommandInterface command = new AddSongCommand(song, userId);
                command.Execute(playlist);
                return Ok("Song added successfully!");
            }
        }

        [HttpPut("{id:int}/invite")]
        public ActionResult InviteCollaborator(int id, [FromQuery, BindRequired] int userId, [FromQuery, BindRequired] int collaboratorUserId)
        {
            if (userId < 0 || collaboratorUserId < 0)
                return BadRequest("User IDs must be zero or greater.");

            lock (DataLock)
            {
                var playlist = PlaylistList.FirstOrDefault(item => item.Id == id);
                if (playlist == null)
                    return NotFound("Playlist not found!");
                if (playlist.CreatedBy != userId)
                    return StatusCode(403, "Only the playlist creator can invite collaborators.");

                try
                {
                    playlist.InviteCollaborator(userId, collaboratorUserId);
                    return Ok("Collaborator invited successfully!");
                }
                catch (InvalidOperationException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost("{id:int}/vote")]
        public ActionResult VoteSong(int id, [FromQuery, BindRequired] int userId, [FromQuery, BindRequired] int songId)
        {
            if (userId < 0 || songId < 0)
                return BadRequest("User ID and song ID must be zero or greater.");

            lock (DataLock)
            {
                var playlist = PlaylistList.FirstOrDefault(item => item.Id == id);
                if (playlist == null)
                    return NotFound("Playlist not found!");
                if (!playlist.CanAccess(userId))
                    return StatusCode(403, "You do not have access to this playlist.");
                if (!playlist.IsCollaborative)
                    return BadRequest("Voting is only available in collaborative playlists.");
                if (!playlist.GetSongs().Any(song => song.Id == songId))
                    return NotFound("Song not found in this playlist!");

                try
                {
                    CommandInterface command = new VoteSongCommand(songId, userId);
                    command.Execute(playlist);
                    return Ok("Vote recorded successfully!");
                }
                catch (InvalidOperationException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("{id:int}/rankings")]
        public ActionResult GetRankings(int id, [FromQuery, BindRequired] int userId)
        {
            if (userId < 0)
                return BadRequest("User ID must be zero or greater.");

            lock (DataLock)
            {
                var playlist = PlaylistList.FirstOrDefault(item => item.Id == id);
                if (playlist == null)
                    return NotFound("Playlist not found!");
                if (!playlist.CanAccess(userId))
                    return StatusCode(403, "You do not have access to this playlist.");

                var rankings = playlist.GetSongs()
                    .OrderByDescending(song => song.Votes)
                    .ThenBy(song => song.Title)
                    .ThenBy(song => song.Id)
                    .Select(song => new
                    {
                        song.Id,
                        song.Title,
                        song.Artist,
                        song.Genre,
                        song.DurationSeconds,
                        song.Votes
                    }).ToList();

                return Ok(rankings);
            }
        }

        [HttpDelete("{id:int}/songs/{songId:int}")]
        public ActionResult RemoveSong(int id, int songId, [FromQuery, BindRequired] int userId)
        {
            if (userId < 0 || songId < 0)
                return BadRequest("User ID and song ID must be zero or greater.");

            lock (DataLock)
            {
                var playlist = PlaylistList.FirstOrDefault(item => item.Id == id);
                if (playlist == null)
                    return NotFound("Playlist not found!");
                if (playlist.CreatedBy != userId)
                    return StatusCode(403, "Only the playlist creator can remove songs.");
                if (!playlist.GetSongs().Any(song => song.Id == songId))
                    return NotFound("Song not found in this playlist!");

                CommandInterface command = new RemoveSongCommand(songId, userId);
                command.Execute(playlist);
                return Ok("Song removed successfully!");
            }
        }

        // Absolute routes let the same controller also manage the song catalog.
        [HttpGet("/api/songs")]
        public ActionResult GetSongs(string? search = null)
        {
            string query = search?.Trim() ?? "";
            lock (DataLock)
            {
                var songs = SongList.Where(song =>
                    song.Title.Contains(query, StringComparison.OrdinalIgnoreCase)
                    || song.Artist.Contains(query, StringComparison.OrdinalIgnoreCase)
                    || song.Genre.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();

                return Ok(songs);
            }
        }

        [HttpPost("/api/songs")]
        public ActionResult AddCatalogSong(SongRequest request)
        {
            lock (DataLock)
            {
                Song song;
                try
                {
                    song = new Song(NextSongId, request.Title, request.Artist, request.Genre, request.DurationSeconds);
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }

                if (SongList.Any(item =>
                    item.Title.Equals(song.Title, StringComparison.OrdinalIgnoreCase)
                    && item.Artist.Equals(song.Artist, StringComparison.OrdinalIgnoreCase)))
                    return BadRequest("This title and artist already exist in the catalog.");

                SongList.Add(song);
                NextSongId++;
                return Ok(song);
            }
        }
    }
}
