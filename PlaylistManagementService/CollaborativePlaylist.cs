namespace PlaylistManagementService
{
    public class CollaborativePlaylist : Playlist
    {
        private readonly HashSet<int> collaboratorIds = new HashSet<int>();

        public override bool IsCollaborative => true;

        public CollaborativePlaylist(int id, string name, int createdBy) : base(id, name, createdBy)
        {
        }

        public override bool CanAccess(int userId)
        {
            return userId == this.CreatedBy || this.collaboratorIds.Contains(userId);
        }

        public override List<int> GetCollaborators()
        {
            return this.collaboratorIds.OrderBy(id => id).ToList();
        }

        public override void InviteCollaborator(int userId, int collaboratorUserId)
        {
            this.CheckOwner(userId);

            if (collaboratorUserId < 0)
                throw new ArgumentException("Collaborator user ID must be zero or greater.");
            if (collaboratorUserId == this.CreatedBy)
                throw new InvalidOperationException("The creator already has access to this playlist.");
            if (!this.collaboratorIds.Add(collaboratorUserId))
                throw new InvalidOperationException("This user is already a collaborator.");
        }

        public override void VoteSong(int songId, int userId)
        {
            this.CheckAccess(userId);
            this.FindSong(songId).Vote(userId);
        }
    }
}
