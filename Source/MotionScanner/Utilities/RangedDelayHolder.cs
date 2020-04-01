using RimWorld;
using System;
using System.Text;
using Verse;

namespace Spotted
{
    class RangedDelayHolder : DelayHolder
    {
        private int leftValue;
        private int rightValue;

        public RangedDelayHolder(int delay) : base(delay)
        {
            InitRange();
        }

        private int GetGlobalAdjustedVal(int value)
        {
            return triggerTime + value;
        }

        private int GetRemainingAdjustedValue(int value)
        {
            return GetGlobalAdjustedVal(value) - Find.TickManager.TicksGame;
        }

        private string ToStringRange(int left, int right)
        {
            StringBuilder rangeDate = new StringBuilder();
            rangeDate.Append(GenDate.ToStringTicksToPeriodVerbose(left, true, true));
            rangeDate.Append(" - ");
            rangeDate.Append(GenDate.ToStringTicksToPeriodVerbose(right, true, true));

            return rangeDate.ToString();
        }

        public override string ToStringGlobalDelayToPeriod()
        {
            return ToStringRange(GetGlobalAdjustedVal(leftValue), GetGlobalAdjustedVal(rightValue));
        }

        public override string ToStringRelativeDelayToPeriod()
        {
            return ToStringRange(leftValue, rightValue);
        }

        public override string ToStringRemainingDelayToPeriod()
        {
            return ToStringRange(GetRemainingAdjustedValue(leftValue), GetRemainingAdjustedValue(rightValue));
        }

        private void InitRange()
        {
            Random random = new Random();
            float interval = random.Next(50) / 10.0f;

            leftValue = delay - random.Next((int)(interval * GenDate.TicksPerHour));
            rightValue = delay + random.Next((int)(interval * GenDate.TicksPerHour));
        }
    }
}
