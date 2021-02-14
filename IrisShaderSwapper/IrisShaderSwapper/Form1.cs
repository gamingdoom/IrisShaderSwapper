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

namespace IrisShaderSwapper
{
    public partial class Form1 : Form
    {
        public string dotMinecraft = "";
        public string configLocation = "";
        public string shaderLocation = "";
        public string shaderPack = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.minecraft";
            Console.WriteLine(appdata);
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.SelectedPath = appdata;
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox1.Text = folderDlg.SelectedPath;
                dotMinecraft = folderDlg.SelectedPath;
                Console.WriteLine(dotMinecraft);
                getConfig(dotMinecraft);
                getShaders(dotMinecraft);
            }
        }
        private void getConfig(string folder)
        {
            Console.WriteLine(folder);
            configLocation = folder + @"\config\iris.properties";
            Console.WriteLine(configLocation);
        }
        private void getShaders(string folder)
        {
            shaderLocation = folder + @"\shaderpacks";
            //FIND ALL FILES IN FOLDER
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(shaderLocation);
            foreach (System.IO.FileInfo f in dir.GetFiles("*.*"))
            {
                comboBox1.Items.Add(f.Name);
            }
        }
        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            shaderPack = comboBox1.Text.ToString();
            Console.WriteLine(shaderPack);
            editConfig(shaderPack, configLocation);
        }
        private void editConfig(string shader, string config) 
        {
            try
            {
                // Check if file already exists. If yes, delete it.     
                if (File.Exists(config))
                {
                    File.Delete(config);
                }

                // Create a new file     
                using (StreamWriter sw = File.CreateText(config))
                {
                    sw.WriteLine(@"#This file stores configuration options for Iris, such as the currently active shaderpack");
                    sw.WriteLine(@"#" + DateTime.Now.ToString());
                    sw.WriteLine(@"shaderPack=" + shader);

                }

                // Write file contents on console.     
                using (StreamReader sr = File.OpenText(config))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
