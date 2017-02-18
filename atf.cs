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
using System.Diagnostics;
using Microsoft.Win32;

namespace ATF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
                e.Effect = DragDropEffects.Copy;
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            // take dropped items and store them
            string[] droppedFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
            // loop through all droppped items and display/add them to the listbox
            foreach (string file in droppedFiles)
            if(Path.GetExtension(file).ToLower() == ".exe")
            {
                    listBox1.Items.Add(file);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            foreach (string item in listBox1.Items)            
            {
                string addToFirewall = "/C netsh advfirewall firewall add rule name=" + Path.GetFileName(item) + " dir=out action=block program=\"" + item + "\"";
                System.Diagnostics.Process proc2 = new System.Diagnostics.Process();
                proc2.StartInfo.CreateNoWindow = false;
                proc2.StartInfo.Verb = "runas";
                proc2.StartInfo.FileName = "cmd";
                proc2.StartInfo.Arguments = "/env /user:" + "Administrator" + " cmd" + addToFirewall;
                proc2.Start();
            }
        }
    }
}
