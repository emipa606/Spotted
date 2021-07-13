using System;
using Verse;

namespace Spotted
{
    public class StoryCondition
    {
        public Condition condition;
        public string defName;
        public Type defType;

        public bool RequirementIsMeet()
        {
            var def = GenDefDatabase.GetDef(defType, defName);

            if (condition == Condition.built)
            {
                return ConditionEvaluator.EvaluateBuilt(def);
            }

            if (condition == Condition.notbuilt)
            {
                return !ConditionEvaluator.EvaluateBuilt(def);
            }

            if (condition == Condition.researched)
            {
                return ConditionEvaluator.EvaluateResearched(def);
            }

            if (condition == Condition.notresearched)
            {
                return !ConditionEvaluator.EvaluateResearched(def);
            }

            if (condition == Condition.powered)
            {
                return ConditionEvaluator.EvaluatePowered(def);
            }

            if (condition == Condition.notpowered)
            {
                return !ConditionEvaluator.EvaluatePowered(def);
            }

            return false;
        }
    }
}