using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Windows.Forms;
using System.Security.Permissions;


namespace SimpleROHookCS
{

    public partial class MainForm : Form,IDisposable
    {
        private SRHSharedData m_SharedData = null;
        private NPCLogger m_npcLogger;

        protected override CreateParams CreateParams
        {
            [SecurityPermission(SecurityAction.Demand,
                Flags = SecurityPermissionFlag.UnmanagedCode)]
            get
            {
                const int WS_EX_TOOLWINDOW = 0x80;
                const long WS_POPUP = 0x80000000L;
                const int WS_VISIBLE = 0x10000000;
                const int WS_SYSMENU = 0x80000;
                const int WS_MAXIMIZEBOX = 0x10000;

                CreateParams cp = base.CreateParams;
                cp.ExStyle = WS_EX_TOOLWINDOW;
                cp.Style = unchecked((int)WS_POPUP) |
                    WS_VISIBLE | WS_SYSMENU | WS_MAXIMIZEBOX;

                cp.Width = 0;
                cp.Height = 0;

                return cp;
            }
        }

        public MainForm()
        {
            InitializeComponent();
            m_SharedData = new SRHSharedData();
        }

        public new void Dispose()
        {
            m_SharedData.Dispose();
            Dispose(true);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TaskTray_notifyIcon.Visible = false;
            Application.Exit();
        }

