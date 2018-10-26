using RimWorld;
using System;
using System.Collections;
using System.Text;
using Verse;

namespace Spotted
{
    static class SpotterUtility
    {
        private static readonly string LetterLabel = "MS.MotionDetected";
        private static readonly float ColonistSpottingPower = 1f;

        private static float GetThreatScale()
        {
            return Find.Storyteller.difficulty.threatScale;
        }

        private static float CalculateSpottingPower(Map map, SpottersCounter spottersCounter)
        {
            float colonistPower = spottersCounter.ActiveColonistsCount() * ColonistSpottingPower;
            float watchtowerPower = spottersCounter.WatchtowersCount() * ThingDefOf.Watchtower.GetStatValueAbstract(StatDefOf.SpottingPower);
            float scannerPower = spottersCounter.PoweredMotionScannersCount() * ThingDefOf.MotionScanner.GetStatValueAbstract(StatDefOf.SpottingPower);
            float satellitePower = spottersCounter.PoweredSatelliteController() * ThingDefOf.SatelliteController.GetStatValueAbstract(StatDefOf.SpottingPower);

            float discoveryPower = (colonistPower + watchtowerPower + scannerPower + satellitePower) / (float)Math.Sqrt(GetThreatScale());

            return discoveryPower;
        }

        private static int CalculateDelay(IncidentParms parms, SpottersCounter spottersCounter)
        {
            float modifier = MotionScannerSettings.GetModifiersDictionary()[parms.raidArrivalMode.defName];
            float watchtowerPower = spottersCounter.WatchtowersCount() * ThingDefOf.Watchtower.GetStatValueAbstract(StatDefOf.SpottingRange);
            float scannerPower = spottersCounter.PoweredMotionScannersCount() * ThingDefOf.MotionScanner.GetStatValueAbstract(StatDefOf.SpottingRange);
            float satellitePower = spottersCounter.PoweredSatelliteController() * ThingDefOf.SatelliteController.GetStatValueAbstract(StatDefOf.SpottingRange);

            float delay = ((MotionScannerSettings.allowedTimeRange.RandomInRange + watchtowerPower + scannerPower + satellitePower) * modifier) * GenDate.TicksPerHour;
            int globalDelay = Find.TickManager.TicksGame + (int)delay;

            return globalDelay;
        }

        private static string GetLetterText(int delay, Map map, SpottersCounter spottersCounter)
        {
            string description = (spottersCounter.PoweredMotionScannersCount() > 0 || spottersCounter.PoweredSatelliteController() > 0) ? "MS.LetterDescScanner".Translate() : "MS.LetterDescColonist".Translate();
            StringBuilder letterText = new StringBuilder();
            letterText.AppendLine(description);
            letterText.Append("MS.LetterTime".Translate());
            letterText.Append(" " + ((delay - Find.TickManager.TicksGame) / GenDate.TicksPerHour).ToString() + "h");

            return letterText.ToString();
        }

        private static void DelayRaid(IncidentParms parms, SpottersCounter spottersCounter)
        {
            int delay = CalculateDelay(parms, spottersCounter);
            Find.LetterStack.ReceiveLetter(LetterLabel.Translate(), GetLetterText(delay, (Map)parms.target, spottersCounter), LetterDefOf.ThreatBig, new TargetInfo(parms.spawnCenter, (Map)parms.target));

            QueuedIncident qi = new QueuedIncident(new FiringIncident(IncidentDefOf.RaidEnemy, null, parms), delay);
            Find.Storyteller.incidentQueue.Add(qi);
        }

        public static bool TryScanForMotion(IncidentParms parms)
        {
            SpottersCounter spottersCounter = new SpottersCounter((Map)parms.target);
            if (CalculateSpottingPower((Map)parms.target, spottersCounter) < new IntRange(0,100).RandomInRange)
            {
                return false;
            }
            
            DelayRaid(parms, spottersCounter);
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
