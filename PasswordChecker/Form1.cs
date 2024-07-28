using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PasswordChecker
{
    public partial class Form1 : Form
    {
        public static string dataRead;
        List<string> wordlist = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Logger.box = rtb1;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.InitialDirectory = ApplicationPath();
            d.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (d.ShowDialog() == DialogResult.OK)
            {
                dataRead = d.FileName;
                Logger.Log("loaded Wordlist file: " + d.FileName);
            }

            string textLines = toolStripTextBox1.Text;
            int x = Int32.Parse(toolStripTextBox1.Text);
            int i = 0;
            wordlist.Clear();

            try
            {
                using (StreamReader sr = new StreamReader(dataRead))
                {
                    for (i = 0; i < x; i++)
                    {
                        if (sr.EndOfStream)
                        {
                            break;
                        }

                        wordlist.Add(sr.ReadLine());
                    }
                }
                Logger.Log("successfully read " + i + " lines...", Color.Green);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, Color.Red);
            }
        }

        public static string ApplicationPath()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Logger.Log("start...", Color.Blue);
            Logger.Log("wordlist: " + wordlist.Count + " Items");

            string passw = toolStripTextBox2.Text;
            int counter = wordlist.Count;
            int runner = 0;

            foreach (var line in wordlist)
            {
                if(line == passw)
                {
                    Logger.Log("successfully found!", Color.Red);
                    Logger.Log("Change your Password !", Color.Red);
                    break;
                }

                if (line.ToLower() == passw.ToLower())
                {
                    Logger.Log("successfully found!", Color.Red);
                    Logger.Log("Change your Password !", Color.Red);
                    break;
                }

                if (line.ToUpper() == passw.ToUpper())
                {
                    Logger.Log("successfully found!", Color.Red);
                    Logger.Log("Change your Password !", Color.Red);
                    break;
                }

                if (runner == counter)
                {
                    Logger.Log("password not found!", Color.Green);
                    Logger.Log("finish...", Color.Green);
                    break;
                }
                runner++;
            }
        }
    }
}
