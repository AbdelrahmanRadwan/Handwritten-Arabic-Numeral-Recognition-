using ReadingMNISTDatabase.Bayesian_Classifier.Test;
using ReadingMNISTDatabase.Bayesian_Classifier.Train;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReadingMNISTDatabase
{
    public partial class Form1 : Form
    {
        //MNIST Data set
        MNIST_Database _MnistTrainingDatabase;
        MNIST_Database _MinstTestingDatabase;
        BuildingTheModule TrainModule;
        TestingTheModule TheTester;
        int[] Output = new int[10];

        public Form1()
        {
            InitializeComponent();
            _MnistTrainingDatabase = new MNIST_Database();
            _MinstTestingDatabase = new MNIST_Database();
        }


        public void RepresentTheOutput()
        {

            label2.Text = " Accuracy Is:  " + TheTester.Accuracy.ToString() + " % ";
            dataGridView1.ColumnCount = 10;
            dataGridView1.Columns[0].Name = "~{0}~";
            dataGridView1.Columns[1].Name = "~{1}~";
            dataGridView1.Columns[2].Name = "~{2}~";
            dataGridView1.Columns[3].Name = "~{3}~";
            dataGridView1.Columns[4].Name = "~{4}~";
            dataGridView1.Columns[5].Name = "~{5}~";
            dataGridView1.Columns[6].Name = "~{6}~";
            dataGridView1.Columns[7].Name = "~{7}~";
            dataGridView1.Columns[8].Name = "~{8}~";
            dataGridView1.Columns[9].Name = "~{9}~";



            string[] row = new string[10];
            for (int i = 0; i < TheTester.NumberOfClasses; i++)
            {
                for (int j = 0; j < TheTester.NumberOfClasses; j++)
                {
                    row[j] = TheTester.ConfusionMatrix[i, j].ToString();
                }
                dataGridView1.Rows.Add(row);
            }
            dataGridView1.Show();
            label2.Show();
        }

        private void Read_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Add the Train File Please.\n");
            _MnistTrainingDatabase.LoadMinstFiles();
            TrainModule = new BuildingTheModule(_MnistTrainingDatabase);
            MessageBox.Show("Wait For the Training please.\n");
            TrainModule.CreateTheModel();//Start The training
            MessageBox.Show("Congratulations, Training done successfully.\n");
            MessageBox.Show("Add the Test File Please.\n");
            _MinstTestingDatabase.LoadMinstFiles();
            TheTester = new TestingTheModule(_MinstTestingDatabase, TrainModule);
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
                TheTester.TestingInstants = Math.Min(TheTester.TestingInstants, Convert.ToInt32(textBox1.Text));
            
            MessageBox.Show("Wait For the Testing of the "+TheTester.TestingInstants.ToString()+" testing instants please.\n");
            TheTester.TestTheModule();//Start The Test
            MessageBox.Show("Congratulations, Testing done successfully.\n");
            RepresentTheOutput();
        }

        private void btn_Show_Click(object sender, EventArgs e)
        {
            Bitmap Bmap = new Bitmap(28, 28);
            int End = 28;
            int i = 0, j = 0, k = 0;
            byte PixelValue;
            ImagePattern IP = null;
            if (rdoBtn_TrainingSet.Checked)
                IP = _MnistTrainingDatabase.m_pImagePatterns[(int)(numericUpDown1.Value - 1)];
            else if (rdoBtn_TestingSet.Checked)
                IP = _MinstTestingDatabase.m_pImagePatterns[(int)(numericUpDown1.Value - 1)];
            while (i < 28)
            {
                k = 0;
                for (j = i * 28; j < End; j++)
                {
                    PixelValue = IP.pPattern[j];
                    if (chckBx_Threshold.Checked && PixelValue < 255)
                        PixelValue = 0;
                    Bmap.SetPixel(k, i, Color.FromArgb(PixelValue, PixelValue, PixelValue));
                    label1.Text = IP.nLabel.ToString();
                    k++;
                }
                i++;
                End = (i + 1) * 28;
            }
            pictureBox1.Image = (Image)Bmap;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Hide();
        }
    }
}
