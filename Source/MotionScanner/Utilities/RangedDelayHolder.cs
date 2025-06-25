using System;
using System.Text;
using RimWorld;
using Verse;

namespace Spotted;

internal class RangedDelayHolder : DelayHolder
{
    private int leftValue;
    private int rightValue;

    public RangedDelayHolder(int delay) : base(delay)
    {
        initRange();
    }

    private int getGlobalAdjustedVal(int value)
    {
        return triggerTime + value;
    }

    private int getRemainingAdjustedValue(int value)
    {
        return getGlobalAdjustedVal(value) - Find.TickManager.TicksGame;
    }

    private static string toStringRange(int left, int right)
    {
        var rangeDate = new StringBuilder();
        rangeDate.Append(left.ToStringTicksToPeriodVerbose());
        rangeDate.Append(" - ");
        rangeDate.Append(right.ToStringTicksToPeriodVerbose());

        return rangeDate.ToString();
    }

    public override string ToStringGlobalDelayToPeriod()
    {
        return toStringRange(getGlobalAdjustedVal(leftValue), getGlobalAdjustedVal(rightValue));
    }

    public override string ToStringRelativeDelayToPeriod()
    {
        return toStringRange(leftValue, rightValue);
    }

    public override string ToStringRemainingDelayToPeriod()
    {
        return toStringRange(getRemainingAdjustedValue(leftValue), getRemainingAdjustedValue(rightValue));
    }

    private void initRange()
    {
        var random = new Random();
        var interval = random.Next(50) / 10.0f;

        leftValue = delay - random.Next((int)(interval * GenDate.TicksPerHour));
        rightValue = delay + random.Next((int)(interval * GenDate.TicksPerHour));
    }
}