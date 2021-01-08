using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsCRUD
{
    static class Program
    {
        public static Form1 f1;
        public static Form2 f2;
        public static Form3 f3;
        public static Form4 f4;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //            Application.Run(new Form1());
            f1 = new Form1();
            Application.Run(f1);
        }
        
    }
}
