using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DespairBox
{
    static class Program
    {
        public static string FileName;
        public static string FilePath;
        public static int CreatOrOpen;

        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DespairBox());






        }
    }
}
