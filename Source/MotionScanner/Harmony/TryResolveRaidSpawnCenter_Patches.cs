using HarmonyLib;
using RimWorld;
using Verse;

namespace Spotted.Harmony
{
    [HarmonyPatch(typeof(PawnsArrivalModeWorker_CenterDrop), "TryResolveRaidSpawnCenter")]
    static class CenterDrop_TryResolveRaidSpawnCenter
    {
        [HarmonyPostfix]
        static void Postfix(PawnsArrivalModeWorker_CenterDrop __instance, IncidentParms parms, ref bool __result)
        {
            if (parms.faction.HostileTo(Faction.OfPlayer) && !SpotterUtility.IncidentIsQueued(parms, IncidentDefOf.RaidEnemy))
            {
                __result = !SpotterUtility.TryScanForMotion(parms, IncidentDefOf.RaidEnemy);
            }
        }
    }

    [HarmonyPatch(typeof(PawnsArrivalModeWorker_EdgeDrop), "TryResolveRaidSpawnCenter")]
    static class EdgeDrop_TryResolveRaidSpawnCenter
    {
        [HarmonyPostfix]
        static void Postfix(PawnsArrivalModeWorker_EdgeDrop __instance, IncidentParms parms, ref bool __result)
        {
            if (parms.faction.HostileTo(Faction.OfPlayer) && !SpotterUtility.IncidentIsQueued(parms, IncidentDefOf.RaidEnemy))
            {
                __result = !SpotterUtility.TryScanForMotion(parms, IncidentDefOf.RaidEnemy);
            }
        }
    }

    [HarmonyPatch(typeof(PawnsArrivalModeWorker_EdgeDropGroups), "TryResolveRaidSpawnCenter")]
    static class EdgeDropGroups_TryResolveRaidSpawnCenter
    {
        [HarmonyPostfix]
        static void Postfix(PawnsArrivalModeWorker_EdgeDropGroups __instance, IncidentParms parms, ref bool __result)
        {
            if (parms.faction.HostileTo(Faction.OfPlayer) && !SpotterUtility.IncidentIsQueued(parms, IncidentDefOf.RaidEnemy))
            {
                __result = !SpotterUtility.TryScanForMotion(parms, IncidentDefOf.RaidEnemy);
            }
        }
    }

    [HarmonyPatch(typeof(PawnsArrivalModeWorker_EdgeWalkIn), "TryResolveRaidSpawnCenter")]
    static class EdgeWalkIn_TryResolveRaidSpawnCenter
    {
        [HarmonyPostfix]
        static void Postfix(PawnsArrivalModeWorker_EdgeWalkIn __instance, IncidentParms parms, ref bool __result)
        {
            if (parms.faction.HostileTo(Faction.OfPlayer) && !SpotterUtility.IncidentIsQueued(parms, IncidentDefOf.RaidEnemy))
            {
                __result = !SpotterUtility.TryScanForMotion(parms, IncidentDefOf.RaidEnemy);
            }
        }
    }

    [HarmonyPatch(typeof(PawnsArrivalModeWorker_EdgeWalkInGroups), "TryResolveRaidSpawnCenter")]
    static class EdgeWalkInGroups_TryResolveRaidSpawnCenter
    {
        [HarmonyPostfix]
        static void Postfix(PawnsArrivalModeWorker_EdgeWalkInGroups __instance, IncidentParms parms, ref bool __result)
        {
            if (parms.faction.HostileTo(Faction.OfPlayer) && !SpotterUtility.IncidentIsQueued(parms, IncidentDefOf.RaidEnemy))
            {
                __result = !SpotterUtility.TryScanForMotion(parms, IncidentDefOf.RaidEnemy);
            }
        }
    }

    [HarmonyPatch(typeof(PawnsArrivalModeWorker_RandomDrop), "TryResolveRaidSpawnCenter")]
    static class RandomDrop_TryResolveRaidSpawnCenter
    {
        [HarmonyPostfix]
        static void Postfix(PawnsArrivalModeWorker_RandomDrop __instance, IncidentParms parms, ref bool __result)
        {
            if (parms.faction.HostileTo(Faction.OfPlayer) && !SpotterUtility.IncidentIsQueued(parms, IncidentDefOf.RaidEnemy))
            {
                __result = !SpotterUtility.TryScanForMotion(parms, IncidentDefOf.RaidEnemy);
            }
        }
    }
}
