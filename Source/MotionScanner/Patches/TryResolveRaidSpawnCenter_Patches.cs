using Harmony;
using RimWorld;

namespace Spotted
{
    [HarmonyPatch(typeof(PawnsArrivalModeWorker_CenterDrop), "TryResolveRaidSpawnCenter")]
    public static class CenterDrop_TryResolveRaidSpawnCenter_Patch
    {
        [HarmonyPostfix]
        public static void DetectTryResolveRaidSpawnCenter_Postfix(PawnsArrivalModeWorker_CenterDrop __instance, IncidentParms parms, ref bool __result)
        {
            if (!SpotterUtility.IncidentIsQueued(parms))
            {
                __result = !SpotterUtility.TryScanForMotion(parms);
            }
        }
    }

    [HarmonyPatch(typeof(PawnsArrivalModeWorker_EdgeDrop), "TryResolveRaidSpawnCenter")]
    public static class EdgeDrop_TryResolveRaidSpawnCenter_Patch
    {
        [HarmonyPostfix]
        public static void DetectTryResolveRaidSpawnCenter_Postfix(PawnsArrivalModeWorker_EdgeDrop __instance, IncidentParms parms, ref bool __result)
        {
            if (!SpotterUtility.IncidentIsQueued(parms))
            {
                __result = !SpotterUtility.TryScanForMotion(parms);
            }
        }
    }

    [HarmonyPatch(typeof(PawnsArrivalModeWorker_EdgeDropGroups), "TryResolveRaidSpawnCenter")]
    public static class EdgeDropGroups_TryResolveRaidSpawnCenter_Patch
    {
        [HarmonyPostfix]
        public static void DetectTryResolveRaidSpawnCenter_Postfix(PawnsArrivalModeWorker_EdgeDropGroups __instance, IncidentParms parms, ref bool __result)
        {
            if (!SpotterUtility.IncidentIsQueued(parms))
            {
                __result = !SpotterUtility.TryScanForMotion(parms);
            }
        }
    }

    [HarmonyPatch(typeof(PawnsArrivalModeWorker_EdgeWalkIn), "TryResolveRaidSpawnCenter")]
    public static class EdgeWalkIn_TryResolveRaidSpawnCenter_Patch
    {
        [HarmonyPostfix]
        public static void DetectTryResolveRaidSpawnCenter_Postfix(PawnsArrivalModeWorker_EdgeWalkIn __instance, IncidentParms parms, ref bool __result)
        {
            if (!SpotterUtility.IncidentIsQueued(parms))
            {
                __result = !SpotterUtility.TryScanForMotion(parms);
            }
        }
    }

    [HarmonyPatch(typeof(PawnsArrivalModeWorker_EdgeWalkInGroups), "TryResolveRaidSpawnCenter")]
    public static class EdgeWalkInGroups_TryResolveRaidSpawnCenter_Patch
    {
        [HarmonyPostfix]
        public static void DetectTryResolveRaidSpawnCenter_Postfix(PawnsArrivalModeWorker_EdgeWalkInGroups __instance, IncidentParms parms, ref bool __result)
        {
            if (!SpotterUtility.IncidentIsQueued(parms))
            {
                __result = !SpotterUtility.TryScanForMotion(parms);
            }
        }
    }

    [HarmonyPatch(typeof(PawnsArrivalModeWorker_RandomDrop), "TryResolveRaidSpawnCenter")]
    public static class RandomDrop_TryResolveRaidSpawnCenter_Patch
    {
        [HarmonyPostfix]
        public static void DetectTryResolveRaidSpawnCenter_Postfix(PawnsArrivalModeWorker_RandomDrop __instance, IncidentParms parms, ref bool __result)
        {
            if (!SpotterUtility.IncidentIsQueued(parms))
            {
                __result = !SpotterUtility.TryScanForMotion(parms);
            }
        }
    }
}