        private void playMusicOnClientStreamPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string musicfilename = m_SharedData.musicfilename;
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Ragnarok Online BGM";
            openFileDialog.InitialDirectory = musicfilename;
            openFileDialog.CheckFileExists = true;
            openFileDialog.Multiselect = false;
            openFileDialog.FileName = musicfilename;
            openFileDialog.Filter = "mp3 file|*.mp3|wave file|*.wav";
            openFileDialog.ShowReadOnly = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                m_SharedData.musicfilename = openFileDialog.FileName;
                m_SharedData.executeorder = true;
            }
        }

        private void UpdateCheckMenu()
        {
            playMusicOnClientStreamPlayerToolStripMenuItem.Enabled
                = (m_SharedData.g_hROWindow == 0)? false : true;
            packetLogToolStripMenuItem.Checked
                = m_SharedData.write_packetlog;
            freeMouseToolStripMenuItem.Checked
                = m_SharedData.freemouse;

            ground_zbias_ToolStripTrackBar.Value
                = m_SharedData.ground_zbias;
            Set_ZBiasValue_groundZBiasToolStripMenuItem(m_SharedData.ground_zbias);

            alphaLeveltoolStripTrackBar.Value
                = m_SharedData.alphalevel;
            Set_AlphaLevelValue_alphaLeveltoolStripMenuItem(m_SharedData.alphalevel);

            showM2EToolStripMenuItem.Checked
                = m_SharedData.m2e;
            showBBEtoolStripMenuItem.Checked
                = m_SharedData.bbe;
            showDeadCelltoolStripMenuItem.Checked
                = m_SharedData.deadcell;
            showChatScopetoolStripMenuItem.Checked
                = m_SharedData.chatscope;
            showCastRangetoolStripMenuItem.Checked
                = m_SharedData.castrange;

            CPUCooler_toolStripTrackBar.Value =
                m_SharedData.cpucoolerlevel;
            Set_CPUCoolerText_toolStripMenuItem(m_SharedData.cpucoolerlevel);

            fixWindowModeVsyncWaitToolStripMenuItem.Checked
                = m_SharedData.fix_windowmode_vsyncwait;
            showFpsToolStripMenuItem.Checked
                = m_SharedData.show_framerate;
            showObjectInformationToolStripMenuItem.Checked
                = m_SharedData.objectinformation;
            kHzAudioModeonBootToolStripMenuItem.Checked
                = m_SharedData._44khz_audiomode;
            chainloadDinputdllasiForDinputfreeRagexesToolStripMenuItem.Checked
                = m_SharedData.chainload;

            nPCLoggerToolStripMenuItem.Checked = m_npcLogger.Visible;
        }


        private void kHzAudioModeonBootToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tsm = (ToolStripMenuItem)sender;
            m_SharedData._44khz_audiomode = tsm.Checked;
        }

        private void packetLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tsm = (ToolStripMenuItem)sender;
            m_SharedData.write_packetlog = tsm.Checked;
        }

        private void freeMouseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tsm = (ToolStripMenuItem)sender;
            m_SharedData.freemouse = tsm.Checked;
        }

        private void showBBEtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tsm = (ToolStripMenuItem)sender;
            m_SharedData.bbe = tsm.Checked;
        }
        private void showDeadCelltoolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tsm = (ToolStripMenuItem)sender;
            m_SharedData.deadcell = tsm.Checked;
        }
        private void showChatScopetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tsm = (ToolStripMenuItem)sender;
            m_SharedData.chatscope = tsm.Checked;
        }
        private void showCastRangetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tsm = (ToolStripMenuItem)sender;
            m_SharedData.castrange = tsm.Checked;
        }
        private void showM2EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tsm = (ToolStripMenuItem)sender;
            m_SharedData.m2e = tsm.Checked;
        }

        private void showFpsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tsm = (ToolStripMenuItem)sender;
            m_SharedData.show_framerate = tsm.Checked;
        }

        private void showObjectInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tsm = (ToolStripMenuItem)sender;
            m_SharedData.objectinformation = tsm.Checked;
        }

        private void fixWindowModeVsyncWaitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tsm = (ToolStripMenuItem)sender;
            m_SharedData.fix_windowmode_vsyncwait = tsm.Checked;
        }

        private void aboutSimpleROHookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutbox = new SRHAboutBox();
            aboutbox.ShowDialog();
        }

        private void TaskTray_contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            UpdateCheckMenu();
        }

        private void ground_zbias_ToolStripTrackBar_Update(object sender, EventArgs e)
        {
            ToolStripTrackBar tsTrackbar = (ToolStripTrackBar)sender;
            m_SharedData.ground_zbias = tsTrackbar.Value;

            Set_ZBiasValue_groundZBiasToolStripMenuItem(m_SharedData.ground_zbias);
        }
        private void Set_ZBiasValue_groundZBiasToolStripMenuItem(int value)
        {
            groundZBiasToolStripMenuItem.Text =
                String.Format("Ground Z Bias {0}", value);
        }

        private void CPUCooler_toolStripTrackBar_Update(object sender, EventArgs e)
        {
            ToolStripTrackBar tsTrackbar = (ToolStripTrackBar)sender;
            m_SharedData.cpucoolerlevel = tsTrackbar.Value;

            Set_CPUCoolerText_toolStripMenuItem(m_SharedData.cpucoolerlevel);
        }
        private void Set_CPUCoolerText_toolStripMenuItem(int value)
        {
            if (value==0)
            {
                CPUCoolerText_toolStripMenuItem.Text = "CPU Cooler OFF";
            }
            else
            {
                CPUCoolerText_toolStripMenuItem.Text =
                    String.Format("CPU Cooler Level {0}", value);
            }
        }

        private void alphaLeveltoolStripTrackBar_Update(object sender, EventArgs e)
        {
            ToolStripTrackBar tsTrackbar = (ToolStripTrackBar)sender;
            m_SharedData.alphalevel = tsTrackbar.Value;

            Set_AlphaLevelValue_alphaLeveltoolStripMenuItem(m_SharedData.alphalevel);
        }
        private void Set_AlphaLevelValue_alphaLeveltoolStripMenuItem(int value)
        {
            alphaLeveltoolStripMenuItem.Text =
                String.Format("Alpha Level {0}", value);
        }

        private void InitTaskTrayMenu()
        {
            ground_zbias_ToolStripTrackBar.SetMinMax(0, 16);
            ground_zbias_ToolStripTrackBar.SetTickFrequency(1);
            ground_zbias_ToolStripTrackBar.SetChangeValue(1, 4);

            alphaLeveltoolStripTrackBar.SetMinMax(0, 255);
            alphaLeveltoolStripTrackBar.SetTickFrequency(4);
            alphaLeveltoolStripTrackBar.SetChangeValue(1, 16);

            CPUCooler_toolStripTrackBar.SetMinMax(0, 3);
            CPUCooler_toolStripTrackBar.SetTickFrequency(1);
            CPUCooler_toolStripTrackBar.SetChangeValue(1, 4);
        }

        private void Window_Load(object sender, EventArgs e)
        {
            InitTaskTrayMenu();

            string curentdirstr = System.IO.Directory.GetCurrentDirectory() + "\\config.ini";
            m_SharedData.configfilepath = curentdirstr;
            if (!File.Exists("config.ini"))
            {
                using( StreamWriter w = new StreamWriter(@"config.ini") )
                {
                    #region config.ini
                    w.WriteLine(@"
[M2E]
; MiniMiniEffect Color Setting
; 0xAARRGGBB
; AA:alpha  00-FF (00:0% (invisible) ---7F:50% (half transparency) ---FF:100% (solid color)
; RR:red    00-FF (0-255)
; GG:green  00-FF (0-255)
; BB:blue   00-FF (0-255)
; 
; Skills with a * are Renewal Skills
; GD_XXXXX represents guild skills
; WE_XXXXX represents wedding skills
; NPC_XXXX represents NPC / monsters exclusive skills
;
; Legend:  SkillIDNumber (Hex) = Color

;NC_MAGMA_ERUPTION*
;RL_B_TRAP*
Skill0010=0x00000000

;MG_SAFETYWALL
;MH_STEINWAND*
;SO_ELEMENTAL_SHIELD*
Skill007E=0x7FF34AFF

;MG_FIREWALL
Skill007F=0x00000000

;AL_WARP
Skill0080=0x7FFFFFFF
Skill0081=0x7FFFFFFF

;PR_SANCTUARY
Skill0083=0x00000000

;PR_MAGNUS
Skill0084=0x00000000

;AL_PNEUMA (only center cell, the outer 3x3 cells uses 86's color definition)
Skill0085=0x7F99FF33


;This next one has so many skills associated to this ID is because it contains
;splash AoE effects which uses another ID to define the splash. Its on my TODO 
;to separate these if possible.
;AC_SHOWER
;CR_GRANDCROSS
;GN_CRAZYWEED_ATK*
;GS_DESPERADO
;KO_BAKURETSU*
;KO_HUUMARANKA*
;KO_MUCHANAGE*
;MA_SHOWER*
;MG_THUNDERSTORM
;MH_XENO_SLASHER*
;NJ_HUUMA
;NJ_RAIGEKISAI
;NPC_DARKGRANDCROSS
;SG_MOON_WARM
;SG_STAR_WARM
;SG_SUN_WARM
;SO_DIAMONDDUST*
;SO_EARTHGRAVE*
;WL_COMET*
;WZ_HEAVENDRIVE
;WZ_METEOR
;WZ_STORMGUST
;WZ_VERMILION
Skill0086=0x7F6912FF

;WZ_FIREPILLAR
Skill0087=0x7F888800
Skill0088=0x7F888800

;WZ_ICEWALL
Skill008D=0x7F0DFFEF

;WZ_QUAGMIRE
Skill008E=0x7F0EAD68

;HT_BLASTMINE
Skill008F=0x00000000

;HT_SKIDTRAP
;MA_SKIDTRAP
Skill0090=0x00000000

;HT_ANKLESNARE
Skill0091=0x00000000

;AS_VENOMDUST
Skill0092=0x00000000

;HT_LANDMINE
;MA_LANDMINE
Skill0093=0x00000000

;HT_SHOCKWAVE
Skill0094=0x00000000

;HT_SANDMAN
;MA_SANDMAN
Skill0095=0x00000000

;HT_FLASHER
Skill0096=0x00000000

;HT_FREEZINGTRAP
;MA_FREEZINGTRAP
Skill0097=0x00000000

;HT_CLAYMORETRAP
Skill0098=0x00000000

;HT_TALKIEBOX
Skill0099=0x00000000

;SA_VOLCANO
Skill009A=0x00000000

;SA_DELUGE
Skill009B=0x00000000

;SA_VIOLENTGALE
Skill009C=0x00000000

;SA_LANDPROTECTOR
Skill009D=0xFF969696

;BD_LULLABY
Skill009E=0x00000000

;BD_RICHMANKIM
Skill009F=0x00000000

;BD_ETERNALCHAOS
Skill00A0=0x00000000

;BD_DRUMBATTLEFIELD
Skill00A1=0x00000000

;BD_RINGNIBELUNGEN
Skill00A2=0x00000000

;BD_ROKISWEIL
Skill00A3=0x00000000

;BD_INTOABYSS
Skill00A4=0x00000000

;BD_SIEGFRIED
Skill00A5=0x00000000

;BA_DISSONANCE
Skill00A6=0x00000000

;BA_WHISTLE
Skill00A7=0x00000000

;BA_ASSASSINCROSS
Skill00A8=0x2F660F12

;BA_POEMBRAGI
Skill00A9=0x2F171FFF

;BA_APPLEIDUN
Skill00AA=0x2FD8DE2A

;DC_UGLYDANCE
Skill00AB=0x00000000

;DC_HUMMING
Skill00AC=0x00000000

;DC_DONTFORGETME
Skill00AD=0xCF00CF00

;DC_FORTUNEKISS
Skill00AE=0x00000000

;DC_SERVICEFORYOU
Skill00AF=0x2F8C27AB

;RG_GRAFFITI
Skill00B0=0x00000000

;AM_DEMONSTRATION
Skill00B1=0x00000000
;WE_CALLBABY
Skill00B2=0x00000000

;WE_CALLPARENT
Skill00B2=0x00000000

;WE_CALLPARTNER
Skill00B2=0x00000000

;PA_GOSPEL
Skill00B3=0x00000000

;HP_BASILICA
Skill00B4=0x00000000

;CG_MOONLIT
Skill00B5=0x00000000

;PF_FOGWALL
Skill00B6=0x7FA87928

;PF_SPIDERWEB
Skill00B7=0x00000000

;HW_GRAVITATION
Skill00B8=0x00000000

;CG_HERMODE
Skill00B9=0x00000000

;NJ_SUITON
Skill00BB=0x00000000

;NJ_TATAMIGAESHI
Skill00BC=0x00000000

;NJ_KAENSIN
Skill00BD=0x00000000

;GS_GROUNDDRIFT
Skill00BE=0x00000000

;GD_LEADERSHIP 
Skill00C1=0x00000000

;GD_GLORYWOUNDS 
Skill00C2=0x00000000

;GD_SOULCOLD 
Skill00C3=0x00000000

;GD_HAWKEYES 
Skill00C4=0x00000000

;NPC_EARTHQUAKE
Skill00C6=0x00000000

;NPC_EVILLAND
Skill00C7=0x7F4F5CB3

;AB_EPICLESIS*
Skill00CA=0x00000000

;WL_EARTHSTRAIN*
Skill00CB=0x00000000

;SC_MANHOLE*
Skill00CC=0x00000000

;SC_DIMENSIONDOOR*
Skill00CD=0x00000000

;SC_CHAOSPANIC*
Skill00CE=0x00000000

;SC_MAELSTROM*
Skill00CF=0x00000000

;SC_BLOODYLUST*
Skill00D0=0x00000000

;SC_FEINTBOMB*
Skill00D1=0x00000000

;RA_MAGENTATRAP*
Skill00D2=0x00000000

;RA_COBALTTRAP*
Skill00D3=0x00000000

;RA_MAIZETRAP*
Skill00D4=0x00000000

;RA_VERDURETRAP*
Skill00D5=0x00000000

;RA_FIRINGTRAP*
Skill00D6=0x00000000

;RA_ICEBOUNDTRAP*
Skill00D7=0x00000000

;RA_ELECTRICSHOCKER*
Skill00D8=0x00000000

;RA_CLUSTERBOMB*
Skill00D9=0x00000000

;WM_REVERBERATION*
Skill00DA=0x00000000

;WM_SEVERE_RAINSTORM*
Skill00DB=0x00000000

;SO_FIREWALK*
Skill00DC=0x00000000

;SO_ELECTRICWALK*
Skill00DD=0x00000000

;WM_POEMOFNETHERWORLD*
Skill00DE=0x00000000

;SO_PSYCHIC_WAVE*
Skill00DF=0x00000000

;SO_CLOUD_KILL*
Skill00E0=0x00000000

;GC_POISONSMOKE*
Skill00E1=0x00000000

;NC_NEUTRALBARRIER*
Skill00E2=0x00000000

;NC_STEALTHFIELD*
Skill00E3=0x00000000

;SO_WARMER*
Skill00E4=0x00000000

;GN_THORNS_TRAP*
Skill00E5=0x00000000

;GN_WALLOFTHORN*
Skill00E6=0x00000000

;GN_DEMONIC_FIRE*
Skill00E7=0x00000000

;GN_FIRE_EXPANSION_SMOKE_POWDER*
Skill00E8=0x00000000

;GN_FIRE_EXPANSION_TEAR_GAS*
Skill00E9=0x00000000

;GN_HELLS_PLANT*
Skill00EA=0x00000000

;SO_VACUUM_EXTREME*
Skill00EB=0x00000000

;LG_BANDING*
Skill00EC=0x00000000

;EL_FIRE_MANTLE*
Skill00ED=0x00000000

;EL_WATER_BARRIER*
Skill00EE=0x00000000

;EL_ZEPHYR*
Skill00EF=0x00000000

;EL_POWER_OF_GAIA*
Skill00F0=0x00000000

;SO_FIRE_INSIGNIA*
Skill00F1=0x00000000

;SO_WATER_INSIGNIA*
Skill00F2=0x00000000

;SO_WIND_INSIGNIA*
Skill00F3=0x00000000

;SO_EARTH_INSIGNIA*
Skill00F4=0x00000000

;MH_POISON_MIST*
Skill00F5=0x00000000

;MH_LAVA_SLIDE*
Skill00F6=0x00000000

;MH_VOLCANIC_ASH*
Skill00F7=0x00000000

;KO_ZENKAI*
Skill00F8=0x00000000

;KO_MAKIBISHI*
Skill00FC=0x00000000

;NPC_VENOMFOG
Skill00FD=0x00000000

;SC_ESCAPE*
Skill00FE=0x00000000

");
                    #endregion
                }
            }

            if (File.Exists("config.xml"))
            {
                #region Load Config XML
                using (XmlReader reader = XmlReader.Create("config.xml"))
                {
                    var configration = new Config();
                    var serializer = new XmlSerializer(typeof(Config));

                    configration = (Config)serializer.Deserialize(reader);

                    m_SharedData.write_packetlog
                        = configration.write_packetlog;
                    m_SharedData.freemouse
                        = configration.freemouse;
                    m_SharedData.ground_zbias
                        = configration.ground_zbias;
                    m_SharedData.alphalevel
                        = configration.alphalevel;
                    m_SharedData.m2e
                        = configration.m2e;
                    m_SharedData.bbe
                        = configration.bbe;
                    m_SharedData.deadcell
                        = configration.deadcell;
                    m_SharedData.chatscope
                        = configration.chatscope;
                    m_SharedData.castrange
                        = configration.castrange;
                    m_SharedData.fix_windowmode_vsyncwait
                        = configration.fix_windowmode_vsyncwait;
                    m_SharedData.show_framerate
                        = configration.show_framerate;
                    m_SharedData.objectinformation
                        = configration.objectinformation;
                    m_SharedData._44khz_audiomode
                        = configration._44khz_audiomode;
                    m_SharedData.cpucoolerlevel
                        = configration.cpucoolerlevel;
                    m_SharedData.chainload
                        = configration.chainload;
                }
                #endregion
            }

            m_npcLogger = new NPCLogger();
            m_npcLogger.StartPosition = FormStartPosition.Manual;
            m_npcLogger.Bounds = Properties.Settings.Default.LoggerWinBounds;
            m_npcLogger.WindowState = Properties.Settings.Default.LoggerWinState;

            if ( Properties.Settings.Default.LoggerWinVisible)
                m_npcLogger.Show();

        }

        private void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_npcLogger.WindowState == FormWindowState.Normal )
                Properties.Settings.Default.LoggerWinBounds = m_npcLogger.Bounds;
            else
                Properties.Settings.Default.LoggerWinBounds = m_npcLogger.RestoreBounds;
            Properties.Settings.Default.LoggerWinState = m_npcLogger.WindowState;

            Properties.Settings.Default.LoggerWinVisible = m_npcLogger.Visible;

            Properties.Settings.Default.Save();

            m_npcLogger.Hide();

            #region Save Config XML
            using (XmlTextWriter writer = new XmlTextWriter("config.xml", System.Text.Encoding.UTF8))
            {
                var configration = new Config();
                var serializer = new XmlSerializer(typeof(Config));

                configration.write_packetlog
                    = m_SharedData.write_packetlog;
                configration.freemouse
                    = m_SharedData.freemouse;
                configration.ground_zbias
                    = m_SharedData.ground_zbias;
                configration.alphalevel
                    = m_SharedData.alphalevel;
                configration.m2e
                    = m_SharedData.m2e;
                configration.bbe
                    = m_SharedData.bbe;
                configration.deadcell
                    = m_SharedData.deadcell;
                configration.chatscope
                    = m_SharedData.chatscope;
                configration.castrange
                    = m_SharedData.castrange;
                configration.fix_windowmode_vsyncwait
                    = m_SharedData.fix_windowmode_vsyncwait;
                configration.show_framerate
                    = m_SharedData.show_framerate;
                configration.objectinformation
                    = m_SharedData.objectinformation;
                configration._44khz_audiomode
                    = m_SharedData._44khz_audiomode;
                configration.cpucoolerlevel
                    = m_SharedData.cpucoolerlevel;
                configration.chainload
                    = m_SharedData.chainload;

                writer.Formatting = Formatting.Indented;
                serializer.Serialize(writer, configration);
            }
            #endregion


        }

        private void nPCLoggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_npcLogger.Visible)
                m_npcLogger.Hide();
            else
                m_npcLogger.Show();

        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_npcLogger.TopMost = true;
                m_npcLogger.TopMost = false;
            }
        }

        private void chainloadDinputdllasiForDinputfreeRagexesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tsm = (ToolStripMenuItem)sender;
            m_SharedData.chainload = tsm.Checked;
        }
    }

    public class Config
    {
        public Config()
        {
            write_packetlog = false;
            freemouse = false;
            ground_zbias = 0;
            alphalevel = 0;
            m2e = true;
            bbe = true;
            deadcell = true;
            chatscope = true;
            castrange = true;
            fix_windowmode_vsyncwait = true;
            show_framerate = true;
            objectinformation = false;
            _44khz_audiomode = false;
            cpucoolerlevel = 0;
            chainload = true;
        }

        public bool write_packetlog { get; set; }
        public bool freemouse { get; set; }
        public int ground_zbias { get; set; }
        public int alphalevel { get; set; }
        public bool m2e { get; set; }
        public bool bbe { get; set; }
        public bool deadcell { get; set; }
        public bool chatscope { get; set; }
        public bool castrange { get; set; }
        public bool fix_windowmode_vsyncwait { get; set; }
        public bool show_framerate { get; set; }
        public bool objectinformation { get; set; }
        public bool _44khz_audiomode { get; set; }
        public int cpucoolerlevel { get; set; }
        public bool chainload { get; set; }

        // without serialize
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string configfilepath { get; set; }
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string musicfilename { get; set; }
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool executeorder { get; set; }
    }

}
