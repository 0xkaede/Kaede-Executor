using Kaede_Executor.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kaede_Executor
{
    public partial class KeyForm : Form
    {
        public KeyForm()
        {
            InitializeComponent();
            Key.Text = KeySys.GetKeyFromini();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
            => Application.Exit();

        private void Key_Enter(object sender, EventArgs e)
        {
            if (Key.Text != "Enter your Key...")
                return;

            Key.ResetText();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
            => KeySys.GenKey();

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (KeySys.CanAccess(Key.Text))
            {
                try
                {
                    Hide();
                    var MainForm = new Executor();
                    MainForm.Closed += (s, arg) => Close();
                    MainForm.Show();
                }
                catch
                {
                    MessageBox.Show("Please turn off your anti virus!", "Dev Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }
            else
                MessageBox.Show("Key was invalid!", "Key Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
