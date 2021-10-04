using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExternalMergeSortLab
{
    public partial class Form1 : Form
    {
        public OpenFileDialog mainFileLoad = new OpenFileDialog();
        public string pattern = @"([A-Za-z])+";
        public string mFilePath = string.Empty;
        public string aFilePath = string.Empty;
        public Form1()
        {
            InitializeComponent();
            mainFileLoad.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            mainFileLoad.InitialDirectory = "c:\\";
            mainFileLoad.FilterIndex = 1;
            mainFileLoad.RestoreDirectory = true;
        }

        public void DownloadFile()
        {
            
        }

        public void ExitSaveResult()
        {
        }

        public void ExternalMergeSort()
        {
            
        }

        private void ReadToFormForShowContent(string path, bool thatFile)
        {
            showContentBoxMain.Text = string.Empty;
            using (StreamReader iRead = new StreamReader(path))
            {
                string line;
                while ((line = iRead.ReadLine()) != null)
                {
                    if (!Regex.IsMatch(line, pattern))
                    {
                        showContentBoxMain.Text += line;
                    }
                    else
                    {
                        MessageBox.Show("В файле присутсвуют постороние символы");
                        showContentBoxMain.Clear();
                        return;
                    }
                }
            }
        }
        
        private void loadFileButton_Click(object sender, EventArgs e)
        {
            if (mainFileLoad.ShowDialog() == DialogResult.OK)
            {
                textMainFilePath.Text = mainFileLoad.FileName;
                mFilePath = mainFileLoad.FileName;
                ReadToFormForShowContent(mFilePath, true);
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            //Creating Additional File
            string nameAddFile = Path.GetFileName(Path.GetDirectoryName(mFilePath)) + "addFile.txt";
            File.Create(nameAddFile).Dispose();
            //MergeSort function
            //show result
        }

        private void label2_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}