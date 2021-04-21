
namespace StegoImage
{
    partial class StegoImage
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.contentBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.loadText = new System.Windows.Forms.Button();
            this.loadImage = new System.Windows.Forms.Button();
            this.txtBox = new System.Windows.Forms.TextBox();
            this.imgBox = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.comboBoxmode = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Красный",
            "Зеленый",
            "Голубой"});
            this.checkedListBox1.Location = new System.Drawing.Point(5, 32);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(110, 64);
            this.checkedListBox1.TabIndex = 0;
            // 
            // picBox
            // 
            this.picBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox.Location = new System.Drawing.Point(292, 3);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(393, 436);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBox.TabIndex = 1;
            this.picBox.TabStop = false;
            // 
            // contentBox
            // 
            this.contentBox.Location = new System.Drawing.Point(7, 232);
            this.contentBox.Multiline = true;
            this.contentBox.Name = "contentBox";
            this.contentBox.ReadOnly = true;
            this.contentBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.contentBox.Size = new System.Drawing.Size(279, 207);
            this.contentBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Каналы для скрытия";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.checkedListBox1);
            this.groupBox1.Location = new System.Drawing.Point(7, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(279, 119);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройка LSB";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "4",
            "8"});
            this.comboBox1.Location = new System.Drawing.Point(240, 13);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(29, 21);
            this.comboBox1.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(125, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Кол-во бит на канал";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.loadText);
            this.groupBox2.Controls.Add(this.loadImage);
            this.groupBox2.Controls.Add(this.txtBox);
            this.groupBox2.Controls.Add(this.imgBox);
            this.groupBox2.Location = new System.Drawing.Point(7, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(279, 71);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Загрузить..";
            // 
            // loadText
            // 
            this.loadText.Location = new System.Drawing.Point(240, 44);
            this.loadText.Name = "loadText";
            this.loadText.Size = new System.Drawing.Size(34, 20);
            this.loadText.TabIndex = 11;
            this.loadText.Text = "...";
            this.loadText.UseVisualStyleBackColor = true;
            this.loadText.Click += new System.EventHandler(this.loadText_Click);
            // 
            // loadImage
            // 
            this.loadImage.Location = new System.Drawing.Point(240, 18);
            this.loadImage.Name = "loadImage";
            this.loadImage.Size = new System.Drawing.Size(34, 20);
            this.loadImage.TabIndex = 10;
            this.loadImage.Text = "...";
            this.loadImage.UseVisualStyleBackColor = true;
            this.loadImage.Click += new System.EventHandler(this.loadImage_Click);
            // 
            // txtBox
            // 
            this.txtBox.Location = new System.Drawing.Point(6, 45);
            this.txtBox.Name = "txtBox";
            this.txtBox.ReadOnly = true;
            this.txtBox.Size = new System.Drawing.Size(228, 20);
            this.txtBox.TabIndex = 9;
            this.txtBox.Text = "Load txt";
            // 
            // imgBox
            // 
            this.imgBox.Location = new System.Drawing.Point(6, 19);
            this.imgBox.Name = "imgBox";
            this.imgBox.ReadOnly = true;
            this.imgBox.Size = new System.Drawing.Size(228, 20);
            this.imgBox.TabIndex = 8;
            this.imgBox.Text = "Load image";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(13, 199);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(113, 23);
            this.startButton.TabIndex = 12;
            this.startButton.Text = "Начать";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // comboBoxmode
            // 
            this.comboBoxmode.FormattingEnabled = true;
            this.comboBoxmode.Items.AddRange(new object[] {
            "Insert",
            "Extract"});
            this.comboBoxmode.Location = new System.Drawing.Point(160, 201);
            this.comboBoxmode.Name = "comboBoxmode";
            this.comboBoxmode.Size = new System.Drawing.Size(121, 21);
            this.comboBoxmode.TabIndex = 13;
            this.comboBoxmode.SelectedIndexChanged += new System.EventHandler(this.comboBoxmode_SelectedIndexChanged);
            // 
            // StegoImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 444);
            this.Controls.Add(this.comboBoxmode);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.contentBox);
            this.Controls.Add(this.picBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "StegoImage";
            this.Text = "StegoImage";
            this.Load += new System.EventHandler(this.StegoImage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.TextBox contentBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button loadText;
        private System.Windows.Forms.Button loadImage;
        private System.Windows.Forms.TextBox txtBox;
        private System.Windows.Forms.TextBox imgBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.ComboBox comboBoxmode;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

