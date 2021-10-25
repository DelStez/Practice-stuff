namespace MergeSort
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textMainFilePath = new System.Windows.Forms.TextBox();
            this.showContentBoxMain = new System.Windows.Forms.TextBox();
            this.showContentBoxMain1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textMainFilePath
            // 
            this.textMainFilePath.Location = new System.Drawing.Point(95, 12);
            this.textMainFilePath.Name = "textMainFilePath";
            this.textMainFilePath.ReadOnly = true;
            this.textMainFilePath.Size = new System.Drawing.Size(332, 20);
            this.textMainFilePath.TabIndex = 0;
            // 
            // showContentBoxMain
            // 
            this.showContentBoxMain.Location = new System.Drawing.Point(12, 76);
            this.showContentBoxMain.Multiline = true;
            this.showContentBoxMain.Name = "showContentBoxMain";
            this.showContentBoxMain.ReadOnly = true;
            this.showContentBoxMain.Size = new System.Drawing.Size(246, 66);
            this.showContentBoxMain.TabIndex = 1;
            // 
            // showContentBoxMain1
            // 
            this.showContentBoxMain1.Location = new System.Drawing.Point(264, 76);
            this.showContentBoxMain1.Multiline = true;
            this.showContentBoxMain1.Name = "showContentBoxMain1";
            this.showContentBoxMain1.ReadOnly = true;
            this.showContentBoxMain1.Size = new System.Drawing.Size(246, 66);
            this.showContentBoxMain1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(435, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.loadFileButton_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "Загрузить";
            // 
            // startButton
            // 
            this.startButton.Enabled = false;
            this.startButton.Location = new System.Drawing.Point(12, 182);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(246, 66);
            this.startButton.TabIndex = 5;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(264, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "2-way:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(264, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 14);
            this.label3.TabIndex = 9;
            this.label3.Text = "3-way:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(264, 182);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(246, 66);
            this.textBox1.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(373, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 14);
            this.label4.TabIndex = 10;
            this.label4.Text = "Time:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(373, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 14);
            this.label5.TabIndex = 11;
            this.label5.Text = "Time;";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(430, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 14);
            this.label6.TabIndex = 12;
            this.label6.Text = "-";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(435, 165);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 14);
            this.label7.TabIndex = 13;
            this.label7.Text = "-";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 257);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.showContentBoxMain1);
            this.Controls.Add(this.showContentBoxMain);
            this.Controls.Add(this.textMainFilePath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "MergeSort";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;

        private System.Windows.Forms.Button startButton;

        private System.Windows.Forms.TextBox showContentBoxMain1;

        private System.Windows.Forms.TextBox showContentBoxMain;

        private System.Windows.Forms.TextBox textMainFilePath;

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;

        private System.Windows.Forms.TextBox textBox1;

        #endregion
    }
}