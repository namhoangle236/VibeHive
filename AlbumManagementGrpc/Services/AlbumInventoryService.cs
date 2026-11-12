using Grpc.Core;
using System.Collections.Concurrent;

namespace AlbumManagementGrpc
{
    // this class fills in the methods from the proto
    public class AlbumInventoryService : AlbumService.AlbumServiceBase
    {
        // one shared dictionary and a counter
        private static ConcurrentDictionary<int, Album> albums = new ConcurrentDictionary<int, Album>();
        private static int nextId = 1;

        // add an album, give it an id, store it, send it back
        public override Task<Album> AddAlbum(Album request, ServerCallContext context)
        {
            // give the album the current id
            request.Id = nextId;
            // then move the counter up by one
            nextId++;
            albums[request.Id] = request;
            return Task.FromResult(request);
        }

        // look up an album by id, or throw not found
        public override Task<Album> GetAlbum(AlbumId request, ServerCallContext context)
        {
            if (albums.TryGetValue(request.Id, out Album album))
            {
                return Task.FromResult(album);
            }
            throw new RpcException(new Status(StatusCode.NotFound, "Album not found"));
        }

        // replace the stored album with the new info
        public override Task<Album> UpdateAlbum(Album request, ServerCallContext context)
        {
            if (!albums.ContainsKey(request.Id))
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Album not found"));
            }
            albums[request.Id] = request;
            return Task.FromResult(request);
        }

        // remove the album by id
        public override Task<Empty> DeleteAlbum(AlbumId request, ServerCallContext context)
        {
            if (!albums.TryRemove(request.Id, out _))
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Album not found"));
            }
            return Task.FromResult(new Empty());
        }

        // send every album back, one at a time
        public override async Task ListAlbums(Empty request, IServerStreamWriter<Album> responseStream, ServerCallContext context)
        {
            foreach (Album album in albums.Values)
            {
                await responseStream.WriteAsync(album);
            }
        }
    }
}