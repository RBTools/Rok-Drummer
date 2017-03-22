using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using RokDrummer.StageKit;
using RokDrummer.x360;
using SlimDX.XInput;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Mix;

namespace RokDrummer
{
    public partial class frmMain : Form
    {
        private readonly NemoTools Tools;
        private readonly DTAParser Parser;
        private readonly MIDIStuff MIDITools;
        private Controller Drums;
        private Controller stageKitController;
        private StageKitController stageKit;
        private LedDisplay ledDisplay;
        private readonly DrumKit Drumkit;
        private static readonly Color mMenuHighlight = Color.FromArgb(135, 0, 0);
        private static readonly Color mMenuBackground = Color.Black;
        private static readonly Color mMenuText = Color.Gray;
        private static readonly Color mMenuBorder = Color.WhiteSmoke;
        private readonly Color ChartOrange = Color.FromArgb(byte.MaxValue, 126, 0);
        private readonly Color ChartBlue = Color.FromArgb(0, 0, byte.MaxValue);
        private readonly Color ChartYellow = Color.FromArgb(242, 226, 0);
        private readonly Color ChartRed = Color.FromArgb(byte.MaxValue, 0, 0);
        private readonly Color ChartGreen = Color.FromArgb(0, byte.MaxValue, 0);
        private List<string> DrumKits;
        private string ActiveDrumKit;
        public double DrumVolume = 1.00;
        public double TrackVolume = 0.75;
        private readonly string ConfigFile;
        private readonly string LayoutFile;
        private const int NORMAL_WIDTH = 934;
        private const int EXTENDED_WIDTH = 1350;
        private bool showUpdateMessage;
        private const string AppName = "Rok Drummer";
        private const string NothingLoaded = "No song loaded...click button to load or drag/drop here...";
        private string SongFile;
        private string SongArtist;
        private string SongTitle;
        private string SongLength;
        private double SongLengthDouble;
        private double PlaybackSeconds;
        private int BassMixer;
        private int BassStream;
        private const int BassBuffer = 100;
        private int MetronomeTempo = 120;
        private readonly string TempMIDI;
        private const int NOTE_SPACER = 4;
        private double PlaybackWindow = 2.0;
        private const int KICK_HEIGHT = 10;
        private Bitmap RESOURCE_BACKGROUND;
        private Bitmap RESOURCE_PANEL;
        private Bitmap RESOURCE_TRACK;
        private Bitmap RESOURCE_TRACK_SOLO;
        private Bitmap RESOURCE_METRONOME;
        private Bitmap RESOURCE_SNARE;
        private Bitmap RESOURCE_TOM_Y;
        private Bitmap RESOURCE_TOM_B;
        private Bitmap RESOURCE_TOM_G;
        private Bitmap RESOURCE_TOM_OD;
        private Bitmap RESOURCE_CYMBAL_Y;
        private Bitmap RESOURCE_CYMBAL_B;
        private Bitmap RESOURCE_CYMBAL_G;
        private Bitmap RESOURCE_CYMBAL_OD;
        private const GamepadButtonFlags FLAGS_SNARE = GamepadButtonFlags.RightThumb | GamepadButtonFlags.B;
        private const GamepadButtonFlags FLAGS_TOM_Y = GamepadButtonFlags.RightThumb | GamepadButtonFlags.Y;
        private const GamepadButtonFlags FLAGS_TOM_B = GamepadButtonFlags.RightThumb | GamepadButtonFlags.X;
        private const GamepadButtonFlags FLAGS_TOM_G = GamepadButtonFlags.RightThumb | GamepadButtonFlags.A;
        private const GamepadButtonFlags FLAGS_HIHAT_OPEN = GamepadButtonFlags.DPadUp | GamepadButtonFlags.RightShoulder | GamepadButtonFlags.Y;
        private const GamepadButtonFlags FLAGS_HIHAT_CLOSED = GamepadButtonFlags.DPadUp | GamepadButtonFlags.LeftThumb | GamepadButtonFlags.RightShoulder | GamepadButtonFlags.Y;
        private const GamepadButtonFlags FLAGS_RIDE = GamepadButtonFlags.DPadDown | GamepadButtonFlags.RightShoulder | GamepadButtonFlags.X;
        private const GamepadButtonFlags FLAGS_CRASH = GamepadButtonFlags.RightShoulder | GamepadButtonFlags.A;
        private const GamepadButtonFlags FLAGS_KICK_PEDAL = GamepadButtonFlags.LeftShoulder;
        private const GamepadButtonFlags FLAGS_HIHAT_PEDAL = GamepadButtonFlags.LeftThumb;
        private const GamepadButtonFlags FLAGS_SNARE_PS3 = GamepadButtonFlags.LeftThumb | GamepadButtonFlags.A;
        private const GamepadButtonFlags FLAGS_TOM_Y_PS3 = GamepadButtonFlags.LeftThumb | GamepadButtonFlags.X;
        private const GamepadButtonFlags FLAGS_TOM_B_PS3 = GamepadButtonFlags.LeftThumb | GamepadButtonFlags.Y;
        private const GamepadButtonFlags FLAGS_TOM_G_PS3 = GamepadButtonFlags.LeftThumb | GamepadButtonFlags.B;

        private const GamepadButtonFlags FLAGS_HIHAT_OPEN_PS3 =
            GamepadButtonFlags.DPadUp | GamepadButtonFlags.RightThumb | GamepadButtonFlags.X;

        private const GamepadButtonFlags FLAGS_HIHAT_CLOSED_PS3 =
            GamepadButtonFlags.RightShoulder | GamepadButtonFlags.DPadUp | GamepadButtonFlags.RightThumb |
            GamepadButtonFlags.X;

        private const GamepadButtonFlags FLAGS_RIDE_PS3 =
            GamepadButtonFlags.DPadDown | GamepadButtonFlags.RightThumb | GamepadButtonFlags.Y;

        private const GamepadButtonFlags FLAGS_CRASH_PS3 = GamepadButtonFlags.RightThumb | GamepadButtonFlags.B;
        private const GamepadButtonFlags FLAGS_HIHAT_PEDAL_PS3 = GamepadButtonFlags.RightShoulder;
        private Keys KEY_SNARE = Keys.A;
        private Keys KEY_FLAM = Keys.W;
        private Keys KEY_TOM_Y = Keys.S;
        private Keys KEY_TOM_B = Keys.D;
        private Keys KEY_TOM_G = Keys.F;
        private Keys KEY_HIHAT = Keys.K;
        private Keys KEY_RIDE = Keys.L;
        private Keys KEY_CRASH = Keys.OemSemicolon;
        private Keys KEY_KICK_PEDAL = Keys.Space;
        private Keys KEY_HIHAT_PEDAL = Keys.J;
        private Keys KEY_VOLUME_UP = Keys.Add;
        private Keys KEY_VOLUME_DOWN = Keys.Subtract;
        private Keys KEY_PREV_KIT = Keys.PageUp;
        private Keys KEY_NEXT_KIT = Keys.PageDown;
        private int STREAM_METRONOME;
        private int STREAM_METRONOME_BEAT;
        private bool STATE_UP;
        private bool STATE_DOWN;
        private bool STATE_LEFT;
        private bool STATE_RIGHT;
        private bool STATE_START;
        private string DefaultDebuggingText;
        private bool FoggerIsOn;

        private readonly List<string> EmulatorFiles = new List<string>
        {
            "dinput8.dll",
            "xbox360cemu.ini",
            "xinput1_3.dll",
            "xinput9_1_0.dll"
        };

        private int MetronomeCount;
        private MIDITrack ActiveChart;
        private List<bool> STATE_INPUT;
        private List<bool> CurrentInputState;
        private List<bool> LastInputState;
        private List<PictureBox> DrumKitComponents;
        private List<Bitmap> DrumKitResources;
        private List<int> DrumKitStreams;
        private const int DRUM_PARTS = 11;
        private readonly string LayoutPath;
        private string CurrentLayout;
        private const int ChartGoal = 630;
        private const int ChartStart = 300;
        private const int ChartLeft = 98;

        [DllImport("xinput1_3.dll", EntryPoint = "#103")]
        private static extern void TurnOffController(int controller);

