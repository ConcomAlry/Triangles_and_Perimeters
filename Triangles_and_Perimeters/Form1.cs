using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace Triangles_and_Perimeters
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Bitmap bmp1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            grf1 = Graphics.FromImage(bmp1);
            pictureBox1.Image = bmp1;

            Bitmap bmp2 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            grf2 = Graphics.FromImage(bmp2);
            pictureBox2.Image = bmp2;

            GeometryWork.CoordinateGridAdd(pictureBox1, grf1);
            GeometryWork.CoordinateGridAdd(pictureBox2, grf2);

            flag1 = false;
            flag2 = false;
            messageBoxFlag = true;
            resultShowFlag = false;

            label23.Text = "";
            label24.Text = "";

            button3.Enabled = false;
            button5.Enabled = false;
        }

        int pointsCounter1;
        int pointsCounter2;

        bool flag1;
        bool flag2;
        bool messageBoxFlag;
        bool resultShowFlag;
        
        int pointsCount1;
        int pointsCount2;

        Graphics grf1;
        Graphics grf2;

        int[,] maxTriangle;
        int[,] maxTriangle2;

        Point[] points1;
        Point[] points2;

        Point[,] pointMaxTriangle;
        Point[] pointMaxTriangleB;

        public static Form2 f2;
        void ResultShow()
        {
            if (resultShowFlag)
            {
                maxTriangle = GeometryWork.MaxTriangleSearch(points1);
                pointMaxTriangle = GeometryWork.ConvertToPointArray(maxTriangle);

                maxTriangle2 = GeometryWork.MaxTrianglesSearchInB(points2, maxTriangle);
                pointMaxTriangleB = GeometryWork.MaxTriangleSearchInB(maxTriangle2);
                button3.Enabled = true;

                Thread.Sleep(1000);
                if (f2 == null)
                {
                    f2 = new Form2(pointMaxTriangle, pointMaxTriangleB);
                }
                f2.Show();
            }   
            else { MessageBox.Show("Введите все точки в соответствии с заданным количеством"); }
        }
        void PointsCountCatchException()
        {
            try
            {
                pointsCount1 = int.Parse(numericUpDown1.Text);
                pointsCount2 = int.Parse(numericUpDown2.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Введите корректное число точек");
                button5.Enabled = false;
                button3.Enabled = false;
                flag1 = false;
                flag2 = false;
                messageBoxFlag = false;
            }
        }
        void ClearData()
        {
            flag1 = false;
            flag2 = false;
            messageBoxFlag = true;
            resultShowFlag = false;
            
            button1.Enabled = true;
            button3.Enabled = false;
            button5.Enabled = false;

            pointsCount1 = 0;
            pointsCount2 = 0;
            points1 = null;
            points2 = null;

            Bitmap bmp1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            grf1 = Graphics.FromImage(bmp1);
            pictureBox1.Image = bmp1;

            Bitmap bmp2 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            grf2 = Graphics.FromImage(bmp2);
            pictureBox2.Image = bmp2;

            GeometryWork.CoordinateGridAdd(pictureBox1, grf1);
            GeometryWork.CoordinateGridAdd(pictureBox2, grf2);

            if (f2 != null)
            {
                f2.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            messageBoxFlag = true;
            PointsCountCatchException();
            if (messageBoxFlag) { contextMenuStrip1.Show(button1, new Point(0, button2.Height)); }                                  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;
            button3.Enabled = false;
            ClearData();
        }

        private void автоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button5.Enabled = true;
            resultShowFlag = true;
            Random random = new Random();
            points1 = GeometryWork.PointsGeneration(random, pointsCount1);
            points2 = GeometryWork.PointsGeneration(random, pointsCount2);
            if (points1 != null && points2 != null)
            {
                if (!GeometryWork.PointsArraysEqualCheck(points1, points2))
                {
                    button1.Enabled = false;
                    SolidBrush Red = new SolidBrush(Color.Red);
                    GeometryWork.PointsAdd(points1, pictureBox1, grf1, Red);
                    SolidBrush Blue = new SolidBrush(Color.Blue);
                    GeometryWork.PointsAdd(points2, pictureBox2, grf2, Blue);
                    pictureBox1.Refresh();
                    pictureBox2.Refresh();
                    ResultShow();
                }
                else if (messageBoxFlag) { MessageBox.Show("Множества точек совпадают"); }
            }
        }

        private void вручнуюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flag1 = true; 
            flag2 = true;
            button5.Enabled = true;
            button3.Enabled = true;
            resultShowFlag = false;
            points1 = new Point[pointsCount1];
            points2 = new Point[pointsCount2];
            pointsCounter1 = 0;
            pointsCounter2 = 0;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (flag1)
            {
                int X1 = (int)Math.Round((double)e.X / 20);
                int Y1 = 20 - (int)Math.Round((double)e.Y / 20);
                label23.Text = "Координаты: " + string.Format("({0},{1})", X1, Y1);
            }
            else label23.Text = "";
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (flag1)
            {
                button1.Enabled = false;
                SolidBrush Red = new SolidBrush(Color.Red);
                int X1 = (int)Math.Round((double)e.X / 20) * 20;
                int Y1 = (int)Math.Round((double)e.Y / 20) * 20;
                int x1 = X1 / 20;
                int y1 = 20 - Y1 / 20;
                GeometryWork.PointsAdd(pictureBox1, grf1, Red, X1, Y1);
                points1[pointsCounter1] = new Point(x1, y1);
                if (pointsCounter1 > 0)
                {
                    for (int i = 0; i < pointsCounter1; i++)
                    {
                        if (points1[pointsCounter1] == points1[i])
                        {
                            pointsCounter1--;
                            MessageBox.Show("Точка с такими координатами уже существует");
                        }
                    }
                }
                pointsCounter1++;
                pictureBox1.Refresh();
                if (pointsCounter1 == points1.Length)
                {
                    flag1 = false;
                }
                if (!flag1 && !flag2)
                {
                    resultShowFlag = true;
                }
            }
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            label23.Text = "";
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (flag2)
            {
                int X1 = (int)Math.Round((double)e.X / 20);
                int Y1 = 20 - (int)Math.Round((double)e.Y / 20);
                label24.Text = "Координаты: " + string.Format("({0},{1})", X1, Y1);
            }
            else label24.Text = "";
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (flag2)
            {
                button1.Enabled = false;
                SolidBrush Blue = new SolidBrush(Color.Blue);
                int X1 = (int)Math.Round((double)e.X / 20) * 20;
                int Y1 = (int)Math.Round((double)e.Y / 20) * 20;
                int x1 = X1 / 20;
                int y1 = 20 - Y1 / 20;
                GeometryWork.PointsAdd(pictureBox2, grf2, Blue, X1, Y1);
                points2[pointsCounter2] = new Point(x1, y1);
                if (pointsCounter2 > 0)
                {
                    for (int i = 0; i < pointsCounter2; i++)
                    {
                        if (points2[pointsCounter2] == points2[i])
                        {
                            pointsCounter2--;
                            MessageBox.Show("Точка с такими координатами уже существует");
                        }
                    }
                }
                pointsCounter2++;
                pictureBox2.Refresh();
                if (pointsCounter2 == points2.Length)
                {
                    flag2 = false;
                }
                if (!flag1 && !flag2)
                {
                    resultShowFlag = true;
                }
            }
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            label24.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {           
            if (!GeometryWork.PointsArraysEqualCheck(points1, points2))
            {
                ResultShow();
            }
            else { MessageBox.Show("Множества точек совпадают"); }
        }

        void SaveFile(Point[] p1, Point[] p2)
        {
            if (p1 != null && p2 != null && !GeometryWork.PointsArraysEqualCheck(p1, p2) && resultShowFlag)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                string fileName = saveFileDialog1.FileName;
                string fileText = "";
                for (int i = 0; i < p1.Length; i++)
                {
                    fileText += p1[i].X + "," + p1[i].Y;
                    if (i == p1.Length - 1)
                    {
                        fileText += ";";
                    }
                    else
                    {
                        fileText += ",";
                    }
                }
                for (int i = 0; i < p2.Length; i++)
                {
                    fileText += p2[i].X + "," + p2[i].Y;
                    if (i != p2.Length - 1)
                    {
                        fileText += ",";
                    }
                }
                File.WriteAllText(fileName, fileText);
                MessageBox.Show("Файл сохранен");
            }
            else { MessageBox.Show("Ошибка: множества точек отсутствуют, не заполнены или совпадают"); }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile(points1, points2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFile(points1, points2);
        }
        
        void OpenFile()
        {
            try
            {
                ClearData();
                resultShowFlag = true;
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                string fileName = openFileDialog1.FileName;
                string fileText = File.ReadAllText(fileName);
                string[] fileText2 = fileText.Split(';');
                int[] coords1 = fileText2[0].Split(',').
                Where(x => !string.IsNullOrWhiteSpace(x)).
                Select(x => int.Parse(x)).ToArray();
                points1 = new Point[coords1.Length / 2];
                for (int i = 0; i < coords1.Length; i += 2)
                {
                    points1[i / 2] = new Point(coords1[i], coords1[i + 1]);
                }
                int[] coords2 = fileText2[1].Split(',').
                Where(x => !string.IsNullOrWhiteSpace(x)).
                Select(x => int.Parse(x)).ToArray();
                points2 = new Point[coords2.Length / 2];
                for (int i = 0; i < coords2.Length; i += 2)
                {
                    points2[i / 2] = new Point(coords2[i], coords2[i + 1]);
                }
                if (!GeometryWork.PointsArraysEqualCheck(points1, points2) && points1.Length > 2 && points2.Length > 2 && 
                    points1.Length < 301 && points1.Length < 301)
                {
                    button1.Enabled = false;
                    button5.Enabled = true;
                    button3.Enabled = true;
                    SolidBrush Red = new SolidBrush(Color.Red);
                    GeometryWork.PointsAdd(points1, pictureBox1, grf1, Red);
                    SolidBrush Blue = new SolidBrush(Color.Blue);
                    GeometryWork.PointsAdd(points2, pictureBox2, grf2, Blue);
                    pictureBox1.Refresh();
                    pictureBox2.Refresh();
                }
                else if (GeometryWork.PointsArraysEqualCheck(points1, points2)) { MessageBox.Show("Множества точек совпадают");  ClearData(); }
                else { MessageBox.Show("Неверный формат файла"); ClearData(); }
            }
            catch (Exception)
            {
                MessageBox.Show("Неверный формат файла");
                ClearData();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("В данной программе пользователь может наблюдать реализацию следующего алгоритма: " +
                "даны 2 конечных множества точек А и В с целочисленными координатами; на каждом из множеств точек " +
                "А и В найти по три точки, формирующие треугольники с максимальными периметрами, причем треугольник, " +
                "построенный на точках 2 множества находится полностью внутри треугольника, построенного на точках 1 " +
                "множества. Результаты работы программы отобразить графически.");
        }

        private void перезапускToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
