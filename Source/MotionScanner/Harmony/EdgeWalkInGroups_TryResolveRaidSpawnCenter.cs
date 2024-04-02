using HarmonyLib;
using RimWorld;

namespace Spotted.Harmony;

[HarmonyPatch(typeof(PawnsArrivalModeWorker_EdgeWalkInGroups),
    nameof(PawnsArrivalModeWorker_EdgeWalkInGroups.TryResolveRaidSpawnCenter))]
internal static class EdgeWalkInGroups_TryResolveRaidSpawnCenter
{
    [HarmonyPostfix]
    private static void Postfix(IncidentParms parms,
        ref bool __result)
    {
        if (parms.faction.HostileTo(Faction.OfPlayer) &&
            !SpotterUtility.IncidentIsQueued(parms, IncidentDefOf.RaidEnemy))
        {
            __result = !SpotterUtility.TryScanForMotion(parms, IncidentDefOf.RaidEnemy);
        }
    }
}