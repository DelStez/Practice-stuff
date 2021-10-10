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

        public Form1()
        {
            InitializeComponent();
            mainFileLoad.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            mainFileLoad.InitialDirectory = "c:\\";
            mainFileLoad.FilterIndex = 1;
            mainFileLoad.RestoreDirectory = true;
        }

        public void ReadToFormForShowContent(string path, bool thatFile)
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
                        else  showContentBoxMain1.Text += (line + " ");
                        startButton.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("В файле присутсвуют постороние символы");
                        showContentBoxMain.Clear();
                        showContentBoxMain1.Clear();
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

        public void SplitMainFile(string pathMainFile)
        {
            //Creating additional files
            FileStream fs_first = new FileStream("adF1.ms", FileMode.OpenOrCreate);
            FileStream fs_second = new FileStream("adF2.ms", FileMode.OpenOrCreate);
            fs_first.Close();
            fs_second.Close();
            using (StreamReader mainFile = File.OpenText(pathMainFile))
            {
                using (StreamWriter fs1 = new StreamWriter("adF1.ms"), fs2 = new StreamWriter("adF2.ms"))
                {
                    //Reading main file
                    string line = string.Empty;
                    int i = 0;
                    while (( line = mainFile.ReadLine()) != null)
                    {
                    
                        var t = new UTF8Encoding(true).GetBytes(Convert.ToString(line));
                        if (i % 2 == 0)
                            fs1.WriteLine(line);
                        else
                            fs2.WriteLine(line);
                        i++;
                    }
                    
                }
            }
        }
        public bool isSorter(string pathMainFile)
        {
            using (StreamReader af1 = File.OpenText(pathMainFile))
            {
                string a1 = "0";
                string a2 = "0";
                a1 = af1.ReadLine();
                a2 = af1.ReadLine();
                while (a1 != null && a2  != null)
                {
                    if (Convert.ToInt32(a1) > Convert.ToInt32(a2))
                        return false;
                    else
                    {
                        a1 = a2;
                        a2 = af1.ReadLine();
                    }

                }        
            }
            return true;
        }
        public void SimpleMergeSort(string pathMainFile)
        {
            int k = 1;
            string a1;
            string a2;
            bool b;
            int f1 = 0, f2 = 0;
            while (true) 
            {
                if(isSorter(pathMainFile)) 
                    break;
                SplitMainFile(pathMainFile);
                using (StreamReader af1 = File.OpenText("adF1.ms"), af2 = File.OpenText("adF2.ms"))
                {
                    using (StreamWriter fs = new StreamWriter(pathMainFile))
                    {
                        string lineOfFile1 = string.Empty, lineOfFile2 = string.Empty; 
                        a1 = af1.ReadLine();
                        a2 = af2.ReadLine();
                        b = true;
                        while (a2 != null && a1 != null)
                        {
                            int i = 0, j = 0;
                            while ((i < k || j < k) && a2 != null && a1 != null)
                            {
                                if (Convert.ToInt32(a1) < Convert.ToInt32(a2))
                                {
                                    fs.WriteLine(a1);
                                    a1 = af1.ReadLine();
                                    i++;
                                }
                                else
                                {
                                    fs.WriteLine(a2);
                                    a2 = af2.ReadLine();
                                    j++;
                                }
                            }
                            while (i < k && a1 != null)
                            {
                                fs.WriteLine(a1);
                                a1 = af1.ReadLine();
                                i++;
                            }
                            while (j < k && a2 != null)
                            {
                                fs.WriteLine(a2);
                                a2 = af2.ReadLine();
                                j++;
                            } 
                        }
                        while (a1 != null)
                        {
                            fs.WriteLine(a1);
                            a1 = af1.ReadLine();
                        }
                        while (a2 != null)
                        {
                            fs.WriteLine(a2);
                            a2 = af2.ReadLine();
                        } 
                        k*=2;
                    }
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            showContentBoxMain1.Clear();
            SimpleMergeSort(mFilePath);
            ReadToFormForShowContent(mFilePath, false);
        }
    }
}