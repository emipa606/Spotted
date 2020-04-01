using RimWorld;
using Verse;

namespace Spotted
{
    public struct AlertIncident
    {
        IDelayHolder delay;
        string description;

        public AlertIncident(IDelayHolder delay, IncidentDef incidentDef) : this()
        {
            SetAlertIncident(delay, incidentDef);
        }

        public void SetAlertIncident(IDelayHolder delay, IncidentDef incidentDef)
        {
            string alertDescription = incidentDef == null ? "S.UnidentifiedMovement".Translate() : incidentDef.LabelCap;

            this.delay = delay;
            description = alertDescription;
        }

        public IDelayHolder GetDelay()
        {
            return delay;
        }

        public string GetDescription()
        {
            return description;
        }
    }
}
