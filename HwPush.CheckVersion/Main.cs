using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HwPush.Base;

namespace HwPush.CheckVersion
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void 版本1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScanVersion f1 = new ScanVersion();
            f1.Show();
        }

        private void 版本2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LookupVersion f2 = new LookupVersion();
            f2.Show();
        }

        private void 版本3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LookupVersionALL f3 = new LookupVersionALL();
            f3.Show();
        }

        private void 版本4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadCenter f4 = new DownloadCenter();
            f4.Show();

        }

        private void Main_Load(object sender, EventArgs e)
        {

         var rsas=   RsaHelper.GenerateKeys();
        }
    }
}
