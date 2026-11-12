namespace AlbumClient
{
    partial class AlbumForm
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
            textBox_title = new TextBox();
            textBox_artist = new TextBox();
            textBox_genre = new TextBox();
            textBox_year = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            checkBox_available = new CheckBox();
            button_Add = new Button();
            textBox_id = new TextBox();
            label5 = new Label();
            button_Get = new Button();
            button_Update = new Button();
            button_Delete = new Button();
            listBox_Albums = new ListBox();
            button_List = new Button();
            label6 = new Label();
            label7 = new Label();
            SuspendLayout();
            // 
            // textBox_title
            // 
            textBox_title.Location = new Point(101, 53);
            textBox_title.Name = "textBox_title";
            textBox_title.Size = new Size(125, 27);
            textBox_title.TabIndex = 0;
            // 
            // textBox_artist
            // 
            textBox_artist.Location = new Point(101, 86);
            textBox_artist.Name = "textBox_artist";
            textBox_artist.Size = new Size(125, 27);
            textBox_artist.TabIndex = 1;
            // 
            // textBox_genre
            // 
            textBox_genre.Location = new Point(101, 119);
            textBox_genre.Name = "textBox_genre";
            textBox_genre.Size = new Size(125, 27);
            textBox_genre.TabIndex = 2;
            // 
            // textBox_year
            // 
            textBox_year.Location = new Point(101, 152);
            textBox_year.Name = "textBox_year";
            textBox_year.Size = new Size(125, 27);
            textBox_year.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(43, 60);
            label1.Name = "label1";
            label1.Size = new Size(43, 20);
            label1.TabIndex = 4;
            label1.Text = "Tittle";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(43, 93);
            label2.Name = "label2";
            label2.Size = new Size(44, 20);
            label2.TabIndex = 5;
            label2.Text = "Artist";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(43, 126);
            label3.Name = "label3";
            label3.Size = new Size(48, 20);
            label3.TabIndex = 6;
            label3.Text = "Genre";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(43, 159);
            label4.Name = "label4";
            label4.Size = new Size(37, 20);
            label4.TabIndex = 7;
            label4.Text = "Year";
            // 
            // checkBox_available
            // 
            checkBox_available.AutoSize = true;
            checkBox_available.Location = new Point(101, 185);
            checkBox_available.Name = "checkBox_available";
            checkBox_available.Size = new Size(93, 24);
            checkBox_available.TabIndex = 8;
            checkBox_available.Text = "Available";
            checkBox_available.UseVisualStyleBackColor = true;
            // 
            // button_Add
            // 
            button_Add.Location = new Point(232, 86);
            button_Add.Name = "button_Add";
            button_Add.Size = new Size(125, 39);
            button_Add.TabIndex = 9;
            button_Add.Text = "Add Album";
            button_Add.UseVisualStyleBackColor = true;
            button_Add.Click += this.button_Add_Click;
            // 
            // textBox_id
            // 
            textBox_id.Location = new Point(613, 74);
            textBox_id.Name = "textBox_id";
            textBox_id.Size = new Size(125, 27);
            textBox_id.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(535, 81);
            label5.Name = "label5";
            label5.Size = new Size(72, 20);
            label5.TabIndex = 11;
            label5.Text = "Album ID";
            // 
            // button_Get
            // 
            button_Get.Location = new Point(544, 117);
            button_Get.Name = "button_Get";
            button_Get.Size = new Size(94, 29);
            button_Get.TabIndex = 12;
            button_Get.Text = "GET";
            button_Get.UseVisualStyleBackColor = true;
            button_Get.Click += this.button_Get_Click;
            // 
            // button_Update
            // 
            button_Update.Location = new Point(558, 323);
            button_Update.Name = "button_Update";
            button_Update.Size = new Size(94, 29);
            button_Update.TabIndex = 13;
            button_Update.Text = "UPDATE";
            button_Update.UseVisualStyleBackColor = true;
            // 
            // button_Delete
            // 
            button_Delete.Location = new Point(644, 117);
            button_Delete.Name = "button_Delete";
            button_Delete.Size = new Size(94, 29);
            button_Delete.TabIndex = 14;
            button_Delete.Text = "DELETE";
            button_Delete.UseVisualStyleBackColor = true;
            button_Delete.Click += this.button_Delete_Click;
            // 
            // listBox_Albums
            // 
            listBox_Albums.FormattingEnabled = true;
            listBox_Albums.Location = new Point(43, 260);
            listBox_Albums.Name = "listBox_Albums";
            listBox_Albums.Size = new Size(351, 164);
            listBox_Albums.TabIndex = 15;
            // 
            // button_List
            // 
            button_List.Location = new Point(43, 225);
            button_List.Name = "button_List";
            button_List.Size = new Size(94, 29);
            button_List.TabIndex = 16;
            button_List.Text = "List Albums";
            button_List.UseVisualStyleBackColor = true;
            button_List.Click += this.button_List_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(426, 300);
            label6.Name = "label6";
            label6.Size = new Size(362, 20);
            label6.TabIndex = 17;
            label6.Text = "ACTUNG! For 'update', both id and fields are required";           
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label7.Location = new Point(267, 9);
            label7.Name = "label7";
            label7.Size = new Size(284, 41);
            label7.TabIndex = 18;
            label7.Text = "ALBUM MANAGER";
            // 
            // AlbumForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(button_List);
            Controls.Add(listBox_Albums);
            Controls.Add(button_Delete);
            Controls.Add(button_Update);
            Controls.Add(button_Get);
            Controls.Add(label5);
            Controls.Add(textBox_id);
            Controls.Add(button_Add);
            Controls.Add(checkBox_available);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox_year);
            Controls.Add(textBox_genre);
            Controls.Add(textBox_artist);
            Controls.Add(textBox_title);
            Name = "AlbumForm";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox_title;
        private TextBox textBox_artist;
        private TextBox textBox_genre;
        private TextBox textBox_year;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private CheckBox checkBox_available;
        private Button button_Add;
        private TextBox textBox_id;
        private Label label5;
        private Button button_Get;
        private Button button_Update;
        private Button button_Delete;
        private ListBox listBox_Albums;
        private Button button_List;
        private Label label6;
        private Label label7;
    }
}
