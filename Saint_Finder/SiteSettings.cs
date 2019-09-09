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
    public partial class SiteSettings : Form
    {
        string appPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\sitesettings.txt";
        public SiteSettings()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            listguncelle();
        }

        private void ServerSettings_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            addsite(textBox1.Text);
            listguncelle();
        }

        private void listguncelle()
        {
            siteslist.DataSource = null;
            List<string> sites = new List<string>();
            string site;
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
                    site = line;
                    sites.Add(site);
                }
                siteslist.DataSource = sites;
            }
        }

        private void addsite(string s)
        {
            List<string> quotelist = File.ReadAllLines(appPath).ToList();
            string flag = quotelist.Where(r=> r == s).FirstOrDefault();

            if ((s == null || s == ""))
            {
                MessageBox.Show("Null input is not allowed!");
            }
            else if(flag != null)
            {
                MessageBox.Show("This website is already added to program.");
            }
            else
            {
                File.AppendAllText(appPath,
                    s + Environment.NewLine);
            }
             
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> sites = new List<string>();
            string silinecek="";
            ListBox.SelectedObjectCollection selectedItems = new ListBox.SelectedObjectCollection(siteslist);
            selectedItems = siteslist.SelectedItems;

            if (siteslist.SelectedIndex != -1)
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
                MessageBox.Show("Please select a website to remove");
            listguncelle();
        }
    }
}
