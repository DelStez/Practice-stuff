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

namespace MergeSort2
{
    public partial class Form1 : Form
    {
        public OpenFileDialog mainFileLoad = new OpenFileDialog();

        public string mFilePath = string.Empty;
        public string aFilePath = string.Empty;
        // We're have file with numbers on every string: If it's true - sort every line, then sort lines together like as big array
        private void ReadandStart(string pathMain)
        {
            using (StreamReader sr = new StreamReader(pathMain))
            {
                string line = string.Empty;
                                
            }
                        
        }

        // "Add" - additional
        private void MergeSort(string nameAddFile)
        {
            
        }

        public Form1()
        {
            InitializeComponent();
            mainFileLoad.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            mainFileLoad.InitialDirectory = "c:\\";
            mainFileLoad.FilterIndex = 1;
            mainFileLoad.RestoreDirectory = true;
        }

        private void ReadToFormForShowContent(string path, bool thatFile)
        {
            using (StreamReader iRead = new StreamReader(path))
            {
                string line;
                while ((line = iRead.ReadLine()) != null)
                {
                    string[] items = line.Split(' ');
                    foreach (var item in items)
                    {
                        int integer = int.Parse(item);
                        if (thatFile)
                        {
                            listMainFile.Items.Add(integer);
                        }
                    }
                }
                // check: what is file
                // readline and parser
                // read to listbox
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
    }
}