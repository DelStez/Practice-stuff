using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MergeSort
{
    public partial class Form1 : Form
    {
        public OpenFileDialog opnMainFile = new OpenFileDialog();
        public string mFilePath = string.Empty;
        public string aFilePath = string.Empty;
        public int Iterators = 0;
        public OpenFileDialog mainFileLoad = new OpenFileDialog();
        public string pattern = @"([A-Za-z])+";
        public TwoWaySort firstSort;
        public TreeWaySort secondSort;
        public Form1()
        {
            InitializeComponent();
            mainFileLoad.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            mainFileLoad.InitialDirectory = "c:\\";
            mainFileLoad.FilterIndex = 1;
            mainFileLoad.RestoreDirectory = true;
        }

        public void ReadToFormForShowContent(string path, bool thatFile, bool first)
        {
            startButton.Enabled = false;
            bool empty = true;
            using (StreamReader iRead = new StreamReader(path))
            {
                string line;
                while ((line = iRead.ReadLine()) != null)
                {
                    empty = false;
                    if (!Regex.IsMatch(line, pattern))
                    {
                        if(thatFile) showContentBoxMain.Text += (line + " ");
                        else
                        {
                            if (first) showContentBoxMain1.Text += (line + " ");
                            else textBox1.Text += (line + " ");
                        } 
                        startButton.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("В файле присутсвуют постороние символы");
                        showContentBoxMain.Clear();
                        showContentBoxMain1.Clear();
                        textBox1.Clear();
                        return;
                    }
                    Iterators++;
                }

                if (empty)
                {
                    MessageBox.Show("Файл пуст");
                }
            }
        }
        
        private void loadFileButton_Click(object sender, EventArgs e)
        {
            if (mainFileLoad.ShowDialog() == DialogResult.OK)
            {
                textMainFilePath.Text = mainFileLoad.FileName;
                mFilePath = mainFileLoad.FileName;
                showContentBoxMain.Clear();
                ReadToFormForShowContent(mFilePath, true, false);
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            string pathResult = string.Empty;
            showContentBoxMain1.Clear();
            textBox1.Clear();
            // Подготовка к сортировкам 
            firstSort = new TwoWaySort(mFilePath);
            secondSort = new TreeWaySort(mFilePath);
            // Сортировки
            pathResult = firstSort.startSort();
            ReadToFormForShowContent(pathResult, false, true);
            pathResult = secondSort.StartSort();
            ReadToFormForShowContent(pathResult, false, false);
            label6.Text = Convert.ToString(firstSort.elapsedTime);
            label7.Text = Convert.ToString(secondSort.elapsedTime);;
        }
    }
}