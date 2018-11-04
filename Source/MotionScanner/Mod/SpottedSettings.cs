using System;
using System.Collections.Generic;
using Verse;

namespace Spotted
{
    class SpottedSettings : ModSettings
    {
        public static IntRange allowedTimeRange = new IntRange(3, 10);
        public static float edgeWalkInModifier = 1f;
        public static float edgeWalkInGroupsModifier = 1f;
        public static float edgeDropModifier = 1f;
        public static float edgeDropGroupsModifier = 1f;
        public static float centerDropModifier = 1f;
        public static float randomDropModifier = 1f;
        public static bool displayAccurateArrivalTime = false;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref allowedTimeRange, "allowedTimeRange", defaultValue: new IntRange(allowedTimeRange.min, allowedTimeRange.max), forceSave: true);
            Scribe_Values.Look(ref edgeWalkInModifier, "edgeWalkInModifier", 1f, true);
            Scribe_Values.Look(ref edgeWalkInGroupsModifier, "edgeWalkInGroupsModifier", 1f, true);
            Scribe_Values.Look(ref edgeDropModifier, "edgeDropModifier", 1f, true);
            Scribe_Values.Look(ref edgeDropGroupsModifier, "edgeDropGroupsModifier", 1f, true);
            Scribe_Values.Look(ref centerDropModifier, "centerDropModifier", 1f, true);
            Scribe_Values.Look(ref randomDropModifier, "randomDropModifier", 1f, true);
            Scribe_Values.Look(ref displayAccurateArrivalTime, "displayAccurateArivalTime", false, true);
        }
    
        public static Dictionary<string, float> GetModifiersDictionary()
        {
            return  new Dictionary<string, float>
            {
                ["EdgeWalkIn"] = edgeWalkInModifier,
                ["EdgeWalkInGroups"] = edgeWalkInGroupsModifier,
                ["EdgeDrop"] = edgeDropModifier,
                ["EdgeDropGroups"] = edgeDropGroupsModifier,
                ["CenterDrop"] = centerDropModifier,
                ["RandomDrop"] = randomDropModifier
            };
        }

        public static Type GetDelayType()
        {
            if (displayAccurateArrivalTime)
                return typeof(DelayHolder);

            return typeof(RangedDelayHolder);
        }
    }
}