using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperheroMemoryGame
{
    public partial class Form1 : Form
    {
        bool allowClick = false; //Boolean allowClick means you can not click picture box
        PictureBox firstGuess;
        Random rnd = new Random();
        Timer clickTimer = new Timer(); //new instance of random number generator class
        int time = 60; //time to play the game
        Timer timer = new Timer { Interval = 1000 }; //time interval for timer
         

        public Form1()
        {
            InitializeComponent(); 
        }

        private PictureBox[] pictureBoxes
        {
            get { return Controls.OfType<PictureBox>().ToArray(); } //adds picture boxes to an array
        }

        private static IEnumerable<Image> images //creates access to query images, links images from resources
        {
            get
            {
                return new Image[] //returns a new image from the array
                {
                    Properties.Resources.img1,
                    Properties.Resources.img2,
                    Properties.Resources.img3,
                    Properties.Resources.img4,
                    Properties.Resources.img5,
                    Properties.Resources.img6,
                    Properties.Resources.img7,
                    Properties.Resources.img8,
                };
            }
        }

        private PictureBox[] pictureBoxes2
        {
            get { return Controls.OfType<PictureBox>().ToArray(); } //adds picture boxes to an array
        }

        private static IEnumerable<Image> images2 //creates access to query images, links images from resources
        {
            get
            {
                return new Image[] //returns a new image from the array
                {
                    Properties.Resources.img9,
                    Properties.Resources.img10,
                    Properties.Resources.img11,
                    Properties.Resources.img12,
                    Properties.Resources.img13,
                    Properties.Resources.img14,
                    Properties.Resources.img15,
                    Properties.Resources.img16,
                };
            }
        }

        private void startGameTimer() //starts timer and uses if statement for 
        {
            timer.Start();
            timer.Tick += delegate
            {
                time--;
                if (time < 0) //if player runs out of time, a message box displays out of time
                {
                    timer.Stop();
                    MessageBox.Show("Out of time");
                    ResetImages();
                }

                var ssTime = TimeSpan.FromSeconds(time); //time in seconds
                label1.Text = "00: " + time.ToString();
            };
        }

            private void ResetImages() //resets picture boxes, allows player to play again
            {
                foreach (var pic in pictureBoxes) 
                {
                    pic.Tag = null; 
                    pic.Visible = true; //makes pictures visible again
                }

                HideImages(); //calls function we created
                setRandomImages(); //calls function we created
                time = 60; 
                timer.Start(); //starts timer again
            }

            private void HideImages() //changes pictures to ? 
            {
                foreach (var pic in pictureBoxes)
                {
                    pic.Image = Properties.Resources.question;
                }
            }

            private PictureBox getFreeSlot() //
            {
                int num;

            do
            {
                num = rnd.Next(0, pictureBoxes.Count());
            }     

                while (pictureBoxes[num].Tag != null);
                return pictureBoxes[num]; 
            }

        private void setRandomImages()
        {
            if (imageSelector.Text.Contains("1")) 
            {
                foreach (var image in images)
                {
                    getFreeSlot().Tag = image;
                    getFreeSlot().Tag = image;
                }
            }
            else if (imageSelector.Text.Contains("2"))
            {
                foreach (var image in images2)
                {
                    getFreeSlot().Tag = image;
                    getFreeSlot().Tag = image;
                }
            }


        }
            private void CLICKTIMER_TICK(object sender, EventArgs e)
            {
                HideImages();
                allowClick = true;
                clickTimer.Stop(); 

            }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void clickImage(object sender, EventArgs e)
        {
            if (!allowClick) return;
            var pic = (PictureBox)sender;
            if (firstGuess == null)
            {
                firstGuess = pic;
                pic.Image = (Image)pic.Tag;
                return; 
            }

            pic.Image = (Image)pic.Tag;
            if (pic.Image == firstGuess.Image && pic != firstGuess)
            {
                pic.Visible = firstGuess.Visible = false;
                { firstGuess = pic;  }
                HideImages(); 
            }

            else
            {
                allowClick = false;
                clickTimer.Start();
            }
            
            firstGuess = null;
            if (pictureBoxes.Any(p => p.Visible)) return;
            listBox1.Items.Add("" + label1.Text + " - " + textBox1.Text);
            MessageBox.Show("You win! Now try again :) ");
            ResetImages();
        }

        private void startGame(object sender, EventArgs e)
        {
            allowClick = true;
            setRandomImages();
            HideImages();
            startGameTimer();
            clickTimer.Interval = 1000;
            clickTimer.Tick += CLICKTIMER_TICK;
            btnStart.Enabled = false; 
        }

        private void instructionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 helpForm = new Form2();
            helpForm.Show(); 
            


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void entername(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) 
            {
                textBox1.Enabled = false; 
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text.Contains("30"))
            {
                time = 30; 
            }
            else if (comboBox1.Text.Contains("60"))
            {
                time = 60; 
            }
            else if (comboBox1.Text.Contains("90"))
            {
                time = 90; 
            }
            else if (comboBox1.Text.Contains("120"))
            {
                time = 120; 
            }
            
        }
    }
}
