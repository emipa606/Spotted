using System;
using UnityEngine;
using Verse;

namespace MotionScanner
{
    class MotionScannerMod : Mod
    {
        public static MotionScannerSettings settings;

        public MotionScannerMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<MotionScannerSettings>();
        }

        public override string SettingsCategory()
        {
            return "MS.SettingsLabel".Translate();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing_Standard = new Listing_Standard();

            listing_Standard.Begin(inRect);
            listing_Standard.Label("MS.DelayRange".Translate(), tooltip: "MS.DelayRangeTooltip".Translate());
            listing_Standard.IntRange(ref MotionScannerSettings.allowedTimeRange, 1, 24);

            listing_Standard.Label("MS.EdgeWalkIn".Translate() + $": {MotionScannerSettings.edgeWalkInModifier}", tooltip: "MS.multiplier".Translate());
            MotionScannerSettings.edgeWalkInModifier = (float)Math.Round(listing_Standard.Slider(MotionScannerSettings.edgeWalkInModifier, 0.1f, 2f), 1);

            listing_Standard.Label("MS.EdgeWalkInGroups".Translate() + $": {MotionScannerSettings.edgeWalkInGroupsModifier}", tooltip: "MS.multiplier".Translate());
            MotionScannerSettings.edgeWalkInGroupsModifier = (float)Math.Round(listing_Standard.Slider(MotionScannerSettings.edgeWalkInGroupsModifier, 0.1f, 2f), 1);

            listing_Standard.Label("MS.EdgeDrop".Translate() + $": {MotionScannerSettings.edgeDropModifier}", tooltip: "MS.multiplier".Translate());
            MotionScannerSettings.edgeDropModifier = (float)Math.Round(listing_Standard.Slider(MotionScannerSettings.edgeDropModifier, 0.1f, 2f), 1);

            listing_Standard.Label("MS.EdgeDropGroups".Translate() + $": {MotionScannerSettings.edgeDropGroupsModifier}", tooltip: "MS.multiplier".Translate());
            MotionScannerSettings.edgeDropGroupsModifier = (float)Math.Round(listing_Standard.Slider(MotionScannerSettings.edgeDropGroupsModifier, 0.1f, 2f), 1);

            listing_Standard.Label("MS.CenterDrop".Translate() + $": {MotionScannerSettings.centerDropModifier}", tooltip: "MS.multiplier".Translate());
            MotionScannerSettings.centerDropModifier = (float)Math.Round(listing_Standard.Slider(MotionScannerSettings.centerDropModifier, 0.1f, 2f), 1);

            listing_Standard.Label("MS.RandomDrop".Translate() + $": {MotionScannerSettings.randomDropModifier}", tooltip: "MS.multiplier".Translate());
            MotionScannerSettings.randomDropModifier = (float)Math.Round(listing_Standard.Slider(MotionScannerSettings.randomDropModifier, 0.1f, 2f), 1);
            listing_Standard.End();

            settings.Write();
        }
    }
}
