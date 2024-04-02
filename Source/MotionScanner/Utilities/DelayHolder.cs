using RimWorld;
using Verse;

namespace Spotted;

internal class DelayHolder(int delay) : IDelayHolder
{
    protected readonly int delay = delay;
    protected readonly int triggerTime = Find.TickManager.TicksGame;

    public virtual int GetGlobalDelay()
    {
        return triggerTime + delay;
    }

    public virtual int GetRelativeDelay()
    {
        return delay;
    }

    public virtual int GetRemainingTicks()
    {
        return GetGlobalDelay() - Find.TickManager.TicksGame;
    }

    public virtual string ToStringGlobalDelayToPeriod()
    {
        return GetGlobalDelay().ToStringTicksToPeriodVerbose();
    }

    public virtual string ToStringRelativeDelayToPeriod()
    {
        return GetRelativeDelay().ToStringTicksToPeriodVerbose();
    }

    public virtual string ToStringRemainingDelayToPeriod()
    {
        return GetRemainingTicks().ToStringTicksToPeriodVerbose();
    }
}