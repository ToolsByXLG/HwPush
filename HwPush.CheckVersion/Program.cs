using HwPush.HwBase;
using System;
using System.Windows.Forms;

namespace HwPush.CheckVersion
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                if (args.Length == 0)
                {
                    Application.Run(new Main());
                }
                else if (args[0] == "1")
                {
                    Application.Run(new ScanVersion());
                }
                else if (args[0] == "2")
                {
                    Application.Run(new LookupVersionALL());
                }
                else if (args[0] == "3")
                {
                    Application.Run(new DownloadCenter());
                }
                else
                {
                    Application.Run(new LookupVersion());
                }


            }
            catch (Exception ex)
            {
                try
                {
                    IOHelper.CreateDir("Exception");
                    IOHelper.CreateFile("Exception\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
                    IOHelper.WriteLine("Exception\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", DateTime.Now.ToString());
                    IOHelper.WriteLine("Exception\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", ex.ToString());
                    MessageBox.Show("程序报错了,请重试~");
                }
                catch
                { }
            }
        }
    }
}
