using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kaede_Executor
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new KeyForm());
            }
            catch
            {
                MessageBox.Show("Please turn off your anti virus!", "Dev Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
