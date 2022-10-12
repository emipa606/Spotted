using RimWorld;
using Verse;

namespace Spotted;

internal class SpottersCounter
{
    private readonly Map map;

    public SpottersCounter(Map map)
    {
        this.map = map;
    }

    public int ActiveColonistsCount()
    {
        return map?.mapPawns.ColonistsSpawnedCount ?? 0;
    }

    public int WatchtowersCount()
    {
        return map == null
            ? 0
            : GetThingCount(map, ThingDefOf.Watchtower, item => item.Faction == Find.FactionManager.OfPlayer);
    }

    public int PoweredMotionScannersCount()
    {
        if (map == null)
        {
            return 0;
        }

        return GetThingCount(map, ThingDefOf.MotionScanner, delegate(Thing item)
        {
            if (item.Faction != Find.FactionManager.OfPlayer)
            {
                return false;
            }

            var compPowerTrader = item.TryGetComp<CompPowerTrader>();
            return compPowerTrader == null || compPowerTrader.PowerOn;
        });
    }

    public int PoweredSatelliteController()
    {
        if (map == null)
        {
            return 0;
        }

        return GetThingCount(map, ThingDefOf.SatelliteController, delegate(Thing item)
        {
            if (item.Faction != Find.FactionManager.OfPlayer)
            {
                return false;
            }

            var compPowerTrader = item.TryGetComp<CompPowerTrader>();
            return compPowerTrader == null || compPowerTrader.PowerOn;
        });
    }

    private int GetThingCount(Map currentMap, ThingDef def, CountDetails action)
    {
        var count = 0;
        var list = currentMap.listerThings.ThingsMatching(ThingRequest.ForDef(def));
        foreach (var item in list)
        {
            if (action(item))
            {
                count++;
            }
        }

        return count;
    }

    private delegate bool CountDetails(Thing item);
}