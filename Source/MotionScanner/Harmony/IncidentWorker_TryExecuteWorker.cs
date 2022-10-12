using RimWorld;

namespace Spotted.Harmony;

internal static class IncidentWorker_TryExecuteWorker
{
    public static bool Prefix(IncidentWorker __instance, bool __result, ref IncidentParms parms)
    {
        if (__instance.def.category == IncidentCategoryDefOf.AllyAssistance)
        {
            return true;
        }

        if (SpotterUtility.IncidentIsQueued(parms, __instance.def))
        {
            return true;
        }

        if (parms == null)
        {
            parms = new IncidentParms();
        }

        return !SpotterUtility.TryScanForMotion(parms, __instance.def);
    }
}