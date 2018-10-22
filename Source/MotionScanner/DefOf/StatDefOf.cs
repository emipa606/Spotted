using RimWorld;

namespace MotionScanner
{
    [DefOf]
    public class StatDefOf
    {
        public static StatDef SpottingPower;
        public static StatDef SpottingRange;

        static StatDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(StatDefOf));
        }
    }
}
