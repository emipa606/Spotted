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
        static Patcher()
        {
            var harmony = HarmonyInstance.Create("TGPAcher.Rimworld.Spotted");
            harmony.PatchAll(Assembly.GetExecutingAssembly());


            // Patch incidents which do not have special "patch"
            var listOfIncidents = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                            from assemblyType in domainAssembly.GetTypes()
                            where typeof(IncidentWorker).IsAssignableFrom(assemblyType)
                            select assemblyType).ToArray();

            foreach (var incident in listOfIncidents)
            {
                if (ConfigDefOf.IncidentConfig.GetArgs().Contains(incident.FullName))
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
