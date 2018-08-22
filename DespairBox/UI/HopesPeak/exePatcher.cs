using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace DespairBox.UI
{
    public partial class exePatcher : Form
    {
        public exePatcher()
        {

            InitializeComponent();
            this.Text = "exe Patcher - " + Program.FileName;
        }

        private void exePatcher_Load(object sender, EventArgs e)
        {

        }

        private void ArchivePathPatchCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (ArchivePathPatchCheck.Checked)
            {
                MessageBox.Show("WARNING! Using this patch will disable sound effects due to an unknown error. Also, make sure to extract all the wads into an /archive/ folder that's in the same directory as the exe.");
            }
        }

        

    }
}
