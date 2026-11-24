using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;

namespace DuckAI
{
    public partial class Form1 : Form
    {
        private readonly WebView2 browser = new();

        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        [DllImport("dwmapi.dll", PreserveSig = true)]
        private static extern int DwmSetWindowAttribute(
            IntPtr hwnd,
            int attribute,
            ref int value,
            int valueSize);

        public Form1()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;

            Text = "Duck.ai";
            Width = 1100;
            Height = 800;

            browser.Dock = DockStyle.Fill;
            Controls.Add(browser);

            Load += async (_, _) =>
            {
                await browser.EnsureCoreWebView2Async();
                browser.Source = new Uri("https://duck.ai/");
            };
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            int darkMode = 1;
            DwmSetWindowAttribute(
                Handle,
                DWMWA_USE_IMMERSIVE_DARK_MODE,
                ref darkMode,
                sizeof(int));
        }
    }
}
