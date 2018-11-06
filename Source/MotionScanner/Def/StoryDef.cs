using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Spotted
{
    public enum Condition { built, notbuilt, researched, notresearched}

    public class StoryCondition
    {
        private static object[] args = new object[] { };

        public Type defType;
        public string defName;
        public Condition condition;

        public bool RequirementIsMeet()
        {
            Def def = GenDefDatabase.GetDef(defType, defName);

            if(condition == Condition.built)
            {
                if (args?.Count() == 0)
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
                    return false;

                return map.listerBuildings.AllBuildingsColonistOfDef(buildingDef).Count() != 0 ? true : false;
            }
            if(condition == Condition.notbuilt)
            {
                if(args?.Count() == 0)
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

                return map.listerBuildings.AllBuildingsColonistOfDef(buildingDef).Count() != 0 ? false : true;
            }
            if(condition == Condition.researched)
            {
                ResearchProjectDef researchDef = (ResearchProjectDef)def;
                return researchDef == null ? false : researchDef.IsFinished;
            }
            if(condition == Condition.notresearched)
            {
                ResearchProjectDef researchDef = (ResearchProjectDef)def;
                return researchDef == null ? false : !researchDef.IsFinished;
            }

            return false;
        }

        public static void SetArgs(object[] _args)
        {
            if(_args == null)
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
    }

    public class StoryDef : Def
    {
        public string storyKey;
        public string storyType;
        public List<StoryCondition> required;
        public List<StoryCondition> optional;
    }
}
