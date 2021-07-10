using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

class Position
{
    public int x;
    public int y;

    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

namespace ShootingGame
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 0: Right, 1: Left, 2: Up, 3: Down
        /// </summary>
        private bool[] isMove = new bool[4] { false, false, false, false };

        private List<Position> bulletPosition = new List<Position>();

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

            foreach (Position item in bulletPosition)
            {
                item.y -= speed;
            }

            bool removeBullet = false;

            do
            {
                removeBullet = false;

                foreach (Position item in bulletPosition)
                {
                    if (item.y < 0)
                    {
                        removeBullet = true;
                        bulletPosition.Remove(item);
                        break;
                    }
                }

            } while (removeBullet);

            tickCount++;

            if (tickCount % 10 == 0)
            {
                bulletPosition.Add(new Position(Player.Location.X + 20, Player.Location.Y));
            }

            Invalidate();
        }

        private void MoveRight()
        {
            if (Player.Location.X > 430) return;

            Player.Location = new System.Drawing.Point(Player.Location.X + speed, Player.Location.Y);
            Invalidate();
        }

        private void MoveLeft()
        {
            if (Player.Location.X < 2) return;

            Player.Location = new System.Drawing.Point(Player.Location.X - speed, Player.Location.Y);
            Invalidate();
        }

        private void MoveUp()
        {
            if (Player.Location.Y < 2) return;

            Player.Location = new System.Drawing.Point(Player.Location.X, Player.Location.Y - speed);
            Invalidate();
        }

        private void MoveDown()
        {
            if (Player.Location.Y > 800) return;

            Player.Location = new System.Drawing.Point(Player.Location.X, Player.Location.Y + speed);
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawBoard(e.Graphics);
        }

        private void DrawBoard(Graphics graphics)
        {
            foreach (Position item in bulletPosition)
            {
                Rectangle now_rt = new Rectangle(item.x, item.y, 10, 10);
                graphics.FillRectangle(Brushes.Green, now_rt);
            }
        }
    }
}
