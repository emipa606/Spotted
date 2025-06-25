using RimWorld;
using Verse;

namespace Spotted.Harmony;

internal static class IncidentWorker_TryExecuteWorker
{
    private static readonly IncidentCategoryDef allyAssistance =
        DefDatabase<IncidentCategoryDef>.GetNamedSilentFail("AllyAssistance");

    public static bool Prefix(IncidentWorker __instance, ref IncidentParms parms)
    {
        if (__instance.def.category == allyAssistance)
        {
            return true;
        }

        if (SpotterUtility.IncidentIsQueued(parms, __instance.def))
        {
            return true;
        }

        parms ??= new IncidentParms();

        return !SpotterUtility.TryScanForMotion(parms, __instance.def);
    }
}