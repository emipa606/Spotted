using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace Spotted
{
    static class SpottedLetter
    {
        private static readonly string LetterLabel = "S.MotionDetected";

        public static Letter NewLetter(IncidentParms parms, IDelayHolder delay, IncidentDef incidentDef = null)
        {
            if (incidentDef == null || parms.points > -1f)
                return LetterMaker.MakeLetter(LetterLabel.Translate(), GetLetterText(delay, parms, incidentDef), LetterDefOf.ThreatBig, new TargetInfo(parms.spawnCenter, (Map)parms.target));
            return LetterMaker.MakeLetter(LetterLabel.Translate(), GetLetterText(delay, parms, incidentDef), LetterDefOf.NeutralEvent, new TargetInfo(parms.spawnCenter, (Map)parms.target));
        }

        private static string GetLetterText(IDelayHolder delay, IncidentParms parms, IncidentDef incidentDef = null)
        {
            StringBuilder letterText = new StringBuilder();

            // description (detected text)
            letterText.AppendLine(GetDescription(parms, incidentDef));

            // count
            letterText.AppendLine("S.LetterQuantityUnidendified".Translate());

            // type
            letterText.AppendLine(GetTypeDescription(parms, incidentDef));

            // arrival time
            letterText.Append("S.LetterTime".Translate());
            letterText.Append(" " + delay.ToStringRelativeDelayToPeriod());

            return letterText.ToString();
        }

        private static string GetDescription(IncidentParms parms, IncidentDef incidentDef = null)
        {
            List<StoryDef> descriptionStoryDefs = DefDatabase<StoryDef>.AllDefs.Where(def => def.storyType == "Spotted.Detected")
                                                                               .MeetRequirements(new object[] { parms.target })
                                                                               .ToList();
            string descKey = descriptionStoryDefs.RandomElement()?.storyKey;
            return descKey == null ? TaggedString.Empty : descKey.Translate();
        }

        private static string GetTypeDescription(IncidentParms parms, IncidentDef incidentDef)
        {
            string storyType = incidentDef == null ? "Spotted.UnidentifiedType" : "Spotted.IdentifiedType";
            List<StoryDef> typeStoryDefs = DefDatabase<StoryDef>.AllDefs.Where(def => def.storyType == storyType)
                                                                        .MeetRequirements(new object[] { parms.target })
                                                                        .ToList();
            string descKey = typeStoryDefs.RandomElement()?.storyKey;
            return descKey == null ? "S.TypeUnidentified".Translate() : (TaggedString)string.Format(descKey.Translate(), incidentDef?.label);
        }
    }
}
