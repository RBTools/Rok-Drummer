namespace RokDrummer
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.cboKits = new System.Windows.Forms.ComboBox();
            this.ConnectionTimer = new System.Windows.Forms.Timer(this.components);
            this.lblConnect = new System.Windows.Forms.Label();
            this.DrumsTimer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimizeToTray = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.doubleBassPedal = new System.Windows.Forms.ToolStripMenuItem();
            this.forceClosedHihat = new System.Windows.Forms.ToolStripMenuItem();
            this.hitVelocityControlsSampleVolume = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.playAlongMode = new System.Windows.Forms.ToolStripMenuItem();
            this.silenceDrumsTrack = new System.Windows.Forms.ToolStripMenuItem();
            this.showChartSelection = new System.Windows.Forms.ToolStripMenuItem();
            this.autoPlayWithChart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.showMetronome = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutGH5 = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutIon = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutRB1 = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutRB2 = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutTron = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutCustom = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.customizeLayout = new System.Windows.Forms.ToolStripMenuItem();
            this.resetLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controllersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectPlayer = new System.Windows.Forms.ToolStripMenuItem();
            this.player1 = new System.Windows.Forms.ToolStripMenuItem();
            this.player2 = new System.Windows.Forms.ToolStripMenuItem();
            this.player3 = new System.Windows.Forms.ToolStripMenuItem();
            this.player4 = new System.Windows.Forms.ToolStripMenuItem();
            this.selectDrumKit = new System.Windows.Forms.ToolStripMenuItem();
            this.rockBand1 = new System.Windows.Forms.ToolStripMenuItem();
            this.rockBand2 = new System.Windows.Forms.ToolStripMenuItem();
            this.rockBand4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.pS3EKit = new System.Windows.Forms.ToolStripMenuItem();
            this.pS3GH5Kit = new System.Windows.Forms.ToolStripMenuItem();
            this.pS4RB4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.turnOff = new System.Windows.Forms.ToolStripMenuItem();
            this.turnOffAll = new System.Windows.Forms.ToolStripMenuItem();
            this.stageKitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stageKitPlayer1 = new System.Windows.Forms.ToolStripMenuItem();
            this.stageKitPlayer2 = new System.Windows.Forms.ToolStripMenuItem();
            this.stageKitPlayer3 = new System.Windows.Forms.ToolStripMenuItem();
            this.stageKitPlayer4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.stageKitDisabled = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.howToUse = new System.Windows.Forms.ToolStripMenuItem();
            this.c3Forums = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.checkForUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.debugDrumInput = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.viewChangeLog = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblDrumVolume = new System.Windows.Forms.Label();
            this.picHiHat = new System.Windows.Forms.PictureBox();
            this.picRide = new System.Windows.Forms.PictureBox();
            this.picCrash = new System.Windows.Forms.PictureBox();
            this.picSnare = new System.Windows.Forms.PictureBox();
            this.picTomG = new System.Windows.Forms.PictureBox();
            this.picTomY = new System.Windows.Forms.PictureBox();
            this.picTomB = new System.Windows.Forms.PictureBox();
            this.picLPedal = new System.Windows.Forms.PictureBox();
            this.picRPedal = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblTrackVolume = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.MetronomeDown = new System.Windows.Forms.PictureBox();
            this.MetronomeUp = new System.Windows.Forms.PictureBox();
            this.MetronomeOn = new System.Windows.Forms.PictureBox();
            this.MetronomeOff = new System.Windows.Forms.PictureBox();
            this.lblTempo = new System.Windows.Forms.Label();
            this.cboCharts = new System.Windows.Forms.ComboBox();
            this.lblDebug = new System.Windows.Forms.Label();
            this.panelHitBox = new System.Windows.Forms.Panel();
            this.panelControls = new System.Windows.Forms.Panel();
            this.picWorking = new System.Windows.Forms.PictureBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblSection = new System.Windows.Forms.Label();
            this.btnPlay = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.updater = new System.ComponentModel.BackgroundWorker();
            this.songPreparer = new System.ComponentModel.BackgroundWorker();
            this.PlaybackTimer = new System.Windows.Forms.Timer(this.components);
            this.NotifyTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.panelMetronome = new System.Windows.Forms.Panel();
            this.Metronome = new System.Windows.Forms.Timer(this.components);
            this.picTrack = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.styleVerticalScroll = new System.Windows.Forms.ToolStripMenuItem();
            this.styleRockBand = new System.Windows.Forms.ToolStripMenuItem();
            this.radioProDrums = new System.Windows.Forms.RadioButton();
            this.radioDrums = new System.Windows.Forms.RadioButton();
            this.panelCharts = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelKits = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHiHat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCrash)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSnare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTomG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTomY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTomB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLPedal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRPedal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MetronomeDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MetronomeUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MetronomeOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MetronomeOff)).BeginInit();
            this.panelControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWorking)).BeginInit();
            this.panelMetronome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTrack)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panelCharts.SuspendLayout();
            this.panelKits.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboKits
            // 
            this.cboKits.AllowDrop = true;
            this.cboKits.BackColor = System.Drawing.Color.Silver;
            this.cboKits.Enabled = false;
            this.cboKits.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboKits.ForeColor = System.Drawing.Color.Black;
            this.cboKits.FormattingEnabled = true;
            this.cboKits.Location = new System.Drawing.Point(6, 26);
            this.cboKits.Name = "cboKits";
            this.cboKits.Size = new System.Drawing.Size(208, 21);
            this.cboKits.TabIndex = 0;
            this.cboKits.TabStop = false;
            this.cboKits.Text = "Select a drum kit...";
            this.toolTip1.SetToolTip(this.cboKits, "Select a drum kit");
            this.cboKits.SelectedIndexChanged += new System.EventHandler(this.cboKits_SelectedIndexChanged);
            this.cboKits.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.cboKits.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            // 
            // ConnectionTimer
            // 
            this.ConnectionTimer.Tick += new System.EventHandler(this.ConnectionTimer_Tick);
            // 
            // lblConnect
            // 
            this.lblConnect.AllowDrop = true;
            this.lblConnect.AutoEllipsis = true;
            this.lblConnect.AutoSize = true;
            this.lblConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnect.ForeColor = System.Drawing.Color.Firebrick;
            this.lblConnect.Location = new System.Drawing.Point(4, 787);
            this.lblConnect.Name = "lblConnect";
            this.lblConnect.Size = new System.Drawing.Size(154, 13);
            this.lblConnect.TabIndex = 13;
            this.lblConnect.Text = "Controller: Disconnnected";
            this.lblConnect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.lblConnect, "Controller status");
            this.lblConnect.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.lblConnect.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            // 
            // DrumsTimer
            // 
            this.DrumsTimer.Interval = 5;
            this.DrumsTimer.Tick += new System.EventHandler(this.DrumsTimer_Tick);
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.controllersToolStripMenuItem,
            this.stageKitToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1344, 24);
            this.menuStrip.TabIndex = 14;
            this.menuStrip.Text = "menuStrip2";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.minimizeToTray,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // minimizeToTray
            // 
            this.minimizeToTray.BackColor = System.Drawing.Color.Black;
            this.minimizeToTray.ForeColor = System.Drawing.Color.White;
            this.minimizeToTray.Name = "minimizeToTray";
            this.minimizeToTray.Size = new System.Drawing.Size(160, 22);
            this.minimizeToTray.Text = "Minimize to tray";
            this.minimizeToTray.Click += new System.EventHandler(this.minimizeToTray_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.exitToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.doubleBassPedal,
            this.forceClosedHihat,
            this.hitVelocityControlsSampleVolume,
            this.toolStripMenuItem2,
            this.playAlongMode,
            this.silenceDrumsTrack,
            this.showChartSelection,
            this.autoPlayWithChart,
            this.toolStripMenuItem3,
            this.showMetronome,
            this.layoutToolStripMenuItem});
            this.optionsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // doubleBassPedal
            // 
            this.doubleBassPedal.BackColor = System.Drawing.Color.Black;
            this.doubleBassPedal.CheckOnClick = true;
            this.doubleBassPedal.ForeColor = System.Drawing.Color.White;
            this.doubleBassPedal.Name = "doubleBassPedal";
            this.doubleBassPedal.Size = new System.Drawing.Size(264, 22);
            this.doubleBassPedal.Text = "Double bass pedal";
            this.doubleBassPedal.Click += new System.EventHandler(this.doubleBassPedal_Click);
            // 
            // forceClosedHihat
            // 
            this.forceClosedHihat.BackColor = System.Drawing.Color.Black;
            this.forceClosedHihat.CheckOnClick = true;
            this.forceClosedHihat.ForeColor = System.Drawing.Color.White;
            this.forceClosedHihat.Name = "forceClosedHihat";
            this.forceClosedHihat.Size = new System.Drawing.Size(264, 22);
            this.forceClosedHihat.Text = "Force closed hi-hat";
            // 
            // hitVelocityControlsSampleVolume
            // 
            this.hitVelocityControlsSampleVolume.BackColor = System.Drawing.Color.Black;
            this.hitVelocityControlsSampleVolume.Checked = true;
            this.hitVelocityControlsSampleVolume.CheckOnClick = true;
            this.hitVelocityControlsSampleVolume.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hitVelocityControlsSampleVolume.ForeColor = System.Drawing.Color.White;
            this.hitVelocityControlsSampleVolume.Name = "hitVelocityControlsSampleVolume";
            this.hitVelocityControlsSampleVolume.Size = new System.Drawing.Size(264, 22);
            this.hitVelocityControlsSampleVolume.Text = "Hit velocity controls sample volume";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(261, 6);
            // 
            // playAlongMode
            // 
            this.playAlongMode.BackColor = System.Drawing.Color.Black;
            this.playAlongMode.CheckOnClick = true;
            this.playAlongMode.ForeColor = System.Drawing.Color.White;
            this.playAlongMode.Name = "playAlongMode";
            this.playAlongMode.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.playAlongMode.Size = new System.Drawing.Size(264, 22);
            this.playAlongMode.Text = "Play-along mode";
            this.playAlongMode.CheckedChanged += new System.EventHandler(this.playAlongMode_CheckedChanged);
            this.playAlongMode.Click += new System.EventHandler(this.playAlongMode_Click);
            // 
            // silenceDrumsTrack
            // 
            this.silenceDrumsTrack.BackColor = System.Drawing.Color.Black;
            this.silenceDrumsTrack.CheckOnClick = true;
            this.silenceDrumsTrack.Enabled = false;
            this.silenceDrumsTrack.ForeColor = System.Drawing.Color.White;
            this.silenceDrumsTrack.Name = "silenceDrumsTrack";
            this.silenceDrumsTrack.Size = new System.Drawing.Size(264, 22);
            this.silenceDrumsTrack.Text = "Silence drums track";
            this.silenceDrumsTrack.Click += new System.EventHandler(this.silenceDrumsTrack_Click);
            // 
            // showChartSelection
            // 
            this.showChartSelection.BackColor = System.Drawing.Color.Black;
            this.showChartSelection.CheckOnClick = true;
            this.showChartSelection.Enabled = false;
            this.showChartSelection.ForeColor = System.Drawing.Color.White;
            this.showChartSelection.Name = "showChartSelection";
            this.showChartSelection.Size = new System.Drawing.Size(264, 22);
            this.showChartSelection.Text = "Show chart selection";
            this.showChartSelection.Click += new System.EventHandler(this.showChartSelection_Click);
            // 
            // autoPlayWithChart
            // 
            this.autoPlayWithChart.BackColor = System.Drawing.Color.Black;
            this.autoPlayWithChart.CheckOnClick = true;
            this.autoPlayWithChart.Enabled = false;
            this.autoPlayWithChart.ForeColor = System.Drawing.Color.White;
            this.autoPlayWithChart.Name = "autoPlayWithChart";
            this.autoPlayWithChart.Size = new System.Drawing.Size(264, 22);
            this.autoPlayWithChart.Text = "AutoPlay with chart";
            this.autoPlayWithChart.Click += new System.EventHandler(this.autoPlayWithChart_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(261, 6);
            // 
            // showMetronome
            // 
            this.showMetronome.BackColor = System.Drawing.Color.Black;
            this.showMetronome.CheckOnClick = true;
            this.showMetronome.ForeColor = System.Drawing.Color.White;
            this.showMetronome.Name = "showMetronome";
            this.showMetronome.Size = new System.Drawing.Size(264, 22);
            this.showMetronome.Text = "Show metronome";
            this.showMetronome.Click += new System.EventHandler(this.showMetronome_Click);
            // 
            // layoutToolStripMenuItem
            // 
            this.layoutToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.layoutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.layoutGH5,
            this.layoutIon,
            this.layoutRB1,
            this.layoutRB2,
            this.layoutTron,
            this.layoutCustom,
            this.toolStripMenuItem6,
            this.customizeLayout,
            this.resetLayoutToolStripMenuItem});
            this.layoutToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.layoutToolStripMenuItem.Name = "layoutToolStripMenuItem";
            this.layoutToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
            this.layoutToolStripMenuItem.Text = "Layout";
            // 
            // layoutGH5
            // 
            this.layoutGH5.BackColor = System.Drawing.Color.Black;
            this.layoutGH5.ForeColor = System.Drawing.Color.White;
            this.layoutGH5.Name = "layoutGH5";
            this.layoutGH5.Size = new System.Drawing.Size(166, 22);
            this.layoutGH5.Text = "Guitar Hero 5";
            this.layoutGH5.Click += new System.EventHandler(this.layoutGH5_Click);
            // 
            // layoutIon
            // 
            this.layoutIon.BackColor = System.Drawing.Color.Black;
            this.layoutIon.ForeColor = System.Drawing.Color.White;
            this.layoutIon.Name = "layoutIon";
            this.layoutIon.Size = new System.Drawing.Size(166, 22);
            this.layoutIon.Text = "Ion Drum Rocker";
            this.layoutIon.Click += new System.EventHandler(this.layoutIon_Click);
            // 
            // layoutRB1
            // 
            this.layoutRB1.BackColor = System.Drawing.Color.Black;
            this.layoutRB1.ForeColor = System.Drawing.Color.White;
            this.layoutRB1.Name = "layoutRB1";
            this.layoutRB1.Size = new System.Drawing.Size(166, 22);
            this.layoutRB1.Text = "Rock Band 1";
            this.layoutRB1.Click += new System.EventHandler(this.layoutRB1_Click);
            // 
            // layoutRB2
            // 
            this.layoutRB2.BackColor = System.Drawing.Color.Black;
            this.layoutRB2.Checked = true;
            this.layoutRB2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.layoutRB2.ForeColor = System.Drawing.Color.White;
            this.layoutRB2.Name = "layoutRB2";
            this.layoutRB2.Size = new System.Drawing.Size(166, 22);
            this.layoutRB2.Text = "Rock Band 2+";
            this.layoutRB2.Click += new System.EventHandler(this.layoutRB2_Click);
            // 
            // layoutTron
            // 
            this.layoutTron.BackColor = System.Drawing.Color.Black;
            this.layoutTron.ForeColor = System.Drawing.Color.White;
            this.layoutTron.Name = "layoutTron";
            this.layoutTron.Size = new System.Drawing.Size(166, 22);
            this.layoutTron.Text = "Tron";
            this.layoutTron.Click += new System.EventHandler(this.layoutTron_Click);
            // 
            // layoutCustom
            // 
            this.layoutCustom.BackColor = System.Drawing.Color.Black;
            this.layoutCustom.Enabled = false;
            this.layoutCustom.ForeColor = System.Drawing.Color.White;
            this.layoutCustom.Name = "layoutCustom";
            this.layoutCustom.Size = new System.Drawing.Size(166, 22);
            this.layoutCustom.Text = "Custom";
            this.layoutCustom.Click += new System.EventHandler(this.layoutCustom_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(163, 6);
            // 
            // customizeLayout
            // 
            this.customizeLayout.BackColor = System.Drawing.Color.Black;
            this.customizeLayout.CheckOnClick = true;
            this.customizeLayout.ForeColor = System.Drawing.Color.White;
            this.customizeLayout.Name = "customizeLayout";
            this.customizeLayout.Size = new System.Drawing.Size(166, 22);
            this.customizeLayout.Text = "Customize layout";
            this.customizeLayout.Click += new System.EventHandler(this.customizeLayout_Click);
            // 
            // resetLayoutToolStripMenuItem
            // 
            this.resetLayoutToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.resetLayoutToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.resetLayoutToolStripMenuItem.Name = "resetLayoutToolStripMenuItem";
            this.resetLayoutToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.resetLayoutToolStripMenuItem.Text = "Reset layout";
            this.resetLayoutToolStripMenuItem.Click += new System.EventHandler(this.resetLayout_Click);
            // 
            // controllersToolStripMenuItem
            // 
            this.controllersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectPlayer,
            this.selectDrumKit,
            this.toolStripMenuItem1,
            this.turnOff,
            this.turnOffAll});
            this.controllersToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.controllersToolStripMenuItem.Name = "controllersToolStripMenuItem";
            this.controllersToolStripMenuItem.Size = new System.Drawing.Size(110, 20);
            this.controllersToolStripMenuItem.Text = "Drum Controllers";
            // 
            // selectPlayer
            // 
            this.selectPlayer.BackColor = System.Drawing.Color.Black;
            this.selectPlayer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.player1,
            this.player2,
            this.player3,
            this.player4});
            this.selectPlayer.ForeColor = System.Drawing.Color.White;
            this.selectPlayer.Name = "selectPlayer";
            this.selectPlayer.Size = new System.Drawing.Size(172, 22);
            this.selectPlayer.Text = "Controller number";
            // 
            // player1
            // 
            this.player1.BackColor = System.Drawing.Color.Black;
            this.player1.Checked = true;
            this.player1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.player1.Enabled = false;
            this.player1.ForeColor = System.Drawing.Color.White;
            this.player1.Name = "player1";
            this.player1.Size = new System.Drawing.Size(115, 22);
            this.player1.Text = "Player 1";
            this.player1.Click += new System.EventHandler(this.player1_Click);
            // 
            // player2
            // 
            this.player2.BackColor = System.Drawing.Color.Black;
            this.player2.Enabled = false;
            this.player2.ForeColor = System.Drawing.Color.White;
            this.player2.Name = "player2";
            this.player2.Size = new System.Drawing.Size(115, 22);
            this.player2.Text = "Player 2";
            this.player2.Click += new System.EventHandler(this.player2_Click);
            // 
            // player3
            // 
            this.player3.BackColor = System.Drawing.Color.Black;
            this.player3.Enabled = false;
            this.player3.ForeColor = System.Drawing.Color.White;
            this.player3.Name = "player3";
            this.player3.Size = new System.Drawing.Size(115, 22);
            this.player3.Text = "Player 3";
            this.player3.Click += new System.EventHandler(this.player3_Click);
            // 
            // player4
            // 
            this.player4.BackColor = System.Drawing.Color.Black;
            this.player4.Enabled = false;
            this.player4.ForeColor = System.Drawing.Color.White;
            this.player4.Name = "player4";
            this.player4.Size = new System.Drawing.Size(115, 22);
            this.player4.Text = "Player 4";
            this.player4.Click += new System.EventHandler(this.player4_Click);
            // 
            // selectDrumKit
            // 
            this.selectDrumKit.BackColor = System.Drawing.Color.Black;
            this.selectDrumKit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rockBand1,
            this.rockBand2,
            this.rockBand4,
            this.toolStripMenuItem8,
            this.pS3EKit,
            this.pS3GH5Kit,
            this.pS4RB4});
            this.selectDrumKit.ForeColor = System.Drawing.Color.White;
            this.selectDrumKit.Name = "selectDrumKit";
            this.selectDrumKit.Size = new System.Drawing.Size(172, 22);
            this.selectDrumKit.Text = "Drum kit type";
            // 
            // rockBand1
            // 
            this.rockBand1.BackColor = System.Drawing.Color.Black;
            this.rockBand1.ForeColor = System.Drawing.Color.White;
            this.rockBand1.Name = "rockBand1";
            this.rockBand1.Size = new System.Drawing.Size(251, 22);
            this.rockBand1.Text = "X360 - RB1 (wired)";
            this.rockBand1.Click += new System.EventHandler(this.rockBand1_Click);
            // 
            // rockBand2
            // 
            this.rockBand2.BackColor = System.Drawing.Color.Black;
            this.rockBand2.Checked = true;
            this.rockBand2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rockBand2.ForeColor = System.Drawing.Color.White;
            this.rockBand2.Name = "rockBand2";
            this.rockBand2.Size = new System.Drawing.Size(251, 22);
            this.rockBand2.Text = "X360 - RB2 / RB3 / E-Kit with MPA";
            this.rockBand2.Click += new System.EventHandler(this.rockBand1_Click);
            // 
            // rockBand4
            // 
            this.rockBand4.BackColor = System.Drawing.Color.Black;
            this.rockBand4.ForeColor = System.Drawing.Color.White;
            this.rockBand4.Name = "rockBand4";
            this.rockBand4.Size = new System.Drawing.Size(251, 22);
            this.rockBand4.Text = "XOne - RB4";
            this.rockBand4.Click += new System.EventHandler(this.rockBand1_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(248, 6);
            // 
            // pS3EKit
            // 
            this.pS3EKit.BackColor = System.Drawing.Color.Black;
            this.pS3EKit.ForeColor = System.Drawing.Color.White;
            this.pS3EKit.Name = "pS3EKit";
            this.pS3EKit.Size = new System.Drawing.Size(251, 22);
            this.pS3EKit.Text = "PS3 - E-Kit with MPA";
            this.pS3EKit.Click += new System.EventHandler(this.rockBand1_Click);
            // 
            // pS3GH5Kit
            // 
            this.pS3GH5Kit.BackColor = System.Drawing.Color.Black;
            this.pS3GH5Kit.ForeColor = System.Drawing.Color.White;
            this.pS3GH5Kit.Name = "pS3GH5Kit";
            this.pS3GH5Kit.Size = new System.Drawing.Size(251, 22);
            this.pS3GH5Kit.Text = "PS3 - GH5";
            this.pS3GH5Kit.Click += new System.EventHandler(this.rockBand1_Click);
            // 
            // pS4RB4
            // 
            this.pS4RB4.BackColor = System.Drawing.Color.Black;
            this.pS4RB4.ForeColor = System.Drawing.Color.White;
            this.pS4RB4.Name = "pS4RB4";
            this.pS4RB4.Size = new System.Drawing.Size(251, 22);
            this.pS4RB4.Text = "PS4 - RB4";
            this.pS4RB4.Click += new System.EventHandler(this.rockBand1_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(169, 6);
            // 
            // turnOff
            // 
            this.turnOff.BackColor = System.Drawing.Color.Black;
            this.turnOff.Enabled = false;
            this.turnOff.ForeColor = System.Drawing.Color.White;
            this.turnOff.Name = "turnOff";
            this.turnOff.Size = new System.Drawing.Size(172, 22);
            this.turnOff.Text = "Turn off active";
            this.turnOff.Click += new System.EventHandler(this.turnOff_Click);
            // 
            // turnOffAll
            // 
            this.turnOffAll.BackColor = System.Drawing.Color.Black;
            this.turnOffAll.Enabled = false;
            this.turnOffAll.ForeColor = System.Drawing.Color.White;
            this.turnOffAll.Name = "turnOffAll";
            this.turnOffAll.Size = new System.Drawing.Size(172, 22);
            this.turnOffAll.Text = "Turn off all";
            this.turnOffAll.Click += new System.EventHandler(this.turnOffAll_Click);
            // 
            // stageKitToolStripMenuItem
            // 
            this.stageKitToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stageKitPlayer1,
            this.stageKitPlayer2,
            this.stageKitPlayer3,
            this.stageKitPlayer4,
            this.toolStripMenuItem7,
            this.stageKitDisabled});
            this.stageKitToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.stageKitToolStripMenuItem.Name = "stageKitToolStripMenuItem";
            this.stageKitToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.stageKitToolStripMenuItem.Text = "Stage Kit";
            // 
            // stageKitPlayer1
            // 
            this.stageKitPlayer1.BackColor = System.Drawing.Color.Black;
            this.stageKitPlayer1.Enabled = false;
            this.stageKitPlayer1.ForeColor = System.Drawing.Color.White;
            this.stageKitPlayer1.Name = "stageKitPlayer1";
            this.stageKitPlayer1.Size = new System.Drawing.Size(115, 22);
            this.stageKitPlayer1.Text = "Player 1";
            this.stageKitPlayer1.Click += new System.EventHandler(this.stageKitPlayer1_Click);
            // 
            // stageKitPlayer2
            // 
            this.stageKitPlayer2.BackColor = System.Drawing.Color.Black;
            this.stageKitPlayer2.Enabled = false;
            this.stageKitPlayer2.ForeColor = System.Drawing.Color.White;
            this.stageKitPlayer2.Name = "stageKitPlayer2";
            this.stageKitPlayer2.Size = new System.Drawing.Size(115, 22);
            this.stageKitPlayer2.Text = "Player 2";
            this.stageKitPlayer2.Click += new System.EventHandler(this.stageKitPlayer2_Click);
            // 
            // stageKitPlayer3
            // 
            this.stageKitPlayer3.BackColor = System.Drawing.Color.Black;
            this.stageKitPlayer3.Enabled = false;
            this.stageKitPlayer3.ForeColor = System.Drawing.Color.White;
            this.stageKitPlayer3.Name = "stageKitPlayer3";
            this.stageKitPlayer3.Size = new System.Drawing.Size(115, 22);
            this.stageKitPlayer3.Text = "Player 3";
            this.stageKitPlayer3.Click += new System.EventHandler(this.stageKitPlayer3_Click);
            // 
            // stageKitPlayer4
            // 
            this.stageKitPlayer4.BackColor = System.Drawing.Color.Black;
            this.stageKitPlayer4.Enabled = false;
            this.stageKitPlayer4.ForeColor = System.Drawing.Color.White;
            this.stageKitPlayer4.Name = "stageKitPlayer4";
            this.stageKitPlayer4.Size = new System.Drawing.Size(115, 22);
            this.stageKitPlayer4.Text = "Player 4";
            this.stageKitPlayer4.Click += new System.EventHandler(this.stageKitPlayer4_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(112, 6);
            // 
            // stageKitDisabled
            // 
            this.stageKitDisabled.BackColor = System.Drawing.Color.Black;
            this.stageKitDisabled.Checked = true;
            this.stageKitDisabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.stageKitDisabled.ForeColor = System.Drawing.Color.White;
            this.stageKitDisabled.Name = "stageKitDisabled";
            this.stageKitDisabled.Size = new System.Drawing.Size(115, 22);
            this.stageKitDisabled.Text = "Disable";
            this.stageKitDisabled.Click += new System.EventHandler(this.stageKitDisabled_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.howToUse,
            this.c3Forums,
            this.toolStripMenuItem5,
            this.checkForUpdates,
            this.debugDrumInput,
            this.toolStripMenuItem4,
            this.viewChangeLog,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // howToUse
            // 
            this.howToUse.BackColor = System.Drawing.Color.Black;
            this.howToUse.ForeColor = System.Drawing.Color.White;
            this.howToUse.Name = "howToUse";
            this.howToUse.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F1)));
            this.howToUse.Size = new System.Drawing.Size(203, 22);
            this.howToUse.Text = "How to use";
            this.howToUse.Click += new System.EventHandler(this.howToUse_Click);
            // 
            // c3Forums
            // 
            this.c3Forums.BackColor = System.Drawing.Color.Black;
            this.c3Forums.ForeColor = System.Drawing.Color.White;
            this.c3Forums.Name = "c3Forums";
            this.c3Forums.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
            this.c3Forums.Size = new System.Drawing.Size(203, 22);
            this.c3Forums.Text = "C3 Forums";
            this.c3Forums.Click += new System.EventHandler(this.c3Forums_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(200, 6);
            // 
            // checkForUpdates
            // 
            this.checkForUpdates.BackColor = System.Drawing.Color.Black;
            this.checkForUpdates.ForeColor = System.Drawing.Color.White;
            this.checkForUpdates.Name = "checkForUpdates";
            this.checkForUpdates.Size = new System.Drawing.Size(203, 22);
            this.checkForUpdates.Text = "Check for updates";
            this.checkForUpdates.Click += new System.EventHandler(this.checkForUpdates_Click);
            // 
            // debugDrumInput
            // 
            this.debugDrumInput.BackColor = System.Drawing.Color.Black;
            this.debugDrumInput.CheckOnClick = true;
            this.debugDrumInput.Enabled = false;
            this.debugDrumInput.ForeColor = System.Drawing.Color.White;
            this.debugDrumInput.Name = "debugDrumInput";
            this.debugDrumInput.Size = new System.Drawing.Size(203, 22);
            this.debugDrumInput.Text = "Debug input";
            this.debugDrumInput.Click += new System.EventHandler(this.debugDrumInput_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(200, 6);
            // 
            // viewChangeLog
            // 
            this.viewChangeLog.BackColor = System.Drawing.Color.Black;
            this.viewChangeLog.ForeColor = System.Drawing.Color.White;
            this.viewChangeLog.Name = "viewChangeLog";
            this.viewChangeLog.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F1)));
            this.viewChangeLog.Size = new System.Drawing.Size(203, 22);
            this.viewChangeLog.Text = "View change log";
            this.viewChangeLog.Click += new System.EventHandler(this.viewChangeLog_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.aboutToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // lblDrumVolume
            // 
            this.lblDrumVolume.AllowDrop = true;
            this.lblDrumVolume.AutoEllipsis = true;
            this.lblDrumVolume.AutoSize = true;
            this.lblDrumVolume.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblDrumVolume.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDrumVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDrumVolume.ForeColor = System.Drawing.Color.Yellow;
            this.lblDrumVolume.Location = new System.Drawing.Point(4, 769);
            this.lblDrumVolume.Name = "lblDrumVolume";
            this.lblDrumVolume.Size = new System.Drawing.Size(110, 13);
            this.lblDrumVolume.TabIndex = 15;
            this.lblDrumVolume.Text = "Drum Volume: 100";
            this.lblDrumVolume.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.lblDrumVolume, "Volume level of samples");
            this.lblDrumVolume.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.lblDrumVolume.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            this.lblDrumVolume.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblDrumVolume_MouseClick);
            // 
            // picHiHat
            // 
            this.picHiHat.BackColor = System.Drawing.Color.Transparent;
            this.picHiHat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picHiHat.Location = new System.Drawing.Point(126, 110);
            this.picHiHat.Name = "picHiHat";
            this.picHiHat.Size = new System.Drawing.Size(187, 102);
            this.picHiHat.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picHiHat.TabIndex = 16;
            this.picHiHat.TabStop = false;
            this.picHiHat.Tag = "5";
            this.toolTip1.SetToolTip(this.picHiHat, "Click to play");
            this.picHiHat.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.picHiHat.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            this.picHiHat.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picHitYC_MouseDown);
            this.picHiHat.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picHiHat_MouseMove);
            this.picHiHat.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picHitYC_MouseUp);
            // 
            // picRide
            // 
            this.picRide.BackColor = System.Drawing.Color.Transparent;
            this.picRide.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picRide.Location = new System.Drawing.Point(492, 23);
            this.picRide.Name = "picRide";
            this.picRide.Size = new System.Drawing.Size(187, 101);
            this.picRide.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picRide.TabIndex = 17;
            this.picRide.TabStop = false;
            this.picRide.Tag = "7";
            this.toolTip1.SetToolTip(this.picRide, "Click to play");
            this.picRide.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.picRide.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            this.picRide.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picHitBC_MouseDown);
            this.picRide.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picHiHat_MouseMove);
            this.picRide.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picHitYC_MouseUp);
            // 
            // picCrash
            // 
            this.picCrash.BackColor = System.Drawing.Color.Transparent;
            this.picCrash.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picCrash.Location = new System.Drawing.Point(635, 118);
            this.picCrash.Name = "picCrash";
            this.picCrash.Size = new System.Drawing.Size(187, 102);
            this.picCrash.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picCrash.TabIndex = 18;
            this.picCrash.TabStop = false;
            this.picCrash.Tag = "8";
            this.toolTip1.SetToolTip(this.picCrash, "Click to play");
            this.picCrash.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.picCrash.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            this.picCrash.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picHitGC_MouseDown);
            this.picCrash.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picHiHat_MouseMove);
            this.picCrash.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picHitYC_MouseUp);
            // 
            // picSnare
            // 
            this.picSnare.BackColor = System.Drawing.Color.Transparent;
            this.picSnare.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picSnare.Location = new System.Drawing.Point(164, 250);
            this.picSnare.Name = "picSnare";
            this.picSnare.Size = new System.Drawing.Size(158, 122);
            this.picSnare.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picSnare.TabIndex = 19;
            this.picSnare.TabStop = false;
            this.picSnare.Tag = "0";
            this.toolTip1.SetToolTip(this.picSnare, "Click to play");
            this.picSnare.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.picSnare.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            this.picSnare.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picHitSnare_MouseDown);
            this.picSnare.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picHiHat_MouseMove);
            this.picSnare.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picHitYC_MouseUp);
            // 
            // picTomG
            // 
            this.picTomG.BackColor = System.Drawing.Color.Transparent;
            this.picTomG.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picTomG.Location = new System.Drawing.Point(636, 250);
            this.picTomG.Name = "picTomG";
            this.picTomG.Size = new System.Drawing.Size(154, 122);
            this.picTomG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picTomG.TabIndex = 20;
            this.picTomG.TabStop = false;
            this.picTomG.Tag = "4";
            this.toolTip1.SetToolTip(this.picTomG, "Click to play");
            this.picTomG.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.picTomG.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            this.picTomG.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picHitGT_MouseDown);
            this.picTomG.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picHiHat_MouseMove);
            this.picTomG.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picHitYC_MouseUp);
            // 
            // picTomY
            // 
            this.picTomY.BackColor = System.Drawing.Color.Transparent;
            this.picTomY.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picTomY.Location = new System.Drawing.Point(318, 170);
            this.picTomY.Name = "picTomY";
            this.picTomY.Size = new System.Drawing.Size(147, 109);
            this.picTomY.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picTomY.TabIndex = 21;
            this.picTomY.TabStop = false;
            this.picTomY.Tag = "2";
            this.toolTip1.SetToolTip(this.picTomY, "Click to play");
            this.picTomY.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.picTomY.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            this.picTomY.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picHitYT_MouseDown);
            this.picTomY.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picHiHat_MouseMove);
            this.picTomY.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picHitYC_MouseUp);
            // 
            // picTomB
            // 
            this.picTomB.BackColor = System.Drawing.Color.Transparent;
            this.picTomB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picTomB.Location = new System.Drawing.Point(492, 171);
            this.picTomB.Name = "picTomB";
            this.picTomB.Size = new System.Drawing.Size(145, 109);
            this.picTomB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picTomB.TabIndex = 22;
            this.picTomB.TabStop = false;
            this.picTomB.Tag = "3";
            this.toolTip1.SetToolTip(this.picTomB, "Click to play");
            this.picTomB.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.picTomB.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            this.picTomB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picHitBT_MouseDown);
            this.picTomB.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picHiHat_MouseMove);
            this.picTomB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picHitYC_MouseUp);
            // 
            // picLPedal
            // 
            this.picLPedal.BackColor = System.Drawing.Color.Transparent;
            this.picLPedal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picLPedal.Location = new System.Drawing.Point(404, 641);
            this.picLPedal.Name = "picLPedal";
            this.picLPedal.Size = new System.Drawing.Size(57, 110);
            this.picLPedal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picLPedal.TabIndex = 23;
            this.picLPedal.TabStop = false;
            this.picLPedal.Tag = "10";
            this.toolTip1.SetToolTip(this.picLPedal, "Click to play");
            this.picLPedal.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.picLPedal.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            this.picLPedal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picHitLPedal_MouseDown);
            this.picLPedal.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picHiHat_MouseMove);
            this.picLPedal.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picHitYC_MouseUp);
            // 
            // picRPedal
            // 
            this.picRPedal.BackColor = System.Drawing.Color.Transparent;
            this.picRPedal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picRPedal.Location = new System.Drawing.Point(499, 641);
            this.picRPedal.Name = "picRPedal";
            this.picRPedal.Size = new System.Drawing.Size(57, 110);
            this.picRPedal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picRPedal.TabIndex = 24;
            this.picRPedal.TabStop = false;
            this.picRPedal.Tag = "9";
            this.toolTip1.SetToolTip(this.picRPedal, "Click to play");
            this.picRPedal.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.picRPedal.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            this.picRPedal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picHitRPedal_MouseDown);
            this.picRPedal.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picHiHat_MouseMove);
            this.picRPedal.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picHitYC_MouseUp);
            // 
            // lblTrackVolume
            // 
            this.lblTrackVolume.AllowDrop = true;
            this.lblTrackVolume.AutoEllipsis = true;
            this.lblTrackVolume.AutoSize = true;
            this.lblTrackVolume.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblTrackVolume.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTrackVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackVolume.ForeColor = System.Drawing.Color.Yellow;
            this.lblTrackVolume.Location = new System.Drawing.Point(4, 751);
            this.lblTrackVolume.Name = "lblTrackVolume";
            this.lblTrackVolume.Size = new System.Drawing.Size(107, 13);
            this.lblTrackVolume.TabIndex = 28;
            this.lblTrackVolume.Text = "Track Volume: 75";
            this.lblTrackVolume.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.lblTrackVolume, "Volume level of backing track");
            this.lblTrackVolume.Visible = false;
            this.lblTrackVolume.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.lblTrackVolume.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            this.lblTrackVolume.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblTrackVolume_MouseClick);
            // 
            // btnStop
            // 
            this.btnStop.AllowDrop = true;
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStop.Enabled = false;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Location = new System.Drawing.Point(353, 47);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(50, 23);
            this.btnStop.TabIndex = 3;
            this.btnStop.TabStop = false;
            this.btnStop.Text = "Stop";
            this.toolTip1.SetToolTip(this.btnStop, "Stop");
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            this.btnStop.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.btnStop.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            // 
            // btnLoad
            // 
            this.btnLoad.AllowDrop = true;
            this.btnLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnLoad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoad.Location = new System.Drawing.Point(14, 47);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(51, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.TabStop = false;
            this.btnLoad.Text = "Load...";
            this.toolTip1.SetToolTip(this.btnLoad, "Load song");
            this.btnLoad.UseVisualStyleBackColor = false;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            this.btnLoad.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.btnLoad.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            // 
            // MetronomeDown
            // 
            this.MetronomeDown.BackColor = System.Drawing.Color.Transparent;
            this.MetronomeDown.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MetronomeDown.Location = new System.Drawing.Point(27, 160);
            this.MetronomeDown.Name = "MetronomeDown";
            this.MetronomeDown.Size = new System.Drawing.Size(46, 16);
            this.MetronomeDown.TabIndex = 30;
            this.MetronomeDown.TabStop = false;
            this.toolTip1.SetToolTip(this.MetronomeDown, "Lower tempo");
            this.MetronomeDown.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.MetronomeDown.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            this.MetronomeDown.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseClick);
            this.MetronomeDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MetronomeDown_MouseDown);
            // 
            // MetronomeUp
            // 
            this.MetronomeUp.BackColor = System.Drawing.Color.Transparent;
            this.MetronomeUp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MetronomeUp.Location = new System.Drawing.Point(83, 160);
            this.MetronomeUp.Name = "MetronomeUp";
            this.MetronomeUp.Size = new System.Drawing.Size(46, 16);
            this.MetronomeUp.TabIndex = 31;
            this.MetronomeUp.TabStop = false;
            this.toolTip1.SetToolTip(this.MetronomeUp, "Raise tempo");
            this.MetronomeUp.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.MetronomeUp.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            this.MetronomeUp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseClick);
            this.MetronomeUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MetronomeUp_MouseDown);
            // 
            // MetronomeOn
            // 
            this.MetronomeOn.BackColor = System.Drawing.Color.Transparent;
            this.MetronomeOn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MetronomeOn.Location = new System.Drawing.Point(27, 43);
            this.MetronomeOn.Name = "MetronomeOn";
            this.MetronomeOn.Size = new System.Drawing.Size(46, 16);
            this.MetronomeOn.TabIndex = 32;
            this.MetronomeOn.TabStop = false;
            this.toolTip1.SetToolTip(this.MetronomeOn, "Start metronome");
            this.MetronomeOn.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.MetronomeOn.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            this.MetronomeOn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseClick);
            this.MetronomeOn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MetronomeOn_MouseDown);
            // 
            // MetronomeOff
            // 
            this.MetronomeOff.BackColor = System.Drawing.Color.Transparent;
            this.MetronomeOff.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MetronomeOff.Location = new System.Drawing.Point(83, 43);
            this.MetronomeOff.Name = "MetronomeOff";
            this.MetronomeOff.Size = new System.Drawing.Size(46, 16);
            this.MetronomeOff.TabIndex = 33;
            this.MetronomeOff.TabStop = false;
            this.toolTip1.SetToolTip(this.MetronomeOff, "Stop metronome");
            this.MetronomeOff.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.MetronomeOff.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            this.MetronomeOff.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseClick);
            this.MetronomeOff.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MetronomeOff_MouseDown);
            // 
            // lblTempo
            // 
            this.lblTempo.AllowDrop = true;
            this.lblTempo.BackColor = System.Drawing.Color.Transparent;
            this.lblTempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTempo.Location = new System.Drawing.Point(35, 76);
            this.lblTempo.Name = "lblTempo";
            this.lblTempo.Size = new System.Drawing.Size(89, 53);
            this.lblTempo.TabIndex = 34;
            this.lblTempo.Text = "120";
            this.lblTempo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblTempo, "Current tempo");
            this.lblTempo.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.lblTempo.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            this.lblTempo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseClick);
            // 
            // cboCharts
            // 
            this.cboCharts.BackColor = System.Drawing.Color.Silver;
            this.cboCharts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCharts.ForeColor = System.Drawing.Color.Black;
            this.cboCharts.FormattingEnabled = true;
            this.cboCharts.Location = new System.Drawing.Point(6, 26);
            this.cboCharts.Name = "cboCharts";
            this.cboCharts.Size = new System.Drawing.Size(141, 21);
            this.cboCharts.TabIndex = 64;
            this.cboCharts.TabStop = false;
            this.toolTip1.SetToolTip(this.cboCharts, "Select chart to draw");
            this.cboCharts.SelectedIndexChanged += new System.EventHandler(this.cboCharts_SelectedIndexChanged);
            this.cboCharts.MouseLeave += new System.EventHandler(this.cboCharts_MouseLeave);
            // 
            // lblDebug
            // 
            this.lblDebug.AllowDrop = true;
            this.lblDebug.AutoEllipsis = true;
            this.lblDebug.BackColor = System.Drawing.Color.Black;
            this.lblDebug.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDebug.ForeColor = System.Drawing.Color.White;
            this.lblDebug.Location = new System.Drawing.Point(0, 785);
            this.lblDebug.Name = "lblDebug";
            this.lblDebug.Size = new System.Drawing.Size(928, 20);
            this.lblDebug.TabIndex = 71;
            this.lblDebug.Text = "DEBUG:";
            this.lblDebug.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.lblDebug, "Click to copy to clipboard");
            this.lblDebug.Visible = false;
            this.lblDebug.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblDebug_MouseClick);
            // 
            // panelHitBox
            // 
            this.panelHitBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.panelHitBox.Location = new System.Drawing.Point(928, 749);
            this.panelHitBox.Name = "panelHitBox";
            this.panelHitBox.Size = new System.Drawing.Size(416, 30);
            this.panelHitBox.TabIndex = 0;
            this.panelHitBox.Visible = false;
            this.panelHitBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseClick);
            // 
            // panelControls
            // 
            this.panelControls.AllowDrop = true;
            this.panelControls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelControls.Controls.Add(this.picWorking);
            this.panelControls.Controls.Add(this.lblTime);
            this.panelControls.Controls.Add(this.lblSection);
            this.panelControls.Controls.Add(this.btnStop);
            this.panelControls.Controls.Add(this.btnPlay);
            this.panelControls.Controls.Add(this.btnLoad);
            this.panelControls.Controls.Add(this.lblStatus);
            this.panelControls.Location = new System.Drawing.Point(928, 1);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(416, 118);
            this.panelControls.TabIndex = 27;
            this.panelControls.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.panelControls.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            this.panelControls.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseClick);
            // 
            // picWorking
            // 
            this.picWorking.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picWorking.Image = global::RokDrummer.Properties.Resources.working;
            this.picWorking.Location = new System.Drawing.Point(283, 2);
            this.picWorking.Name = "picWorking";
            this.picWorking.Size = new System.Drawing.Size(128, 15);
            this.picWorking.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picWorking.TabIndex = 60;
            this.picWorking.TabStop = false;
            this.picWorking.Visible = false;
            // 
            // lblTime
            // 
            this.lblTime.AllowDrop = true;
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.Black;
            this.lblTime.Location = new System.Drawing.Point(127, 47);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(220, 23);
            this.lblTime.TabIndex = 6;
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTime.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.lblTime.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            this.lblTime.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseClick);
            // 
            // lblSection
            // 
            this.lblSection.AllowDrop = true;
            this.lblSection.BackColor = System.Drawing.Color.Transparent;
            this.lblSection.ForeColor = System.Drawing.Color.White;
            this.lblSection.Location = new System.Drawing.Point(0, 85);
            this.lblSection.Name = "lblSection";
            this.lblSection.Size = new System.Drawing.Size(416, 23);
            this.lblSection.TabIndex = 5;
            this.lblSection.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lblSection.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.lblSection.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            this.lblSection.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseClick);
            // 
            // btnPlay
            // 
            this.btnPlay.AllowDrop = true;
            this.btnPlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnPlay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlay.Enabled = false;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Location = new System.Drawing.Point(71, 47);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(50, 23);
            this.btnPlay.TabIndex = 2;
            this.btnPlay.TabStop = false;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            this.btnPlay.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.btnPlay.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            // 
            // lblStatus
            // 
            this.lblStatus.AllowDrop = true;
            this.lblStatus.AutoEllipsis = true;
            this.lblStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Black;
            this.lblStatus.Location = new System.Drawing.Point(6, 22);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(402, 20);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "No song loaded...click button to load or drag/drop here...";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStatus.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.lblStatus.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            this.lblStatus.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseClick);
            // 
            // updater
            // 
            this.updater.WorkerReportsProgress = true;
            this.updater.WorkerSupportsCancellation = true;
            this.updater.DoWork += new System.ComponentModel.DoWorkEventHandler(this.updater_DoWork);
            this.updater.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.updater_RunWorkerCompleted);
            // 
            // songPreparer
            // 
            this.songPreparer.WorkerReportsProgress = true;
            this.songPreparer.WorkerSupportsCancellation = true;
            this.songPreparer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.songPreparer_DoWork);
            this.songPreparer.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.songPreparer_RunWorkerCompleted);
            // 
            // PlaybackTimer
            // 
            this.PlaybackTimer.Interval = 5;
            this.PlaybackTimer.Tick += new System.EventHandler(this.PlaybackTimer_Tick);
            // 
            // NotifyTray
            // 
            this.NotifyTray.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.NotifyTray.BalloonTipText = "Rok Drummer is running in the background";
            this.NotifyTray.BalloonTipTitle = "Rok Drummer";
            this.NotifyTray.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyTray.Icon")));
            this.NotifyTray.Text = "Rok Drummer";
            this.NotifyTray.Visible = true;
            this.NotifyTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyTray_MouseDoubleClick);
            // 
            // panelMetronome
            // 
            this.panelMetronome.AllowDrop = true;
            this.panelMetronome.BackColor = System.Drawing.Color.Transparent;
            this.panelMetronome.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelMetronome.Controls.Add(this.MetronomeOn);
            this.panelMetronome.Controls.Add(this.lblTempo);
            this.panelMetronome.Controls.Add(this.MetronomeOff);
            this.panelMetronome.Controls.Add(this.MetronomeUp);
            this.panelMetronome.Controls.Add(this.MetronomeDown);
            this.panelMetronome.Location = new System.Drawing.Point(740, 582);
            this.panelMetronome.Name = "panelMetronome";
            this.panelMetronome.Size = new System.Drawing.Size(161, 200);
            this.panelMetronome.TabIndex = 35;
            this.panelMetronome.Visible = false;
            this.panelMetronome.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.panelMetronome.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            this.panelMetronome.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseClick);
            this.panelMetronome.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelMetronome_MouseDown);
            this.panelMetronome.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelMetronome_MouseMove);
            this.panelMetronome.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelMetronome_MouseUp);
            // 
            // Metronome
            // 
            this.Metronome.Interval = 500;
            this.Metronome.Tick += new System.EventHandler(this.Metronome_Tick);
            // 
            // picTrack
            // 
            this.picTrack.BackColor = System.Drawing.Color.Black;
            this.picTrack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picTrack.ContextMenuStrip = this.contextMenuStrip1;
            this.picTrack.Location = new System.Drawing.Point(928, 119);
            this.picTrack.Name = "picTrack";
            this.picTrack.Size = new System.Drawing.Size(416, 691);
            this.picTrack.TabIndex = 36;
            this.picTrack.TabStop = false;
            this.picTrack.Paint += new System.Windows.Forms.PaintEventHandler(this.picTrack_Paint);
            this.picTrack.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picTrack_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.styleVerticalScroll,
            this.styleRockBand});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(176, 48);
            // 
            // styleVerticalScroll
            // 
            this.styleVerticalScroll.BackColor = System.Drawing.Color.Black;
            this.styleVerticalScroll.Checked = true;
            this.styleVerticalScroll.CheckOnClick = true;
            this.styleVerticalScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.styleVerticalScroll.ForeColor = System.Drawing.Color.White;
            this.styleVerticalScroll.Name = "styleVerticalScroll";
            this.styleVerticalScroll.Size = new System.Drawing.Size(175, 22);
            this.styleVerticalScroll.Text = "Style: Vertical Scroll";
            this.styleVerticalScroll.Click += new System.EventHandler(this.styleVerticalScroll_Click);
            // 
            // styleRockBand
            // 
            this.styleRockBand.BackColor = System.Drawing.Color.Black;
            this.styleRockBand.CheckOnClick = true;
            this.styleRockBand.ForeColor = System.Drawing.Color.White;
            this.styleRockBand.Name = "styleRockBand";
            this.styleRockBand.Size = new System.Drawing.Size(175, 22);
            this.styleRockBand.Text = "Style: Rock Band";
            this.styleRockBand.Click += new System.EventHandler(this.styleRockBand_Click);
            // 
            // radioProDrums
            // 
            this.radioProDrums.AutoSize = true;
            this.radioProDrums.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.radioProDrums.Checked = true;
            this.radioProDrums.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioProDrums.ForeColor = System.Drawing.Color.Black;
            this.radioProDrums.Location = new System.Drawing.Point(8, 57);
            this.radioProDrums.Name = "radioProDrums";
            this.radioProDrums.Size = new System.Drawing.Size(74, 17);
            this.radioProDrums.TabIndex = 65;
            this.radioProDrums.TabStop = true;
            this.radioProDrums.Text = "Pro Drums";
            this.radioProDrums.UseVisualStyleBackColor = false;
            // 
            // radioDrums
            // 
            this.radioDrums.AutoSize = true;
            this.radioDrums.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.radioDrums.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioDrums.ForeColor = System.Drawing.Color.Black;
            this.radioDrums.Location = new System.Drawing.Point(91, 57);
            this.radioDrums.Name = "radioDrums";
            this.radioDrums.Size = new System.Drawing.Size(55, 17);
            this.radioDrums.TabIndex = 66;
            this.radioDrums.Text = "Drums";
            this.radioDrums.UseVisualStyleBackColor = false;
            // 
            // panelCharts
            // 
            this.panelCharts.BackColor = System.Drawing.Color.Transparent;
            this.panelCharts.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelCharts.Controls.Add(this.label1);
            this.panelCharts.Controls.Add(this.cboCharts);
            this.panelCharts.Controls.Add(this.radioDrums);
            this.panelCharts.Controls.Add(this.radioProDrums);
            this.panelCharts.Location = new System.Drawing.Point(26, 611);
            this.panelCharts.Name = "panelCharts";
            this.panelCharts.Size = new System.Drawing.Size(158, 84);
            this.panelCharts.TabIndex = 69;
            this.panelCharts.Visible = false;
            this.panelCharts.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelMetronome_MouseDown);
            this.panelCharts.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelMetronome_MouseMove);
            this.panelCharts.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelMetronome_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 67;
            this.label1.Text = "Charts:";
            // 
            // panelKits
            // 
            this.panelKits.BackColor = System.Drawing.Color.Transparent;
            this.panelKits.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelKits.Controls.Add(this.label2);
            this.panelKits.Controls.Add(this.cboKits);
            this.panelKits.Location = new System.Drawing.Point(17, 41);
            this.panelKits.Name = "panelKits";
            this.panelKits.Size = new System.Drawing.Size(226, 61);
            this.panelKits.TabIndex = 70;
            this.panelKits.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelMetronome_MouseDown);
            this.panelKits.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelMetronome_MouseMove);
            this.panelKits.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelMetronome_MouseUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Drum Kits:";
            // 
            // frmMain
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1344, 805);
            this.Controls.Add(this.lblDebug);
            this.Controls.Add(this.panelKits);
            this.Controls.Add(this.panelCharts);
            this.Controls.Add(this.panelHitBox);
            this.Controls.Add(this.picTrack);
            this.Controls.Add(this.panelMetronome);
            this.Controls.Add(this.panelControls);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.lblTrackVolume);
            this.Controls.Add(this.lblDrumVolume);
            this.Controls.Add(this.lblConnect);
            this.Controls.Add(this.picRPedal);
            this.Controls.Add(this.picLPedal);
            this.Controls.Add(this.picTomB);
            this.Controls.Add(this.picTomY);
            this.Controls.Add(this.picTomG);
            this.Controls.Add(this.picSnare);
            this.Controls.Add(this.picCrash);
            this.Controls.Add(this.picRide);
            this.Controls.Add(this.picHiHat);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rok Drummer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseClick);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHiHat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCrash)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSnare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTomG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTomY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTomB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLPedal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRPedal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MetronomeDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MetronomeUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MetronomeOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MetronomeOff)).EndInit();
            this.panelControls.ResumeLayout(false);
            this.panelControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWorking)).EndInit();
            this.panelMetronome.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTrack)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panelCharts.ResumeLayout(false);
            this.panelCharts.PerformLayout();
            this.panelKits.ResumeLayout(false);
            this.panelKits.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboKits;
        private System.Windows.Forms.Timer ConnectionTimer;
        private System.Windows.Forms.Label lblConnect;
        private System.Windows.Forms.Timer DrumsTimer;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem controllersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem turnOff;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem turnOffAll;
        private System.Windows.Forms.Label lblDrumVolume;
        private System.Windows.Forms.PictureBox picHiHat;
        private System.Windows.Forms.PictureBox picRide;
        private System.Windows.Forms.PictureBox picCrash;
        private System.Windows.Forms.PictureBox picSnare;
        private System.Windows.Forms.PictureBox picTomG;
        private System.Windows.Forms.PictureBox picTomY;
        private System.Windows.Forms.PictureBox picTomB;
        private System.Windows.Forms.PictureBox picLPedal;
        private System.Windows.Forms.PictureBox picRPedal;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem doubleBassPedal;
        private System.Windows.Forms.ToolStripMenuItem forceClosedHihat;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem howToUse;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem playAlongMode;
        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdates;
        private System.ComponentModel.BackgroundWorker updater;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblTrackVolume;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.ToolStripMenuItem silenceDrumsTrack;
        private System.Windows.Forms.Label lblSection;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.PictureBox picWorking;
        private System.ComponentModel.BackgroundWorker songPreparer;
        private System.Windows.Forms.Timer PlaybackTimer;
        private System.Windows.Forms.NotifyIcon NotifyTray;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minimizeToTray;
        private System.Windows.Forms.Panel panelHitBox;
        private System.Windows.Forms.ToolStripMenuItem showMetronome;
        private System.Windows.Forms.PictureBox MetronomeDown;
        private System.Windows.Forms.PictureBox MetronomeUp;
        private System.Windows.Forms.PictureBox MetronomeOn;
        private System.Windows.Forms.PictureBox MetronomeOff;
        private System.Windows.Forms.Label lblTempo;
        private System.Windows.Forms.Panel panelMetronome;
        private System.Windows.Forms.Timer Metronome;
        private System.Windows.Forms.ToolStripMenuItem debugDrumInput;
        private System.Windows.Forms.ToolStripMenuItem c3Forums;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem viewChangeLog;
        private System.Windows.Forms.ToolStripMenuItem selectPlayer;
        private System.Windows.Forms.ToolStripMenuItem player1;
        private System.Windows.Forms.ToolStripMenuItem player2;
        private System.Windows.Forms.ToolStripMenuItem player3;
        private System.Windows.Forms.ToolStripMenuItem player4;
        private System.Windows.Forms.ToolStripMenuItem selectDrumKit;
        private System.Windows.Forms.ToolStripMenuItem rockBand1;
        private System.Windows.Forms.ToolStripMenuItem rockBand2;
        private System.Windows.Forms.ToolStripMenuItem pS3EKit;
        private System.Windows.Forms.PictureBox picTrack;
        private System.Windows.Forms.ComboBox cboCharts;
        private System.Windows.Forms.ToolStripMenuItem showChartSelection;
        private System.Windows.Forms.RadioButton radioProDrums;
        private System.Windows.Forms.RadioButton radioDrums;
        private System.Windows.Forms.ToolStripMenuItem autoPlayWithChart;
        private System.Windows.Forms.ToolStripMenuItem pS3GH5Kit;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.Panel panelCharts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelKits;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem layoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layoutIon;
        private System.Windows.Forms.ToolStripMenuItem layoutRB1;
        private System.Windows.Forms.ToolStripMenuItem layoutRB2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem customizeLayout;
        private System.Windows.Forms.ToolStripMenuItem resetLayoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layoutCustom;
        private System.Windows.Forms.ToolStripMenuItem layoutTron;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem styleVerticalScroll;
        private System.Windows.Forms.ToolStripMenuItem styleRockBand;
        private System.Windows.Forms.ToolStripMenuItem layoutGH5;
        private System.Windows.Forms.Label lblDebug;
        private System.Windows.Forms.ToolStripMenuItem hitVelocityControlsSampleVolume;
        private System.Windows.Forms.ToolStripMenuItem stageKitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stageKitPlayer1;
        private System.Windows.Forms.ToolStripMenuItem stageKitPlayer2;
        private System.Windows.Forms.ToolStripMenuItem stageKitPlayer3;
        private System.Windows.Forms.ToolStripMenuItem stageKitPlayer4;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem stageKitDisabled;
        private System.Windows.Forms.ToolStripMenuItem pS4RB4;
        private System.Windows.Forms.ToolStripMenuItem rockBand4;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
    }
}

