﻿using ICPServer.Data;
using ICPServer.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Windows;
using MessageBox = System.Windows.Forms.MessageBox;

namespace ICPServer
{

    public partial class MainWindow : Window
    {
        private readonly NotifyIcon ni = new();
        public string Ip { get; set; }
        public string Port { get; set; }

        public MainWindow()
        {
            Startup();

            InitializeComponent();
        }

        private void Startup()
        {

            SetupSystemTrays();

            try
            {
                Ip = Common.GetLocalIp();
                Port = Common.GetSettings().Port;

                IHost Host = Server.HostBuilder(Port);

                Host.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Localhost error: {ex.Message}", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                Close();
            }
        }

        private void SetupSystemTrays()
        {
            ni.Icon = new Icon("trayicon.ico");
            ni.Visible = true;
            ni.DoubleClick += new EventHandler(ShowApp);
            ni.ContextMenuStrip = new ContextMenuStrip();
            ni.ContextMenuStrip.Items.Add("Show", null, ShowApp);
            ni.ContextMenuStrip.Items.Add("Close", null, CloseApp);
        }

        private void ShowApp(object Sender, EventArgs e)
        {
            Show();
            WindowState = WindowState.Normal;
        }

        private void CloseApp(object sender, EventArgs e)
        {
            Close();
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