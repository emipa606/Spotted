using HarmonyLib;
using RimWorld;

namespace Spotted.Harmony
{
    [HarmonyPatch(typeof(PawnsArrivalModeWorker_CenterDrop), "TryResolveRaidSpawnCenter")]
    internal static class CenterDrop_TryResolveRaidSpawnCenter
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
}