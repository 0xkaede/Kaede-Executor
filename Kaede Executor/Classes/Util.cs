using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeAreDevs_API;
using FastColoredTextBoxNS;
using System.Net.NetworkInformation;
using System.Net;

namespace Kaede_Executor
{
    public class Util
    {
        static readonly string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\KaedeExecutor";
        static readonly string Scripts = AppData + "\\Scripts";
        public static readonly string Key = AppData + "\\Key.ini";

        static readonly string[] FileTypes = { "*.txt", "*.lua" };

        public static void PopulateListBox(ListBox lsb, string Folder, string[] FileTypes)
        {
            if (!Directory.Exists(Folder))
                Directory.CreateDirectory(Folder);

            DirectoryInfo dinfo = new DirectoryInfo(Folder);
            foreach (var FileType in FileTypes)
            {
                FileInfo[] Files = dinfo.GetFiles(FileType);
                foreach (FileInfo file in Files)
                {
                    lsb.Items.Add(file.Name);
                }
            }
        }

        public static void CheckInjected(Label label, ExploitAPI api)
        {
            if (api.isAPIAttached())
            {
                label.Text = "True";
                label.ForeColor = Color.Green;
            }
            else
            {
                label.Text = "False";
                label.ForeColor = Color.Red;
            }
        }

        public static void LoadScrips(ListBox Listbox)
        {
            Listbox.Items.Clear();
            PopulateListBox(Listbox, Scripts, FileTypes);
        }

        public static void LoadScrip(FastColoredTextBox FCTB, ListBox Listbox)
            => FCTB.Text = File.ReadAllText($"{Scripts}\\{Listbox.SelectedItem}");

        public static void LoadScrip(FastColoredTextBox FCTB)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                openFileDialog1.Title = "Open";
                FCTB.Text = File.ReadAllText(openFileDialog1.FileName);
            }
        }

        public static void SaveScript(FastColoredTextBox FCTB)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Lua (*.lua)|*.lua";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = Path.GetFullPath(Scripts);
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.CreateNew))
                using (StreamWriter sw = new StreamWriter(s))
                {
                    sw.Write(FCTB.Text);
                }
            }
        }
    }
}
