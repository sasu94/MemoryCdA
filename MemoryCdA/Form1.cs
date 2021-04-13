using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryCdA
{
    public partial class Form1 : Form
    {
        private readonly Dictionary<string, string> bindings = new Dictionary<string, string> {
            { "pictureBox1Label", "pictureBox6Label"},
            { "pictureBox3Label", "pictureBox2Label"},
            { "pictureBox7Label", "pictureBox8Label"},
            { "pictureBox5Label", "pictureBox4Label"},
            { "pictureBox6Label", "pictureBox1Label"},
            { "pictureBox2Label", "pictureBox3Label"},
            { "pictureBox8Label", "pictureBox7Label"},
            { "pictureBox4Label", "pictureBox5Label"}
        };
        bool play = false;
        Label firstLabel;
        PictureBox firstPicture;
        int seconds = 0;

        int found = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (play)
            {
                var box = sender as PictureBox;
                var label = this.Controls[box.Name + "Label"] as Label;
                box.Visible = false;
                label.Visible = true;

                if (firstPicture == null)
                {
                    firstPicture = box;
                    firstLabel = label;
                }
                else
                {
                    var otherTag = bindings[firstLabel.Tag.ToString()];

                    if (otherTag == label.Tag.ToString())
                    {
                        MessageBox.Show("Esatto!");
                        found++;
                    }
                    else
                    {
                        play = false;

                        MessageBox.Show("Ritenta");

                        Thread.Sleep(1000);

                        box.Visible = true;
                        label.Visible = false;

                        firstPicture.Visible = true;
                        firstLabel.Visible = false;



                        play = true;

                    }

                    firstPicture = null;
                    firstLabel = null;

                    if (found == 4)
                    {
                        timer1.Stop();
                        MessageBox.Show("Complimenti, hai trovato tutte le combinazioni in " + seconds + " secondi!");
                        plsPausa.Enabled = false;
                        play = false;
                    }
                }
            }

        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            var box = sender as PictureBox;
            box.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            var box = sender as PictureBox;
            box.BorderStyle = BorderStyle.None;
        }

        private void PlsPlay_Click(object sender, EventArgs e)
        {
            play = true;
            timer1.Start();
            plsPlay.Enabled = false;
            plsPausa.Enabled = plsStop.Enabled = true;

            foreach (var pb in this.Controls.OfType<PictureBox>())
            {
                pb.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            seconds++;
            secondi.Text = $"{seconds} secondi";
        }

        private void plsPausa_Click(object sender, EventArgs e)
        {
            play = false;
            timer1.Stop();
            plsPlay.Enabled = true;
            plsPausa.Enabled = plsStop.Enabled = false;
        }

        private void plsStop_Click(object sender, EventArgs e)
        {

            ResetGame();
        }

        private void ResetGame()
        {
            play = false;
            timer1.Stop();
            seconds = 0;
            secondi.Text = $"{seconds} secondi";
            plsPlay.Enabled = true;
            plsPausa.Enabled = plsStop.Enabled = false;
            foreach (var control in Controls)
            {
                if (control is Label label && label.Tag != null && label.Tag.ToString().Contains("picture"))
                {
                    label.Visible = false;
                }
                else if (control is PictureBox box)
                {
                    box.Enabled = false;
                    box.Visible = true;
                }
            }
        }
    }
}
