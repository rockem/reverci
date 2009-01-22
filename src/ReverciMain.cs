using System;
using System.Windows.Forms;
using Othello.view.forms;

namespace Reverci
{
    public class ReverciMain
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormsGameView());
        }
    }
}