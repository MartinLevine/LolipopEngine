using Lolipop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LolipopTestSimple
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            LolipopConfiguration.ConnectionString = @"Data Source=(local);Initial Catalog=TestOrm;Integrated Security=true";
            LolipopConfiguration.Engine = DataEngine.SqlDataEngine;


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
