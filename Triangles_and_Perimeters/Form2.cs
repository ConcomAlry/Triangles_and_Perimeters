using System.Drawing;
using System.Windows.Forms;

namespace Triangles_and_Perimeters
{
    public partial class Form2 : Form
    {
        public Form2(Point[,] p1, Point[] p2)
        {
            InitializeComponent();

            label1.Text = "";

            Bitmap bmp1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics grf1 = Graphics.FromImage(bmp1);
            pictureBox1.Image = bmp1;

            GeometryWork.CoordinateGridAdd(pictureBox1, grf1);

            Pen Red = new Pen(Color.Red, 2);
            SolidBrush red = new SolidBrush(Color.Red);
            Pen Blue = new Pen(Color.Blue, 2);
            SolidBrush blue = new SolidBrush(Color.Blue);

            for (int i = 0; i < p1.GetLength(1); i++)
            {
                Point[] oneMaxTriangle = new Point[3];
                Point[] oneMaxTriangleDraw = new Point[3];
                oneMaxTriangle[0] = p1[0, i];
                oneMaxTriangle[1] = p1[1, i];
                oneMaxTriangle[2] = p1[2, i];
                for (int j = 0; j < 3; j++)
                {
                    oneMaxTriangleDraw[j] = new Point(20 * oneMaxTriangle[j].X, pictureBox1.Height - 20 * oneMaxTriangle[j].Y);
                }
                GeometryWork.PointsAdd(oneMaxTriangle, pictureBox1, grf1, red);
                grf1.DrawPolygon(Red, oneMaxTriangleDraw);
            }

            Point[] p2Draw = new Point[3];
            for (int i = 0; i < 3; i++)
            {
                p2Draw[i] = new Point(20 * p2[i].X, pictureBox1.Height - 20 * p2[i].Y);
            }

            if (p2[0].X == 0 && p2[0].Y == 0 && p2[1].X == 0 && p2[1].Y == 0)
            {
                label1.Text = "Внутри треугольника с максимальным периметром из\n" +
                    "множества А оказалось менее трёх точек из множества В";
            }
            else
            {
                grf1.DrawPolygon(Blue, p2Draw);
                GeometryWork.PointsAdd(p2, pictureBox1, grf1, blue);
            }

            pictureBox1.Refresh();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dispose();
            Form1.f2 = null;
        }
    }
}
