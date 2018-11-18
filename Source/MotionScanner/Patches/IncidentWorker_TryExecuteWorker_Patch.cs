using RimWorld;

namespace Spotted
{
    public static class IncidentWorker_TryExecuteWorker_Patch
    {
        public static bool TryExecuteWorker_Patch<T>(T __instance, bool __result, ref IncidentParms parms) where T : IncidentWorker
        {
            if (!SpotterUtility.IncidentIsQueued(parms, __instance.def))
            {
                if(parms == null)
                {
                    parms = new IncidentParms();
                }

                __result = !SpotterUtility.TryScanForMotion(parms, __instance.def);
                return __result;
            }

            return true;
        }
    }
}
