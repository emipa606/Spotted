﻿using RimWorld;
using Verse;

namespace Spotted;

public struct AlertIncident
{
    private IDelayHolder delay;
    private string description;

    public AlertIncident(IDelayHolder delay, IncidentDef incidentDef) : this()
    {
        setAlertIncident(delay, incidentDef);
    }

    private void setAlertIncident(IDelayHolder delay, IncidentDef incidentDef)
    {
        string alertDescription = incidentDef?.LabelCap ?? "S.UnidentifiedMovement".Translate();

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