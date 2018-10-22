using RimWorld;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Verse;

namespace MotionScanner
{
    static class MotionScannerUtility
    {
        private static readonly string LetterLabel = "MS.MotionDetected";
        private static readonly float ColonistSpottingPower = 1f;

        private static int PoweredMotionScannersCount(Map map)
        {
            int scannersCount = 0;
            List<Thing> list = map.listerThings.ThingsMatching(ThingRequest.ForDef(ThingDefOf.MotionScanner));
            foreach(var item in list)
            {
                if(item.Faction == Faction.OfPlayer)
                {
                    CompPowerTrader compPowerTrader = item.TryGetComp<CompPowerTrader>();
                    if (compPowerTrader == null || compPowerTrader.PowerOn)
                    {
                        scannersCount += 1;
                    }
                }
            }
            
            return scannersCount;
        }

        private static int ActiveColonistsCount(Map map)
        {
            int colonistsCount = map.mapPawns.ColonistsSpawnedCount;

            return colonistsCount;
        }

        private static float GetThreatScale()
        {
            return Find.Storyteller.difficulty.threatScale;
        }

        private static float CalculateSpottingPower(Map map)
        {
            float discoveryPower = ActiveColonistsCount(map) * ColonistSpottingPower + PoweredMotionScannersCount(map) *
                ThingDefOf.MotionScanner.GetStatValueAbstract(StatDefOf.SpottingPower);
            float adjustedDiscoveryPower = discoveryPower / (float)Math.Sqrt(GetThreatScale());
            
            return adjustedDiscoveryPower;
        }

        private static int CalculateDelay(IncidentParms parms)
        {
            float modifier = MotionScannerSettings.GetModifiersDictionary()[parms.raidArrivalMode.defName];
            float delay = ((MotionScannerSettings.allowedTimeRange.RandomInRange + PoweredMotionScannersCount((Map)parms.target) * 
                ThingDefOf.MotionScanner.GetStatValueAbstract(StatDefOf.SpottingRange)) * modifier) * GenDate.TicksPerHour;
            int globalDelay = Find.TickManager.TicksGame + (int)delay;
            
            return globalDelay;
        }

        private static string GetLetterText(int delay, Map map)
        {
            string description = (PoweredMotionScannersCount(map) > 0) ? "MS.LetterDescScanner".Translate() : "MS.LetterDescColonist".Translate();
            StringBuilder letterText = new StringBuilder();
            letterText.AppendLine(description);
            letterText.Append("MS.LetterTime".Translate());
            letterText.Append(" " + ((delay - Find.TickManager.TicksGame) / GenDate.TicksPerHour).ToString() + "h");

            return letterText.ToString();
        }

        private static void DelayRaid(IncidentParms parms)
        {
            int delay = CalculateDelay(parms);
            Find.LetterStack.ReceiveLetter(LetterLabel.Translate(), GetLetterText(delay, (Map)parms.target), LetterDefOf.ThreatBig, new TargetInfo(parms.spawnCenter, (Map)parms.target));

            QueuedIncident qi = new QueuedIncident(new FiringIncident(IncidentDefOf.RaidEnemy, null, parms), delay);
            Find.Storyteller.incidentQueue.Add(qi);
        }

        public static bool TryScanForMotion(IncidentParms parms)
        {
            if (CalculateSpottingPower((Map)parms.target) < new IntRange(0,100).RandomInRange)
            {
                return false;
            }
            
            DelayRaid(parms);
            return true;
        }

        public static bool IncidentIsQueued(IncidentParms parms)
        {
            IEnumerator qIncidents = Find.Storyteller.incidentQueue.GetEnumerator();
            while (qIncidents.MoveNext())
            {
                QueuedIncident qi = (QueuedIncident)qIncidents.Current;
                if (qi.FiringIncident.parms == parms)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
