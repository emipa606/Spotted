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
            return map == null ? 0 : map.mapPawns.ColonistsSpawnedCount;
        }

        public int WatchtowersCount()
        {
            if(map == null)
            {
                return 0;
            }

            return GetThingCount(map, ThingDefOf.Watchtower, delegate (Thing item)
            {
                if (item.Faction == Find.FactionManager.OfPlayer)
                    return true;

                return false;
            });
        }

        public int PoweredMotionScannersCount()
        {
            if(map == null)
            {
                return 0;
            }

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
            if(map == null)
            {
                return 0;
            }

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
