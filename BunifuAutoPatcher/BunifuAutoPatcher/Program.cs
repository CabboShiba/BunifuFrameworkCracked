using BunifuAutoPatcher.Patch;
using dnlib.DotNet;
using dnlib.DotNet.Writer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BunifuAutoPatcher
{
    internal class Program
    {
        public static Assembly asm;
        static void Main(string[] args)
        {
            try
            {
                Console.Title = $"[{DateTime.Now}] BunifuAutoPatcher by https://github.com/CabboShiba";
                string Patched = args[0];
                string Original = Path.GetDirectoryName(Patched) + @"Bunifu.UI.WinForms-Original.dll";
                File.Copy(Patched, Original, true);
                ModuleContext modCtx = ModuleDef.CreateModuleContext();
                ModuleDefMD module = ModuleDefMD.Load(Original, modCtx);
                RemoveValidation.Remove(module);
                Log("Total Patched Method: " + RemoveValidation.PatchedMethod, "INFO", ConsoleColor.Cyan);
                ModuleWriterOptions options = new ModuleWriterOptions(module);
                options.Logger = DummyLogger.NoThrowInstance;
                module.Write(Patched, options);
            }
            catch (Exception ex)
            {
                Log("Error: " + ex.Message, "ERROR", ConsoleColor.Red);
            }
            Utils.Leave();
        }

        public static void Log(string Data, string Type, ConsoleColor Color)
        {
            Console.ForegroundColor = Color;
            Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")} - {Type}] {Data}");
            Console.ResetColor();
        }
    }
}
