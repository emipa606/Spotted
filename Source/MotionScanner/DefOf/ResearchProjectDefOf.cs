using RimWorld;
using Verse;

namespace Spotted
{
    [DefOf]
    public class ResearchProjectDefOf
    {
        public static ResearchProjectDef BasicScoutingTehniques;

        static ResearchProjectDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(StatDefOf));
        }
    }
}
