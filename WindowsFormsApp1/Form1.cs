using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Выберите путь к папке" })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    webBrowser.Url = new Uri(fbd.SelectedPath);
                    txtPath.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (webBrowser.CanGoBack)
                webBrowser.GoBack();
        }

        private void bntForward_Click(object sender, EventArgs e)
        {
            if (webBrowser.CanGoForward)
                webBrowser.GoForward();
        }

        private void bntObr_Click(object sender, EventArgs e)
        {
            string directoryPath = $"{txtPath.Text}";
            string[] aviFiles = Directory.GetFiles(directoryPath, "*.avi");

            foreach (string aviFile in aviFiles)
            {
                Process process = new Process();
                process.StartInfo.FileName = "DivFix++.exe";
                process.StartInfo.Arguments = $"-i \"{aviFile}\"";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;

                process.Start();
                process.WaitForExit();

                // Optionally, you can capture the output and error messages
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                // You can log or display the output and error if needed
                Console.WriteLine(output);
                if (!string.IsNullOrEmpty(error))
                {
                    MessageBox.Show("Error Input data", "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                    ///Console.WriteLine("Error: " + error);
                }
            }
        }
    }
}
