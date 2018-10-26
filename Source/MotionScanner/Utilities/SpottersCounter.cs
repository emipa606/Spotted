using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Spotted
{
    class SpottersCounter
    {
        private Map map;

        private delegate bool CountDetails(Thing item);

        public SpottersCounter(Map map)
        {
            this.map = map;
        }

        public int ActiveColonistsCount()
        {
            int colonistsCount = map.mapPawns.ColonistsSpawnedCount;

            return colonistsCount;
        }

        public int WatchtowersCount()
        {
            return GetThingCount(map, ThingDefOf.Watchtower, delegate (Thing item)
            {
                if (item.Faction == Find.FactionManager.OfPlayer)
                    return true;

                return false;
            });
        }

        public int PoweredMotionScannersCount()
        {
            return GetThingCount(map, ThingDefOf.MotionScanner, delegate (Thing item)
            {
                if(item.Faction == Find.FactionManager.OfPlayer)
                {
                    CompPowerTrader compPowerTrader = item.TryGetComp<CompPowerTrader>();
                    if (compPowerTrader == null || compPowerTrader.PowerOn)
                    {
                        return true;
                    }
                }

                return false;
            });
        }

        public int PoweredSatelliteController()
        {
            return GetThingCount(map, ThingDefOf.SatelliteController, delegate (Thing item)
            {
                if (item.Faction == Find.FactionManager.OfPlayer)
                {
                    CompPowerTrader compPowerTrader = item.TryGetComp<CompPowerTrader>();
                    if (compPowerTrader == null || compPowerTrader.PowerOn)
                    {
                        return true;
                    }
                }

                return false;
            });
        }

        private int GetThingCount(Map map, ThingDef def, CountDetails action)
        {
            int count = 0;
            List<Thing> list = map.listerThings.ThingsMatching(ThingRequest.ForDef(def));
            foreach (var item in list)
            {
                if (action(item))
                {
                    count++;
                }
            }

            return count;
        }
    }
}
