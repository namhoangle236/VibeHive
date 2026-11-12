namespace MainMenu
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btn_MusicRental_Click(object sender, EventArgs e)
        {
            var form = new MusicRentalManagementGUI.MusicRentalForm();
            form.Show();
        }

        private void btn_PlaylistBuilder_Click(object sender, EventArgs e)
        {
            var form = new PlaylistWinForms.Form1();
            form.Show();
        }

        private void btn_AlbumManagement_Click(object sender, EventArgs e)
        {
            var form = new AlbumClient.AlbumForm();
            form.Show();
        }
    }
}