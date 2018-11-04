﻿using RimWorld;
using System;
using System.Collections;
using System.Text;
using Verse;

namespace Spotted
{
    static class SpotterUtility
    {
        private static readonly string LetterLabel = "S.MotionDetected";
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

        private static IDelayHolder CalculateDelay(IncidentParms parms, SpottersCounter spottersCounter)
        {
            float modifier = SpottedSettings.GetModifiersDictionary()[parms.raidArrivalMode.defName];
            float watchtowerPower = spottersCounter.WatchtowersCount() * ThingDefOf.Watchtower.GetStatValueAbstract(StatDefOf.SpottingRange);
            float scannerPower = spottersCounter.PoweredMotionScannersCount() * ThingDefOf.MotionScanner.GetStatValueAbstract(StatDefOf.SpottingRange);
            float satellitePower = spottersCounter.PoweredSatelliteController() * ThingDefOf.SatelliteController.GetStatValueAbstract(StatDefOf.SpottingRange);

            float delay = ((SpottedSettings.allowedTimeRange.RandomInRange + watchtowerPower + scannerPower + satellitePower) * modifier) * GenDate.TicksPerHour;

            return (IDelayHolder)Activator.CreateInstance(SpottedSettings.GetDelayType(), args: (int)delay);
        }

        private static string GetLetterText(IDelayHolder delay, Map map, SpottersCounter spottersCounter)
        {
            string description = (spottersCounter.PoweredMotionScannersCount() > 0 || spottersCounter.PoweredSatelliteController() > 0) ? "S.LetterDescScanner".Translate() : "S.LetterDescColonist".Translate();
            StringBuilder letterText = new StringBuilder();
            letterText.AppendLine(description);
            letterText.Append("S.LetterTime".Translate());
            letterText.Append(" " + delay.ToStringRelativeDelayToPeriod());

            return letterText.ToString();
        }

        private static void DelayRaid(IncidentParms parms, SpottersCounter spottersCounter)
        {
            IDelayHolder delay = CalculateDelay(parms, spottersCounter);
            Find.LetterStack.ReceiveLetter(LetterLabel.Translate(), GetLetterText(delay, (Map)parms.target, spottersCounter), LetterDefOf.ThreatBig, new TargetInfo(parms.spawnCenter, (Map)parms.target));

            QueuedIncident qi = new QueuedIncident(new FiringIncident(IncidentDefOf.RaidEnemy, null, parms), delay.GetGlobalDelay());
            Find.Storyteller.incidentQueue.Add(qi);
            Alert_Spotted.AddIncident(delay);
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
