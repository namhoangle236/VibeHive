using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistWinForms
{
    public class PlaylistApiClient : IDisposable
    {
        public const string DefaultAddress = "http://localhost:5082/";
        private readonly HttpClient client;

        public PlaylistApiClient(string address = DefaultAddress)
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(address.TrimEnd('/') + "/"),
                Timeout = TimeSpan.FromSeconds(8)
            };
        }

        public Task<List<Playlist>> GetPlaylistsAsync(int userId)
        {
            return SendAsync<List<Playlist>>(HttpMethod.Get, "api/playlists?userId=" + userId);
        }

        public Task<CreatedPlaylistResponse> CreatePlaylistAsync(string name, int userId, bool collaborative)
        {
            return SendAsync<CreatedPlaylistResponse>(HttpMethod.Post, "api/playlists",
                new { Name = name, CreatedBy = userId, IsCollaborative = collaborative });
        }

        public Task<List<Song>> GetSongsAsync(string search)
        {
            return SendAsync<List<Song>>(HttpMethod.Get,
                "api/songs?search=" + Uri.EscapeDataString(search ?? ""));
        }

        public Task<Song> CreateSongAsync(string title, string artist, string genre, int duration)
        {
            return SendAsync<Song>(HttpMethod.Post, "api/songs",
                new { Title = title, Artist = artist, Genre = genre, DurationSeconds = duration });
        }

        public Task<string> AddSongAsync(int playlistId, int songId, int userId)
        {
            return SendAsync<string>(HttpMethod.Put,
                string.Format("api/playlists/{0}/add?userId={1}&songId={2}", playlistId, userId, songId));
        }

        public Task<string> InviteAsync(int playlistId, int userId, int collaboratorId)
        {
            return SendAsync<string>(HttpMethod.Put,
                string.Format("api/playlists/{0}/invite?userId={1}&collaboratorUserId={2}", playlistId, userId, collaboratorId));
        }

        public Task<string> VoteAsync(int playlistId, int songId, int userId)
        {
            return SendAsync<string>(HttpMethod.Post,
                string.Format("api/playlists/{0}/vote?userId={1}&songId={2}", playlistId, userId, songId));
        }

        public Task<List<Song>> GetRankingsAsync(int playlistId, int userId)
        {
            return SendAsync<List<Song>>(HttpMethod.Get,
                string.Format("api/playlists/{0}/rankings?userId={1}", playlistId, userId));
        }

        public Task<string> RemoveSongAsync(int playlistId, int songId, int userId)
        {
            return SendAsync<string>(HttpMethod.Delete,
                string.Format("api/playlists/{0}/songs/{1}?userId={2}", playlistId, songId, userId));
        }

        private async Task<T> SendAsync<T>(HttpMethod method, string path, object body = null) where T : class
        {
            using (var request = new HttpRequestMessage(method, path))
            {
                if (body != null)
                    request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.SendAsync(request))
                {
                    string text = await response.Content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode)
                        throw new InvalidOperationException(ReadError(text, (int)response.StatusCode));

                    if (typeof(T) == typeof(string))
                    {
                        string message = text;
                        if (text.TrimStart().StartsWith("\"", StringComparison.Ordinal))
                            message = JsonConvert.DeserializeObject<string>(text);
                        return (T)(object)message;
                    }

                    T result = JsonConvert.DeserializeObject<T>(text);
                    if (result == null)
                        throw new InvalidOperationException("The API returned an empty response.");
                    return result;
                }
            }
        }

        private static string ReadError(string text, int statusCode)
        {
            try
            {
                JToken token = JToken.Parse(text);
                if (token.Type == JTokenType.String)
                    return token.Value<string>();

                JObject problem = token as JObject;
                if (problem != null)
                {
                    JObject errors = problem["errors"] as JObject;
                    if (errors != null)
                        return string.Join(Environment.NewLine,
                            errors.Properties().SelectMany(field => field.Value.Values<string>()));
                    return (string)problem["detail"] ?? (string)problem["title"] ?? "The request failed.";
                }
            }
            catch (JsonException)
            {
            }

            return string.IsNullOrWhiteSpace(text) ? "API error " + statusCode : text;
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
