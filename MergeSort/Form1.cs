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

        private void SplitMainFile(string pathMainFile)
        {
            //Creating additional files
            FileStream fs_first = new FileStream("adF1.ms", FileMode.OpenOrCreate);
            FileStream fs_second = new FileStream("adF2.ms", FileMode.OpenOrCreate);
            fs_first.Close();
            fs_second.Close();
            using (StreamReader mainFile = File.OpenText(pathMainFile))
            {
                //Reading main file
                string line = string.Empty;
                int i = 0;
                while (( line = mainFile.ReadLine()) != null)
                {
                    using(StreamWriter fs1 = new StreamWriter("adF1.ms"), fs2 = new StreamWriter("adF2.ms"))
                    {
                        int temp = Convert.ToInt32(line);
                        var t = new UTF8Encoding(true).GetBytes(Convert.ToString(temp));
                        if (i % 2 == 0)
                            fs1.WriteLine(t);
                        else
                            fs2.WriteLine(t);
                    }
                }
            }
             
        }

        private void SimpleMergeSort(string pathMainFile)
        {
            int k = 1, a1 = 0, a2 = 0;
            int f1 = 0, f2 = 0;
            using (StreamReader af1 = File.OpenText("adF1.ms"), af2 = File.OpenText("adF2.ms"))
            {
                string lineOfFile1 = string.Empty, lineOfFile2 = string.Empty;
                while ((lineOfFile1 = af1.ReadLine()) != null && (lineOfFile2 = af2.ReadLine()) != null)
                {
                    int i = 0, j = 0;
                    f1 = 0; f2 = 0;
                    while (i < k && j < k && ((lineOfFile1 = af1.ReadLine()) != null) &&((lineOfFile2 = af2.ReadLine()) != null))
                    {
                        a1 = Convert.ToInt32(lineOfFile1);
                        a2 = Convert.ToInt32(lineOfFile2);
                        using (FileStream fs = new FileStream("temp.txt",FileMode.OpenOrCreate))
                        {
                            if (a1 < a2)
                            {
                                var t =  new UTF8Encoding(true).GetBytes(Convert.ToString(a1));
                                fs.Write(t, 0 , t.Length);
                                a1 = Convert.ToInt32(lineOfFile1);
                                i++;
                            }
                            else
                            {
                                var t =  new UTF8Encoding(true).GetBytes(Convert.ToString(a2));
                                fs.Write(t, 0 , t.Length);
                                a2 = Convert.ToInt32(lineOfFile2);
                                j++;
                            }
                        }
                    }
                    using (FileStream fs = new FileStream("temp.txt", FileMode.OpenOrCreate))
                    {
                        while (i<k && (lineOfFile1 = af1.ReadLine()) != null)
                        {
                            var t =  new UTF8Encoding(true).GetBytes(Convert.ToString(a1));
                            fs.Write(t, 0 , t.Length);
                            a1 = Convert.ToInt32(lineOfFile1);
                            i++;
                        }
                        while (j<k && (lineOfFile2 = af2.ReadLine()) != null)
                        {
                            var t =  new UTF8Encoding(true).GetBytes(Convert.ToString(a2));
                            fs.Write(t, 0 , t.Length);
                            a2 = Convert.ToInt32(lineOfFile2);
                            j++;
                        }
                    }
                    using (FileStream fs = new FileStream("temp.txt", FileMode.OpenOrCreate))
                    {
                        while ((lineOfFile1 = af1.ReadLine()) != null)
                        {
                            var t =  new UTF8Encoding(true).GetBytes(Convert.ToString(a1));
                            fs.Write(t, 0 , t.Length);
                            a1 = Convert.ToInt32(lineOfFile1);
                            i++;
                        }
                        while ((lineOfFile2 = af2.ReadLine()) != null)
                        {
                            var t =  new UTF8Encoding(true).GetBytes(Convert.ToString(a2));
                            fs.Write(t, 0 , t.Length);
                            a2 = Convert.ToInt32(lineOfFile2);
                            j++;
                        }
                    }
                    k *= 2;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SplitMainFile(mFilePath);
            SimpleMergeSort(mFilePath);
        }
    }
}