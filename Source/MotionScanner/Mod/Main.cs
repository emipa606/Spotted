using Harmony;
using System.Reflection;
using Verse;

namespace MotionScanner
{
    [StaticConstructorOnStartup]
    class Main
    {
        static Main()
        {
            var harmony = HarmonyInstance.Create("TGPAcher.Rimworld.MotionScanner");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
