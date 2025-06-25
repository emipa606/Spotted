using System;
using RimWorld;
using Verse;

namespace Spotted;

internal static class SpotterUtility
{
    private static readonly float ColonistSpottingPower = 1f;

    private static float getThreatScale()
    {
        return Find.Storyteller.difficulty.threatScale;
    }

    private static float calculateSpottingPower(Map map)
    {
        if (map == null)
        {
            return 0;
        }

        var spottersCounter = new SpottersCounter(map);

        var colonistPower = spottersCounter.ActiveColonistsCount() * ColonistSpottingPower;
        var watchtowerPower = spottersCounter.WatchtowersCount() *
                              ThingDefOf.Watchtower.GetStatValueAbstract(StatDefOf.SpottingPower);
        var scannerPower = spottersCounter.PoweredMotionScannersCount() *
                           ThingDefOf.MotionScanner.GetStatValueAbstract(StatDefOf.SpottingPower);
        var satellitePower = spottersCounter.PoweredSatelliteController() *
                             ThingDefOf.SatelliteController.GetStatValueAbstract(StatDefOf.SpottingPower);

        var discoveryPower = (colonistPower + watchtowerPower + scannerPower + satellitePower) /
                             (float)Math.Sqrt(getThreatScale());

        return discoveryPower;
    }

    private static IDelayHolder calculateDelay(IncidentParms parms)
    {
        if (parms.target == null)
        {
            return (IDelayHolder)Activator.CreateInstance(SpottedSettings.GetDelayType(), 0);
        }

        var spottersCounter = new SpottersCounter((Map)parms.target);

        float modifier;
        try
        {
            modifier = SpottedSettings.GetModifiersDictionary()[parms.raidArrivalMode.defName];
        }
        catch
        {
            modifier = 1f;
        }

        var watchtowerPower = spottersCounter.WatchtowersCount() *
                              ThingDefOf.Watchtower.GetStatValueAbstract(StatDefOf.SpottingRange);
        var scannerPower = spottersCounter.PoweredMotionScannersCount() *
                           ThingDefOf.MotionScanner.GetStatValueAbstract(StatDefOf.SpottingRange);
        var satellitePower = spottersCounter.PoweredSatelliteController() *
                             ThingDefOf.SatelliteController.GetStatValueAbstract(StatDefOf.SpottingRange);

        var delay = (SpottedSettings.allowedTimeRange.RandomInRange + watchtowerPower + scannerPower +
                     satellitePower) * modifier * GenDate.TicksPerHour;

        return (IDelayHolder)Activator.CreateInstance(SpottedSettings.GetDelayType(), (int)delay);
    }

    private static IDelayHolder delayRaid(IncidentParms parms, IncidentDef incidentDef)
    {
        var delay = calculateDelay(parms);
        var qi = new QueuedIncident(new FiringIncident(incidentDef, null, parms), delay.GetGlobalDelay());
        Find.Storyteller.incidentQueue.Add(qi);

        return delay;
    }

    private static IncidentDef tryIdentifyType(IncidentParms parms, IncidentDef incidentDef, float spottingPower)
    {
        if (!ResearchProjectDefOf.MotionScanner.IsFinished)
        {
            return null;
        }

        var multiplier = .1f;
        if (ResearchProjectDefOf.AdvancedScoutingTehniques.IsFinished &&
            ((Map)parms.target).listerBuildings.ColonistsHaveBuildingWithPowerOn(ThingDefOf.SatelliteController))
        {
            multiplier = 1f;
        }

        return spottingPower * multiplier < new IntRange(0, 100).RandomInRange ? null : incidentDef;
    }

    private static void notifySpotted(IncidentParms parms, IDelayHolder delay, IncidentDef incidentDef = null)
    {
        Find.LetterStack.ReceiveLetter(SpottedLetter.NewLetter(parms, delay, incidentDef));
        Alert_Spotted.AddIncident(new AlertIncident(delay, incidentDef));
    }

    public static bool TryScanForMotion(IncidentParms parms, IncidentDef incidentDef)
    {
        // Can spot?
        if (!ResearchProjectDefOf.BasicScoutingTehniques.IsFinished)
        {
            return false;
        }

        // Dont block quests
        if (parms.quest != null || parms.questScriptDef != null || parms.questTag != null)
        {
            return false;
        }

        // Detected
        var spottingPower = calculateSpottingPower((Map)parms.target);
        if (spottingPower < new IntRange(0, 100).RandomInRange)
        {
            return false;
        }

        // Delay
        var delay = delayRaid(parms, incidentDef);

        // Indentify type
        var spottedType = tryIdentifyType(parms, incidentDef, spottingPower);

        // Send Letter and Alert
        notifySpotted(parms, delay, spottedType);

        return true;
    }

    public static bool IncidentIsQueued(IncidentParms parms, IncidentDef incidentDef)
    {
        var qIncidents = Find.Storyteller.incidentQueue.GetEnumerator();
        using var disposable = qIncidents as IDisposable;
        while (qIncidents.MoveNext())
        {
            var qi = (QueuedIncident)qIncidents.Current;
            if (qi?.FiringIncident.parms == parms && qi?.FiringIncident.def == incidentDef)
            {
                return true;
            }
        }

        return false;
    }
}