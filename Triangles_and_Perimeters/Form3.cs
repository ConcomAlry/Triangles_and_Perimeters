using System;
using System.Windows.Forms;

namespace Triangles_and_Perimeters
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++) 
            {
                Opacity +=0.1d;             
            }
        }
    }
}
