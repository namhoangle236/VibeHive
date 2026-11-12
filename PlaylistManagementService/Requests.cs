using System.Text.Json.Serialization;

namespace PlaylistManagementService
{
    public class PlaylistRequest
    {
        public string Name { get; set; } = "";
        [JsonRequired]
        public int CreatedBy { get; set; }
        public bool IsCollaborative { get; set; }
    }

    public class SongRequest
    {
        public string Title { get; set; } = "";
        public string Artist { get; set; } = "";
        public string Genre { get; set; } = "";
        public int DurationSeconds { get; set; }
    }
}
