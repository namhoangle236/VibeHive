namespace MainMenu
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_MusicRental = new Button();
            btn_PlaylistBuilder = new Button();
            btn_AlbumManagement = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // btn_MusicRental
            // 
            btn_MusicRental.Location = new Point(278, 143);
            btn_MusicRental.Name = "btn_MusicRental";
            btn_MusicRental.Size = new Size(264, 51);
            btn_MusicRental.TabIndex = 0;
            btn_MusicRental.Text = "Music Rental Management";
            btn_MusicRental.UseVisualStyleBackColor = true;
            btn_MusicRental.Click += this.btn_MusicRental_Click;
            // 
            // btn_PlaylistBuilder
            // 
            btn_PlaylistBuilder.Location = new Point(278, 230);
            btn_PlaylistBuilder.Name = "btn_PlaylistBuilder";
            btn_PlaylistBuilder.Size = new Size(264, 51);
            btn_PlaylistBuilder.TabIndex = 1;
            btn_PlaylistBuilder.Text = "Playlist Builder";
            btn_PlaylistBuilder.UseVisualStyleBackColor = true;
            btn_PlaylistBuilder.Click += this.btn_PlaylistBuilder_Click;
            // 
            // btn_AlbumManagement
            // 
            btn_AlbumManagement.Location = new Point(278, 316);
            btn_AlbumManagement.Name = "btn_AlbumManagement";
            btn_AlbumManagement.Size = new Size(264, 51);
            btn_AlbumManagement.TabIndex = 2;
            btn_AlbumManagement.Text = "Album Inventory Management";
            btn_AlbumManagement.UseVisualStyleBackColor = true;
            btn_AlbumManagement.Click += this.btn_AlbumManagement_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            label1.Location = new Point(291, 52);
            label1.Name = "label1";
            label1.Size = new Size(241, 46);
            label1.TabIndex = 3;
            label1.Text = "VibeHive Hub";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(btn_AlbumManagement);
            Controls.Add(btn_PlaylistBuilder);
            Controls.Add(btn_MusicRental);
            Name = "MainForm";
            Text = "VibeHive Menu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_MusicRental;
        private Button btn_PlaylistBuilder;
        private Button btn_AlbumManagement;
        private Label label1;
    }
}
