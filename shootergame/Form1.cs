using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace shootergame
{
    public partial class Form1 : Form
    {
        //Create Media
        WindowsMediaPlayer gameMedia;
        WindowsMediaPlayer shootgMedia;
        WindowsMediaPlayer explosion;

        //For Munition Enemies
        PictureBox[] enemiesMunition;
        int enemiesMunitionSpeed;

        //Small stars in background
        PictureBox[] stars;
        int backgroundspeed;
        Random rnd;
        int playerSpeed;

        //For enemies
        PictureBox[] enemies;
        int enemispeed;

        //For Munition
        PictureBox[] munitions;
        int MunitionSpeed;
        int score;
        int dificulty;
        bool GameIsOver;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //for background
            backgroundspeed = 4;
            stars = new PictureBox[15];
            rnd = new Random();
            playerSpeed = 4;
            enemispeed = 4;    //Initialize Enemies Speed
            enemiesMunitionSpeed = 4;
            GameIsOver = false;
            score = 0;
            dificulty = 9;

            munitions = new PictureBox[3];
            MunitionSpeed = 20;

            //load images of munition
            Image munition = Image.FromFile(@"C:\Users\TECHgRO\Desktop\Game\munition.png");

            for (int i = 0; i < munitions.Length; i++)
            {
                munitions[i] = new PictureBox();
                munitions[i].Size = new Size(8, 8);
                munitions[i].Image = munition;
                munitions[i].SizeMode = PictureBoxSizeMode.Zoom;
                munitions[i].BorderStyle = BorderStyle.None;
                this.Controls.Add(munitions[i]);
            }

            //Load Images for Enemies
            Image enemi1 = Image.FromFile(@"C:\Users\TECHgRO\Desktop\Game\E1.png");
            Image enemi2 = Image.FromFile(@"C:\Users\TECHgRO\Desktop\Game\E2.png");
            Image enemi3 = Image.FromFile(@"C:\Users\TECHgRO\Desktop\Game\E3.png");
            Image boss1 = Image.FromFile(@"C:\Users\TECHgRO\Desktop\Game\boss1.png");
            Image boss2 = Image.FromFile(@"C:\Users\TECHgRO\Desktop\Game\boss2.png");

            enemies = new PictureBox[10];

            //initialization Enemies pictureBoxes
            for (int i = 0; i<enemies.Length; i++)
            {
                enemies[i] = new PictureBox();
                enemies[i].Size = new Size(40, 40);
                enemies[i].SizeMode = PictureBoxSizeMode.Zoom;
                enemies[i].BorderStyle = BorderStyle.None;
                enemies[i].Visible = false;
                this.Controls.Add(enemies[i]);
                enemies[i].Location = new Point((i + 1) * 50, -50);
            }

            enemies[0].Image = boss1;
            enemies[1].Image = enemi1;
            enemies[2].Image = enemi2;
            enemies[3].Image = enemi3;
            enemies[4].Image = enemi1;
            enemies[5].Image = enemi2;
            enemies[6].Image = enemi3;
            enemies[7].Image = enemi1;
            enemies[8].Image = enemi2;
            enemies[9].Image = boss2;

            

            //create Window media player
            gameMedia = new WindowsMediaPlayer();
            shootgMedia = new WindowsMediaPlayer();
            explosion = new WindowsMediaPlayer();

            //Load all songs
            gameMedia.URL = "C:\\Users\\TECHgRO\\Desktop\\Game\\GameSong.mp3";
            shootgMedia.URL = "C:\\Users\\TECHgRO\\Desktop\\Game\\shoot.mp3";
            explosion.URL = "C:\\Users\\TECHgRO\\Desktop\\Game\\boom.mp3";

            //setup songs setting
            gameMedia.settings.setMode("loop", true);
            gameMedia.settings.volume = 5;
            shootgMedia.settings.volume = 1;
            explosion.settings.volume = 6;
            gameMedia.controls.play();
            

            //Enemies Munition
            enemiesMunition = new PictureBox[10];
            for (int i =0; i<enemiesMunition.Length; i++)
            {
                enemiesMunition[i] = new PictureBox();
                enemiesMunition[i].Size = new Size(2, 25);
                enemiesMunition[i].Visible = false;
                enemiesMunition[i].BackColor = Color.White;
                int x = rnd.Next(0, 10);
                enemiesMunition[i].Location = new Point(enemies[x].Location.X, enemies[x].Location.Y - 20);
                this.Controls.Add(enemiesMunition[i]);
            }

            //for background design
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i] = new PictureBox();
                stars[i].BorderStyle = BorderStyle.None;
                stars[i].Location = new Point(rnd.Next(20, 580), rnd.Next(-10, 400));  //location is random (dynamic Position)
                
                //stars have two types
                if (i % 2 == 1)
                {
                    stars[i].Size = new Size(2, 2);
                    stars[i].BackColor = Color.Wheat;

                }

                else
                {
                    stars[i].Size = new Size(3, 3);
                    stars[i].BackColor = Color.DarkGray;
                }

                this.Controls.Add(stars[i]);
            }

        }

        private void MoveBgTimer_Tick(object sender, EventArgs e)     //For controlling Background Design
        {
            for (int i=0; i<stars.Length/2; i++)
            {
                stars[i].Top += backgroundspeed;
                if(stars[i].Top >= this.Height)
                {
                    stars[i].Top = -stars[i].Height;
                }
            }

            for (int i = stars.Length /2; i < stars.Length; i++)
            {
                stars[i].Top += backgroundspeed - 2;
                if (stars[i].Top >= this.Height)
                {
                    stars[i].Top = -stars[i].Height;
                }
            }

        }

        private void LeftMoveTimer_Tick(object sender, EventArgs e)     //For controlling Player
        {
            if (Player.Left > 10)
            {
                Player.Left -= playerSpeed;
            }

        }

        private void RightMoveTimer_Tick(object sender, EventArgs e)   //For controlling Player
        {
            if (Player.Right < 580)
            {
                Player.Left += playerSpeed;
            }

        }

        private void DownMoveTimer_Tick(object sender, EventArgs e)   //For controlling Player
        {
            if (Player.Top < 400)
            {
                Player.Top += playerSpeed;
            }

        }

        private void UpMoveTimer_Tick(object sender, EventArgs e)   //For controlling Player
        {
            if (Player.Top > 10)
            {
                Player.Top -= playerSpeed;
            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)   //Key Handling
        {
                if (e.KeyCode == Keys.Right)
                {
                    RightMoveTimer.Start();
                }
                if (e.KeyCode == Keys.Left)
                {
                    LeftMoveTimer.Start();
                }
                if (e.KeyCode == Keys.Down)
                {
                    DownMoveTimer.Start();
                }
                if (e.KeyCode == Keys.Up)
                {
                    UpMoveTimer.Start();
                }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)   //Key Handling
        {
            RightMoveTimer.Stop();
            LeftMoveTimer.Stop();
            DownMoveTimer.Stop();
            UpMoveTimer.Stop();
        }

        private void MoveMunitionTimer_Tick(object sender, EventArgs e)    //for controlling muntion
        {
            shootgMedia.controls.play();
            for (int i = 0; i < munitions.Length; i++)
            {
                if (munitions[i].Top > 0)
                {
                    munitions[i].Visible = true;
                    munitions[i].Top -= MunitionSpeed;
                    Collision();
                }
                else
                {
                    munitions[i].Visible = false;
                    munitions[i].Location = new Point(Player.Location.X + 20, Player.Location.Y - i * 30);
                }
                
            }
        }

        private void MoveEnemiesTimer_Tick(object sender, EventArgs e)    //For Moving Enemies
        {
            MoveEnemies(enemies, enemispeed);
        }

        private void MoveEnemies(PictureBox[] array, int speed)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i].Visible = true;
                array[i].Top += speed;
                if (array[i].Top > this.Height)
                {
                    array[i].Location = new Point((i + 1) * 50, -200);
                }
            }
        }

        private void Collision()
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (munitions[0].Bounds.IntersectsWith(enemies[i].Bounds) || munitions[1].Bounds.IntersectsWith(enemies[i].Bounds) || munitions[2].Bounds.IntersectsWith(enemies[i].Bounds))
                {
                    explosion.controls.play();
                    score += 1;
                    Scorelbl.Text = (score < 10) ? "Score: 0" + score.ToString() : "Score:" + score.ToString();

                    
                    if (enemispeed <= 10 && enemiesMunitionSpeed <=10 && dificulty >=0)
                    {
                        dificulty--;
                        enemispeed++;
                        enemiesMunitionSpeed++;
                    }

                    
                    enemies[i].Location = new Point((i + 1) * 50, -100);
                    
                }

                if (Player.Bounds.IntersectsWith(enemies[i].Bounds))
                {
                    explosion.settings.volume = 30;
                    explosion.controls.play();
                    Player.Visible = false;
                    GameOver("Game Over");
                }
            }
        }

        private void GameOver(string str)
        {
            label.Text = str;
            label.Location = new Point(120, 120);
            label.Visible = true;
            ReplayBtn.Visible = true;
            ExitBtn.Visible = true;

            gameMedia.controls.stop();
            StopTimers();
        }

        private void StopTimers()
        {
            MoveBgTimer.Stop();
            MoveEnemiesTimer.Stop();
            MoveMunitionTimer.Stop();
            EnemiesMunitionTimer.Stop();
        }

        private void StartTimers()
        {
            MoveBgTimer.Start();
            MoveEnemiesTimer.Start();
            MoveMunitionTimer.Start();
            EnemiesMunitionTimer.Start();
        }

        private void EnemiesMunitionTimer_Tick(object sender, EventArgs e)   //For controlling munition of enemies
        {
            for (int i = 0; i < (enemiesMunition.Length - dificulty); i++)
            {
                if(enemiesMunition[i].Top < this.Height)
                {
                    enemiesMunition[i].Visible = true;
                    enemiesMunition[i].Top += enemiesMunitionSpeed;
                    CollisionWithEnemiesMunition();
                }
                else
                {
                    enemiesMunition[i].Visible = false;
                    int x = rnd.Next(0, 10);
                    enemiesMunition[i].Location = new Point(enemies[x].Location.X + 20, enemies[x].Location.Y + 30);
                }
            }
        }

        private void CollisionWithEnemiesMunition()    //Player collide with enemies munition
        {
            for (int i = 0; i<enemiesMunition.Length; i++)
            {
                if (enemiesMunition[i].Bounds.IntersectsWith(Player.Bounds))
                {
                    enemiesMunition[i].Visible = false;
                    explosion.settings.volume = 30;
                    explosion.controls.play();
                    Player.Visible = false;
                    GameOver("Game Over");
                }
            }
        }

        private void Player_Click(object sender, EventArgs e)
        {

        }

        private void ReplayBtn_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponent();
            Form1_Load(e, e);
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }
    }
}
