using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
//using System.Data;
//using System.Xml.Serialization;

namespace trianglePegs
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class board : System.Windows.Forms.Form
	{
        private System.Windows.Forms.ImageList pegImages;
        
        #region hole picture boxes
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.PictureBox pictureBox11;
        private System.Windows.Forms.PictureBox pictureBox12;
        private System.Windows.Forms.PictureBox pictureBox13;
        private System.Windows.Forms.PictureBox pictureBox14;
        private System.Windows.Forms.PictureBox pictureBox15;
        #endregion

        private  System.Collections.ArrayList holes;
        private int pegToMove;
        private int pegDestination;

        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.StatusBar statusBar;
        private System.Windows.Forms.StatusBarPanel message;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem saveMovesMenu;
        private System.Windows.Forms.MenuItem menuNew;
        private System.Windows.Forms.MenuItem undoMenu;
        private System.Windows.Forms.MenuItem editMenu;
        private System.Windows.Forms.MenuItem fileMenu;
        private System.Windows.Forms.MenuItem optionsMenu;
        private System.Windows.Forms.MenuItem randomEmptyMenu;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem emptyHole1Menu;
        private System.Windows.Forms.MenuItem emptyHole2Menu;
        private System.Windows.Forms.MenuItem emptyHole3Menu;
        private System.Windows.Forms.MenuItem emptyHole4Menu;
        private System.Windows.Forms.MenuItem emptyHole5Menu;
        private System.Windows.Forms.MenuItem emptyHole6Menu;
        private System.Windows.Forms.MenuItem emptyHole7Menu;
        private System.Windows.Forms.MenuItem emptyHole8Menu;
        private System.Windows.Forms.MenuItem emptyHole9Menu;
        private System.Windows.Forms.MenuItem emptyHole10Menu;
        private System.Windows.Forms.MenuItem emptyHole11Menu;
        private System.Windows.Forms.MenuItem emptyHole12Menu;
        private System.Windows.Forms.MenuItem emptyHole13Menu;
        private System.Windows.Forms.MenuItem emptyHole14Menu;
        private System.Windows.Forms.MenuItem emptyHole15Menu;
        private System.Windows.Forms.MenuItem SuggestMove;
        private System.Windows.Forms.MenuItem loadMovesMenu;

        private game theGame;
        private StatusBarPanel bestPossible;
        private GameDomain gameDomain;

		public board()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            gameDomain = new GameDomain();

            theGame = new game();
            theGame.SetupBoard(EmptyHoleChoice());
            holes = new System.Collections.ArrayList(15);
            pegToMove = -1;
            pegDestination = -1;

            layoutTheBoard();

            setTheBoard();
            pegToMove = -1;
            pegDestination = -1;
            
        }


        void layoutTheBoard()
        {
            holes.Add(pictureBox1);
            holes.Add(pictureBox2);
            holes.Add(pictureBox3);
            holes.Add(pictureBox4);
            holes.Add(pictureBox5);
            holes.Add(pictureBox6);
            holes.Add(pictureBox7);
            holes.Add(pictureBox8);
            holes.Add(pictureBox9);
            holes.Add(pictureBox10);
            holes.Add(pictureBox11);
            holes.Add(pictureBox12);
            holes.Add(pictureBox13);
            holes.Add(pictureBox14);
            holes.Add(pictureBox15);

            int widthOfPeg = pegImages.ImageSize.Width;
            Width = pegImages.ImageSize.Width * (11+4);

            pictureBox1.Left = (Width /2 ) - (widthOfPeg/2);
            
            pictureBox2.Left = pictureBox1.Left - (widthOfPeg);
            pictureBox3.Left = pictureBox1.Right;

            pictureBox4.Left = pictureBox2.Left - (widthOfPeg);
            pictureBox5.Left = pictureBox1.Left;
            pictureBox6.Left = pictureBox3.Right;
            
            pictureBox7.Left = pictureBox4.Left - (widthOfPeg);
            pictureBox8.Left = pictureBox5.Left - (widthOfPeg);
            pictureBox9.Left = pictureBox5.Right;
            pictureBox10.Left = pictureBox6.Right;

            pictureBox11.Left = pictureBox7.Left - (widthOfPeg);
            pictureBox12.Left = pictureBox8.Left - (widthOfPeg);
            pictureBox13.Left = pictureBox1.Left;
            pictureBox14.Left = pictureBox9.Right;
            pictureBox15.Left = pictureBox10.Right;

            pictureBox1.Top = 10;

            pictureBox2.Top = pictureBox1.Bottom + 10;
            pictureBox3.Top = pictureBox1.Bottom + 10;

            pictureBox4.Top = pictureBox2.Bottom + 10;
            pictureBox5.Top = pictureBox2.Bottom + 10;
            pictureBox6.Top = pictureBox2.Bottom + 10;
            
            pictureBox7.Top = pictureBox4.Bottom + 10;
            pictureBox8.Top = pictureBox4.Bottom + 10;
            pictureBox9.Top = pictureBox4.Bottom + 10;
            pictureBox10.Top = pictureBox4.Bottom + 10;

            pictureBox11.Top = pictureBox7.Bottom + 10;
            pictureBox12.Top = pictureBox7.Bottom + 10;
            pictureBox13.Top = pictureBox7.Bottom + 10;
            pictureBox14.Top = pictureBox7.Bottom + 10;
            pictureBox15.Top = pictureBox7.Bottom + 10;

            for (int idx = 0; idx < 15; idx++)
                ((System.Windows.Forms.PictureBox)holes[idx]).Click += new System.EventHandler(this.ClickedAHole);

            this.emptyHole1Menu.Click += new System.EventHandler(this.optionsMenu_Click);
            this.emptyHole2Menu.Click += new System.EventHandler(this.optionsMenu_Click);
            this.emptyHole3Menu.Click += new System.EventHandler(this.optionsMenu_Click);
            this.emptyHole4Menu.Click += new System.EventHandler(this.optionsMenu_Click);
            this.emptyHole5Menu.Click += new System.EventHandler(this.optionsMenu_Click);
            this.emptyHole6Menu.Click += new System.EventHandler(this.optionsMenu_Click);
            this.emptyHole7Menu.Click += new System.EventHandler(this.optionsMenu_Click);
            this.emptyHole8Menu.Click += new System.EventHandler(this.optionsMenu_Click);
            this.emptyHole9Menu.Click += new System.EventHandler(this.optionsMenu_Click);
            this.emptyHole10Menu.Click += new System.EventHandler(this.optionsMenu_Click);
            this.emptyHole11Menu.Click += new System.EventHandler(this.optionsMenu_Click);
            this.emptyHole12Menu.Click += new System.EventHandler(this.optionsMenu_Click);
            this.emptyHole13Menu.Click += new System.EventHandler(this.optionsMenu_Click);
            this.emptyHole14Menu.Click += new System.EventHandler(this.optionsMenu_Click);
            this.emptyHole15Menu.Click += new System.EventHandler(this.optionsMenu_Click);
            this.randomEmptyMenu.Click += new System.EventHandler(this.optionsMenu_Click);
        }


        void setTheBoard()
        {
            for (int idx = 0; idx < 15; idx++)
                ((System.Windows.Forms.PictureBox)holes[idx]).Image = GetPegImage(idx);

            try
            {
                MoveTuple lastMove = theGame.GetLastMove;
                message.Text = string.Format("last move was from {0} to {1}", lastMove.original, lastMove.destination);
                bestPossible.Text = string.Format("{0}", gameDomain.BestPossibleScore(theGame.Signature));
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                message.Text = string.Format("unable to determine last move");
            }

        }
        

        System.Drawing.Image GetPegImage(int space)
        {
            if (theGame.IsSpaceOpen(space))
            {
                return pegImages.Images[0];
            }
            else
            {
                if ( space == pegToMove)
                {
                    return pegImages.Images[2];
                }
                else
                {
                    return pegImages.Images[1];
                }
            }
        }


		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// 
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(board));
            this.pegImages = new System.Windows.Forms.ImageList(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.pictureBox12 = new System.Windows.Forms.PictureBox();
            this.pictureBox13 = new System.Windows.Forms.PictureBox();
            this.pictureBox14 = new System.Windows.Forms.PictureBox();
            this.pictureBox15 = new System.Windows.Forms.PictureBox();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.message = new System.Windows.Forms.StatusBarPanel();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.fileMenu = new System.Windows.Forms.MenuItem();
            this.menuNew = new System.Windows.Forms.MenuItem();
            this.saveMovesMenu = new System.Windows.Forms.MenuItem();
            this.loadMovesMenu = new System.Windows.Forms.MenuItem();
            this.editMenu = new System.Windows.Forms.MenuItem();
            this.undoMenu = new System.Windows.Forms.MenuItem();
            this.optionsMenu = new System.Windows.Forms.MenuItem();
            this.SuggestMove = new System.Windows.Forms.MenuItem();
            this.randomEmptyMenu = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.emptyHole1Menu = new System.Windows.Forms.MenuItem();
            this.emptyHole2Menu = new System.Windows.Forms.MenuItem();
            this.emptyHole3Menu = new System.Windows.Forms.MenuItem();
            this.emptyHole4Menu = new System.Windows.Forms.MenuItem();
            this.emptyHole5Menu = new System.Windows.Forms.MenuItem();
            this.emptyHole6Menu = new System.Windows.Forms.MenuItem();
            this.emptyHole7Menu = new System.Windows.Forms.MenuItem();
            this.emptyHole8Menu = new System.Windows.Forms.MenuItem();
            this.emptyHole9Menu = new System.Windows.Forms.MenuItem();
            this.emptyHole10Menu = new System.Windows.Forms.MenuItem();
            this.emptyHole11Menu = new System.Windows.Forms.MenuItem();
            this.emptyHole12Menu = new System.Windows.Forms.MenuItem();
            this.emptyHole13Menu = new System.Windows.Forms.MenuItem();
            this.emptyHole14Menu = new System.Windows.Forms.MenuItem();
            this.emptyHole15Menu = new System.Windows.Forms.MenuItem();
            this.bestPossible = new System.Windows.Forms.StatusBarPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.message)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bestPossible)).BeginInit();
            this.SuspendLayout();
            // 
            // pegImages
            // 
            this.pegImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("pegImages.ImageStream")));
            this.pegImages.TransparentColor = System.Drawing.Color.Transparent;
            this.pegImages.Images.SetKeyName(0, "");
            this.pegImages.Images.SetKeyName(1, "");
            this.pegImages.Images.SetKeyName(2, "");
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(128, 48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(104, 72);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.TabIndex = 17;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(144, 72);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(16, 16);
            this.pictureBox3.TabIndex = 18;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Location = new System.Drawing.Point(80, 96);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(16, 16);
            this.pictureBox4.TabIndex = 19;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Location = new System.Drawing.Point(128, 96);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(16, 16);
            this.pictureBox5.TabIndex = 20;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Location = new System.Drawing.Point(168, 96);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(16, 16);
            this.pictureBox6.TabIndex = 21;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox7
            // 
            this.pictureBox7.Location = new System.Drawing.Point(64, 128);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(16, 16);
            this.pictureBox7.TabIndex = 22;
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox8
            // 
            this.pictureBox8.Location = new System.Drawing.Point(104, 128);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(16, 16);
            this.pictureBox8.TabIndex = 23;
            this.pictureBox8.TabStop = false;
            // 
            // pictureBox9
            // 
            this.pictureBox9.Location = new System.Drawing.Point(144, 128);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(16, 16);
            this.pictureBox9.TabIndex = 24;
            this.pictureBox9.TabStop = false;
            // 
            // pictureBox10
            // 
            this.pictureBox10.Location = new System.Drawing.Point(184, 128);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(16, 16);
            this.pictureBox10.TabIndex = 25;
            this.pictureBox10.TabStop = false;
            // 
            // pictureBox11
            // 
            this.pictureBox11.Location = new System.Drawing.Point(48, 152);
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.Size = new System.Drawing.Size(16, 16);
            this.pictureBox11.TabIndex = 26;
            this.pictureBox11.TabStop = false;
            // 
            // pictureBox12
            // 
            this.pictureBox12.Location = new System.Drawing.Point(88, 144);
            this.pictureBox12.Name = "pictureBox12";
            this.pictureBox12.Size = new System.Drawing.Size(16, 16);
            this.pictureBox12.TabIndex = 27;
            this.pictureBox12.TabStop = false;
            // 
            // pictureBox13
            // 
            this.pictureBox13.Location = new System.Drawing.Point(120, 152);
            this.pictureBox13.Name = "pictureBox13";
            this.pictureBox13.Size = new System.Drawing.Size(16, 16);
            this.pictureBox13.TabIndex = 28;
            this.pictureBox13.TabStop = false;
            // 
            // pictureBox14
            // 
            this.pictureBox14.Location = new System.Drawing.Point(168, 152);
            this.pictureBox14.Name = "pictureBox14";
            this.pictureBox14.Size = new System.Drawing.Size(16, 16);
            this.pictureBox14.TabIndex = 29;
            this.pictureBox14.TabStop = false;
            // 
            // pictureBox15
            // 
            this.pictureBox15.Location = new System.Drawing.Point(208, 152);
            this.pictureBox15.Name = "pictureBox15";
            this.pictureBox15.Size = new System.Drawing.Size(16, 16);
            this.pictureBox15.TabIndex = 30;
            this.pictureBox15.TabStop = false;
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 215);
            this.statusBar.Name = "statusBar";
            this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.message,
            this.bestPossible});
            this.statusBar.ShowPanels = true;
            this.statusBar.Size = new System.Drawing.Size(238, 24);
            this.statusBar.SizingGrip = false;
            this.statusBar.TabIndex = 31;
            // 
            // message
            // 
            this.message.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.message.MinWidth = 97;
            this.message.Name = "message";
            this.message.Text = "last move";
            this.message.ToolTipText = "This is status is shown";
            this.message.Width = 97;
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.fileMenu,
            this.editMenu,
            this.optionsMenu});
            // 
            // fileMenu
            // 
            this.fileMenu.Index = 0;
            this.fileMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuNew,
            this.saveMovesMenu,
            this.loadMovesMenu});
            this.fileMenu.Text = "File";
            // 
            // menuNew
            // 
            this.menuNew.Index = 0;
            this.menuNew.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.menuNew.Text = "&New";
            this.menuNew.Click += new System.EventHandler(this.menuNew_Click);
            // 
            // saveMovesMenu
            // 
            this.saveMovesMenu.Index = 1;
            this.saveMovesMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftS;
            this.saveMovesMenu.Text = "&Save Moves";
            this.saveMovesMenu.Click += new System.EventHandler(this.saveMovesMenu_Click);
            // 
            // loadMovesMenu
            // 
            this.loadMovesMenu.Index = 2;
            this.loadMovesMenu.Text = "&Load Moves";
            this.loadMovesMenu.Click += new System.EventHandler(this.loadMovesMenu_Click);
            // 
            // editMenu
            // 
            this.editMenu.Index = 1;
            this.editMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.undoMenu});
            this.editMenu.Text = "Edit";
            // 
            // undoMenu
            // 
            this.undoMenu.Index = 0;
            this.undoMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
            this.undoMenu.Text = "&Undo";
            this.undoMenu.Click += new System.EventHandler(this.undoMenu_Click);
            // 
            // optionsMenu
            // 
            this.optionsMenu.Index = 2;
            this.optionsMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.SuggestMove,
            this.randomEmptyMenu,
            this.menuItem1,
            this.emptyHole1Menu,
            this.emptyHole2Menu,
            this.emptyHole3Menu,
            this.emptyHole4Menu,
            this.emptyHole5Menu,
            this.emptyHole6Menu,
            this.emptyHole7Menu,
            this.emptyHole8Menu,
            this.emptyHole9Menu,
            this.emptyHole10Menu,
            this.emptyHole11Menu,
            this.emptyHole12Menu,
            this.emptyHole13Menu,
            this.emptyHole14Menu,
            this.emptyHole15Menu});
            this.optionsMenu.Text = "Options";
            // 
            // SuggestMove
            // 
            this.SuggestMove.Index = 0;
            this.SuggestMove.Text = "&Suggest Move";
            this.SuggestMove.Click += new System.EventHandler(this.SuggestMove_Click);
            // 
            // randomEmptyMenu
            // 
            this.randomEmptyMenu.Checked = true;
            this.randomEmptyMenu.Index = 1;
            this.randomEmptyMenu.Text = "&Random Empty";
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 2;
            this.menuItem1.Text = "-";
            // 
            // emptyHole1Menu
            // 
            this.emptyHole1Menu.Index = 3;
            this.emptyHole1Menu.Text = "&1";
            // 
            // emptyHole2Menu
            // 
            this.emptyHole2Menu.Index = 4;
            this.emptyHole2Menu.Text = "&2";
            // 
            // emptyHole3Menu
            // 
            this.emptyHole3Menu.Index = 5;
            this.emptyHole3Menu.Text = "&3";
            // 
            // emptyHole4Menu
            // 
            this.emptyHole4Menu.Index = 6;
            this.emptyHole4Menu.Text = "&4";
            // 
            // emptyHole5Menu
            // 
            this.emptyHole5Menu.Index = 7;
            this.emptyHole5Menu.Text = "&5";
            // 
            // emptyHole6Menu
            // 
            this.emptyHole6Menu.Index = 8;
            this.emptyHole6Menu.Text = "&6";
            // 
            // emptyHole7Menu
            // 
            this.emptyHole7Menu.Index = 9;
            this.emptyHole7Menu.Text = "&7";
            // 
            // emptyHole8Menu
            // 
            this.emptyHole8Menu.Index = 10;
            this.emptyHole8Menu.Text = "&8";
            // 
            // emptyHole9Menu
            // 
            this.emptyHole9Menu.Index = 11;
            this.emptyHole9Menu.Text = "&9";
            // 
            // emptyHole10Menu
            // 
            this.emptyHole10Menu.Index = 12;
            this.emptyHole10Menu.Text = "&10";
            // 
            // emptyHole11Menu
            // 
            this.emptyHole11Menu.Index = 13;
            this.emptyHole11Menu.Text = "&11";
            // 
            // emptyHole12Menu
            // 
            this.emptyHole12Menu.Index = 14;
            this.emptyHole12Menu.Text = "&12";
            // 
            // emptyHole13Menu
            // 
            this.emptyHole13Menu.Index = 15;
            this.emptyHole13Menu.Text = "&13";
            // 
            // emptyHole14Menu
            // 
            this.emptyHole14Menu.Index = 16;
            this.emptyHole14Menu.Text = "&14";
            // 
            // emptyHole15Menu
            // 
            this.emptyHole15Menu.Index = 17;
            this.emptyHole15Menu.Text = "&15";
            // 
            // bestPossible
            // 
            this.bestPossible.Name = "bestPossible";
            // 
            // board
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(238, 239);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.pictureBox15);
            this.Controls.Add(this.pictureBox14);
            this.Controls.Add(this.pictureBox13);
            this.Controls.Add(this.pictureBox12);
            this.Controls.Add(this.pictureBox11);
            this.Controls.Add(this.pictureBox10);
            this.Controls.Add(this.pictureBox9);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Menu = this.mainMenu1;
            this.Name = "board";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Triangle Board";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.message)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bestPossible)).EndInit();
            this.ResumeLayout(false);

        }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new board());
		}

        private void ClickedAHole(object sender, System.EventArgs e)
        {
            try
            {
                int holeIdx = holes.IndexOf(sender);
                
                if (pegToMove == -1)
                {
                    pegToMove = holeIdx;
                }
                else 
                {
                    
                    if (pegToMove != holeIdx)
                    {
                        pegDestination = holeIdx;
                        theGame.Move(pegToMove, pegDestination);
                        if (theGame.GameOver)
                        {
                            setTheBoard();
                            MessageBox.Show("there are no moves left");
                        }
                        //MessageBox.Show(String.Format("The Best Possible Score is {0}",gameDomain.BestPossibleScore(theGame.Signature)));
                    }
                    pegToMove = -1;
                    pegDestination = -1;
                }
                setTheBoard();
            }
            catch(Exception except)
            {
                pegToMove = -1;
                pegDestination = -1;
                MessageBox.Show(except.Message);
            }
        }

        private void saveMovesMenu_Click(object sender, System.EventArgs e)
        {
            try
            {
                theGame.SaveToFile(@"c:\game.xml");

            }
            catch (Exception exec)
            {
                MessageBox.Show(exec.Message,exec.Source);
            }

        }

        private void menuNew_Click(object sender, System.EventArgs e)
        {
            theGame = null;
            theGame = new game();
            theGame.SetupBoard(EmptyHoleChoice());
            pegToMove = -1;
            pegDestination = -1;       
            setTheBoard();
        }

        private void undoMenu_Click(object sender, System.EventArgs e)
        {
            try 
            {
                pegToMove = -1;
                pegDestination = -1;
                theGame.Undo();
                setTheBoard();
            }
            catch(Exception except)
            {
                MessageBox.Show(except.Message);
            }       
        }

        private void optionsMenu_Click(object sender, System.EventArgs e)
        {

            emptyHole1Menu.Checked = sender.Equals(emptyHole1Menu);
            emptyHole2Menu.Checked = sender.Equals(emptyHole2Menu);
            emptyHole3Menu.Checked = sender.Equals(emptyHole3Menu);
            emptyHole4Menu.Checked = sender.Equals(emptyHole4Menu);
            emptyHole5Menu.Checked = sender.Equals(emptyHole5Menu);
            emptyHole6Menu.Checked = sender.Equals(emptyHole6Menu);
            emptyHole7Menu.Checked = sender.Equals(emptyHole7Menu);
            emptyHole8Menu.Checked = sender.Equals(emptyHole8Menu);
            emptyHole9Menu.Checked = sender.Equals(emptyHole9Menu);
            emptyHole10Menu.Checked = sender.Equals(emptyHole10Menu);
            emptyHole11Menu.Checked = sender.Equals(emptyHole11Menu);
            emptyHole12Menu.Checked = sender.Equals(emptyHole12Menu);
            emptyHole13Menu.Checked = sender.Equals(emptyHole13Menu);
            emptyHole14Menu.Checked = sender.Equals(emptyHole14Menu);
            emptyHole15Menu.Checked = sender.Equals(emptyHole15Menu);
            randomEmptyMenu.Checked = sender.Equals(randomEmptyMenu);
        }

        private int EmptyHoleChoice()
        {
            if (emptyHole1Menu.Checked) return 0;
            if (emptyHole2Menu.Checked) return 1; 
            if (emptyHole3Menu.Checked) return 2;
            if (emptyHole4Menu.Checked) return 3; 
            if (emptyHole5Menu.Checked) return 4; 
            if (emptyHole6Menu.Checked) return 5; 
            if (emptyHole7Menu.Checked) return 6; 
            if (emptyHole8Menu.Checked) return 7; 
            if (emptyHole9Menu.Checked) return 8; 
            if (emptyHole10Menu.Checked) return 9; 
            if (emptyHole11Menu.Checked) return 10; 
            if (emptyHole12Menu.Checked) return 11; 
            if (emptyHole13Menu.Checked) return 12; 
            if (emptyHole14Menu.Checked) return 13; 
            if (emptyHole15Menu.Checked) return 14; 
            if (randomEmptyMenu.Checked) 
            {
                Random rand = new Random();
                return rand.Next(0, 14);
            }
            return(0);
        }

        private void SuggestMove_Click(object sender, System.EventArgs e)
        {
            //int oldPosition = 0;
            //int newPosition = 0;

            MoveTuple mv = gameDomain.SuggestNextMove(theGame.Signature);
            //if (theGame.SuggestedMove(ref oldPosition, ref newPosition))
            if (mv != null)
            {
                if (MessageBox.Show(string.Format("Move peg at position {0} to position {1}", mv.original + 1, mv.destination + 1), "Suggested Move is:", System.Windows.Forms.MessageBoxButtons.OKCancel)
                    == System.Windows.Forms.DialogResult.OK)
                {
                    theGame.Move(mv.original, mv.destination);
                    if (theGame.GameOver)
                    {
                        setTheBoard();
                        MessageBox.Show("there are no moves left");
                    }
                    pegToMove = -1;
                    pegDestination = -1;
                    setTheBoard();
                }
            }
            else
            {
                MessageBox.Show("There are no suggestions.");
            }
        }

        private void loadMovesMenu_Click(object sender, System.EventArgs e)
        {
            try
            {
                //theGame.LoadFromFile(@"c:\game.xml");
                theGame = game.LoadGameFromFile(@"c:\game.xml");
                pegToMove = -1;
                pegDestination = -1;
                setTheBoard();
            }
            catch (Exception exec)
            {
                MessageBox.Show(exec.Message,exec.Source);
            }
        
        }
	}
}
