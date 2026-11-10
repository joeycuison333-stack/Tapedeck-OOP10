namespace mediaPlayerPrj
{
    partial class TapedeckForm
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>Clean up any resources being used.</summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlHeader = new Panel();
            picLogo = new PictureBox();
            lblAppTitle = new Label();
            lblGroup = new Label();
            btnMinimize = new Button();
            btnClose = new Button();
            pnlVideo = new Panel();
            lblNowCaption = new Label();
            lblTitle = new Label();
            lblArtist = new Label();
            lblExtra = new Label();
            lblElapsed = new Label();
            trackSeek = new TrackBar();
            btnPrevious = new Button();
            btnPlay = new Button();
            btnPause = new Button();
            btnStop = new Button();
            btnNext = new Button();
            btnShuffle = new Button();
            btnRepeat = new Button();
            lblRepeat = new Label();
            lblVolume = new Label();
            trackVolume = new TrackBar();
            lblLibrary = new Label();
            lblCount = new Label();
            txtSearch = new TextBox();
            btnSearch = new Button();
            listView1 = new ListView();
            colTitle = new ColumnHeader();
            colArtist = new ColumnHeader();
            colExtra = new ColumnHeader();
            colDuration = new ColumnHeader();
            colFavorite = new ColumnHeader();
            btnAdd = new Button();
            btnAddFolder = new Button();
            btnRemove = new Button();
            btnFavorite = new Button();
            lblRating = new Label();
            cboRating = new ComboBox();
            lblStatus = new Label();
            toolTip1 = new ToolTip(components);
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackSeek).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackVolume).BeginInit();
            SuspendLayout();
            //
            // pnlHeader (drag handle for the borderless window)
            //
            pnlHeader.BackColor = Color.Transparent;
            pnlHeader.Controls.Add(picLogo);
            pnlHeader.Controls.Add(lblAppTitle);
            pnlHeader.Controls.Add(lblGroup);
            pnlHeader.Controls.Add(btnMinimize);
            pnlHeader.Controls.Add(btnClose);
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(980, 46);
            pnlHeader.TabIndex = 0;
            //
            // picLogo
            //
            picLogo.BackColor = Color.Transparent;
            picLogo.Location = new Point(14, 7);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(32, 32);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabStop = false;
            //
            // lblAppTitle
            //
            lblAppTitle.AutoSize = true;
            lblAppTitle.BackColor = Color.Transparent;
            lblAppTitle.Font = new Font("Tahoma", 14F, FontStyle.Bold);
            lblAppTitle.Location = new Point(52, 9);
            lblAppTitle.Name = "lblAppTitle";
            lblAppTitle.Text = "Tapedeck";
            //
            // lblGroup
            //
            lblGroup.AutoSize = true;
            lblGroup.BackColor = Color.Transparent;
            lblGroup.Location = new Point(190, 17);
            lblGroup.Name = "lblGroup";
            lblGroup.Text = "OOP Group 3 | BSCS 2-A";
            //
            // btnMinimize
            //
            btnMinimize.Location = new Point(912, 11);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.Size = new Size(26, 24);
            btnMinimize.TabIndex = 20;
            btnMinimize.Text = "_";
            btnMinimize.Click += btnMinimize_Click;
            //
            // btnClose
            //
            btnClose.Location = new Point(942, 11);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(26, 24);
            btnClose.TabIndex = 21;
            btnClose.Text = "X";
            btnClose.Click += btnClose_Click;
            //
            // pnlVideo (VLC draws here; nothing is placed on top of it)
            //
            pnlVideo.BackColor = Color.Black;
            pnlVideo.Location = new Point(20, 80);
            pnlVideo.Name = "pnlVideo";
            pnlVideo.Size = new Size(560, 315);
            pnlVideo.TabIndex = 1;
            //
            // lblNowCaption
            //
            lblNowCaption.AutoSize = true;
            lblNowCaption.Location = new Point(32, 411);
            lblNowCaption.Name = "lblNowCaption";
            lblNowCaption.Padding = new Padding(2, 0, 2, 0);
            lblNowCaption.Text = "Now playing";
            //
            // lblTitle
            //
            lblTitle.AutoEllipsis = true;
            lblTitle.Font = new Font("Tahoma", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(36, 430);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(530, 30);
            lblTitle.Text = "Nothing playing";
            //
            // lblArtist
            //
            lblArtist.AutoEllipsis = true;
            lblArtist.Font = new Font("Tahoma", 10F);
            lblArtist.Location = new Point(36, 460);
            lblArtist.Name = "lblArtist";
            lblArtist.Size = new Size(380, 22);
            lblArtist.Text = "Add files with Ctrl+O";
            //
            // lblExtra
            //
            lblExtra.AutoEllipsis = true;
            lblExtra.Location = new Point(36, 482);
            lblExtra.Name = "lblExtra";
            lblExtra.Size = new Size(360, 20);
            lblExtra.Text = "";
            //
            // lblElapsed
            //
            lblElapsed.Font = new Font("Consolas", 10F);
            lblElapsed.Location = new Point(400, 480);
            lblElapsed.Name = "lblElapsed";
            lblElapsed.Size = new Size(166, 22);
            lblElapsed.Text = "00:00:00 / 00:00:00";
            lblElapsed.TextAlign = ContentAlignment.MiddleRight;
            //
            // trackSeek
            //
            trackSeek.AutoSize = false;
            trackSeek.Location = new Point(30, 504);
            trackSeek.Maximum = 1000;
            trackSeek.Name = "trackSeek";
            trackSeek.Size = new Size(540, 28);
            trackSeek.TabIndex = 2;
            trackSeek.TickStyle = TickStyle.None;
            trackSeek.Scroll += trackSeek_Scroll;
            trackSeek.MouseDown += trackSeek_MouseDown;
            trackSeek.MouseUp += trackSeek_MouseUp;
            //
            // btnPrevious
            //
            btnPrevious.Location = new Point(36, 538);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new Size(40, 36);
            btnPrevious.TabIndex = 3;
            btnPrevious.Text = "|<";
            btnPrevious.Click += btnPrevious_Click;
            //
            // btnPlay
            //
            btnPlay.Location = new Point(84, 534);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(44, 44);
            btnPlay.TabIndex = 4;
            btnPlay.Text = ">";
            btnPlay.Click += btnPlay_Click;
            //
            // btnPause (same spot as Play; the form shows one at a time)
            //
            btnPause.Location = new Point(84, 534);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(44, 44);
            btnPause.TabIndex = 5;
            btnPause.Text = "||";
            btnPause.Visible = false;
            btnPause.Click += btnPause_Click;
            //
            // btnStop
            //
            btnStop.Location = new Point(136, 538);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(40, 36);
            btnStop.TabIndex = 6;
            btnStop.Text = "[]";
            btnStop.Click += btnStop_Click;
            //
            // btnNext
            //
            btnNext.Location = new Point(184, 538);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(40, 36);
            btnNext.TabIndex = 7;
            btnNext.Text = ">|";
            btnNext.Click += btnNext_Click;
            //
            // btnShuffle
            //
            btnShuffle.Location = new Point(244, 538);
            btnShuffle.Name = "btnShuffle";
            btnShuffle.Size = new Size(40, 36);
            btnShuffle.TabIndex = 8;
            btnShuffle.Text = "Shf";
            btnShuffle.Click += btnShuffle_Click;
            //
            // btnRepeat
            //
            btnRepeat.Location = new Point(292, 538);
            btnRepeat.Name = "btnRepeat";
            btnRepeat.Size = new Size(40, 36);
            btnRepeat.TabIndex = 9;
            btnRepeat.Text = "Rpt";
            btnRepeat.Click += btnRepeat_Click;
            //
            // lblRepeat
            //
            lblRepeat.AutoSize = true;
            lblRepeat.Font = new Font("Tahoma", 8F);
            lblRepeat.Location = new Point(334, 548);
            lblRepeat.Name = "lblRepeat";
            lblRepeat.Text = "Off";
            //
            // lblVolume
            //
            lblVolume.AutoSize = true;
            lblVolume.Location = new Point(382, 548);
            lblVolume.Name = "lblVolume";
            lblVolume.Text = "Vol 80";
            //
            // trackVolume
            //
            trackVolume.AutoSize = false;
            trackVolume.Location = new Point(436, 542);
            trackVolume.Maximum = 100;
            trackVolume.Name = "trackVolume";
            trackVolume.Size = new Size(134, 28);
            trackVolume.TabIndex = 10;
            trackVolume.TickStyle = TickStyle.None;
            trackVolume.Value = 80;
            trackVolume.ValueChanged += trackVolume_ValueChanged;
            //
            // lblLibrary
            //
            lblLibrary.AutoSize = true;
            lblLibrary.Location = new Point(612, 91);
            lblLibrary.Name = "lblLibrary";
            lblLibrary.Padding = new Padding(2, 0, 2, 0);
            lblLibrary.Text = "Library";
            //
            // lblCount
            //
            lblCount.Location = new Point(812, 594);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(154, 18);
            lblCount.Text = "0 items";
            lblCount.TextAlign = ContentAlignment.MiddleLeft;
            //
            // txtSearch
            //
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Location = new Point(616, 116);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search title or artist";
            txtSearch.Size = new Size(250, 25);
            txtSearch.TabIndex = 11;
            txtSearch.KeyDown += txtSearch_KeyDown;
            //
            // btnSearch
            //
            btnSearch.Location = new Point(872, 115);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(72, 27);
            btnSearch.TabIndex = 12;
            btnSearch.Text = "Search";
            btnSearch.Click += btnSearch_Click;
            //
            // listView1 (the playlist)
            //
            listView1.BorderStyle = BorderStyle.None;
            listView1.Columns.AddRange(new ColumnHeader[] { colTitle, colArtist, colExtra, colDuration, colFavorite });
            listView1.FullRowSelect = true;
            listView1.HideSelection = false;
            listView1.Location = new Point(616, 152);
            listView1.MultiSelect = false;
            listView1.Name = "listView1";
            listView1.Size = new Size(328, 326);
            listView1.TabIndex = 13;
            listView1.View = View.Details;
            listView1.ColumnClick += listView1_ColumnClick;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            listView1.DoubleClick += listView1_DoubleClick;
            listView1.KeyDown += listView1_KeyDown;
            //
            // columns: 0 Title, 1 Artist, 2 Album / Resolution, 3 Duration, 4 Favorite and rating
            //
            colTitle.Text = "Title";
            colTitle.Width = 92;
            colArtist.Text = "Artist";
            colArtist.Width = 70;
            colExtra.Text = "Album/Res";
            colExtra.Width = 62;
            colDuration.Text = "Length";
            colDuration.Width = 58;
            colFavorite.Text = "♥ ★";
            colFavorite.Width = 40;
            //
            // btnAdd
            //
            btnAdd.Location = new Point(616, 490);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(70, 30);
            btnAdd.TabIndex = 14;
            btnAdd.Text = "Add";
            btnAdd.Click += btnAdd_Click;
            //
            // btnAddFolder
            //
            btnAddFolder.Location = new Point(692, 490);
            btnAddFolder.Name = "btnAddFolder";
            btnAddFolder.Size = new Size(92, 30);
            btnAddFolder.TabIndex = 15;
            btnAddFolder.Text = "Add Folder";
            btnAddFolder.Click += btnAddFolder_Click;
            //
            // btnRemove
            //
            btnRemove.Location = new Point(790, 490);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(76, 30);
            btnRemove.TabIndex = 16;
            btnRemove.Text = "Remove";
            btnRemove.Click += btnRemove_Click;
            //
            // btnFavorite
            //
            btnFavorite.Location = new Point(872, 490);
            btnFavorite.Name = "btnFavorite";
            btnFavorite.Size = new Size(72, 30);
            btnFavorite.TabIndex = 17;
            btnFavorite.Text = "♥ Fav";
            btnFavorite.Click += btnFavorite_Click;
            //
            // lblRating
            //
            lblRating.AutoSize = true;
            lblRating.Location = new Point(616, 536);
            lblRating.Name = "lblRating";
            lblRating.Text = "Rating:";
            //
            // cboRating
            //
            cboRating.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRating.FlatStyle = FlatStyle.Flat;
            cboRating.Items.AddRange(new object[] { "Not rated", "★", "★★", "★★★", "★★★★", "★★★★★" });
            cboRating.Location = new Point(670, 532);
            cboRating.Name = "cboRating";
            cboRating.Size = new Size(120, 25);
            cboRating.TabIndex = 18;
            cboRating.SelectedIndexChanged += cboRating_SelectedIndexChanged;
            //
            // lblStatus (messages go here because the title bar is hidden)
            //
            lblStatus.AutoEllipsis = true;
            lblStatus.Location = new Point(14, 594);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(782, 18);
            lblStatus.Text = "Ready";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            //
            // TapedeckForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(980, 620);
            Controls.Add(pnlHeader);
            Controls.Add(pnlVideo);
            Controls.Add(lblNowCaption);
            Controls.Add(lblTitle);
            Controls.Add(lblArtist);
            Controls.Add(lblExtra);
            Controls.Add(lblElapsed);
            Controls.Add(trackSeek);
            Controls.Add(btnPrevious);
            Controls.Add(btnPlay);
            Controls.Add(btnPause);
            Controls.Add(btnStop);
            Controls.Add(btnNext);
            Controls.Add(btnShuffle);
            Controls.Add(btnRepeat);
            Controls.Add(lblRepeat);
            Controls.Add(lblVolume);
            Controls.Add(trackVolume);
            Controls.Add(lblLibrary);
            Controls.Add(lblCount);
            Controls.Add(txtSearch);
            Controls.Add(btnSearch);
            Controls.Add(listView1);
            Controls.Add(btnAdd);
            Controls.Add(btnAddFolder);
            Controls.Add(btnRemove);
            Controls.Add(btnFavorite);
            Controls.Add(lblRating);
            Controls.Add(cboRating);
            Controls.Add(lblStatus);
            Font = new Font("Tahoma", 9F);
            KeyPreview = true;
            MaximizeBox = false;
            Name = "TapedeckForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tapedeck";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackSeek).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackVolume).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlHeader;
        private PictureBox picLogo;
        private Label lblAppTitle;
        private Label lblGroup;
        private Button btnMinimize;
        private Button btnClose;
        private Panel pnlVideo;
        private Label lblNowCaption;
        private Label lblTitle;
        private Label lblArtist;
        private Label lblExtra;
        private Label lblElapsed;
        private TrackBar trackSeek;
        private Button btnPrevious;
        private Button btnPlay;
        private Button btnPause;
        private Button btnStop;
        private Button btnNext;
        private Button btnShuffle;
        private Button btnRepeat;
        private Label lblRepeat;
        private Label lblVolume;
        private TrackBar trackVolume;
        private Label lblLibrary;
        private Label lblCount;
        private TextBox txtSearch;
        private Button btnSearch;
        private ListView listView1;
        private ColumnHeader colTitle;
        private ColumnHeader colArtist;
        private ColumnHeader colExtra;
        private ColumnHeader colDuration;
        private ColumnHeader colFavorite;
        private Button btnAdd;
        private Button btnAddFolder;
        private Button btnRemove;
        private Button btnFavorite;
        private Label lblRating;
        private ComboBox cboRating;
        private Label lblStatus;
        private ToolTip toolTip1;
    }
}
