using RimWorld;
using System.Collections.Generic;
using System.Text;
using Verse;

namespace Spotted
{
    public class Alert_Spotted : Alert
    {
        private static List<int> incidentTicks = new List<int>();

        public Alert_Spotted()
        {
            defaultLabel = "S.SpottedAlertLabel".Translate();
            defaultPriority = AlertPriority.Medium;
        }

        public override string GetExplanation()
        {
            StringBuilder explanation = new StringBuilder();
            foreach(var incident in incidentTicks)
            {
                explanation.AppendLine("S.IncomingRaid".Translate() + " " + (incident - Find.TickManager.TicksGame).ToStringTicksToPeriodVerbose(true, true));
            }

            return explanation.ToString();
        }

        public override AlertReport GetReport()
        {
            incidentTicks.RemoveAll(tick => Find.TickManager.TicksGame > tick);
            return incidentTicks.Count > 0;
        }

        public static void AddIncident(int incidentTick)
        {
            if (incidentTick < 0)
                return;

            incidentTicks.Add(incidentTick);
        }
    }
}
