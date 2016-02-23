using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Test.Fody
{
    public class ModuleWeaver
    {
        public ModuleDefinition ModuleDefinition { get; set; }

        public Action<string> LogInfo { get; set; }

        public Action<string> LogError { get; set; }

        public void Execute()
        {
            LogInfo("Weaving...");
        }
    }
}
