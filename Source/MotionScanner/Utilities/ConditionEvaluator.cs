using System.Linq;
using Verse;

namespace Spotted;

public static class ConditionEvaluator
{
    private static object[] args = { };

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

        var map = (Map)args?[0];
        if (map == null)
        {
            return false;
        }

        var buildingDef = (ThingDef)def;
        if (buildingDef == null)
        {
            return false;
        }

        return map.listerBuildings.AllBuildingsColonistOfDef(buildingDef).Count() != 0;
    }

    public static bool EvaluateResearched(Def def)
    {
        var researchDef = (ResearchProjectDef)def;
        return researchDef is { IsFinished: true };
    }

    public static bool EvaluatePowered(Def def)
    {
        if (args?.Length == 0)
        {
            return false;
        }

        var map = (Map)args?[0];
        if (map == null)
        {
            return false;
        }

        var buildingDef = (ThingDef)def;
        return buildingDef != null && map.listerBuildings.ColonistsHaveBuildingWithPowerOn(buildingDef);
    }
}