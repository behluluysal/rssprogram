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

namespace Saint_Finder
{
    public partial class SearchSettings : Form
    {
        string appPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\searchsettings.txt";
        public SearchSettings()
        {
            InitializeComponent();
            listguncelle();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> sites = new List<string>();
            string silinecek = "";
            ListBox.SelectedObjectCollection selectedItems = new ListBox.SelectedObjectCollection(wordslist);
            selectedItems = wordslist.SelectedItems;

            if (wordslist.SelectedIndex != -1)
            {
                for (int i = selectedItems.Count - 1; i >= 0; i--)
                    silinecek = selectedItems[i].ToString();

                if (!File.Exists(appPath))
                {
                    using (var myFile = File.Create(appPath))
                    {
                        // interact with myFile here, it will be disposed automatically
                    }
                }
                else
                {
                    string[] lines = File.ReadAllLines(appPath);
                    foreach (string line in lines)
                    {
                        sites.Add(line);
                    }
                }
                sites.Remove(silinecek);
                File.WriteAllLines(appPath, sites);
            }
            else
                MessageBox.Show("Please select a keyword to remove");
            listguncelle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addword(textBox1.Text);
            listguncelle();
        }








        private void listguncelle()
        {
            wordslist.DataSource = null;
            List<string> words = new List<string>();
            string word;
            if (!File.Exists(appPath))
            {
                using (var myFile = File.Create(appPath))
                {
                    // interact with myFile here, it will be disposed automatically
                }
            }
            else
            {
                string[] lines = File.ReadAllLines(appPath);
                foreach (string line in lines)
                {
                    word = line;
                    words.Add(word);
                }
                wordslist.DataSource = words;
            }
        }

        private void addword(string s)
        {
            List<string> quotelist = File.ReadAllLines(appPath).ToList();
            string flag = quotelist.Where(r => r == s).FirstOrDefault();

            if ((s == null || s == ""))
            {
                MessageBox.Show("Null input is not allowed!");
            }
            else if (flag != null)
            {
                MessageBox.Show("This keyword is already added to program.");
            }
            else
            {
                File.AppendAllText(appPath,
                    s + Environment.NewLine);
            }

        }

        private void SearchSettings_Load(object sender, EventArgs e)
        {

        }
    }
}
