using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSBullet
{
    public partial class Form1 : Form
    {
        public Form1() { InitializeComponent(); }


        private int count = 10;
        private List<Bullet> bullets;
        private Random random;

        private Bitmap bmp;
        private Graphics gpc;
        private Pen pen;
        //int ang;
        int difficult = 8;
        int score = 0;
        int con1, con2;
        int px = -300, py = -100;
        bool live = true;
        SolidBrush br = new SolidBrush(Color.Red);
        Pen p = new Pen(Color.Blue);
        private void Form1_Load(object sender, EventArgs e)
        {
            bullets = new List<Bullet>();
            random = new Random();

            bmp = new Bitmap(784, 562);
            gpc = Graphics.FromImage(bmp);
            pen = new Pen(Color.Black);
            pen.Width = 3;
            gpc.TranslateTransform(392, 281);
            p.Width = 2;


        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            /*ang += 6;
            ang %= 360;*/
            if (live)
            {
                count--;
                if (count == 0)
                {
                    count = difficult;
                    bullets.Add(new Bullet(random.Next(pbx.Width) - pbx.Width / 2, pbx.Height / -2, random.Next(5) + 1, 90, 3));
                    bullets.Add(new Bullet(pbx.Width / -2, random.Next(pbx.Height) - pbx.Height / 2, random.Next(5) + 1, 0, 3));
                }
            }
            gpc.Clear(BackColor);

            foreach (Bullet bullet in bullets)
            {
                bullet.Fly();
                bullet.Draw(gpc, pen);
                gpc.DrawLine(p, bullet.X + 2, bullet.Y + 2, bullet.X - 2, bullet.Y - 2);
                if (bullet.X >= px - 6 && bullet.X <= px + 6 && bullet.Y >= py - 8 && bullet.Y <= py + 8)
                    live = false;
            }

            for (int i = 0; i < bullets.Count; i++)
                if (bullets[i].X < pbx.Width / -2 || bullets[i].X > pbx.Width / 2 || bullets[i].Y < pbx.Height / -2 || bullets[i].Y > pbx.Height / 2)
                {
                    bullets.RemoveAt(i--);
                    if (live)
                        score += 2;
                }
            Text = bullets.Count.ToString();

            gpc.DrawString("SCORE:" + score.ToString("0000"), new Font("標楷體", 20), br, pbx.Width / 2 - 150, pbx.Height / 2 - 25);
            if (con1 == 3)
            {
                live = true;
                score = 0;
            }
            if (live)
            {
                switch (con1)
                {
                    case 1:
                        py -= 2;
                        if (py - 8 <= pbx.Height / -2)
                            py = pbx.Height / -2 + 8;
                        break;
                    case 2:
                        py += 2;
                        if (py + 8 >= pbx.Height / 2)
                            py = pbx.Height / 2 - 8;
                        break;


                }
                switch (con2)
                {
                    case 1:
                        px -= 2;
                        if (px - 6 <= pbx.Width / -2)
                            px = pbx.Width / -2 + 6;
                        break;
                    case 2:
                        px += 2;
                        if (px + 6 >= pbx.Width / 2)
                            px = pbx.Width / 2 - 6;
                        break;

                }
                gpc.DrawString("O", new Font("標楷體", 18), br, px - 9, py - 10);
                gpc.DrawLine(p, px, py - 8, px, py + 8);
                gpc.DrawLine(p, px - 6, py, px + 6, py);
            }
            else
                gpc.DrawString("X", new Font("標楷體", 18), br, px - 8, py - 8);

            pbx.Image = bmp;
        }

        private void pbx_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 87:
                    con1 = con1 == 2 ? 0 : 1;
                    break;
                case 83:
                    con1 = con1 == 1 ? 0 : 2;
                    break;
                case 32:
                    con1 = 3;
                    break;
                case 65:
                    con2 = con2 == 2 ? 0 : 1;
                    break;
                case 68:
                    con2 = con2 == 1 ? 0 : 2;
                    break;
                case 90:
                    switch (difficult)
                    {
                        case 8:
                            button1.Text = "正常";
                            difficult = 5;
                            button1.BackColor = Color.Yellow;
                            break;
                        case 5:
                            button1.Text = "困難";
                            difficult = 3;
                            button1.BackColor = Color.Red;
                            break;
                        case 3:
                            button1.Text = "簡單";
                            difficult = 8;
                            button1.BackColor = Color.Green;
                            break;

                    }
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 87 || e.KeyValue == 83 || e.KeyValue == 32)
                con1 = 0;

            if (e.KeyValue == 65 || e.KeyValue == 68)
                con2 = 0;
        }
    }
}
