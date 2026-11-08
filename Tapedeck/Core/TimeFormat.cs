namespace mediaPlayerPrj;

/// <summary>
/// One shared place that turns a TimeSpan into hh:mm:ss text.
/// Refactoring (Appendix C.5): FormatTime in the form and MediaItem.DurationText used to
/// repeat the same logic; both now call this method.
/// </summary>
public static class TimeFormat
{
    /// <summary>Text shown when nothing is playing.</summary>
    public const string Zero = "00:00:00";

    /// <summary>Formats a time as hh:mm:ss. Negative values show as zero.</summary>
    public static string ToClock(TimeSpan time)
    {
        if (time < TimeSpan.Zero) time = TimeSpan.Zero;
        return string.Format("{0:00}:{1:00}:{2:00}", (int)time.TotalHours, time.Minutes, time.Seconds);
    }
}
