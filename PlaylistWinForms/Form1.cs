using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlaylistWinForms
{
    public partial class Form1 : Form
    {
        private const int CurrentUserId = 0;
        private readonly PlaylistApiClient api = new PlaylistApiClient();
        private readonly Dictionary<TextBox, string> placeholders = new Dictionary<TextBox, string>();
        private readonly HashSet<TextBox> showingPlaceholder = new HashSet<TextBox>();
        private readonly Timer refreshTimer;
        private readonly ToolTip toolTips;

        private bool busy;
        private bool polling;
        private bool bindingLists;
        private bool closing;
        private int stateVersion;
        private string catalogueSearch = "";

        private Playlist SelectedPlaylist
        {
            get { return lsb_playlists.SelectedItem as Playlist; }
        }

        private Song SelectedCatalogueSong
        {
            get { return lsb_song_catalogue.SelectedItem as Song; }
        }

        private Song SelectedPlaylistSong
        {
            get { return lsb_songs_in_playlist.SelectedItem as Song; }
        }

        public Form1()
        {
            InitializeComponent();

            if (components == null)
                components = new Container();

            refreshTimer = new Timer(components) { Interval = 3000 };
            toolTips = new ToolTip(components);

            Text = "CollaborativePlaylistBuilder - User 0";
            KeyPreview = true;

            lsb_playlists.DisplayMember = "DisplayName";
            lsb_song_catalogue.DisplayMember = "CatalogueDisplay";
            lsb_songs_in_playlist.DisplayMember = "PlaylistDisplay";

            lsb_playlists.HorizontalScrollbar = true;
            lsb_song_catalogue.HorizontalScrollbar = true;
            lsb_songs_in_playlist.HorizontalScrollbar = true;

            ConfigurePlaceholder(txb_new_playlist, "Playlist Name");
            ConfigurePlaceholder(txb_new_song_title, "Title");
            ConfigurePlaceholder(txb_new_song_artist, "Artist");
            ConfigurePlaceholder(txb_new_song_genre, "Genre");

            txb_new_playlist.MaxLength = 100;
            txb_new_song_title.MaxLength = 200;
            txb_new_song_artist.MaxLength = 120;
            txb_new_song_genre.MaxLength = 60;

            txb_new_song_duration.Maximum = 86400;
            txb_new_song_duration.Minimum = 1;
            txb_new_song_duration.Value = 180;

            // Allow the single radio button to be toggled off.
            rbtn_collaborative_playlist.AutoCheck = false;
            rbtn_collaborative_playlist.Click += delegate
            {
                rbtn_collaborative_playlist.Checked =
                    !rbtn_collaborative_playlist.Checked;
            };

            // TEMPORARY. WILL GET LIST OF USERS FROM API LATER
            cmb_users_list.Items.AddRange(new object[] { 1, 2, 3 });
            cmb_users_list.SelectedIndex = 0;
            cmb_users_list.TextChanged += delegate { UpdateButtons(); };

            lsb_song_catalogue.ContextMenuStrip = null;
            catalogueSearch = txb_search.Text.Trim();

            toolTips.SetToolTip(
                txb_search,
                "Search by title, artist, or genre. Leave blank to show all songs.");

            toolTips.SetToolTip(
                btn_search,
                "Search the song catalogue.");

            toolTips.SetToolTip(
                lsb_song_catalogue,
                "Select a song to add to the current playlist. F5 refreshes all lists.");

            toolTips.SetToolTip(
                lsb_songs_in_playlist,
                "Songs are ranked by votes. Select a song to vote or remove it.");

            toolTips.SetToolTip(
                cmb_users_list,
                "Choose or type a collaborator's user ID. You are user 0.");

            toolTips.SetToolTip(
                rbtn_collaborative_playlist,
                "Click to turn collaboration on or off for the new playlist.");

            toolTips.SetToolTip(
                txb_new_song_duration,
                "Song duration in seconds.");

            // Avoid duplicate subscriptions if already wired in the designer.
            btn_search.Click -= btn_search_Click;
            btn_search.Click += btn_search_Click;

            txb_search.KeyDown -= txb_search_KeyDown;
            txb_search.KeyDown += txb_search_KeyDown;

            btn_new_playlist.Click += CreatePlaylist_Click;
            btn_new_song.Click += CreateSong_Click;
            btn_add_to_playlist.Click += AddSong_Click;
            btn_add_collaborator.Click += InviteCollaborator_Click;
            btn_vote.Click += Vote_Click;
            btn_remove_from_playlist.Click += RemoveSong_Click;

            lsb_playlists.SelectedIndexChanged += PlaylistSelectionChanged;
            lsb_song_catalogue.SelectedIndexChanged += SongSelectionChanged;
            lsb_songs_in_playlist.SelectedIndexChanged += SongSelectionChanged;

            refreshTimer.Tick += RefreshTimer_Tick;
            Shown += Form1_Shown;
            KeyDown += Form1_KeyDown;

            UpdateButtons();
        }

        private async void Form1_Shown(object sender, EventArgs e)
        {
            await RunActionAsync(() => RefreshAllAsync());

            if (!closing)
                refreshTimer.Start();
        }

        private async void CreatePlaylist_Click(object sender, EventArgs e)
        {
            await RunActionAsync(async () =>
            {
                string name = ReadInput(txb_new_playlist);

                if (name.Length == 0)
                    throw new InvalidOperationException("Enter a playlist name.");

                CreatedPlaylistResponse result = await api.CreatePlaylistAsync(
                    name,
                    CurrentUserId,
                    rbtn_collaborative_playlist.Checked);

                if (closing)
                    return;

                ResetInput(txb_new_playlist);

                await RefreshAllAsync(result.Id);
                ShowSuccess(result.Message);
            });
        }

        private async void CreateSong_Click(object sender, EventArgs e)
        {
            await RunActionAsync(async () =>
            {
                string title = ReadInput(txb_new_song_title);
                string artist = ReadInput(txb_new_song_artist);
                string genre = ReadInput(txb_new_song_genre);

                if (title.Length == 0 || artist.Length == 0 || genre.Length == 0)
                {
                    throw new InvalidOperationException(
                        "Enter a song title, artist, and genre.");
                }

                Song song = await api.CreateSongAsync(
                    title,
                    artist,
                    genre,
                    (int)txb_new_song_duration.Value);

                if (closing)
                    return;

                catalogueSearch = "";
                txb_search.Clear();

                ResetInput(txb_new_song_title);
                ResetInput(txb_new_song_artist);
                ResetInput(txb_new_song_genre);

                await RefreshAllAsync(preferredCatalogueSongId: song.Id);

                ShowSuccess(
                    "Song added to the catalogue. Select a playlist and click " +
                    "Add Song to Current Playlist to include it.");
            });
        }

        private async void AddSong_Click(object sender, EventArgs e)
        {
            await RunActionAsync(async () =>
            {
                Playlist playlist = RequirePlaylist();
                Song song = SelectedCatalogueSong;

                if (song == null)
                {
                    throw new InvalidOperationException(
                        "Select a song from the catalogue.");
                }

                string message = await api.AddSongAsync(
                    playlist.Id,
                    song.Id,
                    CurrentUserId);

                await RefreshAllAsync(
                    playlist.Id,
                    preferredPlaylistSongId: song.Id);

                ShowSuccess(message);
            });
        }

        private async void InviteCollaborator_Click(object sender, EventArgs e)
        {
            await RunActionAsync(async () =>
            {
                Playlist playlist = RequirePlaylist();
                int collaboratorId;

                if (!int.TryParse(cmb_users_list.Text.Trim(), out collaboratorId)
                    || collaboratorId < 0)
                {
                    throw new InvalidOperationException(
                        "Enter a collaborator user ID of zero or greater.");
                }

                if (collaboratorId == CurrentUserId)
                {
                    throw new InvalidOperationException(
                        "You already own this playlist. Choose another user ID.");
                }

                string message = await api.InviteAsync(
                    playlist.Id,
                    CurrentUserId,
                    collaboratorId);

                await RefreshAllAsync(playlist.Id);
                ShowSuccess(message);
            });
        }

        private async void Vote_Click(object sender, EventArgs e)
        {
            await RunActionAsync(async () =>
            {
                Playlist playlist = RequirePlaylist();
                Song song = SelectedPlaylistSong;

                if (song == null)
                {
                    throw new InvalidOperationException(
                        "Select a song in the playlist.");
                }

                string message = await api.VoteAsync(
                    playlist.Id,
                    song.Id,
                    CurrentUserId);

                await RefreshAllAsync(
                    playlist.Id,
                    preferredPlaylistSongId: song.Id);

                ShowSuccess(message);
            });
        }

        private async void RemoveSong_Click(object sender, EventArgs e)
        {
            await RunActionAsync(async () =>
            {
                Playlist playlist = RequirePlaylist();
                Song song = SelectedPlaylistSong;

                if (song == null)
                {
                    throw new InvalidOperationException(
                        "Select a song in the playlist.");
                }

                string message = await api.RemoveSongAsync(
                    playlist.Id,
                    song.Id,
                    CurrentUserId);

                await RefreshAllAsync(playlist.Id);
                ShowSuccess(message);
            });
        }

        private async void PlaylistSelectionChanged(object sender, EventArgs e)
        {
            if (!bindingLists && !busy && !closing)
                await RunActionAsync(() => RefreshAllAsync());
        }

        private void SongSelectionChanged(object sender, EventArgs e)
        {
            if (!bindingLists)
            {
                stateVersion++;
                UpdateButtons();
            }
        }

        private async void RefreshTimer_Tick(object sender, EventArgs e)
        {
            if (busy || polling || closing || !Visible)
                return;

            polling = true;
            int version = stateVersion;

            try
            {
                await RefreshAllAsync();
            }
            catch (Exception ex)
            {
                if (!closing && version == stateVersion)
                    ReportError(ex, false);
            }
            finally
            {
                polling = false;
            }
        }

        private async void btn_search_Click(object sender, EventArgs e)
        {
            await SearchCatalogueAsync();
        }

        private async void txb_search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await SearchCatalogueAsync();
            }
        }

        private async void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (busy || closing)
                return;

            if (e.Control && e.KeyCode == Keys.F)
            {
                e.SuppressKeyPress = true;
                txb_search.Focus();
                txb_search.SelectAll();
            }
            else if (e.KeyCode == Keys.F5)
            {
                e.SuppressKeyPress = true;
                await RunActionAsync(() => RefreshAllAsync());
            }
        }

        private async Task SearchCatalogueAsync()
        {
            await RunActionAsync(async () =>
            {
                catalogueSearch = txb_search.Text.Trim();
                await RefreshAllAsync();
            });
        }

        private async Task RefreshAllAsync(
            int? preferredPlaylistId = null,
            int? preferredCatalogueSongId = null,
            int? preferredPlaylistSongId = null)
        {
            if (closing)
                return;

            int version = stateVersion;

            int? playlistId = preferredPlaylistId
                ?? (SelectedPlaylist == null ? (int?)null : SelectedPlaylist.Id);

            int? catalogueId = preferredCatalogueSongId
                ?? (SelectedCatalogueSong == null ? (int?)null : SelectedCatalogueSong.Id);

            int? songId = preferredPlaylistSongId
                ?? (SelectedPlaylistSong == null ? (int?)null : SelectedPlaylistSong.Id);

            string search = catalogueSearch;

            List<Playlist> playlists = await api.GetPlaylistsAsync(CurrentUserId);

            Playlist selected = playlists.FirstOrDefault(item => item.Id == playlistId)
                ?? playlists.FirstOrDefault();

            List<Song> catalogue = await api.GetSongsAsync(search);

            List<Song> songs = selected == null
                ? new List<Song>()
                : await api.GetRankingsAsync(selected.Id, CurrentUserId);

            // Ignore an older refresh after a newer user action.
            if (closing || version != stateVersion)
                return;

            bindingLists = true;

            try
            {
                BindList(
                    lsb_playlists,
                    playlists,
                    selected == null ? (int?)null : selected.Id,
                    item => item.Id);

                BindList(
                    lsb_song_catalogue,
                    catalogue,
                    catalogueId,
                    item => item.Id);

                BindList(
                    lsb_songs_in_playlist,
                    songs,
                    songId,
                    item => item.Id);
            }
            finally
            {
                bindingLists = false;
            }

            Text = "CollaborativePlaylistBuilder - User 0"
                + (search.Length == 0 ? "" : " - Search: " + search);

            UpdateButtons();
        }

        private void BindList<T>(
            ListBox list,
            List<T> items,
            int? selectedId,
            Func<T, int> getId) where T : class
        {
            int topIndex = list.TopIndex;
            list.BeginUpdate();

            try
            {
                list.DataSource = items;

                T selected = selectedId.HasValue
                    ? items.FirstOrDefault(item => getId(item) == selectedId.Value)
                    : null;

                list.SelectedItem = selected ?? items.FirstOrDefault();

                if (items.Count > 0)
                    list.TopIndex = Math.Min(Math.Max(0, topIndex), items.Count - 1);
            }
            finally
            {
                list.EndUpdate();
            }
        }

        private Playlist RequirePlaylist()
        {
            if (SelectedPlaylist == null)
            {
                throw new InvalidOperationException(
                    "Select or create a playlist first.");
            }

            return SelectedPlaylist;
        }

        private async Task RunActionAsync(Func<Task> action)
        {
            if (busy || closing)
                return;

            busy = true;
            stateVersion++;

            SetControlsEnabled(false);
            UseWaitCursor = true;

            try
            {
                await action();
            }
            catch (Exception ex)
            {
                if (!closing)
                    ReportError(ex, true);
            }
            finally
            {
                busy = false;

                if (!closing)
                {
                    SetControlsEnabled(true);
                    UseWaitCursor = false;
                    UpdateButtons();
                }
            }
        }

        private void SetControlsEnabled(bool enabled)
        {
            foreach (Control control in Controls)
                control.Enabled = enabled;
        }

        private void UpdateButtons()
        {
            if (closing)
                return;

            Playlist playlist = SelectedPlaylist;
            bool owner = playlist != null && playlist.CreatedBy == CurrentUserId;

            btn_new_playlist.Enabled = !busy;
            btn_new_song.Enabled = !busy;
            btn_search.Enabled = !busy;

            btn_add_to_playlist.Enabled =
                !busy && playlist != null && SelectedCatalogueSong != null;

            btn_vote.Enabled =
                !busy && playlist != null
                && playlist.IsCollaborative
                && SelectedPlaylistSong != null;

            btn_remove_from_playlist.Enabled =
                !busy && owner && SelectedPlaylistSong != null;

            int collaboratorId;

            btn_add_collaborator.Enabled =
                !busy && owner
                && playlist.IsCollaborative
                && int.TryParse(cmb_users_list.Text, out collaboratorId)
                && collaboratorId >= 0
                && collaboratorId != CurrentUserId;
        }

        private void ReportError(Exception exception, bool showMessage)
        {
            bool connectionError =
                exception is HttpRequestException
                || exception is TaskCanceledException;

            string message = connectionError
                ? "Cannot reach the playlist API. Start PlaylistManagementService at "
                    + PlaylistApiClient.DefaultAddress
                : exception.Message;

            if (connectionError)
                Text = "CollaborativePlaylistBuilder - User 0 - API unavailable";

            if (showMessage)
            {
                MessageBox.Show(
                    this,
                    message,
                    "CollaborativePlaylistBuilder",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void ShowSuccess(string message)
        {
            if (!closing)
            {
                MessageBox.Show(
                    this,
                    message,
                    "CollaborativePlaylistBuilder",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void ConfigurePlaceholder(TextBox textBox, string placeholder)
        {
            placeholders[textBox] = placeholder;
            ResetInput(textBox);

            textBox.Enter += delegate
            {
                if (showingPlaceholder.Remove(textBox))
                {
                    textBox.Text = "";
                    textBox.ForeColor = SystemColors.WindowText;
                }
            };

            textBox.Leave += delegate
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                    ResetInput(textBox);
            };
        }

        private string ReadInput(TextBox textBox)
        {
            return showingPlaceholder.Contains(textBox)
                ? ""
                : textBox.Text.Trim();
        }

        private void ResetInput(TextBox textBox)
        {
            showingPlaceholder.Add(textBox);
            textBox.Text = placeholders[textBox];
            textBox.ForeColor = SystemColors.GrayText;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            closing = true;
            stateVersion++;

            refreshTimer.Stop();
            api.Dispose();

            base.OnFormClosed(e);
        }
    }
}