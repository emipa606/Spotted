using RimWorld;

namespace Spotted
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
