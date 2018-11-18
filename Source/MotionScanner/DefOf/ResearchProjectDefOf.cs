using RimWorld;
using Verse;

namespace Spotted
{
    [DefOf]
    public class ResearchProjectDefOf
    {
        public static ResearchProjectDef BasicScoutingTehniques;
        public static ResearchProjectDef TowerBuilding;
        public static ResearchProjectDef MotionScanner;
        public static ResearchProjectDef AdvancedScoutingTehniques;

        static ResearchProjectDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(ResearchProjectDefOf));
        }
    }
}
