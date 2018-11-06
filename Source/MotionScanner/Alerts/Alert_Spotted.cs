using RimWorld;
using System.Collections.Generic;
using System.Text;
using Verse;

namespace Spotted
{
    public class Alert_Spotted : Alert
    {
        private static List<IDelayHolder> incidentTicks = new List<IDelayHolder>();

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
                explanation.AppendLine("S.IncomingRaid".Translate() + " " + incident.ToStringRemainingDelayToPeriod());
            }

            return explanation.ToString();
        }

        public override AlertReport GetReport()
        {
            incidentTicks.RemoveAll(tick => tick.GetRemainingTicks() < 0);
            return incidentTicks.Count > 0;
        }

        public static void AddIncident(IDelayHolder incidentTick)
        {
            if (incidentTick.GetGlobalDelay() < 0)
                return;

            incidentTicks.Add(incidentTick);
        }

        ~Alert_Spotted(){
            incidentTicks.Clear();
        }
    }
}
