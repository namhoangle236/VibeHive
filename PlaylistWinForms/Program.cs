using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlaylistWinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            int userId = 0;

            if (args.Length > 0)
            {
                if (!int.TryParse(args[0], out userId) || userId < 0)
                {
                    MessageBox.Show("Invalid User ID.");
                    return;
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(userId));
        }
    }
}
