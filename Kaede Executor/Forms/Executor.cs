using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeAreDevs_API;

namespace Kaede_Executor
{
    public partial class Executor : Form
    {
        readonly ExploitAPI api = new ExploitAPI();

        public Executor()
        {
            InitializeComponent();

            Util.LoadScrips(listBox1);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
            => api.LaunchExploit();

        private void timer1_Tick(object sender, EventArgs e)
            => Util.CheckInjected(txtIsInjected, api);

        private void pictureBox3_Click(object sender, EventArgs e)
            => Environment.Exit(0);

        private void pictureBox2_Click(object sender, EventArgs e)
            => api.SendLuaScript(fastColoredTextBox1.Text);

        private void pictureBox5_Click(object sender, EventArgs e)
            => Util.SaveScript(fastColoredTextBox1);

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
            => Util.LoadScrip(fastColoredTextBox1, listBox1);

        private void pictureBox4_Click(object sender, EventArgs e)
            => Util.LoadScrip(fastColoredTextBox1);

        private void Form1_Load(object sender, EventArgs e)
            => RichPresenceClient.Start();
    }
}
