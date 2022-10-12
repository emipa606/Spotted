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
        var rangeDate = new StringBuilder();
        rangeDate.Append(left.ToStringTicksToPeriodVerbose());
        rangeDate.Append(" - ");
        rangeDate.Append(right.ToStringTicksToPeriodVerbose());

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
        var random = new Random();
        var interval = random.Next(50) / 10.0f;

        leftValue = delay - random.Next((int)(interval * GenDate.TicksPerHour));
        rightValue = delay + random.Next((int)(interval * GenDate.TicksPerHour));
    }
}