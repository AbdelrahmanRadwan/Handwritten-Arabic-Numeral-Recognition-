namespace ReadingMNISTDatabase
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
            this.Read = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_Show = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.chckBx_Threshold = new System.Windows.Forms.CheckBox();
            this.rdoBtn_TrainingSet = new System.Windows.Forms.RadioButton();
            this.rdoBtn_TestingSet = new System.Windows.Forms.RadioButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // Read
            // 
            this.Read.Location = new System.Drawing.Point(24, 11);
            this.Read.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Read.Name = "Read";
            this.Read.Size = new System.Drawing.Size(163, 37);
            this.Read.TabIndex = 0;
            this.Read.Text = "Read Database";
            this.Read.UseVisualStyleBackColor = true;
            this.Read.Click += new System.EventHandler(this.Read_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(372, 11);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(618, 241);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // btn_Show
            // 
            this.btn_Show.Location = new System.Drawing.Point(996, 11);
            this.btn_Show.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Show.Name = "btn_Show";
            this.btn_Show.Size = new System.Drawing.Size(167, 68);
            this.btn_Show.TabIndex = 2;
            this.btn_Show.Text = "Show";
            this.btn_Show.UseVisualStyleBackColor = true;
            this.btn_Show.Click += new System.EventHandler(this.btn_Show_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(24, 76);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(99, 22);
            this.numericUpDown1.TabIndex = 3;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(315, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 17);
            this.label1.TabIndex = 4;
            // 
            // chckBx_Threshold
            // 
            this.chckBx_Threshold.AutoSize = true;
            this.chckBx_Threshold.Location = new System.Drawing.Point(24, 176);
            this.chckBx_Threshold.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chckBx_Threshold.Name = "chckBx_Threshold";
            this.chckBx_Threshold.Size = new System.Drawing.Size(121, 21);
            this.chckBx_Threshold.TabIndex = 5;
            this.chckBx_Threshold.Text = "With threshold";
            this.chckBx_Threshold.UseVisualStyleBackColor = true;
            // 
            // rdoBtn_TrainingSet
            // 
            this.rdoBtn_TrainingSet.AutoSize = true;
            this.rdoBtn_TrainingSet.Checked = true;
            this.rdoBtn_TrainingSet.Location = new System.Drawing.Point(24, 112);
            this.rdoBtn_TrainingSet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rdoBtn_TrainingSet.Name = "rdoBtn_TrainingSet";
            this.rdoBtn_TrainingSet.Size = new System.Drawing.Size(102, 21);
            this.rdoBtn_TrainingSet.TabIndex = 6;
            this.rdoBtn_TrainingSet.TabStop = true;
            this.rdoBtn_TrainingSet.Text = "TrainingSet";
            this.rdoBtn_TrainingSet.UseVisualStyleBackColor = true;
            // 
            // rdoBtn_TestingSet
            // 
            this.rdoBtn_TestingSet.AutoSize = true;
            this.rdoBtn_TestingSet.Location = new System.Drawing.Point(24, 139);
            this.rdoBtn_TestingSet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rdoBtn_TestingSet.Name = "rdoBtn_TestingSet";
            this.rdoBtn_TestingSet.Size = new System.Drawing.Size(97, 21);
            this.rdoBtn_TestingSet.TabIndex = 7;
            this.rdoBtn_TestingSet.Text = "TestingSet";
            this.rdoBtn_TestingSet.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(24, 257);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1332, 458);
            this.dataGridView1.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(996, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(224, 69);
            this.label2.TabIndex = 9;
            this.label2.Text = "label2";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(125, 215);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(124, 22);
            this.textBox1.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 218);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Test Instants";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1368, 740);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.rdoBtn_TestingSet);
            this.Controls.Add(this.rdoBtn_TrainingSet);
            this.Controls.Add(this.chckBx_Threshold);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.btn_Show);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Read);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Read;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_Show;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chckBx_Threshold;
        private System.Windows.Forms.RadioButton rdoBtn_TrainingSet;
        private System.Windows.Forms.RadioButton rdoBtn_TestingSet;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
    }
}

