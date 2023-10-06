using System;
using System.Windows.Forms;

namespace Triangles_and_Perimeters
{
    static class Program
    {
        ///<summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form3 f3 = new Form3();
            DateTime end = DateTime.Now + TimeSpan.FromSeconds(2);
            f3.Show();
            while (end > DateTime.Now)
            {
                Application.DoEvents();
            }
            f3.Close();
            f3.Dispose();

            Application.Run(new Form1());
        }
    }
}
