using RimWorld;
using Verse;

namespace MotionScanner
{
    [DefOf]
    public static class ThingDefOf
    {
        public static ThingDef MotionScanner;

        static ThingDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(ThingDefOf));
        }
    }
}
