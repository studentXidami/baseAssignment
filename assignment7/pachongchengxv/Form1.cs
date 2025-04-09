using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace pachongchengxv
{
    public partial class Form1 : Form
    {
        private Crawler crawler;

        public Form1()
        {
            InitializeComponent();
            crawler = new Crawler();
            crawler.PageCrawled += Crawler_PageCrawled;
            crawler.ErrorOccurred += Crawler_ErrorOccurred;
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            var startUrl = txtUrl.Text.Trim();
            if (Uri.IsWellFormedUriString(startUrl, UriKind.Absolute))
            {
                lstCrawled.Items.Clear();
                lstErrors.Items.Clear();
                crawler.StartUrl = new Uri(startUrl);
                await Task.Run(() => crawler.Start());
            }
            else
            {
                MessageBox.Show("请输入有效的URL");
            }
        }

        private void Crawler_PageCrawled(string url)
        {
            if (lstCrawled.InvokeRequired)
            {
                lstCrawled.Invoke(new Action<string>(Crawler_PageCrawled), url);
            }
            else
            {
                lstCrawled.Items.Add(url);
            }
        }

        private void Crawler_ErrorOccurred(string url, string message)
        {
            if (lstErrors.InvokeRequired)
            {
                lstErrors.Invoke(new Action<string, string>(Crawler_ErrorOccurred), url, message);
            }
            else
            {
                lstErrors.Items.Add($"{url} ({message})");
            }
        }
    }
}