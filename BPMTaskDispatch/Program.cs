using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BPMTaskDispatch.Win
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());


            bool flag;
            System.Threading.Mutex mutex = new System.Threading.Mutex(true, Application.ProductName, out flag);
            if (flag)
            {
                // 启用应用程序的可视样式
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // 处理当前在消息队列中的所有 Windows 消息
                Application.DoEvents();
                Application.Run(new MainForm());//UnitTest MainForm

                // 释放 System.Threading.Mutex 一次
                mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show(null, "任务监控程序已运行,请不要同时运行多个程序!",
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
        }
    }
}
