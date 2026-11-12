using AlbumManagementGrpc;
using Grpc.Core;
using Grpc.Net.Client;

namespace AlbumClient
{
    public partial class AlbumForm : Form
    {
        private AlbumService.AlbumServiceClient client;

        public AlbumForm()
        {
            InitializeComponent();

            //  port to match running server
            string serverUrl = "https://localhost:7039";
            var channel = GrpcChannel.ForAddress(serverUrl);
            client = new AlbumService.AlbumServiceClient(channel);
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_title.Text) || string.IsNullOrWhiteSpace(textBox_artist.Text))
            {
                MessageBox.Show("Title and artist are required");
                return;
            }

            if (!int.TryParse(textBox_year.Text, out int year))
            {
                MessageBox.Show("Year must be a number");
                return;
            }

            Album added = client.AddAlbum(new Album
            {
                Title = textBox_title.Text,
                Artist = textBox_artist.Text,
                Genre = textBox_genre.Text,
                Year = year,
                Available = checkBox_available.Checked
            });
            MessageBox.Show("Added id " + added.Id);
        }

        private void button_Get_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox_id.Text, out int id))
            {
                MessageBox.Show("Id must be a number");
                return;
            }

            Album album = client.GetAlbum(new AlbumId { Id = id });

            // fill the boxes so we can edit without retyping
            textBox_title.Text = album.Title;
            textBox_artist.Text = album.Artist;
            textBox_genre.Text = album.Genre;
            textBox_year.Text = album.Year.ToString();
            checkBox_available.Checked = album.Available;

            MessageBox.Show(
                album.Id + ": " + album.Title + "\n" +
                "Artist: " + album.Artist + "\n" +
                "Genre: " + album.Genre + "\n" +
                "Year: " + album.Year + "\n" +
                "Available: " + album.Available
            );
        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox_id.Text, out int id))
            {
                MessageBox.Show("Id must be a number");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox_title.Text) || string.IsNullOrWhiteSpace(textBox_artist.Text))
            {
                MessageBox.Show("Title and artist are required");
                return;
            }

            if (!int.TryParse(textBox_year.Text, out int year))
            {
                MessageBox.Show("Year must be a number");
                return;
            }

            Album updated = client.UpdateAlbum(new Album
            {
                Id = id,
                Title = textBox_title.Text,
                Artist = textBox_artist.Text,
                Genre = textBox_genre.Text,
                Year = year,
                Available = checkBox_available.Checked
            });
            MessageBox.Show("Updated " + updated.Id);
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox_id.Text, out int id))
            {
                MessageBox.Show("Id must be a number");
                return;
            }

            client.DeleteAlbum(new AlbumId { Id = id });
            MessageBox.Show("Deleted");
        }

        private void button_List_Click(object sender, EventArgs e)
        {
            listBox_Albums.Items.Clear();

            using var call = client.ListAlbums(new Empty());
            while (call.ResponseStream.MoveNext().Result)
            {
                Album album = call.ResponseStream.Current;
                listBox_Albums.Items.Add(album.Id + ": " + album.Title + " by " + album.Artist);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        
    }
}