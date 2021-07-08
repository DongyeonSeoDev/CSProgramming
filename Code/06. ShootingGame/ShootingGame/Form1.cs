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
        private int speed = 7;

        /// <summary>
        /// 0: Right, 1: Left, 2: Up, 3: Down
        /// </summary>
        private bool[] isMove = new bool[4] { false, false, false, false };

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
    }
}
