namespace PlaylistManagementService
{
    public class PersonalPlaylist : Playlist
    {
        public override bool IsCollaborative => false;

        public PersonalPlaylist(int id, string name, int createdBy) : base(id, name, createdBy)
        {
        }

        public override void InviteCollaborator(int userId, int collaboratorUserId)
        {
            throw new InvalidOperationException("Personal playlists do not accept collaborators.");
        }

        public override void VoteSong(int songId, int userId)
        {
            throw new InvalidOperationException("Voting is only available in collaborative playlists.");
        }
    }
}
