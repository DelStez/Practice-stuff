using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StegoImage
{
    public partial class StegoImage : Form
    {
        public OpenFileDialog openDialog = new OpenFileDialog();
        public Bitmap image;

        public string dir = "c:\\";
        public string fileImagePath = string.Empty;
        public string filetxtPath = string.Empty;
        public string Content = string.Empty;

        public StegoImage()
        {
            InitializeComponent();
            comboBoxmode.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
        }
        #region LoadFiles
        private void loadImage_Click(object sender, EventArgs e)
        {
            openDialog.FileName = string.Empty;
            openDialog.Filter = "Image files(*.jpg, *.bmp, *.png) | *.jpg; *.bmp; *.png;";
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                fileImagePath = openDialog.FileName;
                imgBox.Text = fileImagePath;
                image = (Bitmap)Image.FromFile(fileImagePath);
                ShowImage(image);
            }
        }
        public void ShowTxt(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                
                contentBox.Text = reader.ReadToEnd();
                Content = contentBox.Text;
            }
        }
        public void ShowImage(Bitmap cur)
        {
            picBox.Image = cur;
        }
        private void loadText_Click(object sender, EventArgs e)
        {
            openDialog.FileName = string.Empty;
            openDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openDialog.FilterIndex = 2;
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                filetxtPath = openDialog.FileName;
                txtBox.Text = filetxtPath;
                ShowTxt(filetxtPath);
            }
        }
        #endregion LoadFiles 
        private void startButton_Click(object sender, EventArgs e)
        {
            int numBit = Convert.ToInt32(comboBox1.SelectedItem);
            if (comboBoxmode.SelectedIndex == 0 && picBox.Image != null)
            {
                if (checkedListBox1.CheckedItems.Count != 0 && Content != null)
                {
                    bool[] getRGBcode = { checkedListBox1.GetItemChecked(0), checkedListBox1.GetItemChecked(1), checkedListBox1.GetItemChecked(2)};
                    BitArray mess = new BitArray(Encoding.UTF8.GetBytes(Content));
                    LSB newImageMessage = new LSB(image, Content, getRGBcode, numBit);
                    picBox.Image = newImageMessage.insertTextToImage();
                    SaveFileDialog save = new SaveFileDialog();//диалог. окно -сохранение bmp файла
                    save.Filter = "Image files(*.jpg, *.bmp, *.png) | *.jpg; *.bmp; *.png;";
                    ImageFormat format = ImageFormat.Png;
                    save.InitialDirectory = @"C:\";
                    if (save.ShowDialog() == DialogResult.OK)
                    {
                        string ext = System.IO.Path.GetExtension(save.FileName);
                        switch(ext)
                        {
                            case ".jpg":
                            format = ImageFormat.Jpeg;
                            break;
                            case ".bmp":
                            format = ImageFormat.Bmp;
                            break;
                            case ".png":
                                format = ImageFormat.Png;
                                break;
                        }
                        picBox.Image.Save(save.FileName, format);// сохранение изображения
                    }
                        
                }
                else if (checkedListBox1.CheckedItems.Count == 0)
                    MessageBox.Show("Невыбраны каналы", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (Content == null)
                    MessageBox.Show("Отсуствует текст", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (comboBoxmode.SelectedIndex == 1 && picBox.Image != null)
            {
                bool[] getRGBcode = { checkedListBox1.GetItemChecked(0), checkedListBox1.GetItemChecked(1), checkedListBox1.GetItemChecked(2)};
                LSB newImageMessage = new LSB((Bitmap)picBox.Image, getRGBcode, numBit);// cоздания нового LSB - класса (содержит методы для декодировки LSB метода)
                string temp = newImageMessage.ExtractMessage((Bitmap)picBox.Image);
                contentBox.Text = temp.Replace("\0", "");// показать результат (немного отредактирован для читабельности)
            }
            else
                MessageBox.Show("Отсуствует изображение", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public void SetValue(bool d)
        {
            for(int i = 0; i < checkedListBox1.Items.Count; i++)
                checkedListBox1.SetItemChecked(i, d);
        }
        private void StegoImage_Load(object sender, EventArgs e)
        {
            SetValue(true);
        }

        private void comboBoxmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxmode.SelectedIndex == 0)
            {
                ShowTxt(filetxtPath);
            }
            else
            {
                contentBox.Text = "";
            }
        }
    }
}
