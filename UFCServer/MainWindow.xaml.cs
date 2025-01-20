using System.Windows;
using UFCServer.Utils;
using MessageBox = System.Windows.Forms.MessageBox;

namespace UFCServer
{

    public partial class MainWindow : Window
    {
        public string Ip { get; set; }
        public string Port { get; set; }
        public string vJoyDeviceID { get; set; }


        public MainWindow()
        {
            SetupSystemTrays();

            Ip = Common.GetLocalIp();
            Port = Common.GetSettings().Port;
            vJoyDeviceID = Common.GetSettings().VJoyDeviceId;
            var mainApp = new MainApp();

            try
            {
                mainApp.Startup();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                Close();
            }

            InitializeComponent();
        }

        private void SetupSystemTrays()
        {
            NotifyIcon ni = new()
            {
                Icon = new Icon("Resources/trayicon.ico"),
                Visible = true
            };

            ni.DoubleClick += new EventHandler((_, _) => RevealWindow());
            ni.ContextMenuStrip = new ContextMenuStrip();
            ni.ContextMenuStrip.Items.Add("Show", null, (_, _) => RevealWindow());
            ni.ContextMenuStrip.Items.Add("Close", null, (_, _) => Close());
        }

        private void RevealWindow()
        {
            Show();
            WindowState = WindowState.Normal;
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();
            }
            base.OnStateChanged(e);
        }
    }
}