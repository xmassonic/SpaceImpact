using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceImpact.DesktopUI
{
    public partial class MenuForm : Form
    {
        readonly AboutSI _aboutSi = new AboutSI();
        public MenuForm()
        {
            InitializeComponent();
        }

        private void btnMenuAbout_Click(object sender, EventArgs e)
        {
            _aboutSi.ShowDialog();
        }

        private void btnMenuControls_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "For movement press arrows keys,\nfor shoot press spacebar.\nP - pause, R - resume, Esc - quit.",
                "Controls", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMenuStart_Click(object sender, EventArgs e)
        {
            Program.Play = true;
            Close();
        }

    }
}
