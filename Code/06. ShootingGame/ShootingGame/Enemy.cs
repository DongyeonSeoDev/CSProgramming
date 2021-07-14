using System;
using System.Collections.Generic;
using System.Text;

namespace ShootingGame
{
    class Enemy : Position
    {
        public int hp;

        public Enemy(int x, int y, int hp) : base(x, y)
        {
            this.hp = hp;
        }

        public bool IsDie(int damage)
        {
            hp -= damage;

            if (hp <= 0)
            {
                return true;
            }

            return false;
        }
    }
}
