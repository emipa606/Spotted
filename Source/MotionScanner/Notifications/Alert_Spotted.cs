using RimWorld;
using System.Collections.Generic;
using System.Text;
using Verse;

namespace Spotted
{
    public class Alert_Spotted : Alert
    {
        private static List<AlertIncident> incidents = new List<AlertIncident>();

        public Alert_Spotted()
        {
            defaultLabel = "S.SpottedAlertLabel".Translate();
            defaultPriority = AlertPriority.Medium;
        }

        public override TaggedString GetExplanation()
        {
            StringBuilder explanation = new StringBuilder();
            foreach(var incident in incidents)
            {
                explanation.AppendLine(incident.GetDescription() + "S.Incoming".Translate() + incident.GetDelay().ToStringRemainingDelayToPeriod());
            }
            //remove last \n
            explanation.Remove(explanation.Length - 1, 1);

            return explanation.ToString();
        }

        public override AlertReport GetReport()
        {
            incidents.RemoveAll(incident => incident.GetDelay().GetRemainingTicks() < 0);
            return incidents.Count > 0;
        }

        public static void AddIncident(AlertIncident incident)
        {
            if (incident.GetDelay().GetGlobalDelay() < 0)
                return;

            incidents.Add(incident);
        }

        ~Alert_Spotted(){
            incidents.Clear();
        }
    }
}
