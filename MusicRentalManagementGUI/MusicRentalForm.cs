using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicRentalManagementGUI
{
    public partial class MusicRentalForm : Form
    {

        // --- test users --- //
        //dictionary of users to test module with until main form for entire project supplies users
        private readonly Dictionary<int, string> testUsers = new Dictionary<int, string>
        {
            {1, "Cloud Strife" },
            {2, "Squall Lionheart" },
            {3, "Terra Branford" }
        };

        //http client needed to communicate with APIs
        private static readonly HttpClient client = new HttpClient();

        public MusicRentalForm()
        {
            InitializeComponent();
        }


        // --- needed for testing --- //
        //populate combobox of test users
        private void MusicRentalForm_Load(object sender, EventArgs e)
        {
            foreach (var user in testUsers)
            {
                comboBox_testUser.Items.Add($"{user.Key} - {user.Value}");
            }

            comboBox_testUser.SelectedIndex = 0;
        }


        // ----- music album management ----- //


        // --- ADD ALBUM --- //
        private async void button_albumAdd_Click(object sender, EventArgs e)
        {

            // --- validating input --- //

            //ensuring fields are not left blank
            if (string.IsNullOrWhiteSpace(textBox_albumTitle.Text) ||
                string.IsNullOrWhiteSpace(textBox_albumArtist.Text) ||
                string.IsNullOrWhiteSpace(textBox_albumGenre.Text) ||
                string.IsNullOrWhiteSpace(textBox_albumYear.Text))
            {
                MessageBox.Show("All fields must be filled out", "Error");
                return;
            }

            //ensuring year is an int
            if (!int.TryParse(textBox_albumYear.Text, out int year))
            {
                MessageBox.Show("Year must be a number", "Error");
                return;
            }

            //ensuring year is a valid year
            if (year < 1850 || year > DateTime.Now.Year)
            {
                MessageBox.Show($"Year must be between 1850 and {DateTime.Now.Year}");
                return;
            }


            // --- compiling information from form into request --- //
            var newAlbum = new
            {
                Title = textBox_albumTitle.Text,
                Artist = textBox_albumArtist.Text,
                Genre = textBox_albumGenre.Text,
                Year = year
            };

            var json = JsonConvert.SerializeObject(newAlbum);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage message = null;

            //using try/catch to catch network errors if they crop up
            try
            {
                message = await client.PostAsync("https://localhost:7039/api/Music", content);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error");
                return;
            }

            if (message.IsSuccessStatusCode)
            {
                MessageBox.Show("Album added to catalog!", "Success");
            }
            else
            {
                MessageBox.Show("could not add album..", "error");
            }
        }

        // --- VIEW ALBUMS --- //

        //viewing all albums in catalog - each one noting if they are available to rent or not
        private async void button_albumsViewAll_Click(object sender, EventArgs e)
        {
            listBox_albumsView.Items.Clear();

            HttpResponseMessage message = null;

            try
            {
                message = await client.GetAsync("https://localhost:7039/api/Music");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error");
                return;
            }

            if (message.IsSuccessStatusCode)
            {
                string jsonString = await message.Content.ReadAsStringAsync();

                var albums = JsonConvert.DeserializeObject<List<Music>>(jsonString);

                listBox_albumsView.Items.Add("All albums in catalog");
                foreach (Music album in albums)
                {
                    string availability = album.IsAvailable ? "Available" : "Not Available";

                    listBox_albumsView.Items.Add($"[{availability}] {album.AlbumId}: {album.Title} by {album.Artist} ({album.Genre}, {album.Year})");
                }
            }
            else
            {
                MessageBox.Show("Failed to retrieve albums");
            }
        }


        // --- DELETE ALBUM --- //
        private async void button_albumDelete_Click(object sender, EventArgs e)
        {
            // ensure input is a number
            if (!int.TryParse(textBox_albumDelete.Text, out int albumId))
            {
                MessageBox.Show("Album ID must be a number..", "Error");
                return;
            }

            HttpResponseMessage message = null;

            try
            {
                message = await client.DeleteAsync($"https://localhost:7039/api/Music/{albumId}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error");
                return;
            }

            if (message.IsSuccessStatusCode)
            {
                MessageBox.Show("Album removed from catalog", "Success");
            }
            else
            {
                MessageBox.Show(await message.Content.ReadAsStringAsync(), "Error");
            }
        }


        // ----- Rental Management ----- //


        // --- ADD RENTAL (rent an album) --- //
        private async void button_rentAlbum_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox_rentalAlbumId.Text, out var albumID))
            {
                MessageBox.Show("Album ID must be a number.", "Error");
                return;
            }
            //combobox is zero-indexed so must add plus one to match user key
            int userID = comboBox_testUser.SelectedIndex + 1;


            HttpResponseMessage rentalMessage = null;

            try
            {
                //api determines if album exists and is available, then sets the isAvailable flag to false
                rentalMessage = await client.PostAsync($"https://localhost:7039/api/Rentals?userId={userID}&albumId={albumID}",
                    null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Rental API Error: {ex.Message}", "Error");
                return;
            }

            if (rentalMessage.IsSuccessStatusCode)
            {
                MessageBox.Show("Successfully checked out album!");
            }
            else
            {
                //shows error messages from api
                MessageBox.Show(await rentalMessage.Content.ReadAsStringAsync(), "Error");
            }
        }

        // --- RETURN AN ALBUM  --- //
        private async void button_returnAlbum_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox_returnAlbumId.Text, out var albumID))
            {
                MessageBox.Show("Album ID must be a number.", "Error");
                return;
            }

            // --- communicating with rental API --- //
            //setting return date on active rental
            HttpResponseMessage rentalMessage = null;

            try
            {
                rentalMessage = await client.PostAsync($"https://localhost:7039/api/Rentals/{albumID}/return", null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error");
                return;
            }

            if (rentalMessage.IsSuccessStatusCode)
            {
                MessageBox.Show("Album returned successfully!", "Success");
            }
            else
            {
                MessageBox.Show(await rentalMessage.Content.ReadAsStringAsync(), "Error");
            }
        }


        // --- VIEWING ACTIVE RENTALS --- //
        private async void button_rentalsViewActive_Click(object sender, EventArgs e)
        {
            listBox_rentalsView.Items.Clear();

            //communicating with rental and music apis to populate listview
            HttpResponseMessage rentalsMessage = null;
            HttpResponseMessage albumsMessage = null;

            try
            { 
                rentalsMessage = await client.GetAsync("https://localhost:7039/api/Rentals");

                albumsMessage = await client.GetAsync("https://localhost:7039/api/Music");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"API Error: {ex.Message}", "Error");
                return;
            }

            if (!rentalsMessage.IsSuccessStatusCode)
            {
                MessageBox.Show("Failed to retrieve rentals");
                return;
            }

            if (!albumsMessage.IsSuccessStatusCode)
            {
                MessageBox.Show("Failed to retrieve albums");
                return;
            }

            string rentalsString = await rentalsMessage.Content.ReadAsStringAsync();
            var rentals = JsonConvert.DeserializeObject<List<Rental>>(rentalsString);

            //filtering list to display active rentals
            //anything without return date would be currently rented out
            var activeRentals = rentals.Where(r => r.ReturnDate == null);


            string albumsString = await albumsMessage.Content.ReadAsStringAsync();
            var albums = JsonConvert.DeserializeObject<List<Music>>(albumsString);

            //iterating through rentals to display in listbox
            listBox_rentalsView.Items.Add("Current active rentals:");
            foreach (var rental in activeRentals)
            {
                testUsers.TryGetValue(rental.UserID, out string name);

                var album = albums.FirstOrDefault(a => a.AlbumId == rental.AlbumID);

                listBox_rentalsView.Items.Add(
                    $"User ID: [{rental.UserID}] {name} - Album ID: [{album.AlbumId}] {album.Title} by {album.Artist} ({album.Genre}, {album.Year}) - Rented: {rental.RentalDate}");
            }
        }

        // --- VIEWING HISTORY OF RENTALS --- //
        private async void button_rentalsHistory_Click(object sender, EventArgs e)
        {
            listBox_rentalsView.Items.Clear();

            HttpResponseMessage rentalsMessage = null;
            HttpResponseMessage albumsMessage = null;

            try
            {
                //rentals
                rentalsMessage = await client.GetAsync("https://localhost:7039/api/Rentals");

                //albums 
                albumsMessage = await client.GetAsync("https://localhost:7039/api/Music");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"API Error: {ex.Message}", "Error");
                return;
            }

            if (!rentalsMessage.IsSuccessStatusCode)
            {
                MessageBox.Show("Failed to retrieve rentals..");
                return;
            }

            if (!albumsMessage.IsSuccessStatusCode)
            {
                MessageBox.Show("Failed to retrieve albums from API");
                return;
            }

            string rentalsString = await rentalsMessage.Content.ReadAsStringAsync();
            var rentals = JsonConvert.DeserializeObject<List<Rental>>(rentalsString);

            string albumsString = await albumsMessage.Content.ReadAsStringAsync();
            var albums = JsonConvert.DeserializeObject<List<Music>>(albumsString);

            //iterating through every rental to display in listbox
            listBox_rentalsView.Items.Add("Complete rental records:");
            foreach (var rental in rentals)
            {
                //pulling values from testusers dictionary
                testUsers.TryGetValue(rental.UserID, out string name);

                var album = albums.FirstOrDefault(a => a.AlbumId == rental.AlbumID);

                //returndate is nullable, listbox will show "not returned" when there is no value
                //no value means that the album is currently being rented
                string returned = rental.ReturnDate.HasValue ? rental.ReturnDate.Value.ToString() : "Not yet returned";

                //album fields must be nullable in case album is deleted from catalog
                //a null album in the records could otherwise crash the app
                listBox_rentalsView.Items.Add(
                    $"User ID: [{rental.UserID}] {name} - Album ID: [{rental.AlbumID}] {album?.Title} by {album?.Artist} - Rented on: {rental.RentalDate} Returned on: {returned}");
            }
        }
    }
}
