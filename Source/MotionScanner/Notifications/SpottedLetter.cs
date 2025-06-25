using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace Spotted;

internal static class SpottedLetter
{
    private static readonly string LetterLabel = "S.MotionDetected";

    public static Letter NewLetter(IncidentParms parms, IDelayHolder delay, IncidentDef incidentDef = null)
    {
        if (incidentDef == null || parms.points > -1f)
        {
            return LetterMaker.MakeLetter(LetterLabel.Translate(), getLetterText(delay, parms, incidentDef),
                LetterDefOf.ThreatBig, new TargetInfo(parms.spawnCenter, (Map)parms.target));
        }

        return LetterMaker.MakeLetter(LetterLabel.Translate(), getLetterText(delay, parms, incidentDef),
            LetterDefOf.NeutralEvent, new TargetInfo(parms.spawnCenter, (Map)parms.target));
    }

    private static string getLetterText(IDelayHolder delay, IncidentParms parms, IncidentDef incidentDef = null)
    {
        var letterText = new StringBuilder();

        // description (detected text)
        letterText.AppendLine(getDescription(parms));

        // count
        letterText.AppendLine("S.LetterQuantityUnidendified".Translate());

        // type
        letterText.AppendLine(getTypeDescription(parms, incidentDef));

        // arrival time
        letterText.Append("S.LetterTime".Translate());
        letterText.Append(" " + delay.ToStringRelativeDelayToPeriod());

        return letterText.ToString();
    }

    private static string getDescription(IncidentParms parms)
    {
        var descriptionStoryDefs = DefDatabase<StoryDef>.AllDefs.Where(def => def.storyType == "Spotted.Detected")
            .MeetRequirements([parms.target])
            .ToList();
        var descKey = descriptionStoryDefs.RandomElement()?.storyKey;
        return descKey?.Translate() ?? TaggedString.Empty;
    }

    private static string getTypeDescription(IncidentParms parms, IncidentDef incidentDef)
    {
        var storyType = incidentDef == null ? "Spotted.UnidentifiedType" : "Spotted.IdentifiedType";
        var typeStoryDefs = DefDatabase<StoryDef>.AllDefs.Where(def => def.storyType == storyType)
            .MeetRequirements([parms.target])
            .ToList();
        var descKey = typeStoryDefs.RandomElement()?.storyKey;
        return descKey == null
            ? "S.TypeUnidentified".Translate()
            : (TaggedString)string.Format(descKey.Translate(), incidentDef?.label);
    }
}