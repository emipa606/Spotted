using HarmonyLib;
using RimWorld;

namespace Spotted.Harmony;

[HarmonyPatch(typeof(PawnsArrivalModeWorker_EdgeDrop),
    nameof(PawnsArrivalModeWorker_EdgeDrop.TryResolveRaidSpawnCenter))]
internal static class EdgeDrop_TryResolveRaidSpawnCenter
{
    public static void Postfix(IncidentParms parms, ref bool __result)
    {
        if (parms.faction.HostileTo(Faction.OfPlayer) &&
            !SpotterUtility.IncidentIsQueued(parms, IncidentDefOf.RaidEnemy))
        {
            __result = !SpotterUtility.TryScanForMotion(parms, IncidentDefOf.RaidEnemy);
        }
    }
}