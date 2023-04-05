using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunifuAutoPatcher
{
    internal class Utils
    {
        public static void Leave()
        {
            Program.Log("Press enter to leave...", "INFO", ConsoleColor.Yellow);
            Console.ReadLine();
            Process.GetCurrentProcess().Kill();
        }
    }
}
