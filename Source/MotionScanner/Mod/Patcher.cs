using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Verse;

namespace Spotted
{
    [StaticConstructorOnStartup]
    class Patcher
    {
        private static List<String> incidentWorkers = new List<string>
        {
            "RimWorld.IncidentWorker_DeepDrillInfestation",
            "RimWorld.IncidentWorker_FarmAnimalsWanderIn",
            "RimWorld.IncidentWorker_HerdMigration",
            "RimWorld.IncidentWorker_Infestation",
            "RimWorld.IncidentWorker_ManhunterPack",
            "RimWorld.IncidentWorker_RefugeeChased",
            "RimWorld.IncidentWorker_ThrumboPasses",
            "RimWorld.IncidentWorker_TraderCaravanArrival",
            "RimWorld.IncidentWorker_TransportPodCrash",
            "RimWorld.IncidentWorker_TravelerGroup",
            "RimWorld.IncidentWorker_VisitorGroup",
            "RimWorld.IncidentWorker_WandererJoin",
            "RimWorld.IncidentWorker_WildManWandersIn"
        };

        static Patcher()
        {
            var harmony = HarmonyInstance.Create("TGPAcher.Rimworld.Spotted");
            harmony.PatchAll(Assembly.GetExecutingAssembly());


            // Patch incidents which do not have special "patch"
            var listOfIncidents = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                            from assemblyType in domainAssembly.GetTypes()
                            where typeof(IncidentWorker).IsAssignableFrom(assemblyType)
                            select assemblyType).ToArray();

            foreach(var incident in listOfIncidents)
            {
                if (incidentWorkers.Contains(incident.FullName))
                {
                    Type currentType = incident.GetType();

                    harmony.Patch(AccessTools.Method(AccessTools.TypeByName(incident.FullName), "TryExecuteWorker"),
                        new HarmonyMethod(
                            typeof(IncidentWorker_TryExecuteWorker_Patch)
                            .GetMethod(nameof(IncidentWorker_TryExecuteWorker_Patch.TryExecuteWorker_Patch))
                            .MakeGenericMethod(incident.GetType())));
                }
            }
        }
    }
}
