using System.Linq;
using Verse;

namespace Spotted
{
    public enum Condition { built, notbuilt, researched, notresearched, powered, notpowered}

    public static class ConditionEvaluator
    {
        private static object[] args = new object[] { };

        public static void SetArgs(object[] _args)
        {
            if (_args == null)
            {
                ClearArgs();
                return;
            }

            args = _args;
        }

        public static void ClearArgs()
        {
            args = new object[] { };
        }

        public static bool EvaluateBuilt(Def def)
        {
            if (args?.Length == 0)
            {
                return false;
            }
            
            Map map = (Map)args[0];
            if (map == null)
            {
                return false;
            }

            ThingDef buildingDef = (ThingDef)def;
            if (buildingDef == null)
                return false;

            return map.listerBuildings.AllBuildingsColonistOfDef(buildingDef).Count() != 0 ? true : false;
        }

        public static bool EvaluateResearched(Def def)
        {
            ResearchProjectDef researchDef = (ResearchProjectDef)def;
            return researchDef == null ? false : researchDef.IsFinished;
        }

        public static bool EvaluatePowered(Def def)
        {
            if (args?.Length == 0)
            {
                return false;
            }

            Map map = (Map)args[0];
            if(map == null)
            {
                return false;
            }

            ThingDef buildingDef = (ThingDef)def;
            if (buildingDef == null)
            {
                return false;
            }

            return map.listerBuildings.ColonistsHaveBuildingWithPowerOn(buildingDef);
        }
    }
}
