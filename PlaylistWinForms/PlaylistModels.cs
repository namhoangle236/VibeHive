using System.Collections.Generic;

namespace PlaylistWinForms
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreatedBy { get; set; }
        public bool IsCollaborative { get; set; }
        public List<int> Songs { get; set; } = new List<int>();
        public List<int> CollaboratorIds { get; set; } = new List<int>();

        public string DisplayName
        {
            get { return Name + (IsCollaborative ? " [Collaborative]" : " [Personal]"); }
        }
    }

    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public int DurationSeconds { get; set; }
        public int Votes { get; set; }

        public string CatalogueDisplay
        {
            get
            {
                return string.Format("{0}: {1} - {2} | {3} | {4}:{5:00}",
                    Id, Title, Artist, Genre, DurationSeconds / 60, DurationSeconds % 60);
            }
        }

        public string PlaylistDisplay
        {
            get { return string.Format("{0} | Votes: {1}", CatalogueDisplay, Votes); }
        }
    }

    public class CreatedPlaylistResponse
    {
        public string Message { get; set; }
        public int Id { get; set; }
    }
}
