using RimWorld;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        private static string GetDescription()
        {
            List<StoryDef> descriptionStoryDefs = DefDatabase<StoryDef>.AllDefs.Where(def => def.storyType == "Spotted.Detected").MeetRequirements().ToList();
            string descKey = descriptionStoryDefs.RandomElement()?.storyKey;

            return descKey == null ? string.Empty : descKey.Translate();
        }

        private static string GetLetterText(IDelayHolder delay)
        {
            StringBuilder letterText = new StringBuilder();

            // description (detected text)
            letterText.AppendLine(GetDescription());

            // count
            letterText.AppendLine("Number of aproaching entities is unknown.");

            // type
            // Not Implemented

            // arrival time
            letterText.Append("S.LetterTime".Translate());
            letterText.Append(" " + delay.ToStringRelativeDelayToPeriod());

            return letterText.ToString();
        }

        private static IDelayHolder DelayRaid(IncidentParms parms, SpottersCounter spottersCounter)
        {
            IDelayHolder delay = CalculateDelay(parms, spottersCounter);
            QueuedIncident qi = new QueuedIncident(new FiringIncident(IncidentDefOf.RaidEnemy, null, parms), delay.GetGlobalDelay());
            Find.Storyteller.incidentQueue.Add(qi);

            return delay;
        }

        private static void NotifySpotted(IncidentParms parms, IDelayHolder delay)
        {
            Find.LetterStack.ReceiveLetter(LetterLabel.Translate(), GetLetterText(delay), LetterDefOf.ThreatBig, new TargetInfo(parms.spawnCenter, (Map)parms.target));
            Alert_Spotted.AddIncident(delay);
        }

        public static bool TryScanForMotion(IncidentParms parms)
        {
            // Can spot?
            if (!ResearchProjectDefOf.BasicScoutingTehniques.IsFinished)
            {
                return false;
            }

            // Detected
            SpottersCounter spottersCounter = new SpottersCounter((Map)parms.target);
            if (CalculateSpottingPower((Map)parms.target, spottersCounter) < new IntRange(0,100).RandomInRange)
            {
                return false;
            }
            
            // Delay
            IDelayHolder delay = DelayRaid(parms, spottersCounter);

            // Send Letter and Alert
            StoryCondition.SetArgs(new object[] { parms.target });
            NotifySpotted(parms, delay);
            StoryCondition.ClearArgs();

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
