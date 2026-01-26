namespace Project.Core.Enums
{
    /// <summary>
    /// Gets time units in milliseconds
    /// </summary>
    public enum TimerType
    {
        Second = 1000,
        Minute = 60 * TimerType.Second,
        Hour = 60 * TimerType.Minute,
        Day = 24*TimerType.Hour,
        Week = 7*TimerType.Day
    }
}
