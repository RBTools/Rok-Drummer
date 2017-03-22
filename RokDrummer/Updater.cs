using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace RokDrummer
{
    public partial class Updater : Form
    {
        private string fileURL = "";
        private const string forumURL = "http://customscreators.com/index.php?/topic/13196-rok-drummer-v150-91215-play-your-game-drums-on-pc/";

        public Updater()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        public void SetInfo(string currName, string currVersion, string newName, string newVersion, string date, string url, List<string> changeLog)
        {
            lblCurrent.Text = currName + " " + currVersion;
            lblNew.Text = newName + " " + newVersion;
            lblDate.Text = date;
            lblLink.Text = url;
            fileURL = url;
            foreach (var line in changeLog)
            {
                lstLog.Items.Add(line);
            }
        }

        private void btnForum_Click(object sender, EventArgs e)
        {
            Process.Start(forumURL);
            Dispose();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            MessageBox.Show("I'm going to open the download page for you, but you have to install the updated program yourself. Install as usual.", "Updater", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Process.Start(fileURL);
            Environment.Exit(0);
        }

        private void lblLink_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            btnDownload_Click(null, null);
        }
    }
}
