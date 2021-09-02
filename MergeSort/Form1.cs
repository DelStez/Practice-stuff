using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MergeSort
{
    public partial class Form1 : Form
    {
        public OpenFileDialog opnMainFile = new OpenFileDialog();
        public string mFilePath = string.Empty;
        public string aFilePath = string.Empty;

        public Form1()
        {
            InitializeComponent();
            opnMainFile.InitialDirectory = "c:\\";
            opnMainFile.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            opnMainFile.FilterIndex = 2;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            opnMainFile.RestoreDirectory = true;
            

            if (opnMainFile.ShowDialog() == DialogResult.OK)
            {
                mFilePath = opnMainFile.FileName;
                pathMainFile.Text = mFilePath;
                
            }
        }
    }
}