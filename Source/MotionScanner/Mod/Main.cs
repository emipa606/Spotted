using Harmony;
using System.Reflection;
using Verse;

namespace Spotted
{
    [StaticConstructorOnStartup]
    class Main
    {
        static Main()
        {
            var harmony = HarmonyInstance.Create("TGPAcher.Rimworld.Spotted");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
