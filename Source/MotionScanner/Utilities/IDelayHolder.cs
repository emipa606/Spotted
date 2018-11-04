namespace Spotted
{
    public interface IDelayHolder
    {
        int GetRelativeDelay();
        int GetGlobalDelay();
        int GetRemainingTicks();
        string ToStringGlobalDelayToPeriod();
        string ToStringRelativeDelayToPeriod();
        string ToStringRemainingDelayToPeriod();
    }
}
