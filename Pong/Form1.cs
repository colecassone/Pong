using System;
using System.Drawing;
using System.Media;
using System.Threading;
using System.Windows.Forms;

namespace Pong
{
    public partial class Form1 : Form
    {
        
        Rectangle player1 = new Rectangle(132, 400, 30, 15);
        Rectangle player2 = new Rectangle(132, 100, 30, 15);
       
        Rectangle ball = new Rectangle(132, 216, 10, 10); 
      
        Rectangle goal1 = new Rectangle(112, 426, 40, 10);
        Rectangle goal2 = new Rectangle(112, -5, 40, 10);

        SoundPlayer ping = new SoundPlayer(Properties.Resources.ping);

        int player1Score = 0;
        int player2Score = 0;

        int playerSpeed = 3;
        int ballXSpeed = -5;
        int ballYSpeed = 5;

        bool wDown = false;
        bool sDown = false;
        bool aDown = false;
        bool dDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool leftArrowDown = false;
        bool rightArrowDown = false;

        int escDown = 0; 

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        
        Pen drawPen = new Pen(Color.Red, 5);

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // this is setting checking what keys are down. 
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Escape:
                    // this checks to see if the game is playing or not when esc it pressed then will start or stop the game engine
                    if (gameTimer.Enabled == true)
                    {
                        gameTimer.Stop();
                    }
                    else
                    {
                        gameTimer.Start();
                    }
                    break;
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Escape:
                    escDown = 0;
                    break;
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // this draws the game. 
            e.Graphics.DrawLine(drawPen, 0, 216, 400, 216);
            e.Graphics.FillRectangle(blueBrush, player1);
            e.Graphics.FillRectangle(blueBrush, player2);
            e.Graphics.FillRectangle(whiteBrush, ball);
            e.Graphics.DrawRectangle(drawPen, goal2);
            e.Graphics.DrawRectangle(drawPen, goal1);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {

            ////move ball 
            ball.X += ballXSpeed;
            ball.Y += ballYSpeed;

            ////move player 1 
            if (wDown == true && player1.Y > 220 && !player1.IntersectsWith(goal1))
            {
                player1.Y -= playerSpeed;
            }
            if (sDown == true && player1.Y < this.Height - player1.Height)
            {
                int x = player1.X;
                int y = player1.Y;

                player1.Y += playerSpeed;

                if (player1.IntersectsWith(goal1))
                {
                    player1.X = x;
                    player1.Y = y;
                }
            }

            if (dDown == true && player1.X < this.Width - player1.Width)
            {
                int x = player1.X;
                int y = player1.Y;

                player1.X += playerSpeed;

                if (player1.IntersectsWith(goal1))
                {
                    player1.X = x;
                    player1.Y = y;
                }
            }
            if (aDown == true && player1.X > 0)
            {
                int x = player1.X;
                int y = player1.Y;

                player1.X -= playerSpeed;

                if (player1.IntersectsWith(goal1))
                {
                    player1.X = x;
                    player1.Y = y;
                }
                
            }
            ////move player 2 
            if (upArrowDown == true && player2.Y > 0 && !player2.IntersectsWith(goal2))
            {

                int x = player2.X;
                int y = player2.Y;

                player2.Y -= playerSpeed;

                if (player2.IntersectsWith(goal2))
                {
                    player2.X = x;
                    player2.Y = y;
                }
            }

            if (downArrowDown == true && player2.Y < 200)
            {
                int x = player2.X;
                int y = player2.Y;

                player2.Y += playerSpeed;

                if (player2.IntersectsWith(goal2))
                {
                    player2.X = x;
                    player2.Y = y;
                }
            }
            if (rightArrowDown == true && player2.X < this.Width - player2.Width)
            {
                int x = player2.X;
                int y = player2.Y;

                player2.X += playerSpeed;

                if (player2.IntersectsWith(goal2))
                {
                    player2.X = x;
                    player2.Y = y;
                }               
            }
            if (leftArrowDown == true && player2.X > 0)
            {
                int x = player2.X;
                int y = player2.Y;
                player2.X -= playerSpeed;

                if (player2.IntersectsWith(goal2))
                {
                    player2.X = x;
                    player2.Y = y;
                }
              
            }
 ////check if ball hit top or bottom wall  or back wall or the sides and change direction if it does 
            if (ball.Y < 0 || ball.Y > this.Height - ball.Height)
            {
                ballYSpeed *= -1;
                ;
            }

            if (ball.X < 0 || ball.X > this.Width - ball.Width)
            {
                ballXSpeed *= -1;

            }
  ////check if ball hits either player. If it does change the direction 
            ////and place the ball in front of the player hit 
            // check where the ball hit on the padle 
            // then where it hits change the angle and speed of the ball
            if (player1.IntersectsWith(ball))
            {
                ping.Play();

                Rectangle player1left = new Rectangle(player1.X, player1.Y, 5, player1.Height);
                Rectangle player1center = new Rectangle(player1.X + 10, player1.Y, 20, player1.Height);
                Rectangle player1right = new Rectangle(player1.X + 10, player1.Y, 10, player1.Height);
                if (ballYSpeed > 0)
                {
                    if (player1left.IntersectsWith(ball))
                    {
                        ballXSpeed *= 2;
                        ballYSpeed *= -1;
                        ball.Y = player1.Y - ball.Height - 2;
                    }
                    else if (player1right.IntersectsWith(ball))
                    {
                        ballXSpeed *= 2;
                        ballYSpeed *= -1;
                        ball.Y = player1.Y - ball.Height - 2;
                    }

                    else
                    {
                        ballXSpeed = -5;
                        ballYSpeed *= -1;
                        ball.Y = player1.Y - ball.Height - 2;
                    }
                }
                else
                {
                    if (player1left.IntersectsWith(ball))
                    {
                        ballXSpeed *= 2;
                        ballYSpeed *= -1;
                        ball.Y = player1.Y + ball.Height;
                    }
                    else if (player1right.IntersectsWith(ball))
                    {
                        ballXSpeed *= 2;
                        ballYSpeed *= -1;
                        ball.Y = player1.Y + ball.Height;
                    }
                    else
                    {
                        ballXSpeed = -5;
                        ballYSpeed *= -1;
                        ball.Y = player1.Y + ball.Height;
                    }
                }

            }
            if (player2.IntersectsWith(ball))
            {
                ping.Play();

                Rectangle player2left = new Rectangle(player2.X, player2.Y, 5, player2.Height);
                Rectangle player2center = new Rectangle(player2.X + 10, player2.Y, 20, player2.Height);
                Rectangle player2right = new Rectangle(player2.X + 10, player2.Y, 10, player2.Height);
                if (ballYSpeed > 0)
                {
                    if (player2left.IntersectsWith(ball))
                    {
                        ballXSpeed *= 2;
                        ballYSpeed *= -1;
                        ball.Y = player2.Y - ball.Height - 2;
                    }
                    else if (player2right.IntersectsWith(ball))
                    {
                        ballXSpeed *= 2;
                        ballYSpeed *= -1;
                        ball.Y = player2.Y - ball.Height - 2;
                    }

                    else
                    {
                        ballXSpeed = -5;
                        ballYSpeed *= -1;
                        ball.Y = player2.Y - ball.Height - 2;
                    }
                }
                else
                {
                    if (player2left.IntersectsWith(ball))
                    {
                        ballXSpeed *= 2;
                        ballYSpeed *= -1;
                        ball.Y = player2.Y + ball.Height;
                    }
                    else if (player2right.IntersectsWith(ball))
                    {
                        ballXSpeed *= 2;
                        ballYSpeed *= -1;
                        ball.Y = player2.Y + ball.Height;
                    }

                    else
                    {
                        ballXSpeed = -5;
                        ballYSpeed *= -1;
                        ball.Y = player2.Y + ball.Height;
                    }
                }

            }
            /// check too see if anyone has scored
            /// if they do add to the score
            if (ball.IntersectsWith(goal1))
            {
                player2Score++;
                team2Score.Text = $"{player2Score}";
                if (player2Score == 1)
                {
                    team1Score.Text = "W";
                    team2Score.Text = "L";
                    player1Score = 0;
                    player2Score = 0;
                    this.Refresh();
                    Thread.Sleep(2000);
                    team1Score.Text = "0";
                    team2Score.Text = "0";
                    ballXSpeed = -5;
                }

                this.Refresh();
                Thread.Sleep(2000);
                ball.X = 132;
                ball.Y = 216;
               
            }
            if (ball.IntersectsWith(goal2))
            {
                player1Score++;
                team1Score.Text = $"{player1Score}";
                if (player1Score == 1)
                {
                    team1Score.Text = "L";
                    team2Score.Text = "W";
                    player1Score = 0;
                    player2Score = 0;
                    this.Refresh();
                    Thread.Sleep(2000);
                    team1Score.Text = "0";
                    team2Score.Text = "0";
                    ballXSpeed = -5; 
                }
                this.Refresh();
                Thread.Sleep(2000);
                ball.X = 132;
                ball.Y = 216;



            }

            Refresh();


        }






    }
}
