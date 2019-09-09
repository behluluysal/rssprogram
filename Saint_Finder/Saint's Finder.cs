using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;



namespace Saint_Finder
{

    public partial class Form1 : Form
    {
        string keywordpath = Path.GetDirectoryName(Application.ExecutablePath) + "\\searchsettings.txt";
        string sitepath = Path.GetDirectoryName(Application.ExecutablePath) + "\\sitesettings.txt";
        long timeraut = 0; // otomatik sayac
        long timerman = 1; // manuel sayac
        bool flagfortimer = false;
        public Form1()
        {
            InitializeComponent();

            mainlist();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainlist();
            timerman++;
            textBox3.Text = "Searched "+timerman+" times.";
        }

        private void addSiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(SiteSettings))
                {
                    form.Activate();
                    return;
                }
            }

            Form newForm = new SiteSettings();
            newForm.Show();
        }

        private void searchSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(SearchSettings))
                {
                    form.Activate();
                    return;
                }
            }

            Form newForm = new SearchSettings();
            newForm.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer2.Interval = 3600000;
            timer1.Interval = 1000;
            this.listBox1.MouseDoubleClick += new MouseEventHandler(listBox1_MouseDoubleClick);
        }

        private void mainlist()
        {
            try
            {
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                listBox1.DataSource = null;
                progressBar1.Value = 0;
                string eklenecek = "";
                List<string> bulunanlar = new List<string>();
                int sayi = 0;
                string pattern;
                //Websiteleri listeye atılıyor
                List<string> sites = new List<string>();
                string[] lines = File.ReadAllLines(sitepath);
                foreach (string line in lines)
                {
                    sites.Add(line);
                }

                //Keywords Listeye atılıyor
                List<string> keywords = new List<string>();
                string[] liness = File.ReadAllLines(keywordpath);
                foreach (string line in liness)
                {
                    keywords.Add(line);
                }

                int progressbar = keywords.Count * sites.Count;
                if(progressbar != 0)
                {
                    int load = 100 / progressbar;
                    foreach (var item in keywords)
                    {
                        pattern = item;
                        foreach (var itemm in sites)
                        {
                            WebClient web = new WebClient();
                            String html = web.DownloadString(itemm);
                            html = html.ToLower();
                            html = html.Replace(" ", "");
                            pattern = pattern.ToLower();
                            pattern = pattern.Replace(" ", "");
                            if(html.Contains(pattern))
                            {
                                sayi = Regex.Matches(html, pattern).Count;
                                eklenecek = item + " found! on: " + itemm + " " + sayi + " times.";
                                bulunanlar.Add(eklenecek);
                            }
                                
                            
                            progressBar1.Value += load;
                        }
                    }
                    listBox1.DataSource = bulunanlar;
                    progressBar1.Value = 100;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = true;
                }   
               
            }
            catch (Exception e)
            {
                MessageBox.Show("Some error occured: "+e+". Contact I.T. support with error code.", "Error");
                throw;
            }
            
        }
        private void mainlistaut()
        {
            try
            {
                timer1.Stop();
                timer2.Stop();
                listBox1.DataSource = null;
                progressBar1.Value = 0;
                string eklenecek = "";
                List<string> bulunanlar = new List<string>();
                int sayi = 0;
                string pattern;
                //Websiteleri listeye atılıyor
                List<string> sites = new List<string>();
                string[] lines = File.ReadAllLines(sitepath);
                foreach (string line in lines)
                {
                    sites.Add(line);
                }

                //Keywords Listeye atılıyor
                List<string> keywords = new List<string>();
                string[] liness = File.ReadAllLines(keywordpath);
                foreach (string line in liness)
                {
                    keywords.Add(line);
                }

                int progressbar = keywords.Count * sites.Count;
                int load = 100 / progressbar;
                foreach (var item in keywords)
                {
                    pattern = item;
                    foreach (var itemm in sites)
                    {
                        WebClient web = new WebClient();
                        String html = web.DownloadString(itemm);
                        html = html.ToLower();
                        pattern = pattern.ToLower();
                        if (html.Contains(pattern))
                        {
                            sayi = Regex.Matches(html, pattern).Count;
                            eklenecek = item + " found! on: " + itemm + " " + sayi + " times.";
                            bulunanlar.Add(eklenecek);
                        }
                        progressBar1.Value += load;
                    }
                }
                listBox1.DataSource = bulunanlar;
                progressBar1.Value = 100;
                timer1.Start();
                timer2.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show("Some error occured: " + e + ". Contact I.T. support with error code.", "Error");
                throw;
            }
            
        }



        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer2.Interval = 3600000;
            textBox1.Text = "60";
            timeraut = 0;
            textBox2.Text = "Searched " + timeraut + " times.";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timerman = 0;
            textBox3.Text = "Searched " + timerman + " times.";
            listBox1.DataSource = null;
            progressBar1.Value = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (flagfortimer == false)
            {
                int timerr = 0;
                timerr = int.Parse(textBox1.Text);
                if (timerr < 35792)
                {
                    radioButton1.Checked = true;
                    flagfortimer = true;
                    timerr = timerr * 60000;
                    timer2.Interval = timerr;
                    button1.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    textBox1.Enabled = false;
                    mainlistaut();
                    timeraut++;
                    textBox2.Text = "Searched " + timeraut + " times.";
                    progressBar2.Maximum = timerr / 1000;
                    progressBar2.Value = 0;
                }
                else
                {
                    MessageBox.Show("Timer must be lower than 35792");
                }

            }
            else if (flagfortimer == true)
            {
                timer2.Stop();
                timer1.Stop();
                button1.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                textBox1.Enabled = true;
                progressBar2.Value = 0;
                radioButton1.Checked = false;
                flagfortimer = false;
            }
            else
            {
                MessageBox.Show("An error occured. Please contact with I.T. Support.");
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            listBox1.DataSource = null;
            mainlistaut();
            timeraut++;
            textBox2.Text = "Searched " + timeraut + " times.";
            progressBar2.Value = 0;
        }


        private void timer1_Tick_1(object sender, EventArgs e)
        {
            progressBar2.Value += 1;
        }

        private void ınformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1-) This program searchs websites page\n"+
                            "sources. Which means if you see something\n"+
                            "on a website and can not find it on this\n"+
                            "program that's why. It's not possible to\n"+
                            "search keywords on a website's view. \n\n"+
                            "2-) Keywords can be uppercase or\n"+
                            "lowercase. It doesn't matter.\n\n"+
                            "3-) You can not use Manual Search and\n" +
                            "Automatic Search at the same time.   \n\n" +
                            "4-) Timer Proggress bar will never be \n" +
                            "filled because at the last second   \n" +
                            "search proggress will be started.   \n\n"+
                            "5-) Users can not enter a timer bigger\n"+
                            "than 35792 because of Int-32. It can not \n" +
                            "be changed I guess. \n\n"+
                            "6-) For more information contact me.\n\n" +
                            "+90(541)241-53-34 \n s.b.uysal98@hotmail.com\n\n" +
                            "                                             Developed by SUB","Information");
        }



        void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox1.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                string [] temp = listBox1.SelectedItem.ToString().Split(' ');
                int tempi = temp.Length-3;
                System.Diagnostics.Process.Start(temp[tempi]);
                //do your stuff here
            }
        }

        private void label3_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
