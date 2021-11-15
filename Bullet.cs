
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSBullet
{
    public class Bullet
    {
        public float X { get { return x; } }
        public float Y { get { return y; } }
        public float Speed { get { return speed; } }
        public float Angle { get { return angle; } }
        public float Radius { get { return radius; } }

        private float x, y, speed, angle, radius;

        public Bullet(float x, float y, float speed, float angle, float radius)
        {
            this.x = x;
            this.y = y;
            this.speed = speed;
            this.angle = angle;
            this.radius = radius;
        }

        public void Fly()
        {
            x += speed * (float)Math.Cos(angle * Math.PI / 180);
            y += speed * (float)Math.Sin(angle * Math.PI / 180);
        }

        public void Draw(Graphics g, Pen pen)
        {
            g.DrawEllipse(pen, x - radius, y - radius, radius * 2, radius * 2);
        }
    }
}
