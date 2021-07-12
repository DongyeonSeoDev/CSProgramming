using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShootingGame
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 0: Right, 1: Left, 2: Up, 3: Down
        /// </summary>
        private bool[] isMove = new bool[4] { false, false, false, false };
        
        private List<Position> bulletPosition = new List<Position>();
        private List<Position> enemyPosition = new List<Position>();

        private Random random = new Random();

        private int speed = 7;
        private int tickCount = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right: isMove[0] = true; return;
                case Keys.Left: isMove[1] = true; return;
                case Keys.Up: isMove[2] = true; return;
                case Keys.Down: isMove[3] = true; return;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right: isMove[0] = false; return;
                case Keys.Left: isMove[1] = false; return;
                case Keys.Up: isMove[2] = false; return;
                case Keys.Down: isMove[3] = false; return;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isMove[0])
            {
                MoveRight();
            }

            if (isMove[1])
            {
                MoveLeft();
            }

            if (isMove[2])
            {
                MoveUp();
            }

            if (isMove[3])
            {
                MoveDown();
            }

            foreach (Position bullet in bulletPosition)
            {
                bullet.y -= speed;
            }

            foreach (Position enemy in enemyPosition)
            {
                enemy.y += speed;
            }

            bool removeBullet = false;

            do
            {
                removeBullet = false;

                foreach (Position bullet in bulletPosition)
                {
                    if (bullet.y < 0)
                    {
                        removeBullet = true;
                        bulletPosition.Remove(bullet);
                        break;
                    }
                }

            } while (removeBullet);

            bool removeEnemy = false;

            do
            {
                removeEnemy = false;

                foreach (Position enemy in enemyPosition)
                {
                    if (enemy.y > 900)
                    {
                        removeEnemy = true;
                        enemyPosition.Remove(enemy);
                        break;
                    }

                    if (Player.Location.X - 50 <= enemy.x && Player.Location.X + 50 >= enemy.x && Player.Location.Y - 50 <= enemy.y && Player.Location.Y + 50 >= enemy.y)
                    {
                        GameOver();
                    }

                    foreach (Position bullet in bulletPosition) 
                    {
                        if (bullet.x - 50 <= enemy.x && bullet.x + 10 >= enemy.x && bullet.y - 50 <= enemy.y && bullet.y + 10 >= enemy.y)
                        {
                            removeEnemy = true;
                            enemyPosition.Remove(enemy);
                            bulletPosition.Remove(bullet);
                            break;
                        }
                    }

                    if (removeEnemy) break;
                }

            } while (removeEnemy);

            tickCount++;

            if (tickCount % 10 == 0)
            {
                bulletPosition.Add(new Position(Player.Location.X + 20, Player.Location.Y));
            }

            if (tickCount == 50)
            {
                enemyPosition.Add(new Position(random.Next(0, 430), -50));
                tickCount = 0;
            }

            Invalidate();
        }

        private void GameOver()
        {
            timer1.Enabled = false;

            if (DialogResult.OK == MessageBox.Show("게임이 자동으로 종료됩니다.", "게임오버", MessageBoxButtons.OK))
            {
                this.Close();
            }
        }

        private void MoveRight()
        {
            if (Player.Location.X > 430) return;

            Player.Location = new Point(Player.Location.X + speed, Player.Location.Y);
            Invalidate();
        }

        private void MoveLeft()
        {
            if (Player.Location.X < 2) return;

            Player.Location = new Point(Player.Location.X - speed, Player.Location.Y);
            Invalidate();
        }

        private void MoveUp()
        {
            if (Player.Location.Y < 2) return;

            Player.Location = new Point(Player.Location.X, Player.Location.Y - speed);
            Invalidate();
        }

        private void MoveDown()
        {
            if (Player.Location.Y > 800) return;

            Player.Location = new Point(Player.Location.X, Player.Location.Y + speed);
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawBoard(e.Graphics);
        }

        private void DrawBoard(Graphics graphics)
        {
            foreach (Position bullet in bulletPosition)
            {
                Rectangle now_rt = new Rectangle(bullet.x, bullet.y, 10, 10);
                graphics.FillRectangle(Brushes.Green, now_rt);
            }

            foreach (Position enemy in enemyPosition)
            {
                Rectangle now_rt = new Rectangle(enemy.x, enemy.y, 50, 50);
                graphics.FillRectangle(Brushes.Red, now_rt);
            }
        }
    }
}
