namespace PlaylistWinForms
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txb_new_playlist = new System.Windows.Forms.TextBox();
            this.btn_new_playlist = new System.Windows.Forms.Button();
            this.rbtn_collaborative_playlist = new System.Windows.Forms.RadioButton();
            this.lbl_new_playlist = new System.Windows.Forms.Label();
            this.lbl_new_song = new System.Windows.Forms.Label();
            this.btn_new_song = new System.Windows.Forms.Button();
            this.txb_new_song_title = new System.Windows.Forms.TextBox();
            this.txb_new_song_artist = new System.Windows.Forms.TextBox();
            this.txb_new_song_genre = new System.Windows.Forms.TextBox();
            this.txb_new_song_duration = new System.Windows.Forms.NumericUpDown();
            this.lbl_duration = new System.Windows.Forms.Label();
            this.lsb_playlists = new System.Windows.Forms.ListBox();
            this.lbl_playlists = new System.Windows.Forms.Label();
            this.lsb_song_catalogue = new System.Windows.Forms.ListBox();
            this.lbl_song_catalogue = new System.Windows.Forms.Label();
            this.btn_add_to_playlist = new System.Windows.Forms.Button();
            this.lbl_songs_in_playlist = new System.Windows.Forms.Label();
            this.lsb_songs_in_playlist = new System.Windows.Forms.ListBox();
            this.btn_vote = new System.Windows.Forms.Button();
            this.btn_remove_from_playlist = new System.Windows.Forms.Button();
            this.cmb_users_list = new System.Windows.Forms.ComboBox();
            this.btn_add_collaborator = new System.Windows.Forms.Button();
            this.lbl_add_collaborator = new System.Windows.Forms.Label();
            this.txb_search = new System.Windows.Forms.TextBox();
            this.btn_search = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txb_new_song_duration)).BeginInit();
            this.SuspendLayout();
            // 
            // txb_new_playlist
            // 
            this.txb_new_playlist.Location = new System.Drawing.Point(12, 32);
            this.txb_new_playlist.Name = "txb_new_playlist";
            this.txb_new_playlist.Size = new System.Drawing.Size(209, 22);
            this.txb_new_playlist.TabIndex = 0;
            this.txb_new_playlist.Text = "Playlist Name";
            // 
            // btn_new_playlist
            // 
            this.btn_new_playlist.Location = new System.Drawing.Point(12, 86);
            this.btn_new_playlist.Name = "btn_new_playlist";
            this.btn_new_playlist.Size = new System.Drawing.Size(209, 23);
            this.btn_new_playlist.TabIndex = 1;
            this.btn_new_playlist.Text = "Add Playlist";
            this.btn_new_playlist.UseVisualStyleBackColor = true;
            // 
            // rbtn_collaborative_playlist
            // 
            this.rbtn_collaborative_playlist.AutoSize = true;
            this.rbtn_collaborative_playlist.Location = new System.Drawing.Point(12, 60);
            this.rbtn_collaborative_playlist.Name = "rbtn_collaborative_playlist";
            this.rbtn_collaborative_playlist.Size = new System.Drawing.Size(161, 20);
            this.rbtn_collaborative_playlist.TabIndex = 2;
            this.rbtn_collaborative_playlist.TabStop = true;
            this.rbtn_collaborative_playlist.Text = "Collaborative Playlist?";
            this.rbtn_collaborative_playlist.UseVisualStyleBackColor = true;
            // 
            // lbl_new_playlist
            // 
            this.lbl_new_playlist.AutoSize = true;
            this.lbl_new_playlist.Location = new System.Drawing.Point(12, 13);
            this.lbl_new_playlist.Name = "lbl_new_playlist";
            this.lbl_new_playlist.Size = new System.Drawing.Size(80, 16);
            this.lbl_new_playlist.TabIndex = 3;
            this.lbl_new_playlist.Text = "New Playlist";
            // 
            // lbl_new_song
            // 
            this.lbl_new_song.AutoSize = true;
            this.lbl_new_song.Location = new System.Drawing.Point(12, 141);
            this.lbl_new_song.Name = "lbl_new_song";
            this.lbl_new_song.Size = new System.Drawing.Size(176, 16);
            this.lbl_new_song.TabIndex = 7;
            this.lbl_new_song.Text = "Add New Song to Catalogue";
            // 
            // btn_new_song
            // 
            this.btn_new_song.Location = new System.Drawing.Point(12, 273);
            this.btn_new_song.Name = "btn_new_song";
            this.btn_new_song.Size = new System.Drawing.Size(209, 23);
            this.btn_new_song.TabIndex = 5;
            this.btn_new_song.Text = "Add Song";
            this.btn_new_song.UseVisualStyleBackColor = true;
            // 
            // txb_new_song_title
            // 
            this.txb_new_song_title.Location = new System.Drawing.Point(12, 160);
            this.txb_new_song_title.Name = "txb_new_song_title";
            this.txb_new_song_title.Size = new System.Drawing.Size(209, 22);
            this.txb_new_song_title.TabIndex = 4;
            this.txb_new_song_title.Text = "Title";
            // 
            // txb_new_song_artist
            // 
            this.txb_new_song_artist.Location = new System.Drawing.Point(12, 189);
            this.txb_new_song_artist.Name = "txb_new_song_artist";
            this.txb_new_song_artist.Size = new System.Drawing.Size(209, 22);
            this.txb_new_song_artist.TabIndex = 8;
            this.txb_new_song_artist.Text = "Artist";
            // 
            // txb_new_song_genre
            // 
            this.txb_new_song_genre.Location = new System.Drawing.Point(12, 217);
            this.txb_new_song_genre.Name = "txb_new_song_genre";
            this.txb_new_song_genre.Size = new System.Drawing.Size(209, 22);
            this.txb_new_song_genre.TabIndex = 9;
            this.txb_new_song_genre.Text = "Genre";
            // 
            // txb_new_song_duration
            // 
            this.txb_new_song_duration.Location = new System.Drawing.Point(101, 245);
            this.txb_new_song_duration.Maximum = new decimal(new int[] {
            86399,
            0,
            0,
            0});
            this.txb_new_song_duration.Name = "txb_new_song_duration";
            this.txb_new_song_duration.Size = new System.Drawing.Size(120, 22);
            this.txb_new_song_duration.TabIndex = 10;
            // 
            // lbl_duration
            // 
            this.lbl_duration.AutoSize = true;
            this.lbl_duration.Location = new System.Drawing.Point(15, 246);
            this.lbl_duration.Name = "lbl_duration";
            this.lbl_duration.Size = new System.Drawing.Size(75, 16);
            this.lbl_duration.TabIndex = 11;
            this.lbl_duration.Text = "Duration (s)";
            // 
            // lsb_playlists
            // 
            this.lsb_playlists.FormattingEnabled = true;
            this.lsb_playlists.ItemHeight = 16;
            this.lsb_playlists.Location = new System.Drawing.Point(12, 354);
            this.lsb_playlists.Name = "lsb_playlists";
            this.lsb_playlists.Size = new System.Drawing.Size(209, 148);
            this.lsb_playlists.TabIndex = 12;
            // 
            // lbl_playlists
            // 
            this.lbl_playlists.AutoSize = true;
            this.lbl_playlists.Location = new System.Drawing.Point(12, 335);
            this.lbl_playlists.Name = "lbl_playlists";
            this.lbl_playlists.Size = new System.Drawing.Size(57, 16);
            this.lbl_playlists.TabIndex = 13;
            this.lbl_playlists.Text = "Playlists";
            // 
            // lsb_song_catalogue
            // 
            this.lsb_song_catalogue.FormattingEnabled = true;
            this.lsb_song_catalogue.ItemHeight = 16;
            this.lsb_song_catalogue.Location = new System.Drawing.Point(238, 60);
            this.lsb_song_catalogue.Name = "lsb_song_catalogue";
            this.lsb_song_catalogue.Size = new System.Drawing.Size(550, 148);
            this.lsb_song_catalogue.TabIndex = 14;
            // 
            // lbl_song_catalogue
            // 
            this.lbl_song_catalogue.AutoSize = true;
            this.lbl_song_catalogue.Location = new System.Drawing.Point(235, 13);
            this.lbl_song_catalogue.Name = "lbl_song_catalogue";
            this.lbl_song_catalogue.Size = new System.Drawing.Size(104, 16);
            this.lbl_song_catalogue.TabIndex = 15;
            this.lbl_song_catalogue.Text = "Song Catalogue";
            // 
            // btn_add_to_playlist
            // 
            this.btn_add_to_playlist.Location = new System.Drawing.Point(556, 217);
            this.btn_add_to_playlist.Name = "btn_add_to_playlist";
            this.btn_add_to_playlist.Size = new System.Drawing.Size(232, 23);
            this.btn_add_to_playlist.TabIndex = 16;
            this.btn_add_to_playlist.Text = "Add Song to Current Playlist";
            this.btn_add_to_playlist.UseVisualStyleBackColor = true;
            // 
            // lbl_songs_in_playlist
            // 
            this.lbl_songs_in_playlist.AutoSize = true;
            this.lbl_songs_in_playlist.Location = new System.Drawing.Point(235, 270);
            this.lbl_songs_in_playlist.Name = "lbl_songs_in_playlist";
            this.lbl_songs_in_playlist.Size = new System.Drawing.Size(105, 16);
            this.lbl_songs_in_playlist.TabIndex = 18;
            this.lbl_songs_in_playlist.Text = "Songs In Playlist";
            // 
            // lsb_songs_in_playlist
            // 
            this.lsb_songs_in_playlist.FormattingEnabled = true;
            this.lsb_songs_in_playlist.ItemHeight = 16;
            this.lsb_songs_in_playlist.Location = new System.Drawing.Point(238, 289);
            this.lsb_songs_in_playlist.Name = "lsb_songs_in_playlist";
            this.lsb_songs_in_playlist.Size = new System.Drawing.Size(550, 148);
            this.lsb_songs_in_playlist.TabIndex = 17;
            // 
            // btn_vote
            // 
            this.btn_vote.Location = new System.Drawing.Point(488, 443);
            this.btn_vote.Name = "btn_vote";
            this.btn_vote.Size = new System.Drawing.Size(118, 23);
            this.btn_vote.TabIndex = 19;
            this.btn_vote.Text = "Vote Song";
            this.btn_vote.UseVisualStyleBackColor = true;
            // 
            // btn_remove_from_playlist
            // 
            this.btn_remove_from_playlist.Location = new System.Drawing.Point(612, 443);
            this.btn_remove_from_playlist.Name = "btn_remove_from_playlist";
            this.btn_remove_from_playlist.Size = new System.Drawing.Size(176, 23);
            this.btn_remove_from_playlist.TabIndex = 20;
            this.btn_remove_from_playlist.Text = "Remove from Playlist";
            this.btn_remove_from_playlist.UseVisualStyleBackColor = true;
            // 
            // cmb_users_list
            // 
            this.cmb_users_list.FormattingEnabled = true;
            this.cmb_users_list.Location = new System.Drawing.Point(529, 478);
            this.cmb_users_list.Name = "cmb_users_list";
            this.cmb_users_list.Size = new System.Drawing.Size(121, 24);
            this.cmb_users_list.TabIndex = 21;
            // 
            // btn_add_collaborator
            // 
            this.btn_add_collaborator.Location = new System.Drawing.Point(656, 478);
            this.btn_add_collaborator.Name = "btn_add_collaborator";
            this.btn_add_collaborator.Size = new System.Drawing.Size(132, 23);
            this.btn_add_collaborator.TabIndex = 22;
            this.btn_add_collaborator.Text = "Invite Collaborator";
            this.btn_add_collaborator.UseVisualStyleBackColor = true;
            // 
            // lbl_add_collaborator
            // 
            this.lbl_add_collaborator.AutoSize = true;
            this.lbl_add_collaborator.Location = new System.Drawing.Point(333, 486);
            this.lbl_add_collaborator.Name = "lbl_add_collaborator";
            this.lbl_add_collaborator.Size = new System.Drawing.Size(109, 16);
            this.lbl_add_collaborator.TabIndex = 23;
            this.lbl_add_collaborator.Text = "Add Collaborator";
            // 
            // txb_search
            // 
            this.txb_search.Location = new System.Drawing.Point(471, 32);
            this.txb_search.Name = "txb_search";
            this.txb_search.Size = new System.Drawing.Size(203, 22);
            this.txb_search.TabIndex = 24;
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(681, 32);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(107, 23);
            this.btn_search.TabIndex = 25;
            this.btn_search.Text = "Search";
            this.btn_search.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 520);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.txb_search);
            this.Controls.Add(this.lbl_add_collaborator);
            this.Controls.Add(this.btn_add_collaborator);
            this.Controls.Add(this.cmb_users_list);
            this.Controls.Add(this.btn_remove_from_playlist);
            this.Controls.Add(this.btn_vote);
            this.Controls.Add(this.lbl_songs_in_playlist);
            this.Controls.Add(this.lsb_songs_in_playlist);
            this.Controls.Add(this.btn_add_to_playlist);
            this.Controls.Add(this.lbl_song_catalogue);
            this.Controls.Add(this.lsb_song_catalogue);
            this.Controls.Add(this.lbl_playlists);
            this.Controls.Add(this.lsb_playlists);
            this.Controls.Add(this.lbl_duration);
            this.Controls.Add(this.txb_new_song_duration);
            this.Controls.Add(this.txb_new_song_genre);
            this.Controls.Add(this.txb_new_song_artist);
            this.Controls.Add(this.lbl_new_song);
            this.Controls.Add(this.btn_new_song);
            this.Controls.Add(this.txb_new_song_title);
            this.Controls.Add(this.lbl_new_playlist);
            this.Controls.Add(this.rbtn_collaborative_playlist);
            this.Controls.Add(this.btn_new_playlist);
            this.Controls.Add(this.txb_new_playlist);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.txb_new_song_duration)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txb_new_playlist;
        private System.Windows.Forms.Button btn_new_playlist;
        private System.Windows.Forms.RadioButton rbtn_collaborative_playlist;
        private System.Windows.Forms.Label lbl_new_playlist;
        private System.Windows.Forms.Label lbl_new_song;
        private System.Windows.Forms.Button btn_new_song;
        private System.Windows.Forms.TextBox txb_new_song_title;
        private System.Windows.Forms.TextBox txb_new_song_artist;
        private System.Windows.Forms.TextBox txb_new_song_genre;
        private System.Windows.Forms.NumericUpDown txb_new_song_duration;
        private System.Windows.Forms.Label lbl_duration;
        private System.Windows.Forms.ListBox lsb_playlists;
        private System.Windows.Forms.Label lbl_playlists;
        private System.Windows.Forms.ListBox lsb_song_catalogue;
        private System.Windows.Forms.Label lbl_song_catalogue;
        private System.Windows.Forms.Button btn_add_to_playlist;
        private System.Windows.Forms.Label lbl_songs_in_playlist;
        private System.Windows.Forms.ListBox lsb_songs_in_playlist;
        private System.Windows.Forms.Button btn_vote;
        private System.Windows.Forms.Button btn_remove_from_playlist;
        private System.Windows.Forms.ComboBox cmb_users_list;
        private System.Windows.Forms.Button btn_add_collaborator;
        private System.Windows.Forms.Label lbl_add_collaborator;
        private System.Windows.Forms.TextBox txb_search;
        private System.Windows.Forms.Button btn_search;
    }
}

