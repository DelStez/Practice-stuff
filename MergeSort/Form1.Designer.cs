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
            this.showContentBoxMain.Location = new System.Drawing.Point(12, 57);
            this.showContentBoxMain.Multiline = true;
            this.showContentBoxMain.Name = "showContentBoxMain";
            this.showContentBoxMain.ReadOnly = true;
            this.showContentBoxMain.Size = new System.Drawing.Size(246, 180);
            this.showContentBoxMain.TabIndex = 1;
            // 
            // showContentBoxMain1
            // 
            this.showContentBoxMain1.Location = new System.Drawing.Point(264, 57);
            this.showContentBoxMain1.Multiline = true;
            this.showContentBoxMain1.Name = "showContentBoxMain1";
            this.showContentBoxMain1.ReadOnly = true;
            this.showContentBoxMain1.Size = new System.Drawing.Size(246, 180);
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
            this.startButton.Location = new System.Drawing.Point(12, 243);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(495, 23);
            this.startButton.TabIndex = 5;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 277);
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

        private System.Windows.Forms.Button startButton;

        private System.Windows.Forms.TextBox showContentBoxMain1;

        private System.Windows.Forms.TextBox showContentBoxMain;

        private System.Windows.Forms.TextBox textMainFilePath;

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;

        private System.Windows.Forms.TextBox textBox3;

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;

        #endregion
    }
}