using Emgu.CV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace РО
{
    public partial class Form1 : Form
    {
        private VideoCapture capture = null;

        private double frames;

        private double framesCounter;

        private double fps;

        private bool play = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult res = openFileDialog1.ShowDialog();

                if (res == DialogResult.OK)
                {
                    capture = new VideoCapture(openFileDialog1.FileName);

                    Mat m = new Mat();

                    capture.Read(m);

                    pictureBox1.Image = m.Bitmap;

                    fps = capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps);

                    frames = capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameCount);

                    framesCounter = 1;
                }
                else
                {
                    MessageBox.Show("Video has not choosen!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ReadFrames()
        {
            Mat m = new Mat();

            while(play && framesCounter < frames)
            {
                framesCounter++;
                capture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosFrames, framesCounter);
                capture.Read(m);

                pictureBox1.Image = m.Bitmap;
                //pictureBox2.Image = обработнная картинки

                toolStripLabel1.Text = $"{framesCounter}/{frames}";

                await Task.Delay(10000 / Convert.ToInt32(fps));
            }
        }

        private void распознатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (capture == null)
                    throw new Exception("Video has not choosen!");

                play = true;

                ReadFrames();

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                play = false;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (capture == null)
                    throw new Exception("Video has not choosen!");
                play = true;
                ReadFrames();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                framesCounter-=Convert.ToDouble(numericUpDown1.Value);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            try
            {

                framesCounter += Convert.ToDouble(numericUpDown1.Value);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
