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
            Album added = client.AddAlbum(new Album
            {
                Title = textBox_title.Text,
                Artist = textBox_artist.Text,
                Genre = textBox_genre.Text,
                Year = int.Parse(textBox_year.Text),
                Available = checkBox_available.Checked
            });
            MessageBox.Show("Added id " + added.Id);
        }

        private void button_Get_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox_id.Text);
            Album album = client.GetAlbum(new AlbumId { Id = id });
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
            int id = int.Parse(textBox_id.Text);
            Album updated = client.UpdateAlbum(new Album
            {
                Id = id,
                Title = textBox_title.Text,
                Artist = textBox_artist.Text,
                Genre = textBox_genre.Text,
                Year = int.Parse(textBox_year.Text),
                Available = checkBox_available.Checked
            });
            MessageBox.Show("Updated " + updated.Id);
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox_id.Text);
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