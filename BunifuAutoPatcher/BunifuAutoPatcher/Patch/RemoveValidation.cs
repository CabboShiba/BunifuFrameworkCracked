
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunifuAutoPatcher.Patch
{
    internal class RemoveValidation
    {
        public static int PatchedMethod = 0;
        public static void Remove(ModuleDefMD mod)
        {
            Instruction inst;
            foreach (var type in mod.Types)
            {
                foreach (var method in type.Methods)
                {
                    if (method.IsConstructor && method.HasBody)
                    {
                        for (int i = 0; i < method.Body.Instructions.Count; i++)
                        {
                            inst = method.Body.Instructions[i];
                            if (method.Body.Instructions[i].OpCode == OpCodes.Call && method.Body.Instructions[i].Operand.ToString() == "System.ComponentModel.License Bunifu.Licensing.LicenseValidator::Validate(Bunifu.Licensing.Options.ProductTypes,System.Type,System.Object)")
                            {
                                for (int k = i - 4; k <= i + 1; k++)
                                {
                                    method.Body.Instructions[k].OpCode = OpCodes.Nop;
                                }
                                Program.Log("Patched: " + type.Name, "PATCH", ConsoleColor.Green);
                                PatchedMethod++;
                            }
                        }
                    }
                }
            }
        }
    }
}
