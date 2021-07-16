using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        public static readonly string dirParameter = AppDomain.CurrentDomain.BaseDirectory + @"\file.txt";

        private List<Position> bulletPosition = new List<Position>();
        private List<Enemy> enemys = new List<Enemy>();
        private List<Position> enemyBullets = new List<Position>();
        private List<Position> items = new List<Position>();

        private Random random = new Random();

        private int speed = 7;
        private int enemySpeed = 5;
        private int tickCount = 0;
        private int score = 0;
        private int highScore = 0;
        private int damage = 1;

        public Form1()
        {
            InitializeComponent();
            LoadFile();
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

            foreach (Enemy enemy in enemys)
            {
                enemy.y += enemySpeed;
            }

            foreach (Position enemyBullet in enemyBullets)
            {
                enemyBullet.y += enemySpeed + 2;
            }

            foreach (Position item in items)
            {
                item.y += speed;
            }

            bool remove;

            do
            {
                remove = false;

                foreach (Position bullet in bulletPosition)
                {
                    if (bullet.y < 0)
                    {
                        remove = true;
                        bulletPosition.Remove(bullet);
                        break;
                    }
                }

            } while (remove);

            do
            {
                remove = false;

                foreach (Position enemyBullet in enemyBullets)
                {
                    if (enemyBullet.y > 900)
                    {
                        remove = true;
                        enemyBullets.Remove(enemyBullet);
                        break;
                    }

                    if (Player.Location.X - 10 <= enemyBullet.x && Player.Location.X + 50 >= enemyBullet.x && Player.Location.Y - 10 <= enemyBullet.y && Player.Location.Y + 50 >= enemyBullet.y)
                    {
                        GameOver();
                    }

                    foreach (Position bullet in bulletPosition)
                    {
                        if (bullet.x - 10 <= enemyBullet.x && bullet.x + 10 >= enemyBullet.x && bullet.y - 10 <= enemyBullet.y && bullet.y + 10 >= enemyBullet.y)
                        {
                            remove = true;
                            bulletPosition.Remove(bullet);
                            enemyBullets.Remove(enemyBullet);

                            break;
                        }
                    }

                    if (remove) break;
                }
                
            } while (remove);

            do
            {
                remove = false;

                foreach (Enemy enemy in enemys)
                {
                    if (enemy.y > 900)
                    {
                        remove = true;
                        enemys.Remove(enemy);
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
                            remove = true;
                            bulletPosition.Remove(bullet);

                            if (enemy.IsDie(damage))
                            {
                                enemys.Remove(enemy);

                                score += 100;

                                if (highScore < score)
                                {
                                    highScore = score;
                                    SaveFile();

                                    label2.Text = "HIGHSCORE: " + highScore;
                                }

                                label1.Text = "SCORE: " + score;
                            }

                            break;
                        }
                    }

                    if (remove) break;
                }

            } while (remove);

            do
            {
                remove = false;

                foreach (Position item in items)
                {
                    if (item.y > 900)
                    {
                        remove = true;
                        items.Remove(item);
                        break;
                    }

                    if (Player.Location.X - 30 <= item.x && Player.Location.X + 50 >= item.x && Player.Location.Y - 30 <= item.y && Player.Location.Y + 50 >= item.y)
                    {
                        damage++;

                        remove = true;
                        items.Remove(item);
                        break;
                    }

                    if (remove) break;
                }

            } while (remove);

            tickCount++;

            if (tickCount % 10 == 0)
            {
                bulletPosition.Add(new Position(Player.Location.X + 20, Player.Location.Y));
            }

            if (tickCount % 30 == 0)
            {
                foreach (Enemy enemy in enemys)
                {
                    enemyBullets.Add(new Position(enemy.x + 20, enemy.y));
                }
            }

            if (tickCount % (50 - enemySpeed * 3) == 0)
            {
                enemys.Add(new Enemy(random.Next(0, 430), -50, enemySpeed - 2));
            }
            
            if (tickCount % 300 == 0)
            {
                enemySpeed++;
            }

            if (tickCount == 600)
            {
                items.Add(new Position(random.Next(0, 450), -30));
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

            foreach (Position enemyBullet in enemyBullets)
            {
                Rectangle now_rt = new Rectangle(enemyBullet.x, enemyBullet.y, 10, 10);
                graphics.FillRectangle(Brushes.Magenta, now_rt);
            }

            foreach (Enemy enemy in enemys)
            {
                Rectangle now_rt = new Rectangle(enemy.x, enemy.y, 50, 50);
                graphics.FillRectangle(Brushes.Red, now_rt);
            }

            foreach (Position item in items)
            {
                Rectangle now_rt = new Rectangle(item.x, item.y, 30, 30);
                graphics.FillRectangle(Brushes.Blue, now_rt);
            }
        }

        private void SaveFile()
        {
            if (File.Exists(dirParameter))
            {
                File.Delete(dirParameter);
            }

            string Msg = highScore.ToString();

            FileStream fParameter = new FileStream(dirParameter, FileMode.Create, FileAccess.Write);
            StreamWriter m_WriterParameter = new StreamWriter(fParameter);
            m_WriterParameter.BaseStream.Seek(0, SeekOrigin.End);
            m_WriterParameter.Write(Msg);
            m_WriterParameter.Flush();
            m_WriterParameter.Close();
        }

        private void LoadFile()
        {
            string msg;

            if (File.Exists(dirParameter))
            {
                FileStream fParameter = new FileStream(dirParameter, FileMode.Open, FileAccess.Read);

                StreamReader m_ReaderParameter = new StreamReader(fParameter);
                msg = m_ReaderParameter.ReadLine();
                m_ReaderParameter.Close();

                int.TryParse(msg, out int score);
                highScore = score;
            }
            else
            {
                highScore = 0;
            }

            label2.Text = "HIGHSCORE: " + highScore;
        }
    }
}
