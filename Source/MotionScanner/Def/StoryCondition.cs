using System;
using Verse;

namespace Spotted;

public class StoryCondition
{
    public Condition condition;
    public string defName;
    public Type defType;

    public bool RequirementIsMeet()
    {
        var def = GenDefDatabase.GetDef(defType, defName);

        switch (condition)
        {
            case Condition.built:
                return ConditionEvaluator.EvaluateBuilt(def);
            case Condition.notbuilt:
                return !ConditionEvaluator.EvaluateBuilt(def);
            case Condition.researched:
                return ConditionEvaluator.EvaluateResearched(def);
            case Condition.notresearched:
                return !ConditionEvaluator.EvaluateResearched(def);
            case Condition.powered:
                return ConditionEvaluator.EvaluatePowered(def);
            case Condition.notpowered:
                return !ConditionEvaluator.EvaluatePowered(def);
            default:
                return false;
        }
    }
}