        public frmMain()
        {
            InitializeComponent();
            menuStrip.Renderer = new DarkRenderer();
            contextMenuStrip1.Renderer = new DarkRenderer();
            Width = NORMAL_WIDTH;
            Tools = new NemoTools();
            Parser = new DTAParser();
            MIDITools = new MIDIStuff();
            Drumkit = new DrumKit();
            ledDisplay = new LedDisplay();
            LayoutPath = Application.StartupPath + "\\layouts\\";
            CurrentLayout = LayoutPath + "rb2\\";
            ConfigFile = Application.StartupPath + "\\bin\\drummer.config";
            LayoutFile = Application.StartupPath + "\\bin\\layout.config";
            if (!Directory.Exists(Application.StartupPath + "\\bin\\"))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\bin\\");
            }
            if (!Directory.Exists(Application.StartupPath + "\\kits\\"))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\kits\\");
            }
            TempMIDI = Application.StartupPath + "\\bin\\temp.mid";
            if (!Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, Handle))
            {
                MessageBox.Show("Error initializing BASS.NET:\n" + Bass.BASS_ErrorGetCode() + "\nWon't be able to play any audio!",
                    AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_BUFFER, BassBuffer);
            Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UPDATEPERIOD, 5);
            PrepareResources();
            SelectController(UserIndex.One);
            MouseWheel += frmMain_MouseWheel;
            if (!Directory.Exists(LayoutPath)) return;
            layoutCustom.Enabled = File.Exists(LayoutPath + "custom\\layout.config");
        }

        private void PrepareResources()
        {
            STATE_INPUT = new List<bool>();
            CurrentInputState = new List<bool>();
            LastInputState = new List<bool>();
            DrumKitComponents = new List<PictureBox>();
            DrumKitResources = new List<Bitmap>();
            DrumKitStreams = new List<int>();
            for (var i = 0; i < DRUM_PARTS; i++)
            {
                STATE_INPUT.Add(false);
                CurrentInputState.Add(false);
                LastInputState.Add(false);
                DrumKitComponents.Add(null);
                DrumKitResources.Add(null);
                DrumKitStreams.Add(0);
            }
            DrumKitComponents[Drumkit.Snare] = picSnare;
            DrumKitComponents[Drumkit.Flam] = picSnare;
            DrumKitComponents[Drumkit.TomYellow] = picTomY;
            DrumKitComponents[Drumkit.TomBlue] = picTomB;
            DrumKitComponents[Drumkit.TomGreen] = picTomG;
            DrumKitComponents[Drumkit.HiHatOpen] = picHiHat;
            DrumKitComponents[Drumkit.HiHatClosed] = picHiHat;
            DrumKitComponents[Drumkit.Ride] = picRide;
            DrumKitComponents[Drumkit.Crash] = picCrash;
            DrumKitComponents[Drumkit.KickPedal] = picRPedal;
            DrumKitComponents[Drumkit.HiHatPedal] = picLPedal;
        }

        private void frmMain_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0 && PlaybackSeconds > 1.0)
            {
                PlaybackSeconds -= 1.0;
            }
            else if (e.Delta > 0 && PlaybackSeconds < SongLengthDouble - 1.0)
            {
                PlaybackSeconds += 1.0;
            }
            UpdateTime();
            if (Bass.BASS_ChannelIsActive(BassMixer) != BASSActive.BASS_ACTIVE_PAUSED &&
                Bass.BASS_ChannelIsActive(BassMixer) != BASSActive.BASS_ACTIVE_PLAYING)
            {
                return;
            }
            BassMix.BASS_Mixer_ChannelSetPosition(BassStream,
                Bass.BASS_ChannelSeconds2Bytes(BassStream, PlaybackSeconds));
            ResetPlayedNotes();
        }

        private void SaveConfig()
        {
            var sw = new StreamWriter(ConfigFile, false);
            sw.WriteLine("//Created with " + AppName + " " + GetAppVersion());
            sw.WriteLine("DrumVolume=" + DrumVolume);
            sw.WriteLine("TrackVolume=" + TrackVolume);
            sw.WriteLine("ForceHiHatClosed=" + forceClosedHihat.Checked);
            sw.WriteLine("DoubleBassPedal=" + doubleBassPedal.Checked);
            sw.WriteLine("PlayAlongMode=" + playAlongMode.Checked);
            sw.WriteLine("SilenceDrumTrack=" + silenceDrumsTrack.Checked);
            sw.WriteLine("ShowMetronome=" + showMetronome.Checked);
            sw.WriteLine("MetronomeTempo=" + MetronomeTempo);
            sw.WriteLine("KeyMapSnare=" + KEY_SNARE);
            sw.WriteLine("KeyMapFlam=" + KEY_FLAM);
            sw.WriteLine("KeyMapYellowTom=" + KEY_TOM_Y);
            sw.WriteLine("KeyMapBlueTom=" + KEY_TOM_B);
            sw.WriteLine("KeyMapGreenTom=" + KEY_TOM_G);
            sw.WriteLine("KeyMapHiHat=" + KEY_HIHAT);
            sw.WriteLine("KeyMapRide=" + KEY_RIDE);
            sw.WriteLine("KeyMapCrash=" + KEY_CRASH);
            sw.WriteLine("KeyMapBassPedal=" + KEY_KICK_PEDAL);
            sw.WriteLine("KeyMapHiHatPedal=" + KEY_HIHAT_PEDAL);
            sw.WriteLine("KeyMapNextKit=" + KEY_NEXT_KIT);
            sw.WriteLine("KeyMapPrevKit=" + KEY_PREV_KIT);
            sw.WriteLine("KeyMapVolumeUp=" + KEY_VOLUME_UP);
            sw.WriteLine("KeyMapVolumeDown=" + KEY_VOLUME_DOWN);
            sw.WriteLine("LastActiveKit=" + (cboKits.SelectedIndex == -1 ? "" : cboKits.Items[cboKits.SelectedIndex]));
            sw.WriteLine("RockBand1Kit=" + rockBand1.Checked);
            sw.WriteLine("RockBand2Kit=" + rockBand2.Checked);
            sw.WriteLine("PS3EKit=" + pS3EKit.Checked);
            sw.WriteLine("LayoutPath=" + CurrentLayout);
            sw.WriteLine("ShowChartSelection=" + showChartSelection.Checked);
            sw.WriteLine("AutoPlayChart=" + autoPlayWithChart.Checked);
            sw.WriteLine("DoProDrums=" + radioProDrums.Checked);
            sw.WriteLine("LayoutIon=" + layoutIon.Checked);
            sw.WriteLine("LayoutRB1=" + layoutRB1.Checked);
            sw.WriteLine("LayoutRB2=" + layoutRB2.Checked);
            sw.WriteLine("LayoutTron=" + layoutTron.Checked);
            sw.WriteLine("LayoutCustom=" + layoutCustom.Checked);
            sw.WriteLine("PS3GH5Kit=" + pS3GH5Kit.Checked);
            sw.WriteLine("LayoutGH5=" + layoutGH5.Checked);
            sw.WriteLine("StyleVertical=" + styleVerticalScroll.Checked);
            sw.WriteLine("StyleRockBand=" + styleRockBand.Checked);
            sw.WriteLine("UseVelocityData=" + hitVelocityControlsSampleVolume.Checked);
            sw.Dispose();
            SaveLayout();
        }

        private void SaveLayout()
        {
            var sw = new StreamWriter(LayoutFile, false);
            sw.WriteLine("//Created with " + AppName + " " + GetAppVersion());
            sw.WriteLine("//Do not modify manually ... use the program to modify your layout!");
            sw.WriteLine("DrumKits=" + panelKits.Left + "," + panelKits.Top);
            sw.WriteLine("Charts=" + panelCharts.Left + "," + panelCharts.Top);
            sw.WriteLine("Metronome=" + panelMetronome.Left + "," + panelMetronome.Top);
            for (var i = 0; i < DRUM_PARTS; i++)
            {
                sw.WriteLine(DrumKitComponents[i].Name + "=" + DrumKitComponents[i].Left + "," +
                             DrumKitComponents[i].Top);
            }
            sw.Dispose();
        }

        private void LoadLayout()
        {
            if (!File.Exists(LayoutFile)) return;
            var sr = new StreamReader(LayoutFile);
            try
            {
                sr.ReadLine();
                sr.ReadLine();
                var location = Tools.GetConfigString(sr.ReadLine()).Split(',');
                panelKits.Location = new Point(int.Parse(location[0]), int.Parse(location[1]));
                location = Tools.GetConfigString(sr.ReadLine()).Split(',');
                panelCharts.Location = new Point(int.Parse(location[0]), int.Parse(location[1]));
                location = Tools.GetConfigString(sr.ReadLine()).Split(',');
                panelMetronome.Location = new Point(int.Parse(location[0]), int.Parse(location[1]));
                for (var i = 0; i < DRUM_PARTS; i++)
                {
                    location = Tools.GetConfigString(sr.ReadLine()).Split(',');
                    DrumKitComponents[i].Location = new Point(int.Parse(location[0]), int.Parse(location[1]));
                }
            }
            catch (Exception)
            {}
            sr.Dispose();
        }

        private void LoadConfig()
        {
            if (!File.Exists(ConfigFile))
            {
                if (cboKits.Items.Count > 0)
                {
                    cboKits.SelectedIndex = 0;
                }
                return;
            }
            var sr = new StreamReader(ConfigFile);
            try
            {
                sr.ReadLine();
                DrumVolume = Convert.ToDouble(Tools.GetConfigString(sr.ReadLine()));
                TrackVolume = Convert.ToDouble(Tools.GetConfigString(sr.ReadLine()));
                forceClosedHihat.Checked = sr.ReadLine().Contains("True");
                doubleBassPedal.Checked = sr.ReadLine().Contains("True");
                playAlongMode.Checked = sr.ReadLine().Contains("True");
                silenceDrumsTrack.Checked = sr.ReadLine().Contains("True");
                showMetronome.Checked = sr.ReadLine().Contains("True");
                panelMetronome.Visible = showMetronome.Checked;
                MetronomeTempo = Convert.ToInt16(Tools.GetConfigString(sr.ReadLine()));
                KEY_SNARE = (Keys) Enum.Parse(typeof (Keys), Tools.GetConfigString(sr.ReadLine()));
                KEY_FLAM = (Keys) Enum.Parse(typeof (Keys), Tools.GetConfigString(sr.ReadLine()));
                KEY_TOM_Y = (Keys) Enum.Parse(typeof (Keys), Tools.GetConfigString(sr.ReadLine()));
                KEY_TOM_B = (Keys) Enum.Parse(typeof (Keys), Tools.GetConfigString(sr.ReadLine()));
                KEY_TOM_G = (Keys) Enum.Parse(typeof (Keys), Tools.GetConfigString(sr.ReadLine()));
                KEY_HIHAT = (Keys) Enum.Parse(typeof (Keys), Tools.GetConfigString(sr.ReadLine()));
                KEY_RIDE = (Keys) Enum.Parse(typeof (Keys), Tools.GetConfigString(sr.ReadLine()));
                KEY_CRASH = (Keys) Enum.Parse(typeof (Keys), Tools.GetConfigString(sr.ReadLine()));
                KEY_KICK_PEDAL = (Keys) Enum.Parse(typeof (Keys), Tools.GetConfigString(sr.ReadLine()));
                KEY_HIHAT_PEDAL = (Keys) Enum.Parse(typeof (Keys), Tools.GetConfigString(sr.ReadLine()));
                KEY_NEXT_KIT = (Keys) Enum.Parse(typeof (Keys), Tools.GetConfigString(sr.ReadLine()));
                KEY_PREV_KIT = (Keys) Enum.Parse(typeof (Keys), Tools.GetConfigString(sr.ReadLine()));
                KEY_VOLUME_UP = (Keys) Enum.Parse(typeof (Keys), Tools.GetConfigString(sr.ReadLine()));
                KEY_VOLUME_DOWN = (Keys) Enum.Parse(typeof (Keys), Tools.GetConfigString(sr.ReadLine()));
                ActiveDrumKit = Tools.GetConfigString(sr.ReadLine());
                rockBand1.Checked = sr.ReadLine().Contains("True");
                rockBand2.Checked = sr.ReadLine().Contains("True");
                pS3EKit.Checked = sr.ReadLine().Contains("True");
                CurrentLayout = Tools.GetConfigString(sr.ReadLine());
                showChartSelection.Checked = sr.ReadLine().Contains("True");
                autoPlayWithChart.Checked = sr.ReadLine().Contains("True");
                radioProDrums.Checked = sr.ReadLine().Contains("True");
                radioDrums.Checked = !radioProDrums.Checked;
                layoutIon.Checked = sr.ReadLine().Contains("True");
                layoutRB1.Checked = sr.ReadLine().Contains("True");
                layoutRB2.Checked = sr.ReadLine().Contains("True");
                layoutTron.Checked = sr.ReadLine().Contains("True");
                layoutCustom.Checked = sr.ReadLine().Contains("True");
                pS3GH5Kit.Checked = sr.ReadLine().Contains("True");
                layoutGH5.Checked = sr.ReadLine().Contains("True");
                styleVerticalScroll.Checked = sr.ReadLine().Contains("True");
                styleRockBand.Checked = sr.ReadLine().Contains("True");
                hitVelocityControlsSampleVolume.Checked = sr.ReadLine().Contains("True");
            }
            catch (Exception)
            {}
            sr.Dispose();
            Width = playAlongMode.Checked ? EXTENDED_WIDTH : NORMAL_WIDTH;
            LoadLayout();
            UpdateTrackVolume();
            UpdateMetronome();
            SelectActiveKit();
        }

        private void SelectActiveKit()
        {
            if (string.IsNullOrEmpty(ActiveDrumKit) || cboKits.Items.Count == 0) return;
            for (var i = 0; i < cboKits.Items.Count; i++)
            {
                if (cboKits.Items[i].ToString() != ActiveDrumKit) continue;
                cboKits.SelectedIndex = i;
                break;
            }
        }

        private void UpdateDrumKit()
        {
            if (rockBand1.Checked)
            {
                layoutRB1.PerformClick();
            }
            else if (pS3GH5Kit.Checked)
            {
                layoutGH5.PerformClick();
            }
            else
            {
                layoutRB2.PerformClick();
            }
            if (!pS3EKit.Checked && !pS3GH5Kit.Checked) return;
            foreach (
                var file in
                    EmulatorFiles.Where(
                        file =>
                            !File.Exists(Application.StartupPath + "\\" + file) &&
                            File.Exists(Application.StartupPath + "\\emu\\" + file)))
            {
                try
                {
                    File.Copy(Application.StartupPath + "\\emu\\" + file, Application.StartupPath + "\\" + file);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error copying emulator files:\n" + ex.Message + "\n\nCan't enable that drum kit",
                        AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pS3EKit.Checked = false;
                    pS3GH5Kit.Checked = false;
                    rockBand2.Checked = true;
                    UpdateDrumKit();
                    return;
                }
            }
        }

        private static string GetAppVersion()
        {
            var vers = Assembly.GetExecutingAssembly().GetName().Version;
            return "v" + String.Format("{0}.{1}.{2}", vers.Major, vers.Minor, vers.Build);
        }

        private void LoadLayoutImages()
        {
            try
            {
                RESOURCE_BACKGROUND = (Bitmap) Tools.NemoLoadImage(CurrentLayout + "background.jpg");
                DrumKitResources[Drumkit.Snare] = (Bitmap) Tools.NemoLoadImage(CurrentLayout + "hit_snare.png");
                DrumKitResources[Drumkit.Flam] = DrumKitResources[Drumkit.Snare];
                DrumKitResources[Drumkit.TomYellow] = (Bitmap) Tools.NemoLoadImage(CurrentLayout + "hit_tom_y.png");
                DrumKitResources[Drumkit.TomBlue] = (Bitmap) Tools.NemoLoadImage(CurrentLayout + "hit_tom_b.png");
                DrumKitResources[Drumkit.TomGreen] = (Bitmap) Tools.NemoLoadImage(CurrentLayout + "hit_tom_g.png");
                DrumKitResources[Drumkit.HiHatOpen] = (Bitmap) Tools.NemoLoadImage(CurrentLayout + "hit_cymbal_y.png");
                DrumKitResources[Drumkit.HiHatClosed] = DrumKitResources[Drumkit.HiHatOpen];
                DrumKitResources[Drumkit.Ride] = (Bitmap) Tools.NemoLoadImage(CurrentLayout + "hit_cymbal_b.png");
                DrumKitResources[Drumkit.Crash] = (Bitmap) Tools.NemoLoadImage(CurrentLayout + "hit_cymbal_g.png");
                DrumKitResources[Drumkit.KickPedal] = (Bitmap) Tools.NemoLoadImage(CurrentLayout + "hit_pedal_r.png");
                DrumKitResources[Drumkit.HiHatPedal] = (Bitmap) Tools.NemoLoadImage(CurrentLayout + "hit_pedal_l.png");
                BackgroundImage = RESOURCE_BACKGROUND;
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Some of the layout images were missing so I couldn't load them\n" + AppName +
                    " includes all the necessary images when you download it, " +
                    "if you're going to modify it, you need to make sure your modified files have the exact same name as the original files",
                    AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            UpdateDrumKitComponents();
        }

        private void LoadImages()
        {
            var res = Application.StartupPath + "\\res\\";
            if (!Directory.Exists(res))
            {
                MessageBox.Show("Missing \\res\\ folder, no images will be loaded", AppName, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            try
            {
                LoadLayoutImages();
                RESOURCE_PANEL = (Bitmap) Tools.NemoLoadImage(res + "panel.jpg");
                RESOURCE_METRONOME = (Bitmap) Tools.NemoLoadImage(res + "metronome.png");
                RESOURCE_SNARE = (Bitmap) Tools.NemoLoadImage(res + "snare.png");
                RESOURCE_TOM_Y = (Bitmap) Tools.NemoLoadImage(res + "tom_y.png");
                RESOURCE_TOM_B = (Bitmap) Tools.NemoLoadImage(res + "tom_b.png");
                RESOURCE_TOM_G = (Bitmap) Tools.NemoLoadImage(res + "tom_g.png");
                RESOURCE_TOM_OD = (Bitmap) Tools.NemoLoadImage(res + "tom_od.png");
                RESOURCE_CYMBAL_Y = (Bitmap) Tools.NemoLoadImage(res + "cymbal_y.png");
                RESOURCE_CYMBAL_B = (Bitmap) Tools.NemoLoadImage(res + "cymbal_b.png");
                RESOURCE_CYMBAL_G = (Bitmap) Tools.NemoLoadImage(res + "cymbal_g.png");
                RESOURCE_CYMBAL_OD = (Bitmap) Tools.NemoLoadImage(res + "cymbal_od.png");
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Some of the resource images were missing so I couldn't load them\n" + AppName +
                    " includes all the necessary images when you download it, " +
                    "if you're going to modify it, you need to make sure your modified files have the exact same name as the original files",
                    AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            panelControls.BackgroundImage = RESOURCE_PANEL;
            picTrack.Image = RESOURCE_TRACK;
            panelMetronome.BackgroundImage = RESOURCE_METRONOME;
            try
            {
                STREAM_METRONOME = Bass.BASS_StreamCreateFile(res + "metronome.wav", 0L, 0L, BASSFlag.BASS_SAMPLE_FLOAT);
                STREAM_METRONOME_BEAT = Bass.BASS_StreamCreateFile(res + "metronome_beat.wav", 0L, 0L,
                    BASSFlag.BASS_SAMPLE_FLOAT);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading the metronome WAV file:\n" + ex.Message, AppName, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }

        private void UpdateDrumKitComponents()
        {
            for (var i = 0; i < DRUM_PARTS; i++)
            {
                DrumKitComponents[i].Visible = DrumKitResources[i] != null;
                if (DrumKitResources[i] == null) continue;
                DrumKitComponents[i].Size = DrumKitResources[i].Size;
            }
        }

        private void FixImageTransparency()
        {
            LoadImages();
            var hitBoxes = new List<PictureBox>
            {
                picSnare,
                picTomY,
                picTomB,
                picTomG,
                picHiHat,
                picRide,
                picCrash,
                picLPedal,
                picRPedal
            };
            foreach (var box in hitBoxes)
            {
                var pos = PointToScreen(box.Location);
                pos = PointToClient(pos);
                box.Parent = this;
                box.Location = pos;
                box.BackColor = Color.Transparent;
            }
            var metronomeBoxes = new List<PictureBox> {MetronomeOn, MetronomeOff, MetronomeDown, MetronomeUp};
            foreach (var box in metronomeBoxes)
            {
                var pos = PointToScreen(box.Location);
                pos = PointToClient(pos);
                box.Parent = panelMetronome;
                box.Location = pos;
                box.BackColor = Color.Transparent;
            }
            var position = PointToScreen(lblTempo.Location);
            position = PointToClient(position);
            lblTempo.Parent = panelMetronome;
            lblTempo.Location = position;
            lblTempo.BackColor = Color.Transparent;
        }

        private void SelectController(UserIndex index, bool drums = true)
        {
            try
            {
                if (drums)
                {
                    Drums = new Controller(index);
                    ConnectionTimer.Enabled = true;
                }
                else
                {
                    stageKitController = new Controller(index);
                    stageKit = new StageKitController(((int)index) + 1);
                }
            }
            catch (Exception ex)
            {
                ConnectionTimer.Enabled = false;
                MessageBox.Show("Error creating drums controller:\n" + ex.Message, AppName, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void cboKits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboKits.SelectedIndex == -1)
            {
                ActiveDrumKit = "";
                return;
            }
            ActiveDrumKit = DrumKits[cboKits.SelectedIndex] + "\\";
            LoadDrumKits();
        }

        private void LoadDrumKits()
        {
            var Samples = new List<string>
            {
                "snare",
                "flam",
                "tom_yellow",
                "tom_blue",
                "tom_green",
                "hihat_open",
                "hihat_closed",
                "ride",
                "crash",
                "kick",
                doubleBassPedal.Checked ? "kick" : "hihat_kick",
            };
            for (var i = 0; i < Samples.Count; i++)
            {
                try
                {
                    if (File.Exists(ActiveDrumKit + Samples[i] + ".wav"))
                    {
                        DrumKitStreams[i] = Bass.BASS_StreamCreateFile(ActiveDrumKit + Samples[i] + ".wav", 0L, 0L,
                            BASSFlag.BASS_SAMPLE_FLOAT);
                    }
                    else
                    {
                        MessageBox.Show("Sample '" + Samples[i] + ".wav' is missing!\nSample will NOT play.", AppName,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        DrumKitStreams[i] = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Error loading kit sample: " + Samples[i] + ".wav:\n" + ex.Message + "\nSample will NOT play.",
                        AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            UpdateDrumVolume(false, true);
        }

        private void PlaySample(int index, int velocity = 127)
        {
            if (DrumKitStreams[index] == 0) return;
            if (hitVelocityControlsSampleVolume.Checked)
            {
                Bass.BASS_ChannelSetAttribute(DrumKitStreams[index], BASSAttribute.BASS_ATTRIB_VOL, (float) (velocity/127.0));
            }
            else
            {
                Bass.BASS_ChannelSetAttribute(DrumKitStreams[index], BASSAttribute.BASS_ATTRIB_VOL, 1.0f);
            }
            Bass.BASS_ChannelPlay(DrumKitStreams[index], true);
            STATE_INPUT[index] = true;
            UpdateDrumHits(index);
        }

        private void ConnectionTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                player1.Enabled = new Controller(UserIndex.One).IsConnected;
                player2.Enabled = new Controller(UserIndex.Two).IsConnected;
                player3.Enabled = new Controller(UserIndex.Three).IsConnected;
                player4.Enabled = new Controller(UserIndex.Four).IsConnected;
                stageKitPlayer1.Enabled = player1.Enabled;
                stageKitPlayer2.Enabled = player2.Enabled;
                stageKitPlayer3.Enabled = player3.Enabled;
                stageKitPlayer4.Enabled = player4.Enabled;
                turnOff.Enabled = player1.Enabled | player2.Enabled | player3.Enabled | player4.Enabled;
                turnOffAll.Enabled = turnOff.Enabled;
                debugDrumInput.Enabled = Drums.IsConnected;
            }
            catch (Exception)
            {}
            try
            {
                if (Drums.IsConnected)
                {
                    lblConnect.Text = "Connected";
                    lblConnect.ForeColor = Color.LawnGreen;
                    DrumsTimer.Enabled = true;
                    lblDebug.Visible = debugDrumInput.Checked;
                    if (Drums.IsConnected && string.IsNullOrEmpty(DefaultDebuggingText))
                    {
                        DefaultDebuggingText = GetDebugData(Drums.GetState().Gamepad);
                    }
                    return;
                }
            }
            catch (Exception)
            {}
            lblConnect.Text = "Disconnected";
            lblConnect.ForeColor = Color.Firebrick;
            DrumsTimer.Enabled = false;
            debugDrumInput.Checked = false;
            lblDebug.Visible = false;
            DefaultDebuggingText = "";
        }

        private void ChangeKit(bool previous)
        {
            if (cboKits.Items.Count < 2) return;
            if (previous && cboKits.SelectedIndex > 0)
            {
                cboKits.SelectedIndex--;
            }
            else if (!previous && cboKits.SelectedIndex + 1 < cboKits.Items.Count)
            {
                cboKits.SelectedIndex++;
            }
        }

        public void UpdateDrumVolume(bool lower, bool config = false)
        {
            if (!config)
            {
                if (lower && DrumVolume > 0.0)
                {
                    DrumVolume = Math.Round(DrumVolume - 0.02, 2);
                }
                else if (!lower && DrumVolume < 1.0)
                {
                    DrumVolume = Math.Round(DrumVolume + 0.02, 2);
                }
            }
            if (DrumVolume < 0.00)
            {
                DrumVolume = 0.00;
            }
            else if (DrumVolume > 1.00)
            {
                DrumVolume = 1.00;
            }
            lblDrumVolume.Text = "Drum Volume: " + (DrumVolume*100);
            for (var i = 0; i < DRUM_PARTS; i++)
            {
                Bass.BASS_ChannelSetAttribute(DrumKitStreams[i], BASSAttribute.BASS_ATTRIB_VOL, (float) DrumVolume);
            }
        }

        public void UpdateTrackVolume()
        {
            if (TrackVolume < 0.00)
            {
                TrackVolume = 0.00;
            }
            else if (TrackVolume > 1.00)
            {
                TrackVolume = 1.00;
            }
            lblTrackVolume.Text = "Track Volume: " + (TrackVolume*100);
            if (Bass.BASS_ChannelIsActive(BassMixer) != BASSActive.BASS_ACTIVE_PAUSED &&
                Bass.BASS_ChannelIsActive(BassMixer) != BASSActive.BASS_ACTIVE_PLAYING)
            {
                return;
            }
            Bass.BASS_ChannelSetAttribute(BassMixer, BASSAttribute.BASS_ATTRIB_VOL, (float) TrackVolume);
        }
        
        private int CalculateDrumVelocity(Gamepad gamepad)
        {
            float rawValue = 0;
            var buttons = Drums.GetState().Gamepad.Buttons;
            if (buttons.ToString() == "None") return 0;
            if (buttons.ToString().Contains("A"))
            {
                rawValue = gamepad.RightThumbY + 32768;
            }
            else if (buttons.ToString().Contains("B"))
            {
                rawValue = -gamepad.LeftThumbX + 32768;
            }
            else if (buttons.ToString().Contains("X"))
            {
                rawValue = -gamepad.RightThumbX + 32768;
            }
            else if (buttons.ToString().Contains("Y"))
            {
                rawValue = gamepad.LeftThumbY + 32768;
            }
            const float Range = 32768.0f;
	        const float DZoneMin = 0.15f;
	        const float DZoneMax = 1.30f;
            var NewValue = rawValue / Range;
            if (NewValue < DZoneMin)
            {
                NewValue = DZoneMin;
            }
		    NewValue = NewValue * DZoneMax;
		    return (int)(NewValue * 127.0f);
        }

        private string GetDebugData(Gamepad gamepad)
        {
            var velocity = CalculateDrumVelocity(gamepad);
            var debug = "DEBUG = Buttons: " + gamepad.Buttons +
                        "   |   LThumb (x,y): " + gamepad.LeftThumbX + "," + gamepad.LeftThumbY +
                        "   |   RThumb (x,y): " + gamepad.RightThumbX + "," + gamepad.RightThumbY +
                        "   |   LTrigger: " + gamepad.LeftTrigger +
                        "   |   RTrigger: " + gamepad.RightTrigger +
                        "   |   Vel.: " + velocity +
                        "   |   Hash: " + gamepad.GetHashCode();
            return debug;
        }

        private void DrumsTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                var gamepad = Drums.GetState().Gamepad;
                var velocity = CalculateDrumVelocity(gamepad);
                var Buttons = gamepad.Buttons;
                if (debugDrumInput.Checked)
                {
                    var debug = GetDebugData(gamepad);
                    if (debug != DefaultDebuggingText)
                    {
                        lblDebug.Text = debug;
                    }
                }
                if (Buttons == GamepadButtonFlags.DPadLeft && !STATE_LEFT)
                {
                    ChangeKit(true);
                }
                else if (Buttons == GamepadButtonFlags.DPadRight && !STATE_RIGHT)
                {
                    ChangeKit(false);
                }
                else if (Buttons == GamepadButtonFlags.DPadUp && !STATE_UP)
                {
                    UpdateDrumVolume(false);
                }
                else if (Buttons == GamepadButtonFlags.DPadDown && !STATE_DOWN)
                {
                    UpdateDrumVolume(true);
                }
                else if (Buttons == GamepadButtonFlags.Start && !STATE_START)
                {
                    btnPlay.PerformClick();
                }
                else if (Buttons == GamepadButtonFlags.Back && stageKitController != null && stageKitController.IsConnected)
                {
                    if (FoggerIsOn)
                    {
                        stageKit.TurnFogOff();
                    }
                    else
                    {
                        stageKit.TurnFogOn();
                    }
                    FoggerIsOn = !FoggerIsOn;
                }
                CurrentInputState[Drumkit.HiHatClosed] = HitHiHatClosed(Buttons) || (forceClosedHihat.Checked && HitHiHatOpen(Buttons));
                CurrentInputState[Drumkit.HiHatOpen] = !CurrentInputState[Drumkit.HiHatClosed] && !forceClosedHihat.Checked && HitHiHatOpen(Buttons);
                CurrentInputState[Drumkit.Ride] = HitRide(Buttons);
                CurrentInputState[Drumkit.Crash] = HitCrash(Buttons);
                CurrentInputState[Drumkit.Flam] = HitFlam(Buttons);
                CurrentInputState[Drumkit.Snare] = !CurrentInputState[Drumkit.Flam] && HitSnare(Buttons);
                CurrentInputState[Drumkit.TomYellow] = !CurrentInputState[Drumkit.Flam] && HitTomYellow(Buttons);
                CurrentInputState[Drumkit.TomBlue] = HitTomBlue(Buttons);
                CurrentInputState[Drumkit.TomGreen] = HitTomGreen(Buttons);
                CurrentInputState[Drumkit.KickPedal] = HitKickPedal(Buttons);
                CurrentInputState[Drumkit.HiHatPedal] = HitHiHatPedal(Buttons);
                STATE_LEFT = Buttons == GamepadButtonFlags.DPadLeft;
                STATE_RIGHT = Buttons == GamepadButtonFlags.DPadRight;
                STATE_UP = Buttons == GamepadButtonFlags.DPadUp;
                STATE_DOWN = Buttons == GamepadButtonFlags.DPadDown;
                STATE_START = Buttons == GamepadButtonFlags.Start;
                for (var i = 0; i < DRUM_PARTS; i++)
                {
                    if (CurrentInputState[i] != LastInputState[i])
                    {
                        if (!STATE_INPUT[i])
                        {
                            PlaySample(i, velocity);
                        }
                        else
                        {
                            STATE_INPUT[i] = false;
                            UpdateDrumHits(i);
                        }
                    }
                    LastInputState[i] = CurrentInputState[i];
                }
            }
            catch (Exception)
            {}
        }

        private void UpdateDrumHits(int index)
        {
            UpdateStageKitLights(index, STATE_INPUT[index]);
            Controls.SetChildIndex(DrumKitComponents[index], 2);
            DrumKitComponents[index].Image = STATE_INPUT[index] ? DrumKitResources[index] : null;
        }

        private void UpdateStageKitLights(int index, bool state)
        {
            if (stageKitController == null || !stageKitController.IsConnected) return;
            switch (index)
            {
                case 0:
                    stageKit.DisplayRedAll(ref ledDisplay, state);
                    break;
                case 1:
                    stageKit.DisplayRedAll(ref ledDisplay, state);
                    stageKit.DisplayYellowAll(ref ledDisplay, state);
                    break;
                case 2:
                case 5:
                case 6:
                    stageKit.DisplayYellowAll(ref ledDisplay, state);
                    break;
                case 3:
                case 7:
                    stageKit.DisplayBlueAll(ref ledDisplay, state);
                    break;
                case 4:
                case 8:
                    stageKit.DisplayGreenAll(ref ledDisplay, state);
                    break;
                case 9:
                    if (state)
                    {
                        stageKit.TurnStrobeOn(StrobeSpeed.Slow);
                    }
                    else
                    {
                        stageKit.TurnStrobeOff();
                    }
                    break;
                case 10:
                    if (doubleBassPedal.Checked)
                    {
                        goto case 9;
                    }
                    goto case 6;
            }
        }

        private void turnOff_Click(object sender, EventArgs e)
        {
            try
            {
                if (player1.Checked)
                {
                    TurnOffController(0);
                }
                else if (player2.Checked)
                {
                    TurnOffController(1);
                }
                else if (player3.Checked)
                {
                    TurnOffController(2);
                }
                else if (player4.Checked)
                {
                    TurnOffController(3);
                }
            }
            catch (Exception)
            {
            }
        }

        private void turnOffAll_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < 4; i++)
            {
                try
                {
                    TurnOffController(i);
                }
                catch (Exception)
                {}
            }
        }

        private void UncheckAllPlayers()
        {
            player1.Checked = false;
            player2.Checked = false;
            player3.Checked = false;
            player4.Checked = false;
        }
        
        private void player1_Click(object sender, EventArgs e)
        {
            UncheckAllPlayers();
            player1.Checked = true;
            if (stageKitPlayer1.Checked)
            {
                stageKitPlayer2_Click(sender, e);
            }
            SelectController(UserIndex.One);
        }

        private void player2_Click(object sender, EventArgs e)
        {
            UncheckAllPlayers();
            player2.Checked = true;
            if (stageKitPlayer2.Checked)
            {
                stageKitPlayer1_Click(sender, e);
            }
            SelectController(UserIndex.Two);
        }

        private void player3_Click(object sender, EventArgs e)
        {
            UncheckAllPlayers();
            player3.Checked = true;
            if (stageKitPlayer3.Checked)
            {
                stageKitPlayer1_Click(sender, e);
            }
            SelectController(UserIndex.Three);
        }

        private void player4_Click(object sender, EventArgs e)
        {
            UncheckAllPlayers();
            player4.Checked = true;
            if (stageKitPlayer4.Checked)
            {
                stageKitPlayer1_Click(sender, e);
            }
            SelectController(UserIndex.Four);
        }

        private bool HitSnare(GamepadButtonFlags state)
        {
            var snare = FLAGS_SNARE;
            if (pS3EKit.Checked)
            {
                snare = FLAGS_SNARE_PS3;
            }
            else if (pS3GH5Kit.Checked)
            {
                snare = GamepadButtonFlags.A;
            }
            else if (rockBand1.Checked)
            {
                snare = GamepadButtonFlags.B;
            }
            return state.HasFlag(snare);
        }

        private bool HitTomYellow(GamepadButtonFlags state)
        {
            if (rockBand1.Checked)
            {
                return state.HasFlag(GamepadButtonFlags.Y);
            }
            if (pS3EKit.Checked)
            {
                return state.HasFlag(FLAGS_TOM_Y_PS3) &&
                       !((state.HasFlag(FLAGS_HIHAT_OPEN_PS3) || state.HasFlag(FLAGS_HIHAT_CLOSED_PS3)) &&
                         (state.HasFlag(FLAGS_SNARE_PS3) ||
                          state.HasFlag(FLAGS_TOM_B_PS3) || state.HasFlag(FLAGS_TOM_G_PS3)));
            }
            return state.HasFlag(FLAGS_TOM_Y) &&
                   !((state.HasFlag(FLAGS_HIHAT_OPEN) || state.HasFlag(FLAGS_HIHAT_CLOSED)) &&
                     (state.HasFlag(FLAGS_SNARE) ||
                      state.HasFlag(FLAGS_TOM_B) || state.HasFlag(FLAGS_TOM_G)));
        }

        private bool HitTomBlue(GamepadButtonFlags state)
        {
            if (rockBand1.Checked)
            {
                return state.HasFlag(GamepadButtonFlags.X);
            }
            if (pS3GH5Kit.Checked)
            {
                return state.HasFlag(GamepadButtonFlags.Y);
            }
            if (pS3EKit.Checked)
            {
                return state.HasFlag(FLAGS_TOM_B_PS3) &&
                       !(state.HasFlag(FLAGS_RIDE_PS3) &&
                         (state.HasFlag(FLAGS_SNARE_PS3) || state.HasFlag(FLAGS_TOM_Y_PS3) ||
                          state.HasFlag(FLAGS_TOM_G_PS3)));
            }
            return state.HasFlag(FLAGS_TOM_B) &&
                   !(state.HasFlag(FLAGS_RIDE) &&
                     (state.HasFlag(FLAGS_SNARE) || state.HasFlag(FLAGS_TOM_Y) || state.HasFlag(FLAGS_TOM_G)));
        }

        private bool HitTomGreen(GamepadButtonFlags state)
        {
            if (rockBand1.Checked)
            {
                return state.HasFlag(GamepadButtonFlags.A);
            }
            if (pS3GH5Kit.Checked)
            {
                return state.HasFlag(GamepadButtonFlags.B);
            }
            if (pS3EKit.Checked)
            {
                return state.HasFlag(FLAGS_TOM_G_PS3) &&
                       (!(state.HasFlag(FLAGS_CRASH_PS3) &&
                          (state.HasFlag(FLAGS_SNARE_PS3) || state.HasFlag(FLAGS_TOM_Y_PS3) ||
                           state.HasFlag(FLAGS_TOM_B_PS3))) ||
                        state.HasFlag(FLAGS_HIHAT_CLOSED_PS3) || state.HasFlag(FLAGS_HIHAT_OPEN_PS3) ||
                        state.HasFlag(FLAGS_RIDE_PS3));
            }
            return state.HasFlag(FLAGS_TOM_G) &&
                   (!(state.HasFlag(FLAGS_CRASH) &&
                      (state.HasFlag(FLAGS_SNARE) || state.HasFlag(FLAGS_TOM_Y) || state.HasFlag(FLAGS_TOM_B))) ||
                    state.HasFlag(FLAGS_HIHAT_CLOSED) || state.HasFlag(FLAGS_HIHAT_OPEN) || state.HasFlag(FLAGS_RIDE));
        }

        private bool HitHiHatOpen(GamepadButtonFlags state)
        {
            if (rockBand1.Checked) return false;
            if (pS3GH5Kit.Checked)
            {
                return state.HasFlag(GamepadButtonFlags.X);
            }
            var hihat = FLAGS_HIHAT_OPEN;
            if (pS3EKit.Checked)
            {
                hihat = FLAGS_HIHAT_OPEN_PS3;
            }
            return state.HasFlag(hihat);
        }

        private bool HitHiHatClosed(GamepadButtonFlags state)
        {
            if (rockBand1.Checked) return false;
            var hihat = FLAGS_HIHAT_CLOSED;
            if (pS3EKit.Checked)
            {
                hihat = FLAGS_HIHAT_CLOSED_PS3;
            }
            return state.HasFlag(hihat);
        }

        private bool HitRide(GamepadButtonFlags state)
        {
            if (rockBand1.Checked) return false;
            var ride = FLAGS_RIDE;
            if (pS3EKit.Checked)
            {
                ride = FLAGS_RIDE_PS3;
            }
            return state.HasFlag(ride);
        }

        private bool HitCrash(GamepadButtonFlags state)
        {
            if (rockBand1.Checked) return false;
            if (pS3GH5Kit.Checked)
            {
                return state.HasFlag(GamepadButtonFlags.RightShoulder);
            }
            if (pS3EKit.Checked)
            {
                return state.HasFlag(FLAGS_CRASH_PS3) &&
                       !(state.HasFlag(FLAGS_TOM_G_PS3) &&
                         (state.HasFlag(FLAGS_HIHAT_CLOSED_PS3) || state.HasFlag(FLAGS_HIHAT_OPEN_PS3) ||
                          state.HasFlag(FLAGS_RIDE_PS3)));
            }
            return state.HasFlag(FLAGS_CRASH) &&
                   !(state.HasFlag(FLAGS_TOM_G) &&
                     (state.HasFlag(FLAGS_HIHAT_CLOSED) || state.HasFlag(FLAGS_HIHAT_OPEN) || state.HasFlag(FLAGS_RIDE)));
        }

        private static bool HitKickPedal(GamepadButtonFlags state)
        {
            return state.HasFlag(FLAGS_KICK_PEDAL);
        }

        private bool HitHiHatPedal(GamepadButtonFlags state)
        {
            if (rockBand1.Checked) return false;
            var hihatpedal = FLAGS_HIHAT_PEDAL;
            if (pS3EKit.Checked)
            {
                hihatpedal = FLAGS_HIHAT_PEDAL_PS3;
            }
            return state.HasFlag(hihatpedal);
        }
        
        private bool HitFlam(GamepadButtonFlags state)
        {
            var snare = FLAGS_SNARE;
            var tom = FLAGS_TOM_Y;
            if (pS3EKit.Checked)
            {
                snare = FLAGS_SNARE_PS3;
                tom = FLAGS_TOM_Y_PS3;
            }
            else if (rockBand1.Checked)
            {
                snare = GamepadButtonFlags.B;
                tom = GamepadButtonFlags.Y;
            }
            return state.HasFlag(snare) && state.HasFlag(tom);
        }

        private void picHitYC_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (customizeLayout.Checked)
            {
                ((PictureBox) sender).Cursor = Cursors.NoMove2D;
                return;
            }
            PlaySample(forceClosedHihat.Checked || (!doubleBassPedal.Checked && STATE_INPUT[Drumkit.HiHatPedal])
                ? Drumkit.HiHatClosed
                : Drumkit.HiHatOpen);
        }

        private void picHitSnare_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (customizeLayout.Checked)
            {
                ((PictureBox) sender).Cursor = Cursors.NoMove2D;
                return;
            }
            PlaySample(Drumkit.Snare);
        }

        private void picHitYT_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (customizeLayout.Checked)
            {
                ((PictureBox) sender).Cursor = Cursors.NoMove2D;
                return;
            }
            PlaySample(Drumkit.TomYellow);
        }

        private void picHitBT_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (customizeLayout.Checked)
            {
                ((PictureBox) sender).Cursor = Cursors.NoMove2D;
                return;
            }
            PlaySample(Drumkit.TomBlue);
        }

        private void picHitGT_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (customizeLayout.Checked)
            {
                ((PictureBox) sender).Cursor = Cursors.NoMove2D;
                return;
            }
            PlaySample(Drumkit.TomGreen);
        }

        private void picHitBC_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (customizeLayout.Checked)
            {
                ((PictureBox) sender).Cursor = Cursors.NoMove2D;
                return;
            }
            PlaySample(Drumkit.Ride);
        }

        private void picHitGC_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (customizeLayout.Checked)
            {
                ((PictureBox) sender).Cursor = Cursors.NoMove2D;
                return;
            }
            PlaySample(Drumkit.Crash);
        }

        private void picHitRPedal_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (customizeLayout.Checked)
            {
                ((PictureBox) sender).Cursor = Cursors.NoMove2D;
                return;
            }
            PlaySample(Drumkit.KickPedal);
        }

        private void picHitYC_MouseUp(object sender, MouseEventArgs e)
        {
            ((PictureBox) sender).Cursor = Cursors.Hand;
            if (customizeLayout.Checked) return;
            ((PictureBox) sender).Image = null;
            ActiveControl = null;
            var index = Convert.ToInt16(((PictureBox) sender).Tag);
            switch (index)
            {
                case 0:
                case 1:
                    STATE_INPUT[0] = false;
                    STATE_INPUT[1] = false;
                    break;
                case 5:
                case 6:
                    STATE_INPUT[5] = false;
                    STATE_INPUT[6] = false;
                    break;
                default:
                    STATE_INPUT[index] = false;
                    break;
            }
            UpdateStageKitLights(index, STATE_INPUT[index]);
        }

        private void picHitLPedal_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (customizeLayout.Checked)
            {
                ((PictureBox) sender).Cursor = Cursors.NoMove2D;
                return;
            }
            PlaySample(Drumkit.HiHatPedal);
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            var drumKitsPath = Application.StartupPath + "\\kits\\";
            var kits = Directory.GetDirectories(drumKitsPath);
            if (kits.Any())
            {
                DrumKits = kits.ToList();
                DrumKits.Sort();
                foreach (var kit in DrumKits)
                {
                    cboKits.Items.Add(kit.Replace(drumKitsPath, ""));
                }
                cboKits.Enabled = true;
            }
            LoadConfig();
            FixImageTransparency();
            UpdateTrackStyle();
            Controls.SetChildIndex(panelControls, 1);
            Controls.SetChildIndex(menuStrip, 1);
            updater.RunWorkerAsync();
            if (EmulatorFiles.All(file => File.Exists(Application.StartupPath + "\\emu\\" + file))) return;
            MessageBox.Show("One or more of the required emulator files are missing, will not be able to work with Wii or PS3 drums!",
                AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            pS3EKit.Enabled = false;
            if (!pS3EKit.Checked) return;
            rockBand2.Checked = true;
            UpdateDrumKit();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!picWorking.Visible && !songPreparer.IsBusy)
            {
                StopStageKit();
                SaveConfig();
                Tools.DeleteFile(TempMIDI);
                return;
            }
            MessageBox.Show("Please wait for the current process to finish", AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            e.Cancel = true;
        }

        private void StopStageKit()
        {
            try
            {
                stageKit.TurnAllOff();
            }
            catch (Exception)
            {}
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var version = GetAppVersion();
            var message = AppName + "\nVersion: " + version +
                          "\n© TrojanNemo, 2015\nDedicated to the C3 community\nCreated for the hell of it, don't expect too much from it!\n\n";
            var credits = Tools.ReadHelpFile("credits");
            MessageBox.Show(message + credits, "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void playAlongMode_Click(object sender, EventArgs e)
        {
            Width = playAlongMode.Checked ? EXTENDED_WIDTH : NORMAL_WIDTH;
            StopEverything();
        }

        private void updater_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var path = Application.StartupPath + "\\bin\\update.txt";
            Tools.DeleteFile(path);
            using (var client = new WebClient())
            {
                try
                {
                    client.DownloadFile("http://www.keepitfishy.com/rb3/rokdrummer/update.txt", path);
                }
                catch (Exception)
                {
                }
            }
        }

        private void updater_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            var path = Application.StartupPath + "\\bin\\update.txt";
            if (!File.Exists(path))
            {
                if (showUpdateMessage)
                {
                    MessageBox.Show("Unable to check for updates", AppName, MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
                return;
            }
            var thisVersion = GetAppVersion();
            var newVersion = "v";
            string newName;
            string releaseDate;
            string link;
            var changeLog = new List<string>();
            var sr = new StreamReader(path);
            try
            {
                var line = sr.ReadLine();
                if (line.ToLowerInvariant().Contains("html"))
                {
                    sr.Dispose();
                    if (showUpdateMessage)
                    {
                        MessageBox.Show("Unable to check for updates", AppName, MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                    }
                    return;
                }
                newName = Tools.GetConfigString(line);
                newVersion += Tools.GetConfigString(sr.ReadLine());
                releaseDate = Tools.GetConfigString(sr.ReadLine());
                link = Tools.GetConfigString(sr.ReadLine());
                sr.ReadLine(); //ignore Change Log header
                while (sr.Peek() >= 0)
                {
                    changeLog.Add(sr.ReadLine());
                }
            }
            catch (Exception ex)
            {
                if (showUpdateMessage)
                {
                    MessageBox.Show("Error parsing update file:\n" + ex.Message, AppName, MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
                sr.Dispose();
                return;
            }
            sr.Dispose();
            Tools.DeleteFile(path);
            if (thisVersion.Equals(newVersion))
            {
                if (showUpdateMessage)
                {
                    MessageBox.Show("You have the latest version", AppName, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                return;
            }
            var newInt = Convert.ToInt16(newVersion.Replace("v", "").Replace(".", "").Trim());
            var thisInt = Convert.ToInt16(thisVersion.Replace("v", "").Replace(".", "").Trim());
            if (newInt <= thisInt)
            {
                if (showUpdateMessage)
                {
                    MessageBox.Show(
                        "You have a newer version (" + thisVersion + ") than what's on the server (" + newVersion +
                        ")\nNo update needed!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }
            var updaterForm = new Updater();
            updaterForm.SetInfo(AppName, thisVersion, newName, newVersion, releaseDate, link, changeLog);
            updaterForm.ShowDialog();
        }

        private void playAlongMode_CheckedChanged(object sender, EventArgs e)
        {
            var visible = playAlongMode.Checked;
            lblTrackVolume.Visible = visible;
            silenceDrumsTrack.Enabled = visible;
            showChartSelection.Enabled = visible;
            autoPlayWithChart.Enabled = visible;
            if (!playAlongMode.Checked)
            {
                panelCharts.Visible = false;
            }
        }

        private void lblTrackVolume_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            var Volume = new Volume(this, Cursor.Position, true);
            Volume.Show();
        }

        private void lblDrumVolume_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            var Volume = new Volume(this, Cursor.Position);
            Volume.Show();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (customizeLayout.Checked)
            {
                MessageBox.Show("Can't do that while customizing the layout", AppName, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }
            var ofd = new OpenFileDialog
            {
                Title = "Open song file",
                InitialDirectory = Environment.CurrentDirectory
            };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            if (string.IsNullOrEmpty(ofd.FileName) || !File.Exists(ofd.FileName)) return;
            if (VariousFunctions.ReadFileType(ofd.FileName) != XboxFileType.STFS) return;
            Environment.CurrentDirectory = Path.GetDirectoryName(ofd.FileName);
            SongFile = ofd.FileName;
            picWorking.Visible = true;
            songPreparer.RunWorkerAsync();
        }

        private void loadCON()
        {
            StopEverything();
            SongArtist = "";
            SongTitle = "";
            SongLength = "";
            SongLengthDouble = 0.0;
            if (string.IsNullOrEmpty(SongFile) || !File.Exists(SongFile)) return;
            Tools.DeleteFile(TempMIDI);
            if (!Parser.ExtractDTA(SongFile))
            {
                MessageBox.Show("Something went wrong extracting the songs.dta file, can't play that song", AppName,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Parser.ReadDTA(Parser.DTA) || !Parser.Songs.Any())
            {
                MessageBox.Show("Something went wrong reading the songs.dta file, can't play that song", AppName,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Parser.Songs.Count > 1)
            {
                MessageBox.Show("It looks like this is a pack but I can only work with single songs\nUse Quick Pack Editor in C3 CON Tools to split your pack into individual files",
                    AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            var xPackage = new STFSPackage(SongFile);
            if (!xPackage.ParseSuccess)
            {
                MessageBox.Show("There was an error parsing that song file, can't play that song", AppName,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            byte[] xMogg;
            var internal_name = Parser.Songs[0].InternalName;
            try
            {
                var xFile = xPackage.GetFile("songs/" + internal_name + "/" + internal_name + ".mid");
                if (xFile == null || !xFile.Extract(TempMIDI))
                {
                    MessageBox.Show("There was an error extracting the MIDI file, can't play that song", AppName,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    xPackage.CloseIO();
                    return;
                }
                xFile = xPackage.GetFile("songs/" + internal_name + "/" + internal_name + ".mogg");
                if (xFile == null)
                {
                    MessageBox.Show("There was an error extracting the audio file, can't play that song", AppName,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    xPackage.CloseIO();
                    return;
                }
                xMogg = xFile.Extract();
            }
            catch (Exception)
            {
                MessageBox.Show("There was an error parsing that song file, can't play that song", AppName,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                xPackage.CloseIO();
                return;
            }
            xPackage.CloseIO();
            if (xMogg == null || xMogg.Length == 0)
            {
                MessageBox.Show("There was an error extracting the audio file, can't play that song", AppName,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                xPackage.CloseIO();
                return;
            }
            if (!Tools.DecM(xMogg, true, false, false, DecryptMode.ToMemory))
            {
                MessageBox.Show("That song is encrypted and I can't play it", AppName, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            loadMIDI();
            SongArtist = Parser.Songs[0].Artist;
            SongTitle = Parser.Songs[0].Name;
            SongLengthDouble = ProcessMogg();
            SongLength = Parser.GetSongDuration(SongLengthDouble/1000.0);
        }

        private long ProcessMogg()
        {
            try
            {
                var stream = Bass.BASS_StreamCreateFile(Tools.GetOggStreamIntPtr(false), 0L, Tools.PlayingSongOggData.Length,
                    BASSFlag.BASS_STREAM_DECODE | BASSFlag.BASS_SAMPLE_FLOAT);
                var len = Bass.BASS_ChannelGetLength(stream);
                var totaltime = Bass.BASS_ChannelBytes2Seconds(stream, len); // the total time length
                return (int) (totaltime*1000);
            }
            catch (Exception)
            {
            }
            return 0;
        }

        private void songPreparer_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            panelCharts.Invoke(new MethodInvoker(() => panelCharts.Visible = false));
            cboCharts.Invoke(new MethodInvoker(() => cboCharts.Items.Clear()));
            loadCON();
        }

        private void songPreparer_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            picWorking.Visible = false;
            if (string.IsNullOrEmpty(SongTitle) || string.IsNullOrEmpty(SongArtist)) return;
            panelCharts.Visible = showChartSelection.Checked;
            cboCharts.Items.Add("Expert (" + MIDITools.MIDI_Chart.DrumsX.ChartedNotes.Count + " notes)");
            cboCharts.Items.Add("Hard (" + MIDITools.MIDI_Chart.DrumsH.ChartedNotes.Count + " notes)");
            cboCharts.Items.Add("Medium (" + MIDITools.MIDI_Chart.DrumsM.ChartedNotes.Count + " notes)");
            cboCharts.Items.Add("Easy (" + MIDITools.MIDI_Chart.DrumsE.ChartedNotes.Count + " notes)");
            cboCharts.SelectedIndex = 0;
            picTrack.Focus();
            lblStatus.Text = "[Ready] \"" + SongTitle + "\" by " + SongArtist;
            lblTime.Text = "0:00 / " + SongLength;
            btnPlay.Enabled = true;
        }

        private void loadMIDI()
        {
            if (!File.Exists(TempMIDI)) return;
            MIDITools.Initialize();
            if (!MIDITools.ReadMIDIFile(TempMIDI))
            {
                MessageBox.Show("Couldn't read that MIDI file, won't be able to play the drum chart", AppName,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Tools.DeleteFile(TempMIDI);
                StopEverything();
                return;
            }
            Tools.DeleteFile(TempMIDI);
            if (MIDITools.MIDI_Chart.DrumsX.ChartedNotes.Count != 0)
            {
                MetronomeTempo = (int) MIDITools.MIDI_Chart.AverageBPM;
                UpdateMetronome();
                return;
            }
            MessageBox.Show("That song doesn't have a drums chart, nothing to play", AppName, MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            StopEverything();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            try
            {
                switch (Bass.BASS_ChannelIsActive(BassMixer))
                {
                    case BASSActive.BASS_ACTIVE_PLAYING:
                        StopPlayback(true);
                        UpdateTime();
                        StopStageKit();
                        break;
                    case BASSActive.BASS_ACTIVE_PAUSED:
                        Bass.BASS_ChannelPlay(BassMixer, false);
                        PlaybackTimer.Enabled = true;
                        btnPlay.Text = "Pause";
                        toolTip1.SetToolTip(btnPlay, "Pause");
                        lblStatus.Text = "[Playing] \"" + SongTitle + "\" by " + SongArtist;
                        break;
                    default:
                        StartPlayback();
                        break;
                }
            }
            catch (Exception)
            {
                StartPlayback();
            }
            btnStop.Enabled = true;
        }

        private void DoPracticeSessions()
        {
            if (!MIDITools.PracticeSessions.Any())
            {
                lblSection.Text = "";
                return;
            }
            lblSection.Text = GetCurrentSection(GetCorrectedTime());
        }

        private string GetCurrentSection(double time)
        {
            var curr_session = "";
            foreach (var session in MIDITools.PracticeSessions.TakeWhile(session => session.SectionStart <= time))
            {
                curr_session = session.SectionName;
            }
            return curr_session;
        }

        private double GetCorrectedTime()
        {
            return PlaybackSeconds - ((double) BassBuffer/1000);
        }

        private void PlaybackTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Bass.BASS_ChannelIsActive(BassMixer) == BASSActive.BASS_ACTIVE_PLAYING)
                {
                    // the stream is still playing...
                    var pos = Bass.BASS_ChannelGetPosition(BassStream); // position in bytes
                    PlaybackSeconds = Bass.BASS_ChannelBytes2Seconds(BassStream, pos); // the elapsed time length
                    DrawVisuals();
                    UpdateTime();
                    DoPracticeSessions();
                }
                else
                {
                    StopPlayback();
                }
            }
            catch (Exception)
            {
            }
        }

        private void StartPlayback()
        {
            if (Tools.PlayingSongOggData.Count() == 0)
            {
                MessageBox.Show("Couldn't play that song, sorry", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                StopPlayback();
                return;
            }

            // create a decoder for the OGG file
            BassStream = Bass.BASS_StreamCreateFile(Tools.GetOggStreamIntPtr(false), 0L, Tools.PlayingSongOggData.Length,
                BASSFlag.BASS_STREAM_DECODE | BASSFlag.BASS_SAMPLE_FLOAT);
            var channel_info = Bass.BASS_ChannelGetInfo(BassStream);

            // create a stereo mixer with same frequency rate as the input file
            BassMixer = BassMix.BASS_Mixer_StreamCreate(channel_info.freq, 2, BASSFlag.BASS_MIXER_END);
            BassMix.BASS_Mixer_StreamAddChannel(BassMixer, BassStream, BASSFlag.BASS_MIXER_MATRIX);

            //get and apply channel matrix
            var matrix = Tools.GetChannelMatrix(Parser.Songs[0], channel_info.chans,
                (silenceDrumsTrack.Checked ? "" : "drums|") + "bass|guitar|vocals|keys|backing");
            BassMix.BASS_Mixer_ChannelSetMatrix(BassStream, matrix);

            //set location
            BassMix.BASS_Mixer_ChannelSetPosition(BassStream,
                Bass.BASS_ChannelSeconds2Bytes(BassStream, PlaybackSeconds));

            //apply volume correction to entire track
            Bass.BASS_ChannelSetAttribute(BassMixer, BASSAttribute.BASS_ATTRIB_VOL, (float) TrackVolume);

            //start mix playback
            Bass.BASS_ChannelPlay(BassMixer, true);

            PlaybackTimer.Enabled = true;
            btnPlay.Text = "Pause";
            toolTip1.SetToolTip(btnPlay, "Pause");
            lblStatus.Text = "[Playing] \"" + SongTitle + "\" by " + SongArtist;
        }

        private void StopPlayback(bool Pause = false)
        {
            try
            {
                PlaybackTimer.Enabled = false;
                if (Pause)
                {
                    if (!Bass.BASS_ChannelPause(BassMixer))
                    {
                        MessageBox.Show("Error pausing playback\n" + Bass.BASS_ErrorGetCode());
                    }
                    lblStatus.Invoke(
                        new MethodInvoker(() => lblStatus.Text = "[Paused] \"" + SongTitle + "\" by " + SongArtist));
                }
                else
                {
                    StopBASS();
                    PlaybackSeconds = 0;
                    lblStatus.Invoke(
                        new MethodInvoker(() => lblStatus.Text = "[Ready] \"" + SongTitle + "\" by " + SongArtist));
                    lblSection.Invoke(new MethodInvoker(() => lblSection.Text = ""));
                    for (var i = 0; i < DRUM_PARTS; i++)
                    {
                        DrumKitComponents[i].Image = null;
                        STATE_INPUT[i] = false;
                    }
                    ResetPlayedNotes();
                }
            }
            catch (Exception)
            {
            }
            btnPlay.Invoke(new MethodInvoker(() => btnPlay.Text = "Play"));
            btnPlay.BeginInvoke(new Action(() => toolTip1.SetToolTip(btnPlay, "Play")));
        }

        private void StopBASS()
        {
            try
            {
                Bass.BASS_ChannelStop(BassMixer);
                Bass.BASS_StreamFree(BassMixer);
            }
            catch (Exception)
            {
            }
        }

        private void UpdateTime()
        {
            if (string.IsNullOrEmpty(SongLength))
            {
                lblTime.Invoke(new MethodInvoker(() => lblTime.Text = ""));
                return;
            }
            string time;
            if (PlaybackSeconds >= 3600)
            {
                var hours = (int) (PlaybackSeconds/3600);
                var minutes = (int) (PlaybackSeconds - (hours*3600));
                var seconds = (int) (PlaybackSeconds - (minutes*60));
                time = hours + ":" + (minutes < 10 ? "0" : "") + minutes + ":" + (seconds < 10 ? "0" : "") + seconds;
            }
            else if (PlaybackSeconds >= 60)
            {
                var minutes = (int) (PlaybackSeconds/60);
                var seconds = (int) (PlaybackSeconds - (minutes*60));
                time = minutes + ":" + (seconds < 10 ? "0" : "") + seconds;
            }
            else
            {
                time = "0:" + (PlaybackSeconds < 10 ? "0" : "") + (int) PlaybackSeconds;
            }
            if (lblTime.InvokeRequired)
            {
                lblTime.Invoke(new MethodInvoker(() => lblTime.Text = time + " / " + SongLength));
            }
            else
            {
                lblTime.Text = lblTime.Text = time + " / " + SongLength;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopEverything();
        }

        private void StopEverything()
        {
            StopPlayback();
            StopStageKit();
            Tools.ReleaseStreamHandle(false);
            Tools.PlayingSongOggData = new byte[0];
            PlaybackSeconds = 0;
            SongLengthDouble = 0.0;
            SongLength = "";
            SongArtist = "";
            SongTitle = "";
            lblSection.Invoke(new MethodInvoker(() => lblSection.Text = ""));
            UpdateTime();
            picTrack.Invalidate();
            btnStop.Invoke(new MethodInvoker(() => btnStop.Enabled = false));
            btnPlay.Invoke(new MethodInvoker(() => btnPlay.Enabled = false));
            lblStatus.Invoke(new MethodInvoker(() => lblStatus.Text = NothingLoaded));
            MIDITools.Initialize();
        }

        private void silenceDrumsTrack_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SongArtist) || string.IsNullOrEmpty(SongTitle)) return;
            var wasPlaying = PlaybackTimer.Enabled;
            var time = PlaybackSeconds;
            StopPlayback();
            if (!wasPlaying) return;
            PlaybackSeconds = time;
            StartPlayback();
        }

        private void HandleDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
        }

        private void HandleDragDrop(object sender, DragEventArgs e)
        {
            if (customizeLayout.Checked)
            {
                MessageBox.Show("Can't do that while customizing the layout", AppName, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }
            var files = (string[]) e.Data.GetData(DataFormats.FileDrop);
            Environment.CurrentDirectory = Path.GetDirectoryName(files[0]);
            if (VariousFunctions.ReadFileType(files[0]) != XboxFileType.STFS)
            {
                MessageBox.Show("That's not a valid file to drop here", AppName, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }
            if (!playAlongMode.Checked)
            {
                playAlongMode.Checked = true;
                playAlongMode_Click(null, null);
            }
            SongFile = files[0];
            picWorking.Visible = true;
            songPreparer.RunWorkerAsync();
        }

        private void checkForUpdates_Click(object sender, EventArgs e)
        {
            showUpdateMessage = true;
            updater.RunWorkerAsync();
        }

        private void howToUse_Click(object sender, EventArgs e)
        {
            var message = Tools.ReadHelpFile("drum");
            var help = new HelpForm(AppName + " - Help", message, true);
            help.ShowDialog();
        }

        private void NotifyTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Show();
                WindowState = FormWindowState.Normal;
            }
            else
            {
                WindowState = FormWindowState.Minimized;
            }
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized) return;
            NotifyTray.ShowBalloonTip(250);
            Hide();
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            var pressed = e.KeyCode;
            if (pressed == KEY_SNARE && !STATE_INPUT[Drumkit.Snare])
            {
                PlaySample(Drumkit.Snare);
            }
            else if (pressed == KEY_FLAM && !STATE_INPUT[Drumkit.Flam])
            {
                PlaySample(Drumkit.Flam);
            }
            else if (pressed == KEY_TOM_Y && !STATE_INPUT[Drumkit.TomYellow] && !pS3GH5Kit.Checked)
            {
                PlaySample(Drumkit.TomYellow);
            }
            else if (pressed == KEY_TOM_B && !STATE_INPUT[Drumkit.TomBlue])
            {
                PlaySample(Drumkit.TomBlue);
            }
            else if (pressed == KEY_TOM_G && !STATE_INPUT[Drumkit.TomGreen])
            {
                PlaySample(Drumkit.TomGreen);
            }
            else if (pressed == KEY_HIHAT)
            {
                if ((forceClosedHihat.Checked || (!doubleBassPedal.Checked && STATE_INPUT[Drumkit.HiHatPedal])) &&
                    !STATE_INPUT[Drumkit.HiHatClosed])
                {
                    PlaySample(Drumkit.HiHatClosed);
                }
                else if ((!forceClosedHihat.Checked && (doubleBassPedal.Checked || !STATE_INPUT[Drumkit.HiHatPedal]) &&
                          !STATE_INPUT[Drumkit.HiHatOpen]))
                {
                    PlaySample(Drumkit.HiHatOpen);
                }
            }
            else if (pressed == KEY_RIDE && !STATE_INPUT[Drumkit.Ride] && !pS3GH5Kit.Checked)
            {
                PlaySample(Drumkit.Ride);
            }
            else if (pressed == KEY_CRASH && !STATE_INPUT[Drumkit.Crash])
            {
                PlaySample(Drumkit.Crash);
            }
            else if (pressed == KEY_KICK_PEDAL && !STATE_INPUT[Drumkit.KickPedal])
            {
                PlaySample(Drumkit.KickPedal);
            }
            else if (pressed == KEY_HIHAT_PEDAL && !STATE_INPUT[Drumkit.HiHatPedal] && !pS3GH5Kit.Checked)
            {
                PlaySample(Drumkit.HiHatPedal);
            }
            else if (pressed == KEY_NEXT_KIT)
            {
                ChangeKit(false);
            }
            else if (pressed == KEY_PREV_KIT)
            {
                ChangeKit(true);
            }
            else if (pressed == KEY_VOLUME_UP)
            {
                UpdateDrumVolume(false);
            }
            else if (pressed == KEY_VOLUME_DOWN)
            {
                UpdateDrumVolume(true);
            }
        }

        private void frmMain_KeyUp(object sender, KeyEventArgs e)
        {
            var index = -1;
            var released = e.KeyCode;
            if (released == KEY_SNARE)
            {
                index = Drumkit.Snare;
            }
            else if (released == KEY_FLAM)
            {
                index = Drumkit.Flam;
            }
            else if (released == KEY_TOM_Y && !pS3GH5Kit.Checked)
            {
                index = Drumkit.TomYellow;
            }
            else if (released == KEY_TOM_B)
            {
                index = Drumkit.TomBlue;
            }
            else if (released == KEY_TOM_G)
            {
                index = Drumkit.TomGreen;
            }
            else if (released == KEY_HIHAT)
            {
                STATE_INPUT[Drumkit.HiHatClosed] = false;
                UpdateDrumHits(Drumkit.HiHatClosed);
                index = Drumkit.HiHatOpen;
            }
            else if (released == KEY_RIDE && !pS3GH5Kit.Checked)
            {
                index = Drumkit.Ride;
            }
            else if (released == KEY_CRASH)
            {
                index = Drumkit.Crash;
            }
            else if (released == KEY_KICK_PEDAL)
            {
                index = Drumkit.KickPedal;
            }
            else if (released == KEY_HIHAT_PEDAL && !pS3GH5Kit.Checked)
            {
                index = Drumkit.HiHatPedal;
            }
            if (index == -1) return;
            STATE_INPUT[index] = false;
            UpdateDrumHits(index);
        }

        private void frmMain_MouseClick(object sender, MouseEventArgs e)
        {
            ActiveControl = null;
            picTrack.Focus();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void minimizeToTray_Click(object sender, EventArgs e)
        {
            NotifyTray_MouseDoubleClick(sender, null);
        }

        private sealed class DarkRenderer : ToolStripProfessionalRenderer
        {
            public DarkRenderer() : base(new DarkColors())
            {
            }
        }

        private sealed class DarkColors : ProfessionalColorTable
        {
            public override Color MenuItemSelected
            {
                get { return mMenuHighlight; }
            }

            public override Color MenuItemSelectedGradientBegin
            {
                get { return mMenuHighlight; }
            }

            public override Color MenuItemSelectedGradientEnd
            {
                get { return mMenuHighlight; }
            }

            public override Color MenuBorder
            {
                get { return mMenuBorder; }
            }

            public override Color MenuItemBorder
            {
                get { return mMenuBorder; }
            }

            public override Color MenuItemPressedGradientBegin
            {
                get { return mMenuHighlight; }
            }

            public override Color MenuItemPressedGradientEnd
            {
                get { return mMenuHighlight; }
            }

            public override Color MenuItemPressedGradientMiddle
            {
                get { return mMenuHighlight; }
            }

            public override Color CheckBackground
            {
                get { return mMenuHighlight; }
            }

            public override Color CheckPressedBackground
            {
                get { return mMenuHighlight; }
            }

            public override Color CheckSelectedBackground
            {
                get { return mMenuHighlight; }
            }

            public override Color ButtonSelectedBorder
            {
                get { return mMenuHighlight; }
            }

            public override Color SeparatorDark
            {
                get { return mMenuText; }
            }

            public override Color SeparatorLight
            {
                get { return mMenuText; }
            }

            public override Color ImageMarginGradientBegin
            {
                get { return mMenuBackground; }
            }

            public override Color ImageMarginGradientEnd
            {
                get { return mMenuBackground; }
            }

            public override Color ImageMarginGradientMiddle
            {
                get { return mMenuBackground; }
            }

            public override Color ToolStripDropDownBackground
            {
                get { return mMenuBackground; }
            }
        }

        private void DrawVisuals()
        {
            if (MIDITools.MIDI_Chart == null || MIDITools.MIDI_Chart.DrumsX.ChartedNotes.Count == 0)
            {
                return;
            }
            var isSolo = MIDITools.MIDI_Chart.DrumsX.Solos != null &&
                         MIDITools.MIDI_Chart.DrumsX.Solos.Any(
                             solo => solo.MarkerBegin <= PlaybackSeconds && solo.MarkerEnd > PlaybackSeconds);
            if (isSolo && picTrack.Image != RESOURCE_TRACK_SOLO)
            {
                picTrack.Image = RESOURCE_TRACK_SOLO;
            }
            else if (!isSolo && picTrack.Image != RESOURCE_TRACK)
            {
                picTrack.Image = RESOURCE_TRACK;
            }
            picTrack.Invalidate();
        }

        private void UpdateNoteState(int note, bool isTom)
        {
            var index = -1;
            switch (note)
            {
                case 100:
                case 88:
                case 76:
                case 64:
                    index = isTom ? Drumkit.TomGreen : Drumkit.Crash;
                    break;
                case 99:
                case 87:
                case 75:
                case 63:
                    index = isTom ? Drumkit.TomBlue : (pS3GH5Kit.Checked ? Drumkit.Crash : Drumkit.Ride);
                    break;
                case 98:
                case 86:
                case 74:
                case 62:
                    index = isTom ? (pS3GH5Kit.Checked ? Drumkit.TomBlue : Drumkit.TomYellow) : Drumkit.HiHatOpen;
                    break;
                case 97:
                case 85:
                case 73:
                case 61:
                    index = Drumkit.Snare;
                    break;
                case 96:
                case 84:
                case 72:
                case 60:
                    index = Drumkit.KickPedal;
                    break;
            }
            if (index == -1) return;
            DrumKitComponents[index].Image = null;
            UpdateStageKitLights(index, false);
        }

        private void PlayNoteStream(int note, bool isTom)
        {
            var index = -1;
            switch (note)
            {
                case 100:
                case 88:
                case 76:
                case 64:
                    index = isTom ? Drumkit.TomGreen : Drumkit.Crash;
                    break;
                case 99:
                case 87:
                case 75:
                case 63:
                    index = isTom ? Drumkit.TomBlue : (pS3GH5Kit.Checked ? Drumkit.Crash : Drumkit.Ride);
                    break;
                case 98:
                case 86:
                case 74:
                case 62:
                    index = isTom ? (pS3GH5Kit.Checked ? Drumkit.TomBlue : Drumkit.TomYellow) : Drumkit.HiHatOpen;
                    break;
                case 97:
                case 85:
                case 73:
                case 61:
                    index = Drumkit.Snare;
                    break;
                case 96:
                case 84:
                case 72:
                case 60:
                    index = Drumkit.KickPedal;
                    break;
            }
            if (index == -1) return;
            PlaySample(index, 100);
        }

        private void DrawFills(Graphics graphics)
        {
            if (MIDITools.MIDI_Chart.DrumsX.Fills.Count == 0 && MIDITools.MIDI_Chart.DrumsX.Overdrive.Count == 0)
                return;
            var correctedTime = GetCorrectedTime();
            var fillColor = Color.FromArgb(127, ChartGreen.R, ChartGreen.G, ChartGreen.B);
            foreach (var fill in MIDITools.MIDI_Chart.DrumsX.Fills)
            {
                if (fill.MarkerEnd <= correctedTime) continue;
                if (fill.MarkerBegin > correctedTime + PlaybackWindow) break;
                DrawFill(graphics, fill, correctedTime, fillColor);
                break;
            }
            foreach (var OD in MIDITools.MIDI_Chart.DrumsX.Overdrive)
            {
                if (OD.MarkerEnd <= correctedTime) continue;
                if (OD.MarkerBegin > correctedTime + PlaybackWindow) break;
                fillColor = Color.FromArgb(127, 255, 255, 255);
                DrawFill(graphics, OD, correctedTime, fillColor);
                break;
            }
        }

        private void DrawFill(Graphics graphics, SpecialMarker marker, double correctedTime, Color fillColor)
        {
            var percent = 1.0 - ((marker.MarkerBegin - correctedTime)/PlaybackWindow);
            var posBottom = (styleRockBand.Checked ? ChartStart : 0) +
                            ((styleRockBand.Checked ? ChartGoal - ChartStart : ChartGoal)*percent);
            var height = ((marker.MarkerEnd - marker.MarkerBegin)/PlaybackWindow)*
                         (styleVerticalScroll.Checked ? ChartGoal : ChartGoal - ChartStart);
            var posTop = posBottom - height;
            double leftTop = NOTE_SPACER;
            double leftBottom = NOTE_SPACER;
            double widthTop = picTrack.Width - (NOTE_SPACER*2);
            var widthBottom = widthTop;
            if (styleRockBand.Checked)
            {
                var minTop = styleRockBand.Checked ? ChartStart : 0;
                if (posTop < minTop)
                {
                    posTop = minTop;
                }
                var percent2 = 1.0 - ((marker.MarkerEnd - correctedTime)/PlaybackWindow);
                leftBottom = ChartLeft - (ChartLeft*percent);
                leftTop = ChartLeft - (ChartLeft*percent2);
                if (leftTop > ChartLeft)
                {
                    leftTop = ChartLeft;
                }
                widthTop = picTrack.Width - (leftTop*2);
                widthBottom = picTrack.Width - (leftBottom*2);
            }
            var shape = new[]
            {
                new Point((int) leftTop, (int) posTop), new Point((int) (leftTop + widthTop), (int) posTop),
                new Point((int) (leftBottom + widthBottom), (int) posBottom),
                new Point((int) leftBottom, (int) posBottom)
            };
            using (var solidBrush = new SolidBrush(fillColor))
            {
                graphics.FillPolygon(solidBrush, shape);
            }
        }

        private void DrawNotes(Graphics graphics, bool doKicks)
        {
            if (ActiveChart.ChartedNotes.Count == 0) return;
            var track = ActiveChart;
            var correctedTime = GetCorrectedTime();
            for (var z = 0; z < track.ChartedNotes.Count(); z++)
            {
                var note = track.ChartedNotes[z];
                if (note.NoteEnd <= correctedTime)
                {
                    if (autoPlayWithChart.Checked && note.NoteEnd + 0.5 <= correctedTime && note.Played && !note.Stopped)
                    {
                        UpdateNoteState(note.NoteNumber, note.isTom);
                        note.Stopped = true;
                    }
                    continue;
                }
                if (note.NoteStart > correctedTime + PlaybackWindow) break;
                if (note.NoteColor == Color.Empty)
                {
                    note.NoteColor = GetNoteColor(note.NoteNumber);
                }
                if (note.NoteColor == ChartOrange && !doKicks) continue;
                if (note.NoteColor != ChartOrange && doKicks) continue;

                //play along
                if (autoPlayWithChart.Checked && note.NoteStart <= correctedTime && !note.Played)
                {
                    PlayNoteStream(note.NoteNumber, note.isTom);
                    note.Played = true;
                }

                var percent = 1.0 - ((note.NoteStart - correctedTime)/PlaybackWindow);
                var posY = (styleRockBand.Checked ? ChartStart : 0) +
                           ((styleRockBand.Checked ? ChartGoal - ChartStart : ChartGoal)*percent);
                var minLeft = styleRockBand.Checked ? ChartLeft - (ChartLeft*percent) : NOTE_SPACER;
                var img = note.hasOD ? RESOURCE_TOM_OD : RESOURCE_SNARE;
                var noteLocation = 0;
                if (note.NoteColor == ChartYellow)
                {
                    noteLocation = 1;
                    img = note.isTom || radioDrums.Checked
                        ? (note.hasOD ? RESOURCE_TOM_OD : RESOURCE_TOM_Y)
                        : (note.hasOD ? RESOURCE_CYMBAL_OD : RESOURCE_CYMBAL_Y);
                }
                else if (note.NoteColor == ChartBlue)
                {
                    noteLocation = 2;
                    img = note.isTom || radioDrums.Checked
                        ? (note.hasOD ? RESOURCE_TOM_OD : RESOURCE_TOM_B)
                        : (note.hasOD ? RESOURCE_CYMBAL_OD : RESOURCE_CYMBAL_B);
                }
                else if (note.NoteColor == ChartGreen)
                {
                    noteLocation = 3;
                    img = note.isTom || radioDrums.Checked
                        ? (note.hasOD ? RESOURCE_TOM_OD : RESOURCE_TOM_G)
                        : (note.hasOD ? RESOURCE_CYMBAL_OD : RESOURCE_CYMBAL_G);
                }
                var note_width = (picTrack.Width - (minLeft*2.0))/4.0;
                if (styleRockBand.Checked && note_width > img.Width*0.85)
                {
                    note_width = img.Width*0.85;
                }
                var note_height = styleRockBand.Checked ? img.Height*(note_width/img.Width) : img.Height;
                var lane_width = (picTrack.Width - (minLeft*2))/4;
                var posX = minLeft + (lane_width*noteLocation) + ((lane_width - note_width)/2);
                if (note.NoteColor == ChartOrange)
                {
                    using (var solidBrush = new SolidBrush(note.hasOD ? Color.WhiteSmoke : note.NoteColor))
                    {
                        var kick_width = picTrack.Width - (minLeft*2);
                        var kick_height = styleRockBand.Checked ? KICK_HEIGHT*0.65 : KICK_HEIGHT;
                        graphics.FillRectangle(solidBrush, Round(minLeft), Round(posY - (kick_height/2)),
                            Round(kick_width), Round(kick_height));
                    }
                }
                else
                {
                    graphics.DrawImage(img, Round(posX), Round(posY - (note_height/2)), Round(note_width),
                        Round(note_height));
                }
            }
        }

        private static int Round(double value)
        {
            return (int) (value + 0.5);
        }

        private Color GetNoteColor(int note_number)
        {
            Color color;
            switch (note_number)
            {
                case 96:
                case 84:
                case 72:
                case 60:
                    color = ChartOrange;
                    break;
                case 97:
                case 85:
                case 73:
                case 61:
                    color = ChartRed;
                    break;
                case 110:
                case 98:
                case 86:
                case 74:
                case 62:
                    color = ChartYellow;
                    break;
                case 111:
                case 99:
                case 87:
                case 75:
                case 63:
                    color = ChartBlue;
                    break;
                case 112:
                case 100:
                case 88:
                case 76:
                case 64:
                    color = ChartGreen;
                    break;
                default:
                    color = Color.FromArgb(byte.MaxValue, byte.MaxValue, byte.MaxValue);
                    break;
            }
            return color;
        }

        private void showMetronome_Click(object sender, EventArgs e)
        {
            panelMetronome.Visible = showMetronome.Checked;
            if (!showMetronome.Checked)
            {
                Metronome.Enabled = false;
            }
        }

        private void MetronomeOn_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || Metronome.Enabled) return;
            Metronome.Enabled = true;
        }

        private void MetronomeOff_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || !Metronome.Enabled) return;
            Metronome.Enabled = false;
        }

        private void MetronomeDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            MetronomeTempo = ((MetronomeTempo - 10))/10*10;
            UpdateMetronome();
        }

        private void UpdateMetronome()
        {
            if (MetronomeTempo < 10)
            {
                MetronomeTempo = 10;
            }
            else if (MetronomeTempo > 500)
            {
                MetronomeTempo = 500;
            }
            Metronome.Interval = 60000/MetronomeTempo;
            lblTempo.Invoke(
                new MethodInvoker(() => lblTempo.Text = MetronomeTempo.ToString(CultureInfo.InvariantCulture)));
        }

        private void MetronomeUp_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            MetronomeTempo = ((MetronomeTempo + 10))/10*10;
            UpdateMetronome();
        }

        private void Metronome_Tick(object sender, EventArgs e)
        {
            if (STREAM_METRONOME == 0) return;
            if (MetronomeCount == 4)
            {
                MetronomeCount = 1;
            }
            else
            {
                MetronomeCount++;
            }
            if (MetronomeCount == 1 && STREAM_METRONOME_BEAT != 0)
            {
                Bass.BASS_ChannelPlay(STREAM_METRONOME_BEAT, true);
            }
            else
            {
                Bass.BASS_ChannelPlay(STREAM_METRONOME, true);
            }
        }

        private void viewChangeLog_Click(object sender, EventArgs e)
        {
            const string changelog = "rokdrummer_changelog.txt";
            if (!File.Exists(Application.StartupPath + "\\" + changelog))
            {
                MessageBox.Show("Changelog file is missing!", AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Process.Start(Application.StartupPath + "\\" + changelog);
        }

        private void c3Forums_Click(object sender, EventArgs e)
        {
            Process.Start("http://customscreators.com/index.php?/topic/13196-rok-drummer-v150-91215-play-your-game-drums-on-pc/");
        }

        private void UpdateDrumKitSelection(ToolStripMenuItem item)
        {
            rockBand1.Checked = false;
            rockBand2.Checked = false;
            rockBand4.Checked = false;
            pS3EKit.Checked = false;
            pS3GH5Kit.Checked = false;
            pS4RB4.Checked = false;
            item.Checked = true;
            UpdateDrumKit();
            if (item != pS3EKit && item != pS3GH5Kit) return;
            SaveConfig();
            Application.Restart();
        }

        private void rockBand1_Click(object sender, EventArgs e)
        {
            UpdateDrumKitSelection((ToolStripMenuItem) sender);
        }

        private void picTrack_Paint(object sender, PaintEventArgs e)
        {
            if (PlaybackSeconds == 0) return;
            DrawFills(e.Graphics);
            DrawNotes(e.Graphics, true);
            DrawNotes(e.Graphics, false);
        }

        private void cboCharts_MouseLeave(object sender, EventArgs e)
        {
            picTrack.Focus();
        }

        private void picTrack_MouseClick(object sender, MouseEventArgs e)
        {
            picTrack.Focus();
        }

        private void cboCharts_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboCharts.SelectedIndex)
            {
                case 0:
                    ActiveChart = MIDITools.MIDI_Chart.DrumsX;
                    break;
                case 1:
                    ActiveChart = MIDITools.MIDI_Chart.DrumsH;
                    break;
                case 2:
                    ActiveChart = MIDITools.MIDI_Chart.DrumsM;
                    break;
                case 3:
                    ActiveChart = MIDITools.MIDI_Chart.DrumsE;
                    break;
                default:
                    ActiveChart = new MIDITrack();
                    break;
            }
            if (ActiveChart.ChartedNotes.Count > 0) return;
            MessageBox.Show("No notes to draw, select a different chart", AppName, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            cboCharts.SelectedIndex = 0;
        }

        private void ResetPlayedNotes()
        {
            foreach (var note in ActiveChart.ChartedNotes)
            {
                note.Played = false;
                note.Stopped = false;
            }
        }

        private void showChartSelection_Click(object sender, EventArgs e)
        {
            panelCharts.Visible = showChartSelection.Checked && playAlongMode.Checked;
        }

        private void autoPlayWithChart_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < DRUM_PARTS; i++)
            {
                DrumKitComponents[i].Image = null;
                STATE_INPUT[i] = false;
                LastInputState[i] = false;
                CurrentInputState[i] = false;
            }
        }

        private void customizeLayout_Click(object sender, EventArgs e)
        {
            ConnectionTimer.Enabled = !customizeLayout.Checked;
            StopEverything();
            if (customizeLayout.Checked)
            {
                DrumsTimer.Enabled = false;
            }
            panelMetronome.Visible = showMetronome.Checked || customizeLayout.Checked;
            for (var i = 0; i < DRUM_PARTS; i++)
            {
                DrumKitComponents[i].Image = customizeLayout.Checked ? DrumKitResources[i] : null;
                toolTip1.SetToolTip(DrumKitComponents[i],
                    customizeLayout.Checked ? "Click to reposition" : "Click to play");
            }
            toolTip1.SetToolTip(panelMetronome, customizeLayout.Checked ? "Click to reposition" : "");
        }

        private void picHiHat_MouseMove(object sender, MouseEventArgs e)
        {
            var box = (PictureBox) sender;
            if (!customizeLayout.Checked || box.Cursor != Cursors.NoMove2D) return;
            box.Left = PointToClient(MousePosition).X - (box.Width/2);
            box.Top = PointToClient(MousePosition).Y - (box.Height/2);
        }

        private void panelMetronome_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || !customizeLayout.Checked) return;
            var panel = (Panel) sender;
            panel.Cursor = Cursors.NoMove2D;
        }

        private void panelMetronome_MouseUp(object sender, MouseEventArgs e)
        {
            var panel = (Panel) sender;
            panel.Cursor = Cursors.Default;
        }

        private void panelMetronome_MouseMove(object sender, MouseEventArgs e)
        {
            var panel = (Panel) sender;
            if (!customizeLayout.Checked || panel.Cursor != Cursors.NoMove2D) return;
            panel.Left = PointToClient(MousePosition).X - (panel.Width/2);
            panel.Top = PointToClient(MousePosition).Y - (panel.Height/2);
        }

        private void resetLayout_Click(object sender, EventArgs e)
        {
            Tools.DeleteFile(LayoutFile);
            File.Copy(CurrentLayout + "layout.config", LayoutFile);
            LoadLayout();
        }

        private void doubleBassPedal_Click(object sender, EventArgs e)
        {
            var sound = ActiveDrumKit + (doubleBassPedal.Checked ? "kick" : "hihat_kick") + ".wav";
            if (!File.Exists(sound)) return;
            DrumKitStreams[Drumkit.HiHatPedal] = Bass.BASS_StreamCreateFile(sound, 0L, 0L, BASSFlag.BASS_SAMPLE_FLOAT);
        }

        private void ChangeLayout(string layout)
        {
            var file = LayoutPath + layout + "\\layout.config";
            if (!File.Exists(file)) return;
            Tools.DeleteFile(LayoutFile);
            File.Copy(file, LayoutFile);
            CurrentLayout = LayoutPath + layout + "\\";
            LoadLayoutImages();
            LoadLayout();
        }

        private void layoutIon_Click(object sender, EventArgs e)
        {
            CheckUncheckLayouts(sender);
            ChangeLayout("ion");
        }

        private void layoutRB1_Click(object sender, EventArgs e)
        {
            CheckUncheckLayouts(sender);
            ChangeLayout("rb1");
        }

        private void layoutRB2_Click(object sender, EventArgs e)
        {
            CheckUncheckLayouts(sender);
            ChangeLayout("rb2");
        }

        private void layoutCustom_Click(object sender, EventArgs e)
        {
            CheckUncheckLayouts(sender);
            ChangeLayout("custom");
        }

        private void layoutTron_Click(object sender, EventArgs e)
        {
            CheckUncheckLayouts(sender);
            ChangeLayout("tron");
        }

        private void CheckUncheckLayouts(object sender)
        {
            layoutIon.Checked = false;
            layoutRB1.Checked = false;
            layoutRB2.Checked = false;
            layoutCustom.Checked = false;
            layoutTron.Checked = false;
            layoutGH5.Checked = false;
            ((ToolStripMenuItem) sender).Checked = true;
        }

    private void styleVerticalScroll_Click(object sender, EventArgs e)
        {
            styleVerticalScroll.Checked = true;
            styleRockBand.Checked = false;
            UpdateTrackStyle();
        }

        private void styleRockBand_Click(object sender, EventArgs e)
        {
            styleVerticalScroll.Checked = false;
            styleRockBand.Checked = true;
            UpdateTrackStyle();
        }

        private void UpdateTrackStyle()
        {
            PlaybackWindow = styleVerticalScroll.Checked ? 2.0 : 1.5;//rock band style is a tiny bit faster
            var res = Application.StartupPath + "\\res\\";
            var track = styleRockBand.Checked ? "track2" : "track";
            RESOURCE_TRACK = (Bitmap)Tools.NemoLoadImage(res + track + ".jpg");
            RESOURCE_TRACK_SOLO = (Bitmap)Tools.NemoLoadImage(res + track + "_solo.jpg");
            picTrack.Image = RESOURCE_TRACK;
            panelHitBox.Visible = false;
        }

        private void layoutGH5_Click(object sender, EventArgs e)
        {
            CheckUncheckLayouts(sender);
            ChangeLayout("gh5");
        }

        private void debugDrumInput_Click(object sender, EventArgs e)
        {
            lblDebug.Visible = debugDrumInput.Checked && DrumsTimer.Enabled;
            lblDebug.Text = "DEBUG = ";
        }

        private void lblDebug_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            Clipboard.SetText(lblDebug.Text);
            MessageBox.Show("Debugging info copied to clipboard", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        private void UncheckAllStageKits()
        {
            stageKitPlayer1.Checked = false;
            stageKitPlayer2.Checked = false;
            stageKitPlayer3.Checked = false;
            stageKitPlayer4.Checked = false;
            stageKitDisabled.Checked = false;
        }

        private void stageKitPlayer1_Click(object sender, EventArgs e)
        {
            UncheckAllStageKits();
            stageKitPlayer1.Checked = true;
            if (player1.Checked)
            {
                player2_Click(sender, e);
            }
            SelectController(UserIndex.One, false);
        }

        private void stageKitPlayer2_Click(object sender, EventArgs e)
        {
            UncheckAllStageKits();
            stageKitPlayer2.Checked = true;
            if (player2.Checked)
            {
                player1_Click(sender, e);
            }
            SelectController(UserIndex.Two, false);
        }

        private void stageKitPlayer3_Click(object sender, EventArgs e)
        {
            UncheckAllStageKits();
            stageKitPlayer3.Checked = true;
            if (player3.Checked)
            {
                player1_Click(sender, e);
            }
            SelectController(UserIndex.Three, false);
        }

        private void stageKitPlayer4_Click(object sender, EventArgs e)
        {
            UncheckAllStageKits();
            stageKitPlayer4.Checked = true;
            if (player4.Checked)
            {
                player1_Click(sender, e);
            }
            SelectController(UserIndex.Four, false);
        }

        private void stageKitDisabled_Click(object sender, EventArgs e)
        {
            UncheckAllStageKits();
            stageKitDisabled.Checked = true;
            stageKitController = null;
            StopStageKit();
        }
    }

    public class DrumKit
    {
        public int Snare = 0;
        public int Flam = 1;
        public int TomYellow = 2;
        public int TomBlue = 3;
        public int TomGreen = 4;
        public int HiHatOpen = 5;
        public int HiHatClosed = 6;
        public int Ride = 7;
        public int Crash = 8;
        public int KickPedal = 9;
        public int HiHatPedal = 10;
    }
}
