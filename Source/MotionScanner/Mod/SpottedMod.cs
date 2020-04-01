using System;
using UnityEngine;
using Verse;

namespace Spotted
{
    class SpottedMod : Mod
    {
        public static SpottedSettings settings;

        public SpottedMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<SpottedSettings>();
        }

        public override string SettingsCategory()
        {
            return "S.SettingsLabel".Translate();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing_Standard = new Listing_Standard();

            listing_Standard.Begin(inRect);
            listing_Standard.Label("S.DelayRange".Translate(), tooltip: "S.DelayRangeTooltip".Translate());
            listing_Standard.IntRange(ref SpottedSettings.allowedTimeRange, 1, 24);

            listing_Standard.Label("S.EdgeWalkIn".Translate() + $": {SpottedSettings.edgeWalkInModifier}", tooltip: "S.multiplier".Translate());
            SpottedSettings.edgeWalkInModifier = (float)Math.Round(listing_Standard.Slider(SpottedSettings.edgeWalkInModifier, 0.1f, 2f), 1);

            listing_Standard.Label("S.EdgeWalkInGroups".Translate() + $": {SpottedSettings.edgeWalkInGroupsModifier}", tooltip: "S.multiplier".Translate());
            SpottedSettings.edgeWalkInGroupsModifier = (float)Math.Round(listing_Standard.Slider(SpottedSettings.edgeWalkInGroupsModifier, 0.1f, 2f), 1);

            listing_Standard.Label("S.EdgeDrop".Translate() + $": {SpottedSettings.edgeDropModifier}", tooltip: "S.multiplier".Translate());
            SpottedSettings.edgeDropModifier = (float)Math.Round(listing_Standard.Slider(SpottedSettings.edgeDropModifier, 0.1f, 2f), 1);

            listing_Standard.Label("S.EdgeDropGroups".Translate() + $": {SpottedSettings.edgeDropGroupsModifier}", tooltip: "S.multiplier".Translate());
            SpottedSettings.edgeDropGroupsModifier = (float)Math.Round(listing_Standard.Slider(SpottedSettings.edgeDropGroupsModifier, 0.1f, 2f), 1);

            listing_Standard.Label("S.CenterDrop".Translate() + $": {SpottedSettings.centerDropModifier}", tooltip: "S.multiplier".Translate());
            SpottedSettings.centerDropModifier = (float)Math.Round(listing_Standard.Slider(SpottedSettings.centerDropModifier, 0.1f, 2f), 1);

            listing_Standard.Label("S.RandomDrop".Translate() + $": {SpottedSettings.randomDropModifier}", tooltip: "S.multiplier".Translate());
            SpottedSettings.randomDropModifier = (float)Math.Round(listing_Standard.Slider(SpottedSettings.randomDropModifier, 0.1f, 2f), 1);

            listing_Standard.CheckboxLabeled("S.AccurateArrival".Translate(), ref SpottedSettings.displayAccurateArrivalTime, tooltip: "S.AccurateTooltip".Translate());
            listing_Standard.End();

            settings.Write();
        }
    }
}
