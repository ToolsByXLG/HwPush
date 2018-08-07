using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HwPush.Push
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new QQLogin());
                //Application.Run(new Form2());

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
