using RimWorld;
using Verse;

namespace Spotted
{
    class DelayHolder : IDelayHolder
    {
        protected readonly int delay;
        protected readonly int triggerTime;

        public DelayHolder(int delay)
        {
            this.delay = delay;
            triggerTime = Find.TickManager.TicksGame;
        }

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
            return GenDate.ToStringTicksToPeriodVerbose(GetGlobalDelay(), true, true);
        }

        public virtual string ToStringRelativeDelayToPeriod()
        {
            return GenDate.ToStringTicksToPeriodVerbose(GetRelativeDelay(), true, true);
        }

        public virtual string ToStringRemainingDelayToPeriod()
        {
            return GenDate.ToStringTicksToPeriodVerbose(GetRemainingTicks(), true, true);
        }
    }
}
