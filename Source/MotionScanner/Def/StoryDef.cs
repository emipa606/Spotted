using System;
using System.Collections.Generic;
using Verse;

namespace Spotted
{
    public class StoryCondition
    {
        public Type defType;
        public string defName;
        public Condition condition;

        public bool RequirementIsMeet()
        {
            Def def = GenDefDatabase.GetDef(defType, defName);

            if(condition == Condition.built)
            {
                return ConditionEvaluator.EvaluateBuilt(def);
            }
            if(condition == Condition.notbuilt)
            {
                return !ConditionEvaluator.EvaluateBuilt(def);
            }
            if(condition == Condition.researched)
            {
                return ConditionEvaluator.EvaluateResearched(def);
            }
            if(condition == Condition.notresearched)
            {
                return !ConditionEvaluator.EvaluateResearched(def);
            }
            if(condition == Condition.powered)
            {
                return ConditionEvaluator.EvaluatePowered(def);
            }
            if(condition == Condition.notpowered)
            {
                return !ConditionEvaluator.EvaluatePowered(def);
            }

            return false;
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
