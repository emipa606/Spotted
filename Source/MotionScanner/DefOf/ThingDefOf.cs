using RimWorld;
using Verse;

namespace Spotted
{
    [DefOf]
    public static class ThingDefOf
    {
        public static ThingDef Watchtower;

        public static ThingDef MotionScanner;

        public static ThingDef SatelliteController;

        static ThingDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(ThingDefOf));
        }
    }
}